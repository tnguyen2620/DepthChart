using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DepthChart.Tests")]

namespace DepthChart.Models;

public class TeamDepthChart(Team team)
{
    private readonly Dictionary<Position, List<PositionEntry>> _data = new();

    internal bool HasPosition(Position position) => _data.ContainsKey(position);
    internal List<PositionEntry> GetEntriesAtPosition(Position position) => _data[position];

    public void AddPlayer(Position position, Player player, int? depth = null)
    {
        // validate inputs
        if (player == null)
            throw new ArgumentNullException(nameof(player));
        
        if (team.Sport != position.Sport || team.Sport != player.Sport || position.Sport != player.Sport)
            throw new ArgumentException("Team, position and player must have matching sport.");
                
        if (depth < 0)
            throw new ArgumentException("Position depth cannot be a negative number.");

        // if position does not exist, add it with the player at the specified depth or at depth 0 if depth is null
        if (!HasPosition(position))
        {
            _data.Add(position, [new PositionEntry(depth ?? 0, player)]);
            return;
        }
        
        var entries = GetEntriesAtPosition(position);
        
        //check if player already exists at that position
        if (entries.Any(x => x.Player == player))
            throw new Exception($"Player {player.Name} already exists at position {position.ShortCode} of the {team.Name}-{team.Sport.ToString()}.");
        
        // if no depth value is provided, add the player at the end of the depth chart
        if (depth == null)
        {
            var maxDepth = entries.Max(x => x.Depth);
            entries.Add(new PositionEntry (maxDepth + 1, player));
            return;
        }
        
        // if the specified depth is already occupied, shift down all players at that depth and below
        if (entries.Any(x=> x.Depth == depth.Value))
        {
            foreach (var item in entries.Where(x=> x.Depth >= depth))
                item.Depth++;
        }
        
        entries.Add(new PositionEntry(depth.Value, player));
    }

    public List<Player> RemovePlayer(Position position, Player player)
    {
        // validate inputs
        if (player == null)
            throw new ArgumentNullException(nameof(player));

        if (team.Sport != position.Sport || team.Sport != player.Sport || position.Sport != player.Sport)
            throw new ArgumentException("Team, position and player must have matching sport.");
        
        // if position does not exist, return empty list
        if (!HasPosition(position))
            return new List<Player>();

        var entries = GetEntriesAtPosition(position);

        // if player does not exist at that position, return empty list
        if (entries.All(x => x.Player != player))
            return new List<Player>();

        entries.Remove(entries.First(x => x.Player == player));

        return [player];
    }
    
    public List<Player> GetBackups(Position position, Player player)
    {
        // validate inputs
        if (player == null)
            throw new ArgumentNullException(nameof(player));
        
        if (team.Sport != position.Sport || team.Sport != player.Sport || position.Sport != player.Sport)
            throw new ArgumentException("Team, position and player must have matching sport.");
        
        // if position does not exist, return empty list
        if (!HasPosition(position))
            return new List<Player>();
        
        var entries = GetEntriesAtPosition(position);
        
        // if player does not exist at that position, return empty list
        if(entries.All(x => x.Player != player))
            return new List<Player>();
        
        var playerDepth = entries.First(x => x.Player == player).Depth;
        
        var backupPlayers = entries
            .Where(x => x.Depth > playerDepth)
            .Select(x => x.Player)
            .ToList();

        return backupPlayers;
    }

    public void GetFullDepthChart()
    {
        foreach (var kpv in _data)
        {
            Console.Write($"{kpv.Key.Sport.ToString()}:{kpv.Key.ShortCode} - ");
            Console.WriteLine(string.Join(", ", kpv.Value.Select(x => $"{x.Depth}:(#{x.Player.Number} - {x.Player.Name})")));
        }
    }
}