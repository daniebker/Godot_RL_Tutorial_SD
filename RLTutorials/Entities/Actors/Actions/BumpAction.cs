using Godot;
using SupaRL.Entities;

namespace SupaRL {
public partial class BumpAction : ActionWithDirection
{
	public BumpAction(Entity entity, int x, int y) : base(entity, new Vector2I(x,y))
	{

	}

	public BumpAction(Entity entity, Vector2I offset) : base(entity, offset)
	{
	}

	public override void Perform()
	{
		Vector2I destination = Entity.GridPosition + Offset;

		var blockingEntity = Entity.MapData.GetBlockingEntityAtLocation(destination);
		if (blockingEntity == null)
		{
			new MovementAction(Entity, Offset).Perform();
		}
		else
		{
			new MeleeAction(Entity, Offset).Perform();
		}
	}
}
}
