using AOC.ConsoleApp.Day06;

var lines = ReadLines("../../../Day06/input.txt");
//var lines = ReadLines("../../../Day06/input_small.txt");

ISolver solver = new Day06Solver(lines);

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