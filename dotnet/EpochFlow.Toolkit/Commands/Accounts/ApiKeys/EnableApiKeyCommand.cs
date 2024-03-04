using System.ComponentModel;
using System.Diagnostics;
using EpochFlow.ApiClient;
using EpochFlow.ApiClient.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;
using Spectre.Console;
using Spectre.Console.Cli;

namespace EpochFlow.Toolkit.Commands.Accounts.ApiKeys;

public sealed class EnableApiKeyCommand : AsyncCommand<EnableApiKeyCommand.Settings>
{
    private readonly ILogger<EnableApiKeyCommand> _logger;
    private readonly IServiceProvider _serviceProvider;

    public EnableApiKeyCommand(ILogger<EnableApiKeyCommand> logger, IServiceProvider serviceProvider)
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

        var stopwatch = Stopwatch.StartNew();
        var response = await epochFlowApi.EnableApiKey(settings.Id);
        stopwatch.Stop();
        _logger.LogInformation(
            "Completed with status code: status code: [{StatusCode}] in {Duration}ms",
            response.StatusCode,
            stopwatch.ElapsedMilliseconds);

        _logger.LogIfError(response);

        return 0;
    }

    public sealed class Settings : EpochFlowBaseSettings
    {
       [CommandOption("--id")]
        [Description("Api key id")]
        public string Id { get; set; } = string.Empty;

        public override ValidationResult Validate()
        {
            var baseValidationResult = base.Validate();
            if (!baseValidationResult.Successful)
            {
                return baseValidationResult;
            }

            if (string.IsNullOrWhiteSpace(Id)) return ValidationResult.Error("Specify Api key id with '--id'");

            return ValidationResult.Success();
        }
    }
}