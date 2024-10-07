using Godot;


namespace SupaRL {
public partial class FighterComponent : BaseComponent
{

	private int _maxHp;
	private int _hp;
	private int _defense;

	public FighterComponent(FighterComponentDefinition definition)
	{
		_maxHp = definition.MaxHp;
		_hp = definition.Hp;
		_defense = definition.Defense;
	
	}
}
}
