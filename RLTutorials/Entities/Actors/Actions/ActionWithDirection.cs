using Godot;
using SupaRL.Entities;

public partial class ActionWithDirection : Action
{
    public Vector2I Offset { get; set; }

    public ActionWithDirection() { }

    public ActionWithDirection(Vector2I offset)
    {
        Offset = offset;
    }

    public override void Perform(Game game, Entity entity)
    {
        throw new System.NotImplementedException("Calling base action Perform method. This should be overridden by a subclass.");
    }
}
