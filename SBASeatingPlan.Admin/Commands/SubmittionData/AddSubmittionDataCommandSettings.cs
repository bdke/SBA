using Spectre.Console.Cli;

namespace SBASeatingPlan.Admin.Commands.SubmittionData
{
    internal class AddSubmittionDataCommandSettings : SubmittionDataCommandSettings
    {
        [CommandOption("-a|--age")]
        public int Age { get; set; } = -1;
        [CommandOption("-e|--employment")]
        public string Employment { get; set; } = string.Empty;
        [CommandOption("-t|--grad-time")]
        public int GradTime { get; set; } = -1;
        [CommandOption("-n|--name")]
        public string Name { get; set; } = string.Empty;
        [CommandOption("-s|--seat")]
        public int SeatsRequired { get; set; } = -1;
        [CommandOption("-g|--sex")]
        public string Sex { get; set; } = string.Empty;
    }
}
