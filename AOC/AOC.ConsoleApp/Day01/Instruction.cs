internal record Instruction
{
    public required Direction Direction { get; set; }
    public required int Offset { get; set; }
}
