﻿using System.ComponentModel;
using System.Diagnostics;
using EpochFlow.ApiClient;
using EpochFlow.CpuMetrics.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;
using Spectre.Console;
using Spectre.Console.Cli;

namespace EpochFlow.Toolkit.Commands.Sets;

public sealed class DeleteSetCommand : AsyncCommand<DeleteSetCommand.Settings>
{
    private readonly ILogger<DeleteSetCommand> _logger;
    private readonly IServiceProvider _serviceProvider;

    public DeleteSetCommand(ILogger<DeleteSetCommand> logger, IServiceProvider serviceProvider)
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
        var response = await epochFlowApi.DeleteSet(settings.SetId);
        stopwatch.Stop();
        _logger.LogInformation(
            "Completed with status code: status code: [{StatusCode}] in {Duration}ms",
            response.StatusCode,
            stopwatch.ElapsedMilliseconds);

        _logger.LogIfError(response);

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

        [CommandOption("--id")]
        [Description("Set Id")]
        public string SetId { get; set; } = string.Empty;

        public override ValidationResult Validate()
        {
            if (string.IsNullOrWhiteSpace(ApiUrl))
            {
                ApiUrl = Environment.GetEnvironmentVariable("epochflow_url") ?? string.Empty;
                if (string.IsNullOrWhiteSpace(ApiUrl))
                {
                    return ValidationResult.Error("Specify Api url with '--url' or 'epochflow_url' environment variable.");
                }
            }

            if (string.IsNullOrWhiteSpace(AccountId))
            {
                AccountId = Environment.GetEnvironmentVariable("epochflow_account") ?? string.Empty;
                if (string.IsNullOrWhiteSpace(AccountId))
                {
                    return ValidationResult.Error("Specify Account Id with '--account' or 'epochflow_account' environment variable.");
                }
            }

            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                ApiKey = Environment.GetEnvironmentVariable("epochflow_key") ?? string.Empty;
                if (string.IsNullOrWhiteSpace(ApiKey))
                {
                    return ValidationResult.Error("Specify Api Key with '--key' or 'epochflow_key' environment variable.");
                }
            }

            if (string.IsNullOrWhiteSpace(SetId))
            {
                return ValidationResult.Error("Specify Set Id with '--id'");
            }

            return ValidationResult.Success();
        }
    }
}