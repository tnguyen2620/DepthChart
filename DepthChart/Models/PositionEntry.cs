namespace DepthChart.Models;

public class PositionEntry(int depth, Player player)
{
    public int Depth { get; set; } = depth;
    public Player Player { get; set; } = player;
}