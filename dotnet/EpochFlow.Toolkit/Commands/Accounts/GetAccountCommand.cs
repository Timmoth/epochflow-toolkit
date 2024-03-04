using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using EpochFlow.ApiClient;
using EpochFlow.ApiClient.Utilities;
using EpochFlow.Toolkit.Commands.Accounts.ApiKeys;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;
using Spectre.Console;
using Spectre.Console.Cli;

namespace EpochFlow.Toolkit.Commands.Accounts;

public sealed class GetAccountCommand : AsyncCommand<GetAccountCommand.Settings>
{
    private readonly ILogger<GetAccountCommand> _logger;
    private readonly IServiceProvider _serviceProvider;

    public GetAccountCommand(ILogger<GetAccountCommand> logger, IServiceProvider serviceProvider)
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
        var response = await epochFlowApi.GetAccount();
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
       
        public override ValidationResult Validate()
        {
            var baseValidationResult = base.Validate();
            if (!baseValidationResult.Successful)
            {
                return baseValidationResult;
            }

            return ValidationResult.Success();
        }
    }
}