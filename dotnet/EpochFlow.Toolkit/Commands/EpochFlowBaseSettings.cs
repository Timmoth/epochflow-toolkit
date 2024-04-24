using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace EpochFlow.Toolkit.Commands;

public abstract class EpochFlowBaseSettings : CommandSettings
{
    [CommandOption("--url")]
    [Description("API Url")]
    public string ApiUrl { get; set; } = string.Empty;

    [CommandOption("--key")]
    [Description("API key")]
    public string ApiKey { get; set; } = string.Empty;

    [CommandOption("--emulator")]
    [Description("Use emulator")]
    public bool Emulator { get; set; } = false;

    public override ValidationResult Validate()
    {
        if (string.IsNullOrWhiteSpace(ApiUrl))
        {
            if (!Emulator)
            {
                ApiUrl = Environment.GetEnvironmentVariable("epochflow_url") ?? string.Empty;
                if (string.IsNullOrWhiteSpace(ApiUrl))
                    return ValidationResult.Error(
                        "Specify Api url with '--url' or set 'epochflow_url' environment variable.");
            }

            ApiUrl = "http://localhost:8085";
        }

        if (string.IsNullOrWhiteSpace(ApiKey) && !Emulator)
        {
            ApiKey = Environment.GetEnvironmentVariable("epochflow_key") ?? string.Empty;
            if (string.IsNullOrWhiteSpace(ApiKey))
                return ValidationResult.Error(
                    "Specify Api Key with '--key' or set 'epochflow_key' environment variable.");
        }

        return ValidationResult.Success();
    }
}