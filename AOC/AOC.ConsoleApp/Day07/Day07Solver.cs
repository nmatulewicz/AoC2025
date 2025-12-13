using AOC.ConsoleApp.Shared;

namespace AOC.ConsoleApp.Day07;

/// <summary>
/// Solution 1: 1537
/// Solution 2: 18818811755665
/// </summary> 

internal class Day07Solver : ISolver
{
    private readonly Grid _grid;
    private readonly Dictionary<GridPosition<char>, UInt128> _timelinesPerPosition;

    public Day07Solver(IEnumerable<string> lines)
    {
        _grid = new Grid(lines);
        _timelinesPerPosition = [];
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
        //var splittersFromBottomToTop = _grid.Where(IsSplitter).Reverse();
        //foreach (var splitter in splittersFromBottomToTop)
        //{
        //    DetermineNumberOfDifferentTimelines(splitter);
        //}
        return DetermineNumberOfDifferentTimelines(_grid.First(IsSplitter)).ToString();
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

    public UInt128 DetermineNumberOfDifferentTimelines(GridPosition<char> position)
    {
        //if (!position.IsValidPosition) throw new InvalidOperationException(
        //    "Cannot determine number of different lifetimes for invalid position.");

        if (!IsSplitter(position) && !IsStart(position)) throw new ArgumentException(
            "cannot determine number of timelines of non-splitter position.");

        if (_timelinesPerPosition.TryGetValue(position, out var numberOfTimelines)) return numberOfTimelines;

        // determine number of timelines on the left
        var leftNeighbour = position.GetNeighbour(0, -1);
        var next = leftNeighbour;
        UInt128 lifetimesLeft;
        while (true)
        {
            if (!next.IsValidPosition) {
                lifetimesLeft = 1;
                break;
            }
            else if (IsSplitter(next))
            {
                lifetimesLeft = DetermineNumberOfDifferentTimelines(next);
                break;
            }
            else
            {
                next = next.GetNeighbour(1, 0); // move to down neighbour
            }
        }

        // determine number of timelines on the right
        var rightNeighbour = position.GetNeighbour(0, 1);
        next = rightNeighbour;
        UInt128 lifetimesRight;
        while (true)
        {
            if (!next.IsValidPosition) {
                lifetimesRight = 1;
                break;
            }
            else if (IsSplitter(next))
            {
                lifetimesRight = DetermineNumberOfDifferentTimelines(next);
                break;
            }
            else
            {
                next = next.GetNeighbour(1, 0); // move to down neighbour
            }
        }

        var totalLifetimes = lifetimesLeft + lifetimesRight;
        _timelinesPerPosition[position] = totalLifetimes;
        return totalLifetimes;
    }
}
