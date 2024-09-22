using Godot;
using SupaRL.Entities;

public partial class MovementAction : Action
{
    public Vector2I Offset { get; set; }

    public MovementAction()
    {
        Offset = Vector2I.Zero;
    }

    public MovementAction(int x, int y)
    {
        Offset = new Vector2I(x, y);
    }

    public MovementAction(Vector2I offset)
    {
        Offset = offset;
    }

    public override void Perform(Game game, Entity entity)
    {
        Vector2 destination = entity.GridPosition + Offset;
        var mapData = game.GetMapData();
        var destinationTile = mapData.GetTile((Vector2I)destination);

        if (destinationTile == null || !destinationTile.IsWalkable())
        {
            return;
        }

        entity.Move(Offset);
    }

}
