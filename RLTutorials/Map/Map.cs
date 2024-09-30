using Godot;
using SupaRL.Entities;

namespace SupaRL.Map
{
	public partial class Map : Node2D
	{
		public MapData MapData { get; set; }
		private DungeonGenerator _dungeonGenerator;

		public override void _Ready()
		{
			_dungeonGenerator = GetNode<DungeonGenerator>("DungeonGenerator");
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
