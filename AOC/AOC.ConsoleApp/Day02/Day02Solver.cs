namespace AOC.ConsoleApp.Day02;

// 18828970995 ==> Too low

internal class Day02Solver : ISolver
{
    public string SolveFirstChallenge(IEnumerable<string> lines)
    {
        var rangeStrings = lines.First().Split(',');
        var ranges = rangeStrings.Select(str =>
        {
            var splittedString = str.Split('-');
            var start = UInt128.Parse(splittedString[0]);
            var end = UInt128.Parse(splittedString[1]);
            return new Range { Start = start, End = end };
        });

        UInt128 sum = 0;
        foreach (var range in ranges)
        {
            var invalidCodes = range.GetInvalidCodes();
            Console.WriteLine(string.Join(", ", invalidCodes));
            sum += invalidCodes.Aggregate((UInt128)0, (sum, code) => sum += UInt128.Parse(code));
        }
        return sum.ToString();
    }

    public string SolveSecondChallenge(IEnumerable<string> lines)
    {
        throw new NotImplementedException();
    }
}

internal record Range
{
    private string _startString => Start.ToString();
    private string _endString => End.ToString();

    public required UInt128 Start { get; set; }
    public required UInt128 End { get; set; }

    public IEnumerable<string> GetInvalidCodes()
    {
        var numberOfDigitsStart = _startString.Length;
        var numberOfDigitsEnd = _endString.Length;

        for (var numberOfDigits = numberOfDigitsStart; numberOfDigits <= numberOfDigitsEnd; numberOfDigits++)
        {
            if (numberOfDigits % 2 != 0) continue;

            var numberOfDigitsDevidedBy2 = numberOfDigits / 2;
            var lowerBoundFirstHalfToCheck = (uint) Math.Pow(10, numberOfDigitsDevidedBy2 - 1);
            var upperBoundFirstHalfToCheck = (uint) Math.Pow(10, numberOfDigitsDevidedBy2) - 1;

            for (var firstHalfToCheck = lowerBoundFirstHalfToCheck; firstHalfToCheck <= upperBoundFirstHalfToCheck; firstHalfToCheck++)
            {
                var firstHalfToCheckString = firstHalfToCheck.ToString();
                var correspondingInvalidCode = firstHalfToCheckString + firstHalfToCheckString;
                var invalidCodeAsInt = UInt128.Parse(correspondingInvalidCode);

                if (invalidCodeAsInt >= Start && invalidCodeAsInt <= End) yield return correspondingInvalidCode;
            }
        }
    }
}