using Godot;

public partial class MovementAction : Action{
    public Vector2I Offset { get; set; }

    public MovementAction() {
        Offset = Vector2I.Zero;
    }

    public MovementAction(int x, int y) {
        Offset = new Vector2I(x, y);
    }

    public MovementAction(Vector2I offset) {
        Offset = offset;
    }
}
