using Godot;


namespace SupaRL {
// TODO: Could this be an abstract class?
public partial class BaseAIComponent : BaseComponent
{
	public virtual void Perform() {
		throw new System.NotImplementedException();
	}

	public Vector2[] GetPointPathTo(Vector2I destination) {
		var mapData = GetMapData();
		return mapData.Pathfinder.GetPointPath(Entity.GridPosition, destination);
	}
}
}
