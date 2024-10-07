using Godot;
using SupaRL.Entities;

namespace SupaRL {
public partial class BaseComponent : Node
{
	public Entity Entity { get; set; }

	public override void _Ready()
	{
		Entity = GetParent<Entity>();
	}

	public MapData GetMapData() => 
		Entity.MapData;
}
}
