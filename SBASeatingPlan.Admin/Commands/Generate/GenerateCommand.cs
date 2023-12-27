using Spectre.Console;
using Spectre.Console.Cli;
using System.Runtime.InteropServices;

namespace SBASeatingPlan.Admin.Commands.Generate
{
    internal class GenerateCommand : Command<GenerateCommandSettings>
    {
        public override int Execute(CommandContext context, GenerateCommandSettings settings)
        {
            var submittions = AdminCLI.GetSubmittionDatas().Result;

            while (true)
            {
                var datas = submittions.Values;

                var allComb = new List<IEnumerable<IEnumerable<SubmittionModel>>>();
                for (var i = 8; i >= 1; i--)
                {
                    var comb = datas
                        .DifferentCombinations(i)
                        .Where(v => v.Sum(x => x.SeatsRequired) <= 8)
                        .ToList();
                    var arr = CollectionsMarshal.AsSpan(comb);
                    var removes = new List<int>();
                    if (settings.BalanceGender && i > 2)
                    {
                        for (var j = 1; j < arr.Length; j++)
                        {
                            ref var item = ref arr[j];
                            var maleCount = item.Count(v => v.Sex == "Male");
                            if (maleCount > item.Count() / 2)
                                removes.Add(j);
                        }
                        removes.OrderDescending().ToList().ForEach(comb.RemoveAt);
                        removes.Clear();
                    }
                    if (settings.Age && i > 2)
                    {
                        for (var j = 1; j < arr.Length; j++)
                        {
                            ref var item = ref arr[j];
                            if (item == null) continue;
                            var averageAge = item.Average(v => v.Age);
                            var sd = Math.Sqrt(item.Average(v => Math.Pow(v.Age - averageAge, 2)));
                            if (sd > 8)
                                removes.Add(j);
                        }
                        removes.OrderDescending().ToList().ForEach(comb.RemoveAt);
                        removes.Clear();
                    }
                    if (settings.Employment && i > 2)
                    {
                        for (var j = 1; j < arr.Length; j++)
                        {
                            var item = arr[j];
                            if (item == null) continue;
                            var s = item.Select(v => item.Count(x => x.Employment == v.Employment));
                            if (s.Average() < 4)
                                removes.Add(j);
                        }
                        removes.OrderDescending().ToList().ForEach(comb.RemoveAt);
                        removes.Clear();
                    }
                    allComb.Add(comb);
                }

                var arranged = new List<string>();
                var arrangement = new List<IEnumerable<SubmittionModel>>();
                for (var i = 0; i < allComb.Count; i++)
                {
                    var comb = allComb[i];
                    if (!comb.Any())
                        continue;
                    var random = new Random();
                    var index = random.Next(0, comb.Count() - 1);
                    var c = comb.ElementAt(index);
                    arranged.AddRange(c.Select(v => v.EntryCode));
                    arrangement.Add(c);

                    allComb = allComb.Select(c =>
                    {
                        return c.Where(v => !v.Any(x => arranged.Contains(x.EntryCode)));
                    }).ToList();
                    if (allComb[i].Any())
                        i--;
                }

                using var csv = new StreamWriter(settings.OutputPath);
                csv.WriteLine($"id,name,seats,entry_code");
                for (var i = 0; i < arrangement.Count; i++)
                {
                    arrangement[i]
                        .Select(v => $"{i},{v.Name},{v.SeatsRequired},{v.EntryCode}")
                        .ToList()
                        .ForEach(csv.WriteLine);
                }

                AnsiConsole.MarkupLine($"[green]File[/] {settings.OutputPath} [green]Generated[/]");
                return 0;
            }
        }
    }
}
