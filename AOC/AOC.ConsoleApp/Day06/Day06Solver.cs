using System.Text.RegularExpressions;

namespace AOC.ConsoleApp.Day06;

/// <summary>
/// Solution 1: 5335495999141
/// Solution 2: 10142723156431
/// </summary>

internal class Day06Solver : ISolver
{
    private readonly IEnumerable<IEnumerable<UInt128>> _rows;
    private readonly IEnumerable<IEnumerable<UInt128>> _groupedColumns;
    private readonly IEnumerable<Operation> _operations;

    public Day06Solver(IEnumerable<string> lines)
    {
        var linesWithOnlySingleWhiteSpaces = lines.Select(line => Regex.Replace(line, @"\s+", " ").Trim());
        var rows = linesWithOnlySingleWhiteSpaces.Select(line => line.Split(' ')).ToList();
        _operations = rows.Last().Select(str => str switch
        {
            "+" => Operation.Addition,
            "*" => Operation.Multiply,
            _ => throw new Exception($"Unexpected operation '{str}'")
        });
        _rows = rows
            .Take(rows.Count - 1)
            .Select(row => row.Select(UInt128.Parse));

        var numberOfColumns = lines.ElementAt(0).Length;
        var columns = new string[numberOfColumns];
        for (var i = 0; i < numberOfColumns; i++)
        {
            columns[i] = lines
                .Take(lines.Count() - 1)
                .Select(line => line.ElementAt(i))
                .Aggregate("", (res, character) => res + character);
        }

        var numberGroups = _operations.Select(_ => new List<UInt128>()).ToArray();
        var numberGroupIndex = 0;
        foreach (var column in columns)
        {
            if (string.IsNullOrWhiteSpace(column))
            {
                numberGroupIndex++;
                continue;
            }

            var value = UInt128.Parse(column);
            numberGroups[numberGroupIndex].Add(value); 
        }

        _groupedColumns = numberGroups;
    }

    public string SolveFirstChallenge()
    {
        UInt128[] results = _rows.ElementAt(0).ToArray();
        for (var col = 0; col < results.Count(); col++)
        {
            foreach (var row in _rows.Skip(1))
            {
                var operation = _operations.ElementAt(col);
                switch (operation)
                {
                    case Operation.Addition:
                        results[col] += row.ElementAt(col);
                        break;
                    case Operation.Multiply:
                        results[col] *= row.ElementAt(col);
                        break;
                }
            }
        }
        return results.Aggregate((UInt128) 0, (sum, value) => sum + value).ToString();
    }

    public string SolveSecondChallenge()
    {
        var total = UInt128.Zero;
        foreach (var (index, group) in _groupedColumns.Index())
        {
            var operation = _operations.ElementAt(index);
            var result = group.First();
            foreach (var value in group.Skip(1))
            {
                switch (operation)
                {
                    case Operation.Addition:
                        result += value;
                        break;
                    case Operation.Multiply:
                        result *= value;
                        break;
                }
            }
            total += result;
        }
        return total.ToString();
    }

    public static UInt128 Apply(Operation operation, UInt128 left, UInt128 right)
    {
        return operation switch
        {
            Operation.Addition => left + right,
            Operation.Multiply => left * right,
        };
    }
}

public enum Operation
{
    Addition,
    Multiply,
}
