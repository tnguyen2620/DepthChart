using DepthChart.Models.Enums;

namespace DepthChart.Models.SportPositions;

public class NBA
{
    public static Position PG => new(Sport.NBA, "PG");
    public static Position SG => new(Sport.NBA, "SG");
    public static Position SF => new(Sport.NBA, "SF");
    public static Position PF => new(Sport.NBA, "PF");
    public static Position C => new(Sport.NBA, "C");
}