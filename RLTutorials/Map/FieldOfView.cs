using Godot;
using Godot.Collections;
using System;

namespace SupaRL
{
public partial class FieldOfView : Node
{
	int [,] _multipliers = new int[,]{
{1, 0,  0, -1, -1,  0,  0,  1},
			{0, 1, -1,  0,  0, -1,  1,  0},
			{0, 1,  1,  0,  0, -1, -1,  0},
			{1, 0,  0,  1, -1,  0,  0, -1},
	};

	private Array<Tile> _fov = new();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	   public void UpdateFov(MapData mapData, Vector2I origin, int radius)
		{
			ClearFov();
			var startTile = mapData.GetTile(origin);
			startTile.IsInView = true;
			_fov = new Array<Tile> { startTile };
			for (int i = 0; i < 8; i++)
			{
				CastLight(mapData, origin.X, origin.Y, radius, 1, 1.0f, 0.0f,
					_multipliers[0, i], _multipliers[1, i], _multipliers[2, i], _multipliers[3, i]);
			}
		}

		private void ClearFov()
		{
			foreach (var tile in _fov)
			{
				tile.IsInView = false;
			}
			_fov = new();
		}

	   // https://www.roguebasin.com/index.php?title=C%2B%2B_shadowcasting_implementation
		public void CastLight(MapData mapData, int x, int y, int radius, int row,
			float startSlope, float endSlope, int xx, int xy, int yx, int yy)
		{
			if (startSlope < endSlope) return;

			var nextStartSlope = startSlope;
			for (int i = row; i <= radius; i++)
			{
				var blocked = false;
				var dy = -i;
				for (int dx = -i; dx < 1; dx++)
				{
					var lSlope = (dx - 0.5f) / (dy + 0.5f);
					var rSlope = (dx + 0.5f) / (dy - 0.5f);
					if (startSlope < rSlope)
						continue;
					else if (endSlope > lSlope)
						break;

					var sax = dx * xx + dy * xy;
					var say = dx * yx + dy * yy;
					if ((sax < 0 && Mathf.Abs(sax) > x) || (say < 0 && Mathf.Abs(say) > y))
						continue;

					var ax = x + sax;
					var ay = y + say;
					if (ax >= mapData.Width || ay >= mapData.Height)
						continue;

					var radius2 = radius * radius;
					var currentTile = mapData.GetTile(ax, ay);
					if ((dx * dx + dy * dy) < radius2)
					{
						currentTile.IsInView = true;
						_fov.Add(currentTile);
					}

					if (blocked)
					{
						if (!currentTile.IsTransparent())
						{
							nextStartSlope = rSlope;
							continue;
						}
						else
						{
							blocked = false;
							startSlope = nextStartSlope;
						}
					}
					else if (!currentTile.IsTransparent())
					{
						blocked = true;
						nextStartSlope = rSlope;
						CastLight(mapData, x, y, radius, i + 1, startSlope, lSlope, xx, xy, yx, yy);
					}
				}
				if (blocked)
					break;
			}
		}
	}
}
