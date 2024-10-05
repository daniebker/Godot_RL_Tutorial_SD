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
    }
}
