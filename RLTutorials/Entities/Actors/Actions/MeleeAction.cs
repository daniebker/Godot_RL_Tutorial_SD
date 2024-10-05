using Godot;
using SupaRL.Entities;

public partial class MeleeAction : ActionWithDirection 
{
    public MeleeAction() { }
    public MeleeAction(Vector2I offset)
    {
        Offset = offset;
    }

    public override void  Perform(Game game, Entity entity)
    {
        Vector2I destination = entity.GridPosition + Offset;

        var blockingEntity = game.GetMapData().GetBlockingEntityAtLocation(destination);
        if (blockingEntity == null)
        {
            GD.Print("No entity to attack");
        }

        GD.Print("Attacking entity " + blockingEntity.GetName());
    }
}
