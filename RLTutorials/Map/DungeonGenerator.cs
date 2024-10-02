using Godot;
using Godot.Collections;
using System.Linq;
using SupaRL.Entities;

namespace SupaRL.Map
{
    public partial class DungeonGenerator : Node
    {
        [ExportCategory("Map Dimensions")]

        [Export]
        public int MapWidth { get; set; } = 80;

        [Export]
        public int MapHeight { get; set; } = 45;

        [ExportCategory("Rooms RNG")]

        [Export]
        public int MaxRooms { get; set; } = 30;

        [Export]
        public int MinRoomSize { get; set; } = 6;

        [Export]
        public int MaxRoomSize { get; set; } = 10;

        [ExportCategory("Monsters RNG")]
        [Export]
        public int MaxMonstersPerRoom { get; set; } = 2;

        private RandomNumberGenerator _rng = new RandomNumberGenerator();

        public const string ORC = "orc";
        public const string TROLL = "troll";
        public static readonly Dictionary<string, EntityDefinition> entityTypes = new(){
            { ORC, ResourceLoader.Load<EntityDefinition>("res://assets/definitions/entities/actors/entity_definition_orc.tres") },
            { TROLL, ResourceLoader.Load<EntityDefinition>("res://assets/definitions/entities/actors/entity_definition_troll.tres") },
        };

        public override void _Ready()
        {
            _rng.Randomize();
        }

        public MapData GenerateDungeon(Entity player)
        {
            var dungeon = new MapData(MapWidth, MapHeight);
            dungeon.Entities.Add(player);

            var rooms = new Array<Rect2I>();

            for (int i = 0; i < MaxRooms; i++)
            {
                var roomWidth = _rng.RandiRange(MinRoomSize, MaxRoomSize);
                var roomHeight = _rng.RandiRange(MinRoomSize, MaxRoomSize);
                var x = _rng.RandiRange(0, MapWidth - roomWidth - 1);
                var y = _rng.RandiRange(0, MapHeight - roomHeight - 1);

                var newRoom = new Rect2I(x, y, roomWidth, roomHeight);

                var intersects = rooms.Any(room => newRoom.Intersects(room.Grow(-1)));

                if (intersects)
                {
                    continue;
                }

                CarveRoom(dungeon, newRoom);

                if (!rooms.Any())
                {
                    player.GridPosition = newRoom.GetCenter();
                }
                else
                {
                    TunnelBetween(dungeon, rooms.Last().GetCenter(), newRoom.GetCenter());
                }

                PlaceEntities(dungeon, newRoom);

                rooms.Add(newRoom);
            }

            return dungeon;
        }

        private void PlaceEntities(MapData dungeon, Rect2I room)
        {
            int numMonsters = _rng.RandiRange(0, MaxMonstersPerRoom);

            for (int i = 0; i < numMonsters; i++)
            {
                var x = _rng.RandiRange(room.Position.X+1, room.End.X-1);
                var y = _rng.RandiRange(room.Position.Y+1, room.End.Y-1);

                var entityPosition = new Vector2I(x, y);
                var canPlace = true;

                foreach (var entity in dungeon.Entities)
                {
                    if (entity.GridPosition == entityPosition)
                    {
                        canPlace = false;
                        break;
                    }
                }
                if(canPlace)
                {
                    Entity newEntity;
                    if(_rng.Randf() < 0.8)
                    {
                        newEntity = new Entity(entityPosition, entityTypes[ORC]);
                    } else
                    {
                        newEntity = new Entity(entityPosition, entityTypes[TROLL]);
                    }
                    dungeon.Entities.Add(newEntity);
                }
            }
        }

        private static void CarveFloor(MapData dungeon, int x, int y)
        {
            var tilePosition = new Vector2I(x, y);
            var tile = dungeon.GetTile(tilePosition);
            tile.SetTileType(MapData.tileTypes[MapData.FLOOR]);
        }

        private static void CarveRoom(MapData dungeon, Rect2I room)
        {
            var inner = room.Grow(-1);
			for (int y = inner.Position.Y; y <= inner.End.Y; y++)
			{
                for (int x = inner.Position.X; x <= inner.End.X; x++)
				{
                    CarveFloor(dungeon, x, y);
				}
            }
        }

        private static void TunnelHorizontal(MapData dungeon, int y, int xStart, int xEnd)
        {
            var xMin = Mathf.Min(xStart, xEnd);
            var xMax = Mathf.Max(xStart, xEnd);
            for (int x = xMin; x <= xMax; x++)
            {
                CarveFloor(dungeon, x, y);
            }
        }
        private static void TunnelVertical(MapData dungeon, int x, int yStart, int yEnd)
        {
            var yMin = Mathf.Min(yStart, yEnd);
            var yMax = Mathf.Max(yStart, yEnd);
            for (int y = yMin; y <= yMax; y++)
            {
                CarveFloor(dungeon, x, y);
            }
        }

        private void TunnelBetween(MapData dungeon, Vector2I start, Vector2I end)
        {
            if (_rng.Randf() < 0.5)
            {
                TunnelHorizontal(dungeon, start.Y, start.X, end.X);
                TunnelVertical(dungeon, end.X, start.Y, end.Y);
            }
            else
            {
                TunnelVertical(dungeon, start.X, start.Y, end.Y);
                TunnelHorizontal(dungeon, end.Y, start.X, end.X);
            }
        }
    }
}
