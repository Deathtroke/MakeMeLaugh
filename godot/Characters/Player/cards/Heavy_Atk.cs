using Godot;
using System;
using Godot.Collections;

public partial class Heavy_Atk : Card
{
	public override void apply_effects(Array<Node> targets)
	{
		var damage_effect = new Damage_Effect();
		damage_effect.amount = 8;
		damage_effect.execute(targets);
	}
	
	public override Card Duplicate()
	{
		var instance = new Heavy_Atk();
		
		instance.id = this.id;
		instance.Effect = this.Effect;
		instance.Target = this.Target;
		instance.Ap_cost = this.Ap_cost;
		instance.icon = this.icon.Duplicate(true) as Texture2D; // Assuming Texture2D has a Duplicate method
		
		instance.description = this.description;
		
		return instance;
	}
}
