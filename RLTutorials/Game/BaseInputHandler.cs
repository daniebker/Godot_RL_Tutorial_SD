using Godot;
using SupaRL.Entities; 

namespace SupaRL 
{
	public abstract partial class BaseInputHandler : Node
	{
		public virtual Action GetAction(Entity player) =>
			null;
	}
}
