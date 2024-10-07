using Godot;
using SupaRL.Entities;

namespace SupaRL {
public partial class ActionWithDirection : Action
{
	public Vector2I Offset { get; set; }

	public ActionWithDirection(Entity entity, Vector2I offset) : base(entity)
	{
		Offset = offset;
	}

	public override void Perform()
	{
		throw new System.NotImplementedException("Calling base action Perform method. This should be overridden by a subclass.");
	}
}
}
