using Godot;
using SupaRL.Utls;

namespace SupaRL.Entities
{
    public partial class Entity : Sprite2D
    {
        private EntityDefinition _entityDefinition;
        public void SetEntityType(EntityDefinition entityDefinition)
        {
            _entityDefinition = entityDefinition;
            Texture = entityDefinition.Texture;
            Modulate = entityDefinition.Color;
        }

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
            SetEntityType(entityDefinition);
        }

        public void Move(Vector2I offset) =>
            GridPosition += offset;

        public bool IsBlocking() =>
            _entityDefinition.IsBlocking;

        public string GetEntityName() =>
            _entityDefinition.Name;
    }
}
