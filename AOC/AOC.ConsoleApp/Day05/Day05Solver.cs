using AOC.ConsoleApp.Day02;

namespace AOC.ConsoleApp.Day05;

/// <summary>
/// Solution 1: 513
/// Solution 2: 339668510830757
/// </summary>

internal class Day05Solver : ISolver
{
    public readonly IEnumerable<AocRange> _ranges;
    public readonly IEnumerable<UInt128> _values;

    public Day05Solver(IEnumerable<string> lines)
    {
        var whiteLineIndex = lines
            .Index()
            .First(tuple => string.IsNullOrWhiteSpace(tuple.Item))
            .Index;
        _ranges = lines.TakeWhile(line => !string.IsNullOrWhiteSpace(line))
            .Select(AocRange.FromString);
        _values = lines.SkipWhile(line => !string.IsNullOrWhiteSpace(line))
            .Skip(1)
            .Select(UInt128.Parse);
    }

    public string SolveFirstChallenge()
    {
        var freshIngredientsCount = 0;
        foreach(var value in _values)
        {
            if (_ranges.Any(range => range.Contains(value))) freshIngredientsCount++;
        }
        return freshIngredientsCount.ToString();
    }

    public string SolveSecondChallenge()
    {
        var nonOverlappingRanges = new List<AocRange>();

        foreach(var range in _ranges)
        {
            var rangesToCombine = nonOverlappingRanges.Where(range.CanBeCombinedWith);

            var resultingRange = range;
            foreach (var other in rangesToCombine.ToList())
            {
                resultingRange.tryCombineWith(other, out var combinedRange);
                nonOverlappingRanges.Remove(other);
                resultingRange = combinedRange!;
            }
            nonOverlappingRanges.Add(resultingRange);
        }

        var nonOverlappingRangesSorted = nonOverlappingRanges.OrderBy(range => range.Start);
        foreach (var range in nonOverlappingRanges)
        {
            Console.WriteLine($"{range}         Span: {range.Span}");
        }


        var freshIngredientsCount = nonOverlappingRanges
            .Select(range => range.Span)
            .Aggregate((UInt128)0, (total, value) => total += value);


        return freshIngredientsCount.ToString();
    }
}
