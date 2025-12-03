
namespace AOC.ConsoleApp.Day03;

/// <summary>
/// Solution 1: 16946
/// Solution 2: 168627047606506
/// </summary>

internal class Day03Solver : ISolver
{
    private readonly IEnumerable<string> _banks;
    public Day03Solver (IEnumerable<string> lines)
    {
        _banks = lines;
    }

    public string SolveFirstChallenge()
    {
        return _banks
            .Sum(bank => int.Parse(GetLargestPossibleJoltage(bank, 2)))
            .ToString();
    }

    public string SolveSecondChallenge()
    {
        return _banks
            .Select(bank => UInt128.Parse(GetLargestPossibleJoltage(bank, numberOfDigits: 12)))
            .Aggregate((UInt128)0, (sum, joltage) => sum + joltage)
            .ToString();
    }

    private string GetLargestPossibleJoltage(string bank, int numberOfDigits)
    {
        if (numberOfDigits == 1) return "" + GetGreatestDigit(bank, 0, bank.Length);

        var firstJoltageDigit = GetGreatestDigit(bank, startIndex: 0, endIndex: bank.Length - (numberOfDigits - 1));
        var firstOccurranceOfFirstDigit = bank.IndexOf(firstJoltageDigit);
        var remainingJoltageDigits = GetLargestPossibleJoltage(bank[(firstOccurranceOfFirstDigit + 1)..], numberOfDigits - 1);
        return "" + firstJoltageDigit + remainingJoltageDigits;
    }

    private static char GetGreatestDigit(string bank, int startIndex, int endIndex)
    {
        var substring = bank.Substring(startIndex, endIndex - startIndex);
        return substring.MaxBy(digitString => int.Parse("" + digitString));
    }
}
