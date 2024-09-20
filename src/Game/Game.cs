using Godot;
using System;
using SupaRL.src.utls;

public partial class Game : Node2D
{
	Vector2I playerGridPosition = Vector2I.Zero;
	Sprite2D player;
	EventHandler eventHandler;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<Sprite2D>("Player");
		eventHandler = GetNode<EventHandler>("EventHandler");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var action = eventHandler.GetAction();

		if (action is MovementAction movementAction)
		{
			playerGridPosition += movementAction.Offset;
			player.Position = Grid.GridToWorld(playerGridPosition);
		}
		else if (action is EscapeAction)
		{
			GetTree().Quit();
		}
	}
}
