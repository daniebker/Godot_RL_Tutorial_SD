using Godot;
using SupaRL.Entities;


namespace SupaRL {
public partial class MeleeAction : ActionWithDirection 
{
	public MeleeAction(Entity entity, Vector2I offset) : base(entity, offset)
	{
	}

	public override void  Perform()
	{
		Vector2I destination = Entity.GridPosition + Offset;

		var blockingEntity = Entity.MapData.GetBlockingEntityAtLocation(destination);
		if (blockingEntity == null)
		{
			GD.Print("No entity to attack");
		}

		GD.Print("Attacking entity " + blockingEntity.EntityName);
	}
}
}
