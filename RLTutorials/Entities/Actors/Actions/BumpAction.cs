using Godot;
using SupaRL.Entities;

public partial class BumpAction : ActionWithDirection
{
    public BumpAction() { }

    public BumpAction(int x, int y)
    {
        Offset = new Vector2I(x, y);
    }

    public BumpAction(Vector2I offset)
    {
        Offset = offset;
    }

    public override void Perform(Game game, Entity entity)
    {
        Vector2I destination = entity.GridPosition + Offset;

        var blockingEntity = game.GetMapData().GetBlockingEntityAtLocation(destination);
        GD.Print($"Blocking entity: {blockingEntity}");
        if (blockingEntity == null)
        {
            new MovementAction(Offset).Perform(game, entity);
        }
        else
        {
            new MeleeAction(Offset).Perform(game, entity);
        }
    }
}
