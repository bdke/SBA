namespace SBASeatingPlan.Admin.Commands.Generate
{
    internal static class GenerateHelper
    {
        public static IEnumerable<IEnumerable<T>> DifferentCombinations<T>(this IEnumerable<T> elements, int k) =>
            k == 0 ? new[] { Array.Empty<T>() } :
              elements.SelectMany((e, i) =>
                elements.Skip(i + 1).DifferentCombinations(k - 1).Select(c => (new[] { e }).Concat(c)));
    }
}
