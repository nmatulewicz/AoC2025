using AOC.ConsoleApp.Shared;

namespace AOC.ConsoleApp.Day08;

/// <summary>
/// Solution 1: 42315
/// Solution 2:
/// </summary>

internal class Day08Solver : ISolver
{
    private readonly IEnumerable<Vector> _vectors;
    private readonly PriorityQueue<(Vector, Vector), double> _closestVectors = new();
    private readonly IDictionary<Vector, HashSet<Vector>> _circuitPerVector = new Dictionary<Vector, HashSet<Vector>>();

    public Day08Solver(IEnumerable<string> lines)
    {
        _vectors = lines.Select(lines =>
        {
            var values = lines.Split(',');
            return new Vector
            {
                X = int.Parse(values[0]),
                Y = int.Parse(values[1]),
                Z = int.Parse(values[2]),
            };
        });
    }

    public string SolveFirstChallenge()
    {
        FillPriorityQueue();
        MakeClosestConnections(1000);

        var circuits = _circuitPerVector.Values.Distinct();
        var threeLargestCircuitSizes = circuits
            .Select(circuit => circuit.Count)
            .OrderDescending()
            .Take(3);

        var product = threeLargestCircuitSizes.ElementAt(0) 
            * threeLargestCircuitSizes.ElementAt(1) 
            * threeLargestCircuitSizes.ElementAt(2);
        return product.ToString();
    }

    private void MakeClosestConnections(int n = 1000)
    {
        var connectionCount = 0;
        while (connectionCount < n)
        {
            var pairToConnect = _closestVectors.Dequeue();
            (var vector1, var vector2) = pairToConnect;
            var IsVector1InCircuit = _circuitPerVector.TryGetValue(vector1, out var circuit1);
            var IsVector2InCircuit = _circuitPerVector.TryGetValue(vector2, out var circuit2);

            connectionCount++;

            if (IsVector1InCircuit && IsVector2InCircuit
                && circuit1 == circuit2) continue;


            if (IsVector1InCircuit && IsVector2InCircuit
                && circuit1 != circuit2)
            {
                circuit1!.UnionWith(circuit2!);
                foreach (var vector in circuit2!)
                {
                    _circuitPerVector[vector] = circuit1;
                }
            }

            else if (IsVector1InCircuit && !IsVector2InCircuit)
            {
                circuit1!.Add(vector2);
                _circuitPerVector[vector2] = circuit1;
            }

            else if (IsVector2InCircuit && !IsVector1InCircuit)
            {
                circuit2!.Add(vector1);
                _circuitPerVector[vector1] = circuit2;
            }

            else
            {
                var circuit = new HashSet<Vector>
                {
                    vector1,
                    vector2,
                };
                _circuitPerVector[vector1] = circuit;
                _circuitPerVector[vector2] = circuit;
            }
        }
    }

    private void FillPriorityQueue()
    {
        var vectorArray = _vectors.ToArray();
        for (var i = 0; i < vectorArray.Length - 1; i++)
        {
            var vector1 = vectorArray[i];
            for (var j = i + 1; j < vectorArray.Length; j++)
            {
                var vector2 = vectorArray[j];
                var distance = vector1.GetDistanceTo(vector2);
                _closestVectors.Enqueue((vector1, vector2), distance);
            }
        }
    }

    public string SolveSecondChallenge()
    {
        return "";
    }
}
