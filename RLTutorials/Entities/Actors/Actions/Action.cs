using Godot;
using SupaRL.Entities;

public partial class Action : RefCounted
{

    public virtual void Perform(Game game, Entity entity)
    {
        throw new System.NotImplementedException("Calling base action Perform method. This should be overridden by a subclass.");
    }
}
