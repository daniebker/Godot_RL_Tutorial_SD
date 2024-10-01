using Godot;
using SupaRL.Utls;
using SupaRL.Entities;
using SupaRL.Map;

public partial class Game : Node2D
{
	Vector2I _playerGridPosition = Vector2I.Zero;
	Entity _player;
	EventHandler _eventHandler;
	readonly EntityDefinition playerDefinition = ResourceLoader
			.Load<EntityDefinition>("res://assets/definitions/entities/actors/entity_definition_player.tres");
	Node2D _entities;
	Map _map;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_eventHandler = GetNode<EventHandler>("EventHandler");
		_entities = GetNode<Node2D>("Entities");
		_map = GetNode<Map>("Map");

		var camera = GetNode<Camera2D>("Camera2D");

		_player = new Entity(Vector2I.Zero, playerDefinition);
		RemoveChild(camera);
		_player.AddChild(camera);
		_entities.AddChild(_player);
		_map.Generate(_player);
		_map.UpdateFieldOfView(_player.GridPosition);
	}

	public override void _PhysicsProcess(double delta)
	{
		var action = _eventHandler.GetAction();
		var previousPlayerPosition = _player.GridPosition;
		action?.Perform(this, _player);

		if (_player.GridPosition != previousPlayerPosition)
		{
			_map.UpdateFieldOfView(_player.GridPosition);
		}
	}

	public MapData GetMapData()
	{
		return _map.MapData;
	}

}
