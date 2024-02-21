using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using EpochFlow.ApiClient;
using EpochFlow.ApiClient.Data;
using EpochFlow.CpuMetrics.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;
using Spectre.Console;
using Spectre.Console.Cli;

namespace EpochFlow.Toolkit.Commands.Sets.Tags;

public sealed class GetDataCommand : AsyncCommand<GetDataCommand.Settings>
{
    private readonly ILogger<GetDataCommand> _logger;
    private readonly IServiceProvider _serviceProvider;

    public GetDataCommand(ILogger<GetDataCommand> logger, IServiceProvider serviceProvider)
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

        var start = new DateTimeOffset(settings.Start).ToUnixTimeSeconds();
        var end = new DateTimeOffset(settings.End).ToUnixTimeSeconds();

        var tags = settings.Tags.Split(",").ToList();
        var stopwatch = Stopwatch.StartNew();
        var response = await epochFlowApi.GetData(settings.SetId, GetDataRequest.Create(start, end, tags));
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

        [CommandOption("--id")]
        [Description("Set Id")]
        public string SetId { get; set; } = string.Empty;

        [CommandOption("--start")]
        [Description("Start datetime")]
        public DateTime Start { get; set; } = DateTime.Now.AddMonths(-1);

        [CommandOption("--end")]
        [Description("End datetime")]
        public DateTime End { get; set; } = DateTime.Now;

        [CommandOption("--tags")]
        [Description("Tags")]
        public string Tags { get; set; } = string.Empty;

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