using Spectre.Console.Cli;
using System.ComponentModel;
using Spectre.Console;

namespace EpochFlow.Toolkit.Commands
{
    public abstract class EpochFlowBaseSettings : CommandSettings
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

            if (string.IsNullOrWhiteSpace(AccountId) && !Emulator)
            {
                AccountId = Environment.GetEnvironmentVariable("epochflow_account") ?? string.Empty;
                if (string.IsNullOrWhiteSpace(AccountId))
                    return ValidationResult.Error(
                        "Specify Account Id with '--account' or set 'epochflow_account' environment variable.");
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
}