using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using EpochFlow.ApiClient;
using EpochFlow.ApiClient.Measurements;
using EpochFlow.ApiClient.Models;
using EpochFlow.ApiClient.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;
using Spectre.Console;
using Spectre.Console.Cli;

namespace EpochFlow.Toolkit.Commands.Sets.Data;

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
        httpClient.DefaultRequestHeaders.Add("X-API-Key", settings.ApiKey);

        var epochFlowApi = RestService.For<IEpochFlowV1>(httpClient, new RefitSettings());

        var start = new DateTimeOffset(settings.Start).ToUnixTimeSeconds();
        var end = new DateTimeOffset(settings.End).ToUnixTimeSeconds();

        var stopwatch = Stopwatch.StartNew();
        var response = await epochFlowApi.GetMeasurements(settings.ProjectId, settings.SetId,
            start, end, settings.Source, settings.Tag, QueryResolution.Hour, QueryAggregation.Average, new List<string>());
        stopwatch.Stop();
        _logger.LogInformation(
            "Completed with status code: status code: [{StatusCode}] in {Duration}ms",
            response.StatusCode,
            stopwatch.ElapsedMilliseconds);

        if (response.IsSuccessStatusCode && response.Content != null)
            _logger.LogInformation(JsonSerializer.Serialize(response.Content.Data, new JsonSerializerOptions
            {
                WriteIndented = true
            }));
        else
            _logger.LogIfError(response);
        return 0;
    }

    public sealed class Settings : ProjectBaseSettings
    {
        [CommandOption("--id")]
        [Description("Set Id")]
        public string SetId { get; set; } = string.Empty;

        [CommandOption("--start")]
        [Description("Start datetime")]
        public DateTime Start { get; set; } = DateTime.Now.AddMonths(-1);

        [CommandOption("--end")]
        [Description("End datetime")]
        public DateTime End { get; set; } = DateTime.Now;

        [CommandOption("--source")]
        [Description("Source")]
        public string Source { get; set; } = string.Empty;

        [CommandOption("--tag")]
        [Description("Tag")]
        public string Tag { get; set; } = string.Empty;

        public override ValidationResult Validate()
        {
            var baseValidationResult = base.Validate();
            if (!baseValidationResult.Successful) return baseValidationResult;

            if (string.IsNullOrWhiteSpace(SetId)) return ValidationResult.Error("Specify Set Id with '--id'");

            return ValidationResult.Success();
        }
    }
}