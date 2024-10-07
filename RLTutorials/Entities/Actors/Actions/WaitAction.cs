using Godot;
using SupaRL.Entities; 


namespace SupaRL {
public partial class WaitAction : Action
{
	public WaitAction(Entity entity) : base(entity)
	{
	}

	public override void Perform()
	{
		// noop
	}
}
}
