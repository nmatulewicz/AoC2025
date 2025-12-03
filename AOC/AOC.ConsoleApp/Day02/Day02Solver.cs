using System.Text;

namespace AOC.ConsoleApp.Day02;

// Part 1
// 18893502033 ==> Correct!!  

// Part 2
// 26202168557 ==> Correct!!

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
            var invalidCodes = range.GetInvalidCodes(numberOfRepetitions: 2);
            Console.WriteLine(string.Join(", ", invalidCodes));
            sum += invalidCodes.Aggregate((UInt128)0, (sum, code) => sum += UInt128.Parse(code));
        }
        return sum.ToString();
    }

    public string SolveSecondChallenge(IEnumerable<string> lines)
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
}

internal record Range
{
    private string _startString => Start.ToString();
    private string _endString => End.ToString();

    public required UInt128 Start { get; set; }
    public required UInt128 End { get; set; }

    public IEnumerable<string> GetInvalidCodes()
    {
        var numberOfDigitsEnd = _endString.Length;
        var invalidCodes = new List<string>();
        for (var i = 2; i <= numberOfDigitsEnd; i++) 
        {
            invalidCodes.AddRange(GetInvalidCodes(i));
        }
        return invalidCodes.Distinct();
    }


    public IEnumerable<string> GetInvalidCodes(int numberOfRepetitions)
    {
        var numberOfDigitsStart = _startString.Length;
        var numberOfDigitsEnd = _endString.Length;

        for (var numberOfDigits = numberOfDigitsStart; numberOfDigits <= numberOfDigitsEnd; numberOfDigits++)
        {
            if (numberOfDigits % numberOfRepetitions != 0) continue;

            var numberOfDigitsDevidedByN = numberOfDigits / numberOfRepetitions;
            var lowerBoundFirstPartToCheck = (uint)Math.Pow(10, numberOfDigitsDevidedByN - 1);
            var upperBoundFirstPartToCheck = (uint)Math.Pow(10, numberOfDigitsDevidedByN) - 1;

            for (var firstHalfToCheck = lowerBoundFirstPartToCheck; firstHalfToCheck <= upperBoundFirstPartToCheck; firstHalfToCheck++)
            {
                var firstHalfToCheckString = firstHalfToCheck.ToString();
                var sb = new StringBuilder();
                for (var i = 0; i < numberOfRepetitions; i++)
                {
                    sb.Append(firstHalfToCheckString);
                }
                var correspondingInvalidCode = sb.ToString();
                var invalidCodeAsInt = UInt128.Parse(correspondingInvalidCode);

                if (invalidCodeAsInt >= Start && invalidCodeAsInt <= End) yield return correspondingInvalidCode;
            }
        }
    }
}