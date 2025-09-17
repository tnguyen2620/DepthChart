using DepthChart.Models.Enums;

namespace DepthChart.Models.SportPositions;

public class MLB
{
    public static Position C => new(Sport.MLB, "C");
    public static Position B1 => new(Sport.MLB, "1B");
    public static Position B2 => new(Sport.MLB, "2B");
    public static Position B3 => new(Sport.MLB, "3B");
    public static Position SS => new(Sport.MLB, "SS");
    public static Position LF => new(Sport.MLB, "LF");
    public static Position CF => new(Sport.MLB, "CF");
    public static Position RF => new(Sport.MLB, "RF");
    public static Position DH => new(Sport.MLB, "DH");
    public static Position SP => new(Sport.MLB, "SP");
    public static Position RP => new(Sport.MLB, "RP");
}