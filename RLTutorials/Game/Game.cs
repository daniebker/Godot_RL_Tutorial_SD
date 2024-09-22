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


        var size = GetViewportRect().Size.Floor() / 2;
        var player_start_pos = Grid.WorldToGrid((Vector2I)size);
        _player = new Entity(player_start_pos, playerDefinition);
        _entities.AddChild(_player);

        var npc = new Entity(player_start_pos + Vector2I.Right, playerDefinition);
        npc.Modulate = Colors.OrangeRed;
        _entities.AddChild(npc);

    }

    public override void _PhysicsProcess(double delta)
    {
        var action = _eventHandler.GetAction();
        action?.Perform(this, _player);
    }

    public MapData GetMapData()
    {
        return _map.MapData;
    }

}
