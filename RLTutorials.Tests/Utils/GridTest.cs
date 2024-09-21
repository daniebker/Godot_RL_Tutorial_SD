using Godot;
using SupaRL.src.utls;
namespace RLTutorials.Tests;

public class GridTest
{
    [Fact]
    public void ShouldConvertWorlCoordinatesToGrid()
    {
        Assert.Equal(
            Vector2I.Zero,
            Grid.WorldToGrid(Vector2I.Zero));
    }

    [Fact]
    public void ShouldConvertGridCoordinatesToWorld()
    {
        Assert.Equal(
            Vector2I.Zero,
            Grid.GridToWorld(Vector2I.Zero));
    }
}
