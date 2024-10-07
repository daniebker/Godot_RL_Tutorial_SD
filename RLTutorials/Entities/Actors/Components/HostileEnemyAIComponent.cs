using Godot;


namespace SupaRL {
public partial class HostileEnemyAIComponent : BaseAIComponent
{
	private Vector2[] _path;
	public override void Perform()
	{
		var target = GetMapData().Player;
		var targetGridPosition = target.GridPosition; 
		var offset = targetGridPosition - Entity.GridPosition;
		var distance = Mathf.Max(Mathf.Abs(offset.X), Mathf.Abs(offset.Y));

		if (GetMapData().GetTile(Entity.GridPosition).IsInView)
		{
			if (distance <= 1)
				new MeleeAction(Entity, offset).Perform();

			_path = GetPointPathTo(targetGridPosition);
			_path = _path[1..^0];
		}
		if (_path !=null && _path.Length > 0)
		{
			var destination = (Vector2I)_path[0];
			if (GetMapData().GetBlockingEntityAtLocation(destination) != null)
				new WaitAction(Entity).Perform();

			_path = _path[1..^0];
			var moveOffset = destination - Entity.GridPosition;
			new MovementAction(Entity, moveOffset).Perform();
		}

		new WaitAction(Entity).Perform();
	}
}
}
