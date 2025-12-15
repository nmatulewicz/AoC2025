namespace AOC.ConsoleApp.Shared;

internal record Vector
{
    public required int X;
    public required int Y;
    public required int Z;

    public double GetDistanceTo(Vector other)
    {
        return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2) + Math.Pow(Z - other.Z, 2));
    }
}
