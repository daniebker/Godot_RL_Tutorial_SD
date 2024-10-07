using Godot;
using SupaRL.Utls;

namespace SupaRL
{
	public partial class Tile : Sprite2D
	{
		private TileDefinition definition;

		private bool _isExplored = false;
		public bool IsExplored
		{
			get => _isExplored;
			set
			{
				_isExplored = value;
				if (_isExplored && !Visible)
				{
					Visible = true;
				}
			}
		}
		
		private bool _isInView = false;
		public bool IsInView 
		{
			get => _isInView;
			set 
			{
				_isInView = value;
				Modulate = _isInView ? definition.ColorLit : definition.ColorDark;
				if (_isInView && !IsExplored)
				{
					IsExplored = true;
				}
			}
		}

		public Tile() { }
		public Tile(Vector2I gridPosition, TileDefinition tileDefinition)
		{
			Visible = false;
			Centered = false;
			Position = Grid.GridToWorld(gridPosition);
			SetTileType(tileDefinition);
		}

		public void SetTileType(TileDefinition tileDefinition)
		{
			definition = tileDefinition;
			Texture = definition.Texture;
			Modulate = definition.ColorDark;
		}

		public bool IsWalkable() =>
			definition.IsWalkable;

		public bool IsTransparent() =>
			definition.IsTransparent;
	}
}
