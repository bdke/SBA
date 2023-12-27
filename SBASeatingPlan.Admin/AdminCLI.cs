using Firebase.Database;
using SBASeatingPlan.Admin.Commands.Generate;
using SBASeatingPlan.Admin.Commands.SubmittionData;
using SBASeatingPlan.Admin.Commands.UserData;
using Spectre.Console.Cli;

namespace SBASeatingPlan.Admin
{
    internal class AdminCLI
    {
        public static FirebaseClient Database = new("https://lkpfcsba-default-rtdb.asia-southeast1.firebasedatabase.app/");
        public static CommandApp CommandApp = new();
        static async Task<int> Main(string[] args)
        {
            CommandApp.Configure(config =>
            {
                config.AddBranch<UserDataCommandSettings>("user", c =>
                {
                    c.AddCommand<AddUserDataCommand>("add");
                    c.AddCommand<DeleteUserDataCommand>("delete");
                    c.AddCommand<EditUserDataCommand>("edit");
                });

                config.AddBranch<SubmittionDataCommandSettings>("submittion", c =>
                {
                    c.AddCommand<AddSubmittionDataCommand>("add");
                });

                config.AddCommand<GenerateCommand>("generate");
            });
            return await CommandApp.RunAsync(args);
        }

        public static async Task<Dictionary<string, UserModel>> GetUserDatas()
        {
            var r = await Database.Child("users").OnceAsync<UserModel>();
            return new(r.Select(v => new KeyValuePair<string, UserModel>(v.Key, v.Object)));
        }

        public static async Task<Dictionary<string, SubmittionModel>> GetSubmittionDatas()
        {
            var r = await Database.Child("submittions").OnceAsync<SubmittionModel>();
            return new(r.Select(v => new KeyValuePair<string, SubmittionModel>(v.Key, v.Object)));
        }
    }
}
