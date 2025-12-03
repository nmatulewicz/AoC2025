namespace AOC.ConsoleApp.Day03;

/// <summary>
/// Part 1: 16946
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
        return _banks.Sum(GetLargestPossibleJoltage).ToString();
    }

    public string SolveSecondChallenge()
    {
        throw new NotImplementedException();
    }

    private int GetLargestPossibleJoltage(string bank)
    {
        var firstJoltageDigit = GetGreatestDigit(bank, startIndex: 0, endIndex: bank.Length - 1);
        var firstOccurranceOfFirstDigit = bank.IndexOf(firstJoltageDigit);
        var secondJoltageDigit = GetGreatestDigit(bank, startIndex: firstOccurranceOfFirstDigit + 1, bank.Length);
        return int.Parse("" + firstJoltageDigit + secondJoltageDigit);
    }

    private static char GetGreatestDigit(string bank, int startIndex, int endIndex)
    {
        var substring = bank.Substring(startIndex, endIndex - startIndex);
        return substring.MaxBy(digitString => int.Parse("" + digitString));
    }
}
