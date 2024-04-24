using System.ComponentModel;
using System.Diagnostics;
using EpochFlow.ApiClient;
using EpochFlow.ApiClient.Measurements;
using EpochFlow.ApiClient.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;
using Spectre.Console.Cli;

namespace EpochFlow.CpuMetrics.CpuMetrics;

public sealed class CpuMetricsCommand : AsyncCommand<CpuMetricsCommand.Settings>
{
    private readonly ILogger<CpuMetricsCommand> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeProvider _timeProvider;

    public CpuMetricsCommand(ILogger<CpuMetricsCommand> logger, IServiceProvider serviceProvider,
        TimeProvider timeProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _timeProvider = timeProvider;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

        while (true)
        {
            _ = cpuCounter.NextValue(); // Call NextValue() once before using it

            await Task.Delay(10000);
            var cpuUsage = cpuCounter.NextValue();

            _logger.LogInformation($"Current CPU usage: {cpuUsage}%");

            using var scope = _serviceProvider.CreateScope();
            var httpClient = scope.ServiceProvider.GetRequiredService<HttpClient>();
            httpClient.BaseAddress = new Uri(settings.ApiUrl);
            httpClient.DefaultRequestHeaders.Add("X-API-Key", settings.ApiKey);

            var epochFlowApi = RestService.For<IEpochFlowV1>(httpClient, new RefitSettings());

            var request = Measurement.Create(_timeProvider.GetUtcNow().ToUnixTimeSeconds(), cpuUsage,
                "generator", Array.Empty<string>());
            var response = await epochFlowApi.PostMeasurement(settings.ProjectId, settings.SetId, request
            );

            _logger.LogIfError(response);

            await Task.Delay(20000);
        }
    }

    public sealed class Settings : CommandSettings
    {
        [CommandOption("--url")]
        [Description("Epochflow API Url")]
        [DefaultValue("https://localhost:7125")]
        public string ApiUrl { get; set; } = string.Empty;

        [CommandOption("--key")]
        [Description("Epochflow API key")]
        public string ApiKey { get; set; } = string.Empty;

        [CommandOption("--project")]
        [Description("Epochflow Project Id")]
        public string ProjectId { get; set; } = string.Empty;

        [CommandOption("--set")]
        [Description("Epochflow Set Id")]
        public string SetId { get; set; } = string.Empty;
    }
}