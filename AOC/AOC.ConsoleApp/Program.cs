using AOC.ConsoleApp.Day07;

var lines = ReadLines("../../../Day07/input.txt");
//var lines = ReadLines("../../../Day07/input_small.txt");

ISolver solver = new Day07Solver(lines);

Console.WriteLine($"Solution 1: {solver.SolveFirstChallenge()}");
Console.WriteLine($"Solution 2: {solver.SolveSecondChallenge()}");

static IEnumerable<string> ReadLines(string fileName)
{
    var lines = new List<string>();

    var reader = new StreamReader(fileName);
    while (reader.ReadLine() is string line)
    {
        lines.Add(line);
    }
    reader.Close();

    return lines;
}