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

		public override void _Ready()
		{
			_dungeonGenerator = GetNode<DungeonGenerator>("DungeonGenerator");
			_fieldOfView = GetNode<FieldOfView>("FieldOfView");
		}

		public void UpdateFieldOfView(Vector2I playerPosition)
			{
				_fieldOfView.UpdateFov(MapData, playerPosition, FovRadius);
			}

		public void Generate(Entity player)
		{
			MapData = _dungeonGenerator.GenerateDungeon(player);
			PlaceTiles();
		}

		private void PlaceTiles()
		{
			foreach (var tile in MapData.Tiles)
			{
				AddChild(tile);
			}
		}
	}
}
