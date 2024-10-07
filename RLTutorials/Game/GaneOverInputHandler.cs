using Godot;
using SupaRL.Entities;

namespace SupaRL 
{
    public partial class GameOverInputHandler : BaseInputHandler
    {
        public override Action GetAction(Entity player)
        {
            Action action = null;
            if (Input.IsActionJustPressed("quit")) 
                action = new EscapeAction(player);
            
            return action;
        }
    }
}
