using Godot;

namespace SupaRL 
{
    public partial class SignalBus : Node
    {
        [Signal]
        public delegate void PlayerDiedEventHandler();
    }
}
