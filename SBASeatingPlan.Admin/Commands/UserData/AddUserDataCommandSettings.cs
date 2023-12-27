using Spectre.Console.Cli;

namespace SBASeatingPlan.Admin.Commands.UserData
{
    internal class AddUserDataCommandSettings : UserDataCommandSettings
    {
        [CommandArgument(0, "[Email]")]
        public string Email { get; set; } = string.Empty;
    }
}
