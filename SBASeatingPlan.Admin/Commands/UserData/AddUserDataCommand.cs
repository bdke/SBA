using Spectre.Console;
using Spectre.Console.Cli;
using Firebase.Database.Query;

namespace SBASeatingPlan.Admin.Commands.UserData
{
    internal class AddUserDataCommand : Command<AddUserDataCommandSettings>
    {
        public override int Execute(CommandContext context, AddUserDataCommandSettings settings)
        {
            var model = new UserModel(settings.EntryCode)
            {
                Email = settings.Email
            };
            var datas = AdminCLI.GetUserDatas().Result;
            var user = datas.FirstOrDefault(v => v.Value.EntryCode == model.EntryCode);
            if (user.Key != null)
            {
                AnsiConsole.MarkupLine($"[red]entry code[/] {settings.EntryCode} [red]already exists[/]");
                return 1;
            }

            AdminCLI.Database.Child("users").PostAsync(model).Wait();
            AnsiConsole.MarkupLine($"[green]Successfully add[/] {settings.EntryCode} [green]to database[/]");
            return 0;
        }
    }
}
