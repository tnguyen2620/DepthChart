using DepthChart.Models.Enums;

namespace DepthChart.Models;

public class Position(Sport sport, string shortCode) : IEquatable<Position>
{
    public Sport Sport { get; } = sport;
    public string ShortCode { get; } = shortCode;

    public bool Equals(Position? other)
    {
        if (other == null)
            return false;

        return Sport == other.Sport && ShortCode == other.ShortCode;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Sport, ShortCode);
    }
}