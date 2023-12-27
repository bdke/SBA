using Firebase.Database.Query;
using Spectre.Console;
using Spectre.Console.Cli;

namespace SBASeatingPlan.Admin.Commands.UserData
{
    internal class DeleteUserDataCommand : Command<DeleteUserDataCommandSettings>
    {
        public override int Execute(CommandContext context, DeleteUserDataCommandSettings settings)
        {
            var datas = AdminCLI.GetUserDatas().Result;
            var user = datas.FirstOrDefault(v => v.Value.EntryCode == settings.EntryCode);
            if (user.Key == null)
            {
                AnsiConsole.MarkupLine($"[red]entry code[/] {settings.EntryCode} [red]does not exist[/]");
                return 1;
            }
            if (settings.DeleteEmail)
                AdminCLI.Database.Child("users").Child(user.Key).PutAsync(new UserModel(settings.EntryCode)).Wait();
            else
                AdminCLI.Database.Child("users").Child(user.Key).DeleteAsync().Wait();
            var extraMessage = settings.DeleteEmail ? "'s email " : "";
            AnsiConsole.MarkupLine($"[green]Successfully delete[/] {settings.EntryCode} [green]{extraMessage}in database[/]");
            return 0;
        }
    }
}
