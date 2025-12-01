
var lines = ReadLines("../../../Day01/input.txt");
//var lines = ReadLines("../../../Day01/input_small.txt");

ISolver solver = new Day01Solver();

Console.WriteLine($"Solution 1: {solver.SolveFirstChallenge(lines)}");
Console.WriteLine($"Solution 2: {solver.SolveSecondChallenge(lines)}");

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