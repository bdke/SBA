using Spectre.Console.Cli;

namespace SBASeatingPlan.Admin.Commands.UserData
{
    public class UserDataCommandSettings : CommandSettings
    {
        [CommandArgument(0, "<EntryCode>")]
        public string EntryCode { get; set; } = string.Empty;
    }
}
