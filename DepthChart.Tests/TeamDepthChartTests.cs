using DepthChart.Models;
using DepthChart.Models.Enums;
using DepthChart.Models.SportPositions;

namespace DepthChart.Tests;

public class TeamDepthChartTests
{
    [Fact]
    public void AddPlayerToDepthChart_WhenCalledWithNonMatchingSports_ShouldThrowArgumentException()
    {
        // Arrange
        var bostonNba = new Team(Sport.NBA, "Boston");
        var bostonNbaDepthChart = new TeamDepthChart(bostonNba);
        var player = new Player(12, "Tom Brady", Sport.NFL);
        var depth = 3;
        var position = NBA.PG;
        
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => bostonNbaDepthChart.AddPlayer(position, player, depth));
        Assert.Equal("Team, position and player must have matching sport.", exception.Message);
    }
    
    [Fact]
    public void AddPlayerToDepthChart_WhenCalledWithANegativeDepth_ShouldThrowArgumentException()
    {
        // Arrange
        var balMlb = new Team(Sport.MLB, "BaltimoreOrioles");
        var balMlbDepthChart = new TeamDepthChart(balMlb);
        var player = new Player (12, "Tom Brady", Sport.MLB);
        var depth = -1;
        var position = MLB.B3;
        
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => balMlbDepthChart.AddPlayer(position, player, depth));
        Assert.Equal("Position depth cannot be a negative number.", exception.Message);
    }
    
    [Fact]
    public void AddPlayerToDepthChart_WhenCalledWithNullDepth_ShouldAddPlayerToNextAvailableDepth()
    {
        // Arrange
        var tbNfl = new Team(Sport.NFL, "TampaBay");
        var depthChart = new TeamDepthChart(tbNfl);
        var player1 = new Player(12, "Tom Brady", Sport.NFL);
        var player2 = new Player(18, "Peyton Manning", Sport.NFL);
        var position = NFL.KR;
        
        // Act
        depthChart.AddPlayer(position, player1, null);
        
        // Assert
        Assert.True(depthChart.HasPosition(position));
        Assert.Contains(depthChart.GetEntriesAtPosition(position), x => x.Player == player1);
        Assert.Equal(0, depthChart.GetEntriesAtPosition(position).First(x => x.Player == player1).Depth);
        Assert.Equal(player1, depthChart.GetEntriesAtPosition(position). First(x => x.Depth == 0).Player);
        
        // Act
        depthChart.AddPlayer(NFL.KR, player2, null);
        
        // Assert
        Assert.Contains(depthChart.GetEntriesAtPosition(position), x => x.Player == player2);
        Assert.Equal(1, depthChart.GetEntriesAtPosition(position).First(x => x.Player == player2).Depth);
        Assert.Equal(player2, depthChart.GetEntriesAtPosition(position). First(x => x.Depth == 1).Player);
    }
    
    [Fact]
    public void AddPlayerToDepthChart_WhenCalledToAddPlayerToAnEmptyPositionWithAValidDepth_ShouldAddPlayerToDepthChartAtDesiredDepth()
    {
        // Arrange
        var tbNfl = new Team(Sport.NFL, "TampaBay");
        var depthChart = new TeamDepthChart(tbNfl);
        var player = new Player(12, "Tom Brady", Sport.NFL);
        var depth = 3;
        var position = NFL.RWR;
        
        // Act
        depthChart.AddPlayer(position, player, depth);
        
        // Assert
        Assert.True(depthChart.HasPosition(position));
        Assert.Contains(depthChart.GetEntriesAtPosition(position), x => x.Player == player);
        Assert.Equal(depth, depthChart.GetEntriesAtPosition(position).First(x=> x.Player == player).Depth);
        Assert.Equal(player, depthChart.GetEntriesAtPosition(position).First(x => x.Depth == depth).Player);
    }
    
    [Fact]
    public void AddPlayerToDepthChart_WhenCalledToAddPlayerToAPositionWithExistingPlayer_ShouldThrowException()
    {
        // Arrange
        var tbNfl = new Team(Sport.NFL, "TampaBay");
        var depthChart = new TeamDepthChart(tbNfl);
        var player = new Player(12, "Tom Brady", Sport.NFL);
        var depth = 1;
        var newDepth = 2;
        var position = NFL.RWR;
        
        // Act
        depthChart.AddPlayer(position, player, depth);
        
        // Act & Assert
        var exception = Assert.Throws<Exception>(() => depthChart.AddPlayer(position, player, newDepth));
        Assert.Equal($"Player {player.Name} already exists at position {position.ShortCode} of the {tbNfl.Name}-{tbNfl.Sport.ToString()}.", exception.Message);
    }
    
    [Fact]
    public void AddPlayerToDepthChart_WhenCalledWithAValidDepth_ShouldAddPlayerAtDesiredDepth_AndPushExistingPlayersDown()
    {
        // Arrange
        var tbNfl = new Team(Sport.NFL, "TampaBay");
        var depthChart = new TeamDepthChart(tbNfl);
        var player1 = new Player(12, "Tom Brady", Sport.NFL);
        var player2 = new Player(18, "Peyton Manning", Sport.NFL);
        var player3 = new Player(16, "Joe Montana", Sport.NFL);
        var player4 = new Player(13, "Dan Marino", Sport.NFL);
        
        var depth1 = 1;
        var depth2 = 2;
        var depth3 = 3;
        
        var position = NFL.LWR;
        
        // Act
        depthChart.AddPlayer(position, player1, depth1);
        depthChart.AddPlayer(position, player2, depth2);
        depthChart.AddPlayer(position, player3, depth3);
        depthChart.AddPlayer(position, player4, depth2); //player4 should take the place of player2
        
        // Assert
        Assert.True(depthChart.HasPosition(position));
        Assert.Contains(depthChart.GetEntriesAtPosition(position), x => x.Player == player1);
        Assert.Contains(depthChart.GetEntriesAtPosition(position), x => x.Player == player2);
        Assert.Contains(depthChart.GetEntriesAtPosition(position), x => x.Player == player3);
        Assert.Contains(depthChart.GetEntriesAtPosition(position), x => x.Player == player4);
        
        Assert.Equal(player1, depthChart.GetEntriesAtPosition(position).First(x=>x.Depth == depth1).Player);
        Assert.Equal(player4, depthChart.GetEntriesAtPosition(position).First(x => x.Depth == depth2).Player);
        Assert.Equal(player2, depthChart.GetEntriesAtPosition(position).First(x => x.Depth == depth2+1).Player);
        Assert.Equal(player3, depthChart.GetEntriesAtPosition(position).First(x => x.Depth == depth3+1).Player);
    }
    
    [Fact]
    public void AddPlayerToDepthChart_WhenCalledToAddAPlayerToMultiplePosition_ShouldSucceed()
    {
        // Arrange
        var tbNfl = new Team(Sport.NFL, "TampaBay");
        var depthChart = new TeamDepthChart(tbNfl);
        var player = new Player(12, "Tom Brady", Sport.NFL);
        
        var position1 = NFL.LWR;
        var position2 = NFL.RWR;
        var position3 = NFL.QB;
        var depth1 = 1;
        var depth2 = 2;
        var depth3 = 3;
        
        // Act
        depthChart.AddPlayer(position1, player, depth1);
        depthChart.AddPlayer(position2, player, depth2);
        depthChart.AddPlayer(position3, player, depth3);
        
        // Assert
        Assert.True(depthChart.HasPosition(position1));
        Assert.True(depthChart.HasPosition(position2));
        Assert.True(depthChart.HasPosition(position3));
        
        Assert.Contains(depthChart.GetEntriesAtPosition(position1), x => x.Player == player);
        Assert.Contains(depthChart.GetEntriesAtPosition(position2), x => x.Player == player);
        Assert.Contains(depthChart.GetEntriesAtPosition(position3), x => x.Player == player);
        
        Assert.Equal(player, depthChart.GetEntriesAtPosition(position1).First(x=>x.Depth == depth1).Player);
        Assert.Equal(player, depthChart.GetEntriesAtPosition(position2).First(x => x.Depth == depth2).Player);
        Assert.Equal(player, depthChart.GetEntriesAtPosition(position3).First(x => x.Depth == depth3).Player);
    }
    
    [Fact]
    public void RemovePlayerFromDepthChart_WhenCalledWithAPositionThatDoesNotExist_ShouldReturnAnEmptyList()
    {
        // Arrange
        var tbNfl = new Team(Sport.NFL, "TampaBay");
        var depthChart = new TeamDepthChart(tbNfl);
        var player = new Player(12, "Tom Brady", Sport.NFL);
        var position = NFL.LWR;
        
        // Act
        var result = depthChart.RemovePlayer(position, player);
        
        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public void RemovePlayerFromDepthChart_WhenCalledWithAPlayerThatDoesNotExist_ShouldReturnAnEmptyList()
    {
        // Arrange
        var tbNfl = new Team(Sport.NFL, "TampaBay");
        var depthChart = new TeamDepthChart(tbNfl);
        var player1 = new Player(12, "Tom Brady", Sport.NFL);
        var player2 = new Player(18, "Peyton Manning", Sport.NFL);
        var position = NFL.LWR;
        
        depthChart.AddPlayer(position, player1, 1);
        
        // Act
        var result = depthChart.RemovePlayer(position, player2);
        
        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public void RemovePlayerFromDepthChart_WhenCalledWithAPlayerThatExists_ShouldRemovePlayerFromDepthChartAndReturnThePlayer()
    {
        // Arrange
        var tbNfl = new Team(Sport.NFL, "TampaBay");
        var depthChart = new TeamDepthChart(tbNfl);
        var player1 = new Player(12, "Tom Brady", Sport.NFL);
        var player2 = new Player(18, "Peyton Manning", Sport.NFL);
        var position = NFL.LWR;
                
        depthChart.AddPlayer(position, player1, 1);
        depthChart.AddPlayer(position, player2, 2);
        
        // Act
        var result = depthChart.RemovePlayer(position, player1);
        
        // Assert
        Assert.DoesNotContain(depthChart.GetEntriesAtPosition(position), x => x.Player == player1);
        Assert.Contains(depthChart.GetEntriesAtPosition(position), x => x.Player == player2);
        Assert.Single(result);
        Assert.Equal(player1, result[0]);
    }
    
    [Fact]
    public void GetBackups_WhenCalledWithAPositionThatDoesNotExist_ShouldReturnAnEmptyList()
    {
        // Arrange
        var tbNfl = new Team(Sport.NFL, "TampaBay");
        var depthChart = new TeamDepthChart(tbNfl);
        var player = new Player(12, "Tom Brady", Sport.NFL);
        var position = NFL.LWR;
        
        // Act
        var result = depthChart.GetBackups(position, player);
        
        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public void GetBackups_WhenCalledWithAPlayerThatDoesNotExist_ShouldReturnAnEmptyList()
    {
        // Arrange
        var tbNfl = new Team(Sport.NFL, "TampaBay");
        var depthChart = new TeamDepthChart(tbNfl);
        var player1 = new Player(12, "Tom Brady", Sport.NFL);
        var player2 = new Player(18, "Peyton Manning", Sport.NFL);
        var position = NFL.LWR;
        
        depthChart.AddPlayer(position, player1, 1);
        
        // Act
        var result = depthChart.GetBackups(position, player2);
        
        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public void GetBackups_WhenCalledWithAPlayerThatExists_ShouldReturnAListWithThePlayersWithLowerDepth()
    {
        // Arrange
        var tbNfl = new Team(Sport.NFL, "TampaBay");
        var depthChart = new TeamDepthChart(tbNfl);
        var player1 = new Player(12, "Tom Brady", Sport.NFL);
        var player2 = new Player(18, "Peyton Manning", Sport.NFL);
        var player3 = new Player(16, "Joe Montana", Sport.NFL);
        var player4 = new Player(13, "Dan Marino", Sport.NFL);
        
        var position = NFL.LWR;
        
        depthChart.AddPlayer(position, player1, 0);
        depthChart.AddPlayer(position, player2, 1);
        depthChart.AddPlayer(position, player3, 2);
        depthChart.AddPlayer(position, player4, 3);
        
        // Act
        var result = depthChart.GetBackups(position, player2);
        
        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(player3, result);
        Assert.Contains(player4, result);
    }
    
    [Fact]
    public void GetBackups_WhenCalledWithAPlayerThatExistsAtTheLastDepth_ShouldReturnAnEmptyList()
    {
        // Arrange
        var tbNfl = new Team(Sport.NFL, "TampaBay");
        var depthChart = new TeamDepthChart(tbNfl);
        var player1 = new Player(12, "Tom Brady", Sport.NFL);
        var player2 = new Player(18, "Peyton Manning", Sport.NFL);
        var player3 = new Player(16, "Joe Montana", Sport.NFL);
        var player4 = new Player(13, "Dan Marino", Sport.NFL);
        
        var position = NFL.LWR;
        
        depthChart.AddPlayer(position, player1, 0);
        depthChart.AddPlayer(position, player2, 1);
        depthChart.AddPlayer(position, player3, 2);
        depthChart.AddPlayer(position, player4, 3);
        
        // Act
        var result = depthChart.GetBackups(position, player4);
        
        // Assert
        Assert.Empty(result);
    }
}