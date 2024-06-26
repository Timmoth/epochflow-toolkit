﻿using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using EpochFlow.ApiClient;
using EpochFlow.ApiClient.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;
using Spectre.Console;
using Spectre.Console.Cli;

namespace EpochFlow.Toolkit.Commands.Sets.Tags;

public sealed class ListTagsCommand : AsyncCommand<ListTagsCommand.Settings>
{
    private readonly ILogger<ListTagsCommand> _logger;
    private readonly IServiceProvider _serviceProvider;

    public ListTagsCommand(ILogger<ListTagsCommand> logger, IServiceProvider serviceProvider)
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
        var response = await epochFlowApi.ListMeasurementTags(settings.ProjectId, settings.SetId);
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
        [CommandOption("--id")]
        [Description("Set Id")]
        public string SetId { get; set; } = string.Empty;

        public override ValidationResult Validate()
        {
            var baseValidationResult = base.Validate();
            if (!baseValidationResult.Successful) return baseValidationResult;

            if (string.IsNullOrWhiteSpace(SetId)) return ValidationResult.Error("Specify Set Id with '--id'");

            return ValidationResult.Success();
        }
    }
}