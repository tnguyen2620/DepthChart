using DepthChart.Models;
using DepthChart.Models.Enums;
using DepthChart.Models.SportPositions;

namespace DepthChart;

class Program
{
    static void Main()
    {
        // Create NFL players
        var tomBrady = new Player(12, "Tom Brady", Sport.NFL);
        var blaineGabbert = new Player(11, "Blaine Gabbert", Sport.NFL);
        var kyleTrask = new Player(2, "Kyle Trask", Sport.NFL);
        var mikeEvans = new Player(13, "Mike Evans", Sport.NFL);
        var jaelonDarden = new Player(1, "Jaelon Darden", Sport.NFL);
        var scottMiller = new Player(10, "Scott Miller", Sport.NFL);
        
        // Create Tampa Bay Team and its depth chart
        var tampaBayTeam = new Team(Sport.NFL, "Tampa Bay");
        var tbDepthChart = new DepthChart.Models.TeamDepthChart(tampaBayTeam);
        
        
        // Add players to depth chart
        tbDepthChart.AddPlayer(NFL.QB, tomBrady, 0);
        tbDepthChart.AddPlayer(NFL.QB, blaineGabbert, 1);
        tbDepthChart.AddPlayer(NFL.QB, kyleTrask, 2);

        tbDepthChart.AddPlayer(NFL.LWR, mikeEvans, 0);
        tbDepthChart.AddPlayer(NFL.LWR, jaelonDarden, 1);
        tbDepthChart.AddPlayer(NFL.LWR, scottMiller, 2);

        // Print outputs
        Console.WriteLine("--- getBackups(\"QB\", TomBrady) ---");
        var tomBackups = tbDepthChart.GetBackups(NFL.QB, tomBrady);
        foreach (var item in tomBackups)
            Console.WriteLine($"#{item.Number} - {item.Name}");

        Console.WriteLine();
        Console.WriteLine("--- getBackups(\"LWR\", JaelonDarden) ---");
        var jaelonBackups = tbDepthChart.GetBackups(NFL.LWR, jaelonDarden);
        foreach (var item in jaelonBackups)
            Console.WriteLine($"#{item.Number} - {item.Name}");

        Console.WriteLine();
        Console.WriteLine("--- getBackups(\"QB\", MikeEvans) ---");
        var mikeBackups = tbDepthChart.GetBackups(NFL.QB, mikeEvans);
        foreach (var item in mikeBackups)
            Console.WriteLine($"#{item.Number} - {item.Name}");

        Console.WriteLine();
        Console.WriteLine("--- getBackups(\"QB\", BlaineGabbert) ---");
        var blaineBackUps = tbDepthChart.GetBackups(NFL.QB, blaineGabbert);
        foreach (var item in blaineBackUps)
            Console.WriteLine($"#{item.Number} - {item.Name}");

        Console.WriteLine();
        Console.WriteLine("--- getBackups(\"QB\", KyleTrask) ---");
        var kyleBackups = tbDepthChart.GetBackups(NFL.QB, kyleTrask);
        foreach (var item in kyleBackups)
            Console.WriteLine($"#{item.Number} - {item.Name}");

        Console.WriteLine();
        Console.WriteLine("--- getFullDepthChart() ---");
        tbDepthChart.GetFullDepthChart(); // print full depth chart

        Console.WriteLine();
        Console.WriteLine("--- removePlayerFromDepthChart(\"LWR\", MikeEvans) ---");
        var mikeRemoved = tbDepthChart.RemovePlayer(NFL.LWR, mikeEvans);
        foreach (var player in mikeRemoved)
            Console.WriteLine($"#{player.Number} - {player.Name}");

        Console.WriteLine();
        Console.WriteLine("--- getFullDepthChart() ---");
        tbDepthChart.GetFullDepthChart();  // print full depth chart
    }
}