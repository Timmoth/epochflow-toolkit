using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using EpochFlow.ApiClient;
using EpochFlow.ApiClient.Models;
using EpochFlow.ApiClient.Tables;
using EpochFlow.ApiClient.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Refit;
using Spectre.Console;
using Spectre.Console.Cli;

namespace EpochFlow.Toolkit.Commands.Sets.Events;

public sealed class ExportMeasurementsCommand : AsyncCommand<ExportMeasurementsCommand.Settings>
{
    private readonly ILogger<ExportMeasurementsCommand> _logger;
    private readonly IServiceProvider _serviceProvider;

    public ExportMeasurementsCommand(ILogger<ExportMeasurementsCommand> logger, IServiceProvider serviceProvider)
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
        var stopwatch = Stopwatch.StartNew();


        var tableQuery = new TableQuery()
        {
            Start = new DateTimeOffset(settings.Start).ToUnixTimeSeconds(),
            End = new DateTimeOffset(settings.End).ToUnixTimeSeconds(),
            Resolution = QueryResolution.Minute,
            Queries = new TableSubQuery[]
            {
                new MeasurementQuery()
                {
                    Aggregation = new List<QueryAggregation>() { QueryAggregation.Average },
                    Name = "value",
                    SetId = settings.SetId,
                    Sources = new List<string>()
                    {
                        settings.Source
                    }, Tags = new List<string>()
                }
            }
        };

        var response = await epochFlowApi.GetTable(
            settings.ProjectId,
            Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(tableQuery, SerializerExtensions.Options)))
        );

        stopwatch.Stop();
        _logger.LogInformation(
            "Completed with status code: status code: [{StatusCode}] in {Duration}ms",
            response.StatusCode,
            stopwatch.ElapsedMilliseconds);

        _logger.LogIfError(response);

        var csv = new StringBuilder();

        // Add the header row
        csv.AppendLine("timestamp,event,correlation,insert,source,tags,string_state,numeric_state");

        // Add data rows
        foreach (var dataPoint in response.Content.Data)
        {
            csv.AppendLine(string.Join(",", dataPoint));
        }

        // Write the CSV content to a file
        await File.WriteAllTextAsync(settings.File, csv.ToString());

        return 0;
    }

    public sealed class Settings : ProjectBaseSettings
    {
        [CommandOption("--id")]
        [Description("Set Id")]
        public string SetId { get; set; } = string.Empty;

        [CommandOption("--file")]
        [Description("File")]
        public string File { get; set; } = string.Empty;

        [CommandOption("--source")]
        [Description("Source name")]
        public string Source { get; set; } = string.Empty;

        [CommandOption("--start")]
        [Description("Start")]
        public DateTime Start { get; set; }

        [CommandOption("--end")]
        [Description("End")]
        public DateTime End { get; set; }

        public override ValidationResult Validate()
        {
            var baseValidationResult = base.Validate();
            if (!baseValidationResult.Successful) return baseValidationResult;


            return ValidationResult.Success();
        }
    }
}