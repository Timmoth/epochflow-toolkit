using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using EpochFlow.ApiClient;
using EpochFlow.ApiClient.Permissions;
using EpochFlow.ApiClient.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;
using Spectre.Console;
using Spectre.Console.Cli;

namespace EpochFlow.Toolkit.Commands.Accounts.ApiKeys;

public sealed class CreateApiKeyCommand : AsyncCommand<CreateApiKeyCommand.Settings>
{
    private readonly ILogger<CreateApiKeyCommand> _logger;
    private readonly IServiceProvider _serviceProvider;

    public CreateApiKeyCommand(ILogger<CreateApiKeyCommand> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        using var scope = _serviceProvider.CreateScope();
        var httpClient = scope.ServiceProvider.GetRequiredService<HttpClient>();
        httpClient.BaseAddress = new Uri(settings.ApiUrl);
        httpClient.DefaultRequestHeaders.Add("X-Account-Id", settings.AccountId);
        httpClient.DefaultRequestHeaders.Add("X-API-Key", settings.ApiKey);

        var epochFlowApi = RestService.For<IEpochFlowV1>(httpClient, new RefitSettings());

        long? expiresAt = settings.ExpiresAt == null
            ? null
            : new DateTimeOffset(settings.ExpiresAt.Value).ToUnixTimeSeconds();

        var permissions = new List<ApiKeyPermission>();
        foreach (var section in settings.Permissions.Split("&"))
        {
            var subSections = section.Split(";").ToList();
            if (subSections.Count != 3)
                _logger.LogError("Invalid permissions, must be in format 'set_id;tag1,tag2;read,write'");

            var tags = subSections[1].Split(",").ToList();
            var allTags = tags.Contains("all");
            var operations = AllowedOperations.Undefined;
            if (subSections[2].Contains("read")) operations |= AllowedOperations.Read;
            if (subSections[2].Contains("write")) operations |= AllowedOperations.Write;

            permissions.Add(new ApiKeyPermission
            {
                SetId = subSections[0],
                AllTags = allTags,
                Tags = tags,
                AllowedOperations = operations
            });
        }

        var stopwatch = Stopwatch.StartNew();
        var response = await epochFlowApi.CreateApiKey(new CreateApiKey
        {
            IsAdmin = settings.IsAdmin,
            KeyName = settings.KeyName,
            ExpiresAt = expiresAt,
            AllSetOperations = settings.AllSetOperations,
            Permissions = permissions
        });
        stopwatch.Stop();
        _logger.LogInformation(
            "Completed with status code: status code: [{StatusCode}] in {Duration}ms",
            response.StatusCode,
            stopwatch.ElapsedMilliseconds);

        if (response.IsSuccessStatusCode && response.Content != null)
            _logger.LogInformation(JsonSerializer.Serialize(response.Content, new JsonSerializerOptions
            {
                WriteIndented = true
            }));
        else
            _logger.LogIfError(response);

        return 0;
    }

    public sealed class Settings : EpochFlowBaseSettings
    {
        [CommandOption("--name")]
        [Description("Key name")]
        public string KeyName { get; set; } = string.Empty;

        [CommandOption("--expiry")]
        [Description("Key expires at")]
        public DateTime? ExpiresAt { get; set; } = null;

        [CommandOption("--admin")]
        [Description("Is admin")]
        public bool IsAdmin { get; set; } = false;

        [CommandOption("--permissions")]
        [Description("Key permissions")]
        public string Permissions { get; set; } = string.Empty;

        public override ValidationResult Validate()
        {
            var baseValidationResult = base.Validate();
            if (!baseValidationResult.Successful) return baseValidationResult;
            if (string.IsNullOrWhiteSpace(KeyName)) return ValidationResult.Error("Specify Key name with '--name'");

            if (KeyName.Length <= 3) return ValidationResult.Error("Key name must be at least three characters.");

            if (KeyName.Length > 256) return ValidationResult.Error("Key name must be 256 characters or less.");

            return ValidationResult.Success();
        }
    }
}