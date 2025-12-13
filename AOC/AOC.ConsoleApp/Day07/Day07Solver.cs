using AOC.ConsoleApp.Shared;

namespace AOC.ConsoleApp.Day07;

/// <summary>
/// Solution 1: 1537
/// Solution 2:
/// </summary> 

internal class Day07Solver : ISolver
{
    private readonly Grid _grid;

    public Day07Solver(IEnumerable<string> lines)
    {
        _grid = new Grid(lines);
    }

    public string SolveFirstChallenge()
    {
        var start = _grid.First(IsStart);
        TrySendBeam(start.GetNeighbour(1, 0));

        Console.WriteLine(_grid.ToString());
        var splitters = _grid.Where(IsSplitter);
        var activatedSplitters = splitters.Where(IsActivated);

        return activatedSplitters.Count().ToString();
    }

    public string SolveSecondChallenge()
    {
        return "";
    }

    public static bool IsStart(GridPosition<char> position)
    {
        return position.Value == 'S';
    }

    public static bool IsSplitter(GridPosition<char> position)
    {
        return position.Value == '^';
    }

    public static bool IsEmptySpace(GridPosition<char> position)
    {
        return position.Value == '.';
    }

    public static bool IsBeam(GridPosition<char> position)
    {
        return position.Value == '|';
    }

    public static bool IsActivated(GridPosition<char> position)
    {
        var topNeighbour = position.GetNeighbour(-1, 0);
        return IsBeam(topNeighbour);
    }

    public static void TrySendBeam(GridPosition<char> position)
    {
        if (!position.IsValidPosition) return;
        else if (IsBeam(position)) return;
        else if (IsEmptySpace(position))
        {
            position.Value = '|';
            var nextPosition = position.GetNeighbour(1, 0);
            TrySendBeam(nextPosition);
        }
        else if (IsSplitter(position))
        {
            TrySendBeam(position.GetNeighbour(0, -1));
            TrySendBeam(position.GetNeighbour(0, 1));
        }
    }
}
