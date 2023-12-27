using Spectre.Console.Cli;

namespace SBASeatingPlan.Admin.Commands.SubmittionData
{
    internal class SubmittionDataCommandSettings : CommandSettings
    {
        [CommandArgument(0, "<EntryCode>")]
        public string EntryCode { get; set; } = string.Empty;
    }
}
