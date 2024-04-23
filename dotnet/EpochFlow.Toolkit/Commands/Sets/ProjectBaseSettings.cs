using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace EpochFlow.Toolkit.Commands.Sets;

public class ProjectBaseSettings : EpochFlowBaseSettings
{
    [CommandOption("--project")]
    [Description("Project Id")]
    public string ProjectId { get; set; } = string.Empty;
    public override ValidationResult Validate()
    {
        var baseValidationResult = base.Validate();
        if (!baseValidationResult.Successful) return baseValidationResult;

        if (string.IsNullOrWhiteSpace(ProjectId))
        {
            return ValidationResult.Error(
                "Specify Project Id with '--project'.");
        }

        return ValidationResult.Success();
    }
}