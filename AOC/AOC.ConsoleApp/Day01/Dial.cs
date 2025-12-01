internal class Dial
{
    public int PointerValue { get; private set; } = 50;
    public int ZeroCount { get; private set; } = 0;

    public void MoveLeft(int offset)
    {

        if (PointerValue != 0 && PointerValue - offset <= 0) ZeroCount++;

        PointerValue -= offset;
        PointerValue = (PointerValue + 100) % 100;
    }

    public void MoveRight(int offset)
    {
        if (PointerValue != 0 && PointerValue + offset >= 100) ZeroCount++;

        PointerValue += offset;
        PointerValue = PointerValue % 100;
    }

    public void Execute(Instruction instruction)
    {
        ZeroCount += instruction.Offset / 100;
        var delta = instruction.Offset % 100;
        if (delta == 0) return;

        if (instruction.Direction == Direction.Left) MoveLeft(delta);
        else MoveRight(delta);
    }
}
