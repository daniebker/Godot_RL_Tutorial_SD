using Godot;
using SupaRL.Entities;

namespace  SupaRL {
	public partial class Game : Node2D
	{
		Vector2I _playerGridPosition = Vector2I.Zero;
		Entity _player;
		InputHandler _inputHandler;
		readonly EntityDefinition playerDefinition = ResourceLoader
			.Load<EntityDefinition>("res://assets/definitions/entities/actors/entity_definition_player.tres");
		Map _map;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_inputHandler = GetNode<InputHandler>("InputHandler");
			_map = GetNode<Map>("Map");

			var camera = GetNode<Camera2D>("Camera2D");

			_player = new Entity(null, Vector2I.Zero, playerDefinition);
			RemoveChild(camera);
			_player.AddChild(camera);
			_map.Generate(_player);
			_map.UpdateFieldOfView(_player.GridPosition);
		}

		public override void _PhysicsProcess(double delta)
		{
			var action = _inputHandler.GetAction(_player);
			var previousPlayerPosition = _player.GridPosition;
			if (action != null) {
				action.Perform();
				HandleEnemyTurns();
			}

			if (_player.GridPosition != previousPlayerPosition)
			{
				_map.UpdateFieldOfView(_player.GridPosition);
			}
		}

		private void HandleEnemyTurns()
		{
			foreach (var entity in _map.MapData.GetActors())
			{
				if (entity == _player) continue;
				if (entity.IsAlive)
				{
					entity.AIComponent.Perform();
				}
			}
		}

		public MapData GetMapData()
		{
			return _map.MapData;
		}

	}
}
