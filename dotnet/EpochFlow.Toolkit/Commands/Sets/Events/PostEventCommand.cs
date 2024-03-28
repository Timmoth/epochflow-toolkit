using System.ComponentModel;
using System.Diagnostics;
using EpochFlow.ApiClient;
using EpochFlow.ApiClient.Events;
using EpochFlow.ApiClient.Measurements;
using EpochFlow.ApiClient.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;
using Spectre.Console;
using Spectre.Console.Cli;

namespace EpochFlow.Toolkit.Commands.DataGenerator;

public sealed class PostEventCommand : AsyncCommand<PostEventCommand.Settings>
{
    private readonly ILogger<PostEventCommand> _logger;
    private readonly IServiceProvider _serviceProvider;

    public PostEventCommand(ILogger<PostEventCommand> logger, IServiceProvider serviceProvider)
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
        var response = await epochFlowApi.PostEvent(settings.SetId,
            new EventDataPoint()
            {
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Tag = settings.Tag,
                Event = settings.Event,
                CorrelationId = settings.CorrelationId
            });

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
        [Description("Set Id")]
        public string SetId { get; set; } = string.Empty;

        [CommandOption("--tag")]
        [Description("Tag name")]
        public string Tag { get; set; } = string.Empty;

        [CommandOption("--event")]
        [Description("Event name")]
        public string Event { get; set; } = string.Empty;

        [CommandOption("--correlation_id")]
        [Description("Correlation Id")]
        public long CorrelationId { get; set; }

        public override ValidationResult Validate()
        {
            var baseValidationResult = base.Validate();
            if (!baseValidationResult.Successful) return baseValidationResult;

            if (string.IsNullOrWhiteSpace(SetId)) return ValidationResult.Error("Specify Set Id with '--id'");

            if (string.IsNullOrWhiteSpace(Tag)) return ValidationResult.Error("Specify Tag name with '--tag'");

            if (string.IsNullOrWhiteSpace(Event)) return ValidationResult.Error("Specify Event name with '--tag'");

            return ValidationResult.Success();
        }
    }
}