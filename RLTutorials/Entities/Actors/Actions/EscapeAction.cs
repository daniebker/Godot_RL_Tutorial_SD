using SupaRL.Entities;

namespace SupaRL {
public partial class EscapeAction : Action {
	public EscapeAction(Entity entity) : base(entity) {}

	public override void Perform() {
		Entity.GetTree().Quit();
	}
}}
