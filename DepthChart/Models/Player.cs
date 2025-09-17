using DepthChart.Models.Enums;

namespace DepthChart.Models;

public record Player(int Number, string Name, Sport Sport)
{
    public int Number { get; set; } = Number;
    public string Name { get; set; } = Name;
    public Sport Sport { get; set; } = Sport;
}