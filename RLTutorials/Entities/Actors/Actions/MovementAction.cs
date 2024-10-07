using Godot;
using SupaRL.Entities;


namespace SupaRL {
public partial class MovementAction : ActionWithDirection
{
	public MovementAction(Entity entity,Vector2I offset) : base(entity, offset) 
	{
	}

	public override void Perform()
	{
		Vector2I destination = Entity.GridPosition + Offset;

		var mapData = Entity.MapData;
		var destinationTile = mapData.GetTile(destination);

		if (destinationTile == null || !destinationTile.IsWalkable())
		{
			return;
		}

		if(mapData.GetBlockingEntityAtLocation(destination) != null)
		{
			return;
		}

		Entity.Move(Offset);
	}
}
}
