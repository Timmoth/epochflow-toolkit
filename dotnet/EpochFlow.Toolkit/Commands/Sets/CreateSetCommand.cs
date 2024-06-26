﻿using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using EpochFlow.ApiClient;
using EpochFlow.ApiClient.Measurements.Sets;
using EpochFlow.ApiClient.Models;
using EpochFlow.ApiClient.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;
using Spectre.Console;
using Spectre.Console.Cli;

namespace EpochFlow.Toolkit.Commands.Sets;

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
        httpClient.DefaultRequestHeaders.Add("X-API-Key", settings.ApiKey);

        var epochFlowApi = RestService.For<IEpochFlowV1>(httpClient, new RefitSettings());

        var stopwatch = Stopwatch.StartNew();
        var response = await epochFlowApi.CreateMeasurementSet(settings.ProjectId,CreateMeasurementSet.Create(settings.SetName, settings.SamplePeriod, settings.RetentionPeriod));
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

    public sealed class Settings : ProjectBaseSettings
    {
        [CommandOption("--name")]
        [Description("Set name")]
        public string SetName { get; set; } = string.Empty;

        [CommandOption("--sample-period")]
        [Description("Sample period, in minutes")]
        public int SamplePeriod { get; set; } = 60;

        [CommandOption("--retention-period")]
        [Description("Retention period, in days")]
        public int? RetentionPeriod { get; set; }
        public override ValidationResult Validate()
        {
            var baseValidationResult = base.Validate();
            if (!baseValidationResult.Successful) return baseValidationResult;

            if (string.IsNullOrWhiteSpace(SetName)) return ValidationResult.Error("Specify Set name with '--name'");

            if (SetName.Length <= 3) return ValidationResult.Error("Set name must be at least three characters.");

            if (SetName.Length > 256) return ValidationResult.Error("Set name must be 256 characters or less.");

            if(SamplePeriod is <= 0 or > 3600)
                return ValidationResult.Error("Sample period must be between 1 and 3600 (1 day)");
            
            return ValidationResult.Success();
        }
    }
}