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
            Grid.WorldToGrid(new Vector2I(0, 0)));
    }
}
