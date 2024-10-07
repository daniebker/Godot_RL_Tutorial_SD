using Godot;
using SupaRL.Utls;


namespace SupaRL.Entities
{
	public partial class Entity : Sprite2D
	{
		public enum AIType { NONE, HOSTILE };
		public Entity(MapData mapData, Vector2I startPosition, EntityDefinition entityDefinition)
		{
			MapData = mapData;
			Centered = false;
			GridPosition = startPosition;
			SetEntityType(entityDefinition);
		}

		private EntityDefinition _entityDefinition;
		public void SetEntityType(EntityDefinition entityDefinition)
		{
			EntityType = entityDefinition.EntityType;
			_entityDefinition = entityDefinition;
			Texture = entityDefinition.Texture;
			Modulate = entityDefinition.Color;
			IsBlocking = entityDefinition.IsBlocking;
			EntityName = entityDefinition.Name;

			switch (entityDefinition.AIType)
			{
				case AIType.HOSTILE:
					AIComponent = new HostileEnemyAIComponent();
					AddChild(AIComponent);
					break;
			}
	
			if (entityDefinition.FighterDefinition != null)
			{
				FighterComponent = new FighterComponent(entityDefinition.FighterDefinition);
				AddChild(FighterComponent);
			}
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

		public MapData MapData { get;  set; }

		public void Move(Vector2I offset) {
			MapData.UnregisterBlockingEntity(this);
			GridPosition += offset;
			MapData.RegisterBlockingEntity(this);
		}

		public bool IsBlocking {
			get; private set;
		}

		public string EntityName {get; private set;}

		private EntityType entityType;
		public EntityType EntityType
		{
			get => entityType;
			set
			{
				entityType = value;
				ZIndex = (int)entityType;
			}
		}

		public FighterComponent FighterComponent { get; set; }
		public BaseAIComponent AIComponent { get; set; }

		public bool IsAlive => AIComponent != null;
	}
}
