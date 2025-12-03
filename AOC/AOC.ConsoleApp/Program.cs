
using AOC.ConsoleApp.Day02;

var lines = ReadLines("../../../Day02/input.txt");
//var lines = ReadLines("../../../Day02/input_small.txt");

ISolver solver = new Day02Solver(lines);

Console.WriteLine($"Solution 1: {solver.SolveFirstChallenge()}");
Console.WriteLine($"Solution 2: {solver.SolveSecondChallenge()}");

IEnumerable<string> ReadLines(string fileName)
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