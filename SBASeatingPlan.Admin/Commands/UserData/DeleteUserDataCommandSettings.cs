using Spectre.Console.Cli;

namespace SBASeatingPlan.Admin.Commands.UserData
{
    internal class DeleteUserDataCommandSettings : UserDataCommandSettings
    {
        [CommandOption("-d|--delete-email")]
        public bool DeleteEmail { get; set; } = false;
    }
}
