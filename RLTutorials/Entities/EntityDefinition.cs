using Godot;

namespace SupaRL.Entities
{
	[GlobalClass]
	public partial class EntityDefinition : Resource
	{
		[ExportCategory("Visuals")]

		[Export]
		public string Name { get; set; }= "Unnamed Entity";

		[Export]
		public AtlasTexture Texture { get; set; }

		[Export(PropertyHint.ColorNoAlpha)]
		public Color Color { get; set; } = Colors.White;

		[ExportCategory("Mechanics")]
		[Export]
		public bool IsBlocking { get; set; } = true;

		[Export]
		public EntityType EntityType { get; set; } = EntityType.ACTOR;
				
		[ExportCategory("Components")]
		[Export]
		public FighterComponentDefinition FighterDefinition { get; set; }

		[Export]
		public Entity.AIType AIType { get; set; } = Entity.AIType.NONE;
	}
}
