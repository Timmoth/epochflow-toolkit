using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using EpochFlow.ApiClient;
using EpochFlow.ApiClient.Models;
using EpochFlow.ApiClient.Sets;
using EpochFlow.CpuMetrics.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;
using Spectre.Console;
using Spectre.Console.Cli;

namespace EpochFlow.Toolkit.Commands;

public sealed class CreateSetCommand : AsyncCommand<CreateSetCommand.Settings>
{
    private readonly ILogger<CreateSetCommand> _logger;
    private readonly IServiceProvider _serviceProvider;

    public CreateSetCommand(ILogger<CreateSetCommand> logger, IServiceProvider serviceProvider)
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

        var collisionMode = settings.CollisionMode == "overwrite" ? CollisionMode.Overwrite : CollisionMode.Combine;
        var stopwatch = Stopwatch.StartNew();
        var response = await epochFlowApi.CreateSet(CreateSet.Create(settings.SetName, collisionMode));
        stopwatch.Stop();
        _logger.LogInformation(
            "Completed with status code: status code: [{StatusCode}] in {Duration}ms",
            response.StatusCode,
            stopwatch.ElapsedMilliseconds);

        if (response.IsSuccessStatusCode && response.Content != null)
        {
            _logger.LogInformation(JsonSerializer.Serialize(response.Content, new JsonSerializerOptions()
            {
                WriteIndented = true
            }));
        }
        else
        {
            _logger.LogIfError(response);
        }

        return 0;
    }

    public sealed class Settings : CommandSettings
    {
        [CommandOption("--url")]
        [Description("API Url")]
        public string ApiUrl { get; set; } = string.Empty;

        [CommandOption("--account")]
        [Description("Account Id")]
        public string AccountId { get; set; } = string.Empty;

        [CommandOption("--key")]
        [Description("API key")]
        public string ApiKey { get; set; } = string.Empty;

        [CommandOption("--name")]
        [Description("Set name")]
        public string SetName { get; set; } = string.Empty;

        [CommandOption("--collision_mode")]
        [Description("Collision mode, either [combine, overwrite]")]
        public string CollisionMode { get; set; } = string.Empty;

        public override ValidationResult Validate()
        {
            if (string.IsNullOrWhiteSpace(ApiUrl))
            {
                ApiUrl = Environment.GetEnvironmentVariable("epochflow_url") ?? string.Empty;
                if (string.IsNullOrWhiteSpace(ApiUrl))
                {
                    return ValidationResult.Error("Specify Api url with '--url' or set 'epochflow_url' environment variable.");
                }
            }

            if (string.IsNullOrWhiteSpace(AccountId))
            {
                AccountId = Environment.GetEnvironmentVariable("epochflow_account_id") ?? string.Empty;
                if (string.IsNullOrWhiteSpace(AccountId))
                {
                    return ValidationResult.Error("Specify Account Id with '--account' or set 'epochflow_account_id' environment variable.");
                }
            }

            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                ApiKey = Environment.GetEnvironmentVariable("epochflow_api_key") ?? string.Empty;
                if (string.IsNullOrWhiteSpace(ApiKey))
                {
                    return ValidationResult.Error("Specify Api Key with '--key' or set 'epochflow_api_key' environment variable.");
                }
            }

            if (string.IsNullOrWhiteSpace(SetName))
            {
                return ValidationResult.Error("Specify Set name with '--name'");
            }

            if (SetName.Length <= 3)
            {
                return ValidationResult.Error("Set name must be at least three characters.");
            }

            if (SetName.Length > 256)
            {
                return ValidationResult.Error("Set name must be 256 characters or less.");
            }

            if (string.IsNullOrWhiteSpace(CollisionMode))
            {
                return ValidationResult.Error("Specify Collision mode with '--collision_mode'");
            }

            if (Enum.TryParse<CollisionMode>(CollisionMode, out var collision))
            {
                return ValidationResult.Error("Collision mode must be either [overwrite, combine]");
            }

            return ValidationResult.Success();
        }
    }
}