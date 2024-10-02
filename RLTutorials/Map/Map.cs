using Godot;
using SupaRL.Entities;

namespace SupaRL.Map
{
	public partial class Map : Node2D
	{
		[Export]
		public int FovRadius = 8;

		public MapData MapData { get; set; }
		private DungeonGenerator _dungeonGenerator;
		private FieldOfView _fieldOfView;
        private Node2D _entities;
        private Node2D _tiles;

		public override void _Ready()
		{
			_dungeonGenerator = GetNode<DungeonGenerator>("DungeonGenerator");
			_fieldOfView = GetNode<FieldOfView>("FieldOfView");
            _entities = GetNode<Node2D>("Entities");
            _tiles = GetNode<Node2D>("Tiles");
		}

        public void UpdateFieldOfView(Vector2I playerPosition)
        {
            _fieldOfView.UpdateFov(MapData, playerPosition, FovRadius);
        }

		public void Generate(Entity player)
		{
			MapData = _dungeonGenerator.GenerateDungeon(player);
			PlaceTiles();
            PlaceEntities();
		}

		private void PlaceTiles()
		{
			foreach (var tile in MapData.Tiles)
			{
				_tiles.AddChild(tile);
			}
		}

        private void PlaceEntities()
        {
            foreach (var entity in MapData.Entities)
            {
                _entities.AddChild(entity);
            }
        }
	}
}
