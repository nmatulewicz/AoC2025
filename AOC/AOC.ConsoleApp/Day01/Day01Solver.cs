internal class Day01Solver : ISolver
{
    // 6022 TOO high

    public string SolveFirstChallenge(IEnumerable<string> lines)
    {
        var instructions = lines.Select(ToInstruction);
        var dial = new Dial();

        var zeroCount = 0;
        foreach (var instruction in instructions)
        {
            dial.Execute(instruction);
            if (dial.PointerValue == 0) zeroCount++;
        }

        return zeroCount.ToString();
    }

    public string SolveSecondChallenge(IEnumerable<string> lines)
    {
        var instructions = lines.Select(ToInstruction);
        var dial = new Dial();

        foreach (var instruction in instructions)
        {
            dial.Execute(instruction);
        }

        return dial.ZeroCount.ToString();
    }

    public Instruction ToInstruction(string line)
    {
        var direction = line[0] == 'L' ? Direction.Left : Direction.Right;
        var offset = int.Parse(line[1..]);

        return new Instruction
        {
            Direction = direction,
            Offset = offset,
        };
    }
}
