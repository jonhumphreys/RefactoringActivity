namespace RefactoringActivity.Tests;

using System;
using Xunit;

public class TextAdventureTests
{
    [Fact]
    public void Player_InitialValuesAreSetCorrectly()
    {
        // Arrange
        var player = new Player(100);

        // Act & Assert
        Assert.Equal(100, player.Health);
        Assert.Equal("Start", player.CurrentLocation);
        Assert.Empty(player.Inventory);
    }

    [Fact]
    public void Player_CanAddAndCheckItems()
    {
        // Arrange
        var player = new Player(100);

        // Act
        player.Inventory.Add("key");

        // Assert
        Assert.Contains("key", player.Inventory);
        Assert.True(player.Inventory.Contains("key"));
    }

    [Fact]
    public void World_InitializesLocationsCorrectly()
    {
        // Arrange
        var world = new World();

        // Act
        var startDescription = world.GetLocationDescription("Start");
        var forestDescription = world.GetLocationDescription("Forest");

        // Assert
        Assert.Contains("starting point", startDescription);
        Assert.Contains("dark forest", forestDescription);
    }

    [Fact]
    public void World_CanMovePlayerBetweenLocations()
    {
        // Arrange
        var world = new World();
        var player = new Player(100);

        // Act
        bool moved = world.MovePlayer(player, "north");

        // Assert
        Assert.True(moved);
        Assert.Equal("Forest", player.CurrentLocation);
    }

    [Fact]
    public void World_PreventsInvalidMovement()
    {
        // Arrange
        var world = new World();
        var player = new Player(100);

        // Act
        bool moved = world.MovePlayer(player, "west");

        // Assert
        Assert.False(moved);
        Assert.Equal("Start", player.CurrentLocation);
    }

    [Fact]
    public void World_CanTakeItemFromLocation()
    {
        // Arrange
        var player = new Player(100);
        var location = new Location("Start", "You are at the starting point of your adventure.");

        // Act
        bool taken = player.TakeItem("map", location);

        // Assert
        Assert.True(taken);
        Assert.Contains("map", player.Inventory);
    }

    [Fact]
    public void World_PreventsTakingNonexistentItem()
    {
        // Arrange
        var player = new Player(100);
        var location = new Location("Start", "You are at the starting point of your adventure.");

        // Act
        bool taken = player.TakeItem("nonexistent", location);

        // Assert
        Assert.False(taken);
    }

    [Fact]
    public void Puzzle_CanBeSolvedWithCorrectAnswer()
    {
        // Arrange
        var puzzle = new Puzzle("riddle", "What has to be broken before you can use it?", "egg");

        // Act
        Console.SetIn(new System.IO.StringReader("egg\n"));
        bool solved = puzzle.Solve();

        // Assert
        Assert.True(solved);
    }

    [Fact]
    public void Puzzle_CannotBeSolvedWithIncorrectAnswer()
    {
        // Arrange
        var puzzle = new Puzzle("riddle", "What has to be broken before you can use it?", "egg");

        // Act
        Console.SetIn(new System.IO.StringReader("stone\n"));
        bool solved = puzzle.Solve();

        // Assert
        Assert.False(solved);
    }
    
}
