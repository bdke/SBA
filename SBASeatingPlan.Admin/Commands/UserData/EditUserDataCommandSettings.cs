using Spectre.Console.Cli;

namespace SBASeatingPlan.Admin.Commands.UserData
{
    internal class EditUserDataCommandSettings : UserDataCommandSettings
    {
        [CommandArgument(0, "[Email]")]
        public string Email { get; set; } = string.Empty;
    }
}
