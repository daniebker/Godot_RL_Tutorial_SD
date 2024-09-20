using Godot;

namespace SupaRL.src.utls
{
    public partial class Grid
    {
        public static readonly Vector2I tileSize = new Vector2I(8,8);

        public static Vector2I GridToWorld(Vector2I gridPosition)
        {
            return gridPosition * tileSize;
        }

        public static Vector2I WorldToGrid(Vector2I worldPosition)
        {
            return worldPosition / tileSize;
        }
    }

}
