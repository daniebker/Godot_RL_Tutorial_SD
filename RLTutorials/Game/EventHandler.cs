using Godot;

public partial class EventHandler : Node {
    public Action GetAction() {
        Action action = null;

        // TODO: Wrap Input in a Facade and Pass it to the event handler
        if (Input.IsActionJustPressed("ui_up")) {
            action = new BumpAction(0, -1);
        } else if (Input.IsActionJustPressed("ui_down")) {
            action = new BumpAction(0, 1);
        } else if (Input.IsActionJustPressed("ui_left")) {
            action = new BumpAction(-1, 0);
        } else if (Input.IsActionJustPressed("ui_right")) {
            action = new BumpAction(1, 0);
        } else if (Input.IsActionJustPressed("ui_cancel")) {
            action = new EscapeAction();
        }
        return action;
    }
}
