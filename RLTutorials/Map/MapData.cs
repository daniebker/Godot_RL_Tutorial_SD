using Godot;
using Godot.Collections;
using System.Linq;
using SupaRL.Entities;

namespace SupaRL
{
	public partial class MapData : RefCounted
	{
		public const string FLOOR = "floor";
		public const string WALL = "wall";
		public static readonly Dictionary<string, TileDefinition> tileTypes = new Dictionary<string, TileDefinition> {
			{ FLOOR, ResourceLoader.Load<TileDefinition>("res://assets/definitions/tiles/tile_definition_floor.tres") },
			{ WALL, ResourceLoader.Load<TileDefinition>("res://assets/definitions/tiles/tile_definition_wall.tres") },
		};

		public int Width { get; set; }
		public int Height { get; set; }
		public Array<Tile> Tiles { get; set; }
		public Array<Entity> Entities { get; set; }
		private Entity _player;
		public Entity Player => _player;
		private float _entityPathfindingWeight = 10.0f;
		private AStarGrid2D _pathfinder;
		public AStarGrid2D Pathfinder => _pathfinder;

		public MapData(int mapWidth, int mapHeight, Entity player)
		{
			Width = mapWidth;
			Height = mapHeight;
			Entities = new();
			_player = player;
			SetupTiles();
		}

		private void SetupTiles()
		{
			Tiles = new Array<Tile>();
			for (int y = 0; y < Height; y++)
			{
				for (int x = 0; x < Width; x++)
				{
					var tilePosition = new Vector2I(x, y);
					var tile = new Tile(tilePosition, tileTypes[WALL]);
					Tiles.Add(tile);
				}
			}
		}

		public Tile GetTile(Vector2I gridPosition)
		{
			var tileIndex = GridToIndex(gridPosition);
			if (tileIndex == -1) return null;
			return Tiles[tileIndex];
		}

		public Tile GetTile(int x, int y) =>
			GetTile(new Vector2I(x, y));

		private int GridToIndex(Vector2I gridPosition)
		{
			if (!IsInBounds(gridPosition)) return -1;
			return gridPosition.Y * Width + gridPosition.X;
		}

		private bool IsInBounds(Vector2I coordinate)
		{
			return coordinate.X >= 0
				&& coordinate.X < Width
				&& coordinate.Y >= 0
				&& coordinate.Y < Height;
		}

		public Entity GetBlockingEntityAtLocation(Vector2I gridPosition)
		{
			foreach (var entity in Entities)
			{
				if (entity.GridPosition == gridPosition && entity.IsBlocking)
				{
					return entity;
				}
			}
			return null;
		}

		public void RegisterBlockingEntity(Entity entity)
		{
			 _pathfinder.SetPointWeightScale(entity.GridPosition, _entityPathfindingWeight);
		}

		public void UnregisterBlockingEntity(Entity entity)
		{
			_pathfinder.SetPointWeightScale(entity.GridPosition, 1.0f);
		}

		public System.Collections.Generic.IEnumerable<Entity> GetActors() =>
			 Entities.Where(e => e.EntityType==EntityType.ACTOR && e.IsAlive);
			
		public Entity GetActorAtLocation(Vector2I location) =>
			 GetActors().FirstOrDefault(a => a.GridPosition == location);
			

		public void SetupPathfinder()
		{
			_pathfinder = new AStarGrid2D();
			_pathfinder.Region = new Rect2I(Vector2I.Zero, new Vector2I(Width, Height));
			_pathfinder.Update();
			for(int i = 0; i < Width; i++)
			{
				for(int j = 0; j < Height; j++)
				{
					var gridPosition = new Vector2I(i, j);
					var tile = GetTile(gridPosition);
					_pathfinder.SetPointSolid(gridPosition, !tile.IsWalkable());
				}
			}
			foreach (var entity in Entities)
			{
				if(entity.IsBlocking)
				{
					RegisterBlockingEntity(entity);
				}
			}
		}
	}
}
