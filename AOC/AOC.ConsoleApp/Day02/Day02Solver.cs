namespace AOC.ConsoleApp.Day02;

// Part 1
// 18893502033 ==> Correct!!  

// Part 2
// 26202168557 ==> Correct!!

internal class Day02Solver : ISolver
{
    private IEnumerable<AocRange> _ranges;

    public Day02Solver(IEnumerable<string> lines)
    {
        var rangeStrings = lines.First().Split(',');
        _ranges = rangeStrings.Select(str =>
        {
            var splittedString = str.Split('-');
            var start = UInt128.Parse(splittedString[0]);
            var end = UInt128.Parse(splittedString[1]);
            return new AocRange { Start = start, End = end };
        });
    }

    public string SolveFirstChallenge()
    { 
        UInt128 sum = 0;
        foreach (var range in _ranges)
        {
            var invalidCodes = range.GetInvalidCodes(numberOfRepetitions: 2);
            Console.WriteLine(string.Join(", ", invalidCodes));
            sum += invalidCodes.Aggregate((UInt128)0, (sum, code) => sum += UInt128.Parse(code));
        }
        return sum.ToString();
    }

    public string SolveSecondChallenge()
    {
        UInt128 sum = 0;
        foreach (var range in _ranges)
        {
            var invalidCodes = range.GetInvalidCodes();
            Console.WriteLine(string.Join(", ", invalidCodes));
            sum += invalidCodes.Aggregate((UInt128)0, (sum, code) => sum += UInt128.Parse(code));
        }
        return sum.ToString();
    }
}
