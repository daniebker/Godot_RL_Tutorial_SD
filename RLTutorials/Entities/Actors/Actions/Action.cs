using Godot;
using SupaRL.Entities;

namespace SupaRL {
public partial class Action : RefCounted
{
	public Entity Entity { get; set;}

	public Action(Entity entity)
	{
		Entity = entity;
	}

	public virtual void Perform()
	{
		throw new System.NotImplementedException("Calling base action Perform method. This should be overridden by a subclass.");
	}

	public MapData MapData  => Entity.MapData;
}
}
