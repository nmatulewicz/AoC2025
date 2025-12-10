using AOC.ConsoleApp.Shared;

namespace AOC.ConsoleApp.Day04;

/// <summary>
/// Solution 1: 1397
/// Solution 2:
/// </summary>

public class Day04Solver(IEnumerable<string> lines) 
    : ISolver
{
    private readonly Grid _grid = new Grid(lines);

    public string SolveFirstChallenge()
    {
        var accessibleRollsCount = 0;
        foreach(var position in _grid)
        {
            if (IsAccassibleRollOfPaper(position)) accessibleRollsCount++;
        }
        return accessibleRollsCount.ToString();
    }

    public string SolveSecondChallenge()
    {
        var totalRemovalCount = 0;
        while (true)
        {
            var removalCountDelta = 0;
            foreach (var position in _grid)
            {
                if (IsAccassibleRollOfPaper(position)) 
                {
                    RemoveRoll(position);
                    removalCountDelta++;
                }
            }

            if (removalCountDelta == 0) break;
            totalRemovalCount += removalCountDelta;
        }

        return totalRemovalCount.ToString();
    }

    private static bool IsRollOfPaper(GridPosition<char> position)
    {
        return position.Value == '@';
    }

    private static bool IsAccassibleRollOfPaper(GridPosition<char> position)
    {
        if (!IsRollOfPaper(position)) return false;

        var neighbours = position.GetAllDirectNeighbours(includeDiagonalNeigbours: true);
        var neighbouringRollsOfPaper = neighbours.Where(IsRollOfPaper);
        return neighbouringRollsOfPaper.Count() < 4;
    }

    private static void RemoveRoll(GridPosition<char> position)
    {
        if (!IsRollOfPaper(position)) throw new InvalidOperationException(
            "Cannot remove roll of paper from position which does not contain a roll of paper.");

        position.Value = '.';
    }
}
