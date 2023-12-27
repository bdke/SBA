using Firebase.Database.Query;
using Spectre.Console;
using Spectre.Console.Cli;

namespace SBASeatingPlan.Admin.Commands.UserData
{
    internal class EditUserDataCommand : Command<EditUserDataCommandSettings>
    {
        public override int Execute(CommandContext context, EditUserDataCommandSettings settings)
        {
            var model = new UserModel(settings.EntryCode)
            {
                Email = settings.Email
            };
            var datas = AdminCLI.GetUserDatas().Result;
            var user = datas.FirstOrDefault(v => v.Value.EntryCode == model.EntryCode);
            if (user.Key == null)
            {
                AnsiConsole.MarkupLine($"[red]entry code[/] {settings.EntryCode} [red]does not exist[/]");
                return 1;
            }

            AdminCLI.Database.Child("users").Child(user.Key).PutAsync(model).Wait();
            AnsiConsole.MarkupLine($"[green]Successfully edit[/] {settings.EntryCode} [green]in database[/]");
            return 0;
        }
    }
}
