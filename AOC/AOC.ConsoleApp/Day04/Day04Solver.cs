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
            if (!IsRollOfPaper(position)) continue;

            var neighbours = position.GetAllDirectNeighbours(includeDiagonalNeigbours: true);
            var neighbouringRollsOfPaper = neighbours.Where(IsRollOfPaper);
            if (neighbouringRollsOfPaper.Count() < 4) accessibleRollsCount++;
        }
        return accessibleRollsCount.ToString();
    }

    public string SolveSecondChallenge()
    {
        return "";
    }

    private static bool IsRollOfPaper(GridPosition<char> position)
    {
        return position.Value == '@';
    }
}
