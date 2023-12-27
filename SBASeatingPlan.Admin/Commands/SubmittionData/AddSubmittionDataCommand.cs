using Firebase.Database.Query;
using Spectre.Console;
using Spectre.Console.Cli;

namespace SBASeatingPlan.Admin.Commands.SubmittionData
{
    internal class AddSubmittionDataCommand : Command<AddSubmittionDataCommandSettings>
    {
        public override int Execute(CommandContext context, AddSubmittionDataCommandSettings settings)
        {
            var model = new SubmittionModel()
            {
                Age = settings.Age,
                Employment = settings.Employment,
                EntryCode = settings.EntryCode,
                GradTime = settings.GradTime,
                Name = settings.Name,
                SeatsRequired = settings.SeatsRequired,
                Sex = settings.Sex,
            };

            var datas = AdminCLI.GetSubmittionDatas().Result;
            var user = datas.FirstOrDefault(v => v.Value.EntryCode == model.EntryCode);
            if (user.Key != null)
            {
                AnsiConsole.MarkupLine($"[red]entry code[/] {settings.EntryCode} [red]already exists[/]");
                return 1;
            }

            AdminCLI.Database.Child("submittions").PostAsync(model).Wait();
            AnsiConsole.MarkupLine($"[green]Successfully add[/] {settings.EntryCode} [green]to database[/]");
            return 0;
        }
    }
}
