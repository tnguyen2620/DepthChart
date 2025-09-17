# README

### A few notes on the solution:
- The solution contain 2 projects, the `DepthChart` project is a Console App with all the classes and models required. Running the `Program.cs` file will provide the same output as the challenge description. The test project `DepthChart.Tests` have unit tests to test the functionality of the `TeamDepthChart` class. The tests cover various scenarios including adding players, removing players, and retrieving backups. The tests also include edge cases such as adding a player to a non-existent position and removing a player who is not in the depth chart.
- This solution allows for multiple sports with the ability to have many teams per sports.
- A team object contains the `Sport` type (`enum`) and the `Name` of the team (`string`).
- Each time a `TeamDepthChart` object is created, it is associated with a team. This allows the application to have
  different depth charts for different teams.
- The `Position` class contains the `Sport` type (`enum`) and the position shortcode (`string`) (can use `enum` if needed).
- The `Player` class has a `Sport` property to indicate the sport the player plays. 
- Validations are added to the `AddPlayer()`, `RemovePlayer()` and `GetBackups()` methods as we don't allow adding
  players of different sports to our depth chart. `ArgumentException` is thrown if the team, the player, and the position don't have matching sport.
- As each team needs to provide a depth chart every game round, we can even extend the DepthChart to have the ability
  to store the depth chart for each game round. This can be done by adding a `GameRound` property to the `TeamDepthChart`
  class. The constructor will look like this `public TeamDepthChart(Team team, int gameRound)`.
- There are comments in the `TeamDepthChart.cs` file to explain the logic behind each method. This is for the purpose of this submission. In a real-world scenario,
  I would avoid adding comments to the code and instead focus on writing clean, self-explanatory code with meaningful method and variable names.

### A few suggestions:
- We can add a method to move a player from one position to another; it might be useful in some cases.
- We can add a method called `ConsolidatePlayers()` to have all players in a depth chart to `align left`, i.e., a position `LWR` has three players at
  depth 1, 2, 5. Calling this method will result in the 3 players at depth 1, 2, 3.
