using System.Text;

namespace AOC.ConsoleApp.Day02;

internal record AocRange
{
    private string _startString => Start.ToString();
    private string _endString => End.ToString();

    public required UInt128 Start { get; set; }
    public required UInt128 End { get; set; }

    public UInt128 Span => End - Start + 1;

    /// <summary>
    /// Creates a new range for a string input like "27-32".
    /// </summary>
    public static AocRange FromString(string str)
    {
        var splittedString = str.Split('-');
        var start = UInt128.Parse(splittedString[0]);
        var end = UInt128.Parse(splittedString[1]);
        return new AocRange { Start = start, End = end };
    }

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

    public bool Contains(UInt128 value)
    {
        return value >= Start && value <= End;
    }

    public bool CanBeCombinedWith(AocRange other)
    {
        return this.End <= other.End && this.End + 1 >= other.Start
            || this.End > other.End && this.Start - 1 <= other.End;
    }

    public bool tryCombineWith(AocRange other, out AocRange? combinedRange)
    {
        if (!this.CanBeCombinedWith(other))
        {
            combinedRange = null;
            return false;
        }

        combinedRange = new AocRange
        {
            Start = this.Start <= other.Start ? this.Start : other.Start,
            End = this.End >= other.End ? this.End : other.End,
        };
        return true;
    }

    public override string ToString()
    {
        return $"{Start}-{End}";
    }
}