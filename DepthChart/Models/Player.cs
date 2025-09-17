using DepthChart.Models.Enums;

namespace DepthChart.Models;

public class Player(int number, string name, Sport sport)
{
    public int Number { get; set; } = number;
    public string Name { get; set; } = name;
    public Sport Sport { get; set; } = sport;
}