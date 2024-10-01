using Godot;
using SupaRL.Utls;

namespace SupaRL.Entities
{
    public partial class Entity : Sprite2D
    {
        private Vector2I _gridPosition;
        public Vector2I GridPosition
        {
            get => _gridPosition;
            set
            {
                _gridPosition = value;
                Position = Grid.GridToWorld(_gridPosition);
            }
        }

        public Entity() { }

        public Entity(Vector2I startPosition, EntityDefinition entityDefinition)
        {
            Centered = false;
            GridPosition = startPosition;
            Texture = entityDefinition.Texture;
            Modulate = entityDefinition.Color;
        }

        public void Move(Vector2I offset) =>
            GridPosition += offset;
    }
}
