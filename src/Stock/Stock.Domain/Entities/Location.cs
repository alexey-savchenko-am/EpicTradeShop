using SharedKernel;
using System.Reflection.Emit;
using System;

namespace Stock.Domain.Entities;

public sealed class Location
    : ValueObject
{
    public string Zone { get; }
    public int Row { get; } // проход
    public string Place { get; } 
    public int Level { get; } // ярус 

    public Location(string zone, int row, string place, int level)
    {
        Zone = zone;
        Row = row;
        Place = place;
        Level = level;
    }

    public override string ToString()
    {
        // A-01-027-01
        return $"{Zone}-{Row}-{Place}-{Level}";
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Zone; 
        yield return Row;
        yield return Place;
        yield return Level;
    }
}
