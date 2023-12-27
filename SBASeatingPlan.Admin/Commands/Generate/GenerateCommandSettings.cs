using Spectre.Console.Cli;

namespace SBASeatingPlan.Admin.Commands.Generate
{
    internal class GenerateCommandSettings : CommandSettings
    {
        [CommandOption("-b|--balance")]
        public bool BalanceGender { get; set; } = false;
        [CommandOption("-a|--age")]
        public bool Age { get; set; } = false;
        [CommandOption("-e|--employment")]
        public bool Employment { get; set; } = false;
        [CommandOption("-o|--output")]
        public string OutputPath { get; set; } = "result.csv";
    }
}
