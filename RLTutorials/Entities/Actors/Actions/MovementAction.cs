using Godot;
using SupaRL.Entities;

public partial class MovementAction : ActionWithDirection
{
    public MovementAction() {}
    public MovementAction(Vector2I offset)
    {
        Offset = offset;
    }

    public override void Perform(Game game, Entity entity)
    {
        Vector2I destination = entity.GridPosition + Offset;

        var mapData = game.GetMapData();
        var destinationTile = mapData.GetTile(destination);

        if (destinationTile == null || !destinationTile.IsWalkable())
        {
            return;
        }

        if(game.GetMapData().GetBlockingEntityAtLocation(destination) != null)
        {
            return;
        }

        entity.Move(Offset);
    }
}
