using System.ComponentModel;
using System.Diagnostics;
using EpochFlow.ApiClient;
using EpochFlow.ApiClient.Measurements;
using EpochFlow.ApiClient.Utilities;
using EpochFlow.Toolkit.Commands.Sets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;
using Spectre.Console;
using Spectre.Console.Cli;

namespace EpochFlow.Toolkit.Commands.DataGenerator;

public sealed class GenerateDataCommand : AsyncCommand<GenerateDataCommand.Settings>
{
    private readonly ILogger<GenerateDataCommand> _logger;
    private readonly IServiceProvider _serviceProvider;

    public GenerateDataCommand(ILogger<GenerateDataCommand> logger, IServiceProvider serviceProvider)
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

        for (var i = 0; i < 30; i++)
        {
            var stopwatch = Stopwatch.StartNew();
            var response = await epochFlowApi.PostMeasurement(settings.ProjectId, settings.SetId,
                new Measurement(DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Random.Shared.NextDouble(),
                    settings.Source, Array.Empty<string>()));

            stopwatch.Stop();
            _logger.LogInformation(
                "Completed with status code: status code: [{StatusCode}] in {Duration}ms",
                response.StatusCode,
                stopwatch.ElapsedMilliseconds);

            _logger.LogIfError(response);

            await Task.Delay(10000);
        }

        return 0;
    }

    public sealed class Settings : ProjectBaseSettings
    {
        [CommandOption("--id")]
        [Description("Set Id")]
        public string SetId { get; set; } = string.Empty;

        [CommandOption("--tag")]
        [Description("Source name")]
        public string Source { get; set; } = string.Empty;

        public override ValidationResult Validate()
        {
            var baseValidationResult = base.Validate();
            if (!baseValidationResult.Successful) return baseValidationResult;

            if (string.IsNullOrWhiteSpace(SetId)) return ValidationResult.Error("Specify Set Id with '--id'");

            if (string.IsNullOrWhiteSpace(Source)) return ValidationResult.Error("Specify Source name with '--source'");

            return ValidationResult.Success();
        }
    }
}