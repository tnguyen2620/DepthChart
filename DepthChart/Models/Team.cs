using DepthChart.Models.Enums;

namespace DepthChart.Models;

public class Team(Sport sport, string name)
{
    public Sport Sport { get; set; } = sport;
    public string Name { get; set; } = name;
}