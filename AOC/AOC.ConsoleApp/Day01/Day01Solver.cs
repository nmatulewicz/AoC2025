internal class Day01Solver : ISolver
{
    protected IEnumerable<Instruction> _instructions;

    public Day01Solver(IEnumerable<string> lines)
    {
        _instructions = lines.Select(ToInstruction);
    }

    public string SolveFirstChallenge()
    {
        var dial = new Dial();

        var zeroCount = 0;
        foreach (var instruction in _instructions)
        {
            dial.Execute(instruction);
            if (dial.PointerValue == 0) zeroCount++;
        }

        return zeroCount.ToString();
    }

    public string SolveSecondChallenge()
    {
        var dial = new Dial();

        foreach (var instruction in _instructions)
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
