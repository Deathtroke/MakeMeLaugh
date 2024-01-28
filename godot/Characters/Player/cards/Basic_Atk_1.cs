using Godot;
using System;
using Godot.Collections;

public partial class Basic_Atk_1 : Card
{
	public override void apply_effects(Array<Node> targets)
	{
		var damage_effect = new Damage_Effect();
		damage_effect.amount = 5;
		damage_effect.execute(targets);
	}
	
	public override Card Duplicate()
	{
		var instance = new Basic_Atk_1();
		
		instance.id = this.id;
		instance.Effect = this.Effect;
		instance.Target = this.Target;
		instance.Ap_cost = this.Ap_cost;
		instance.icon = this.icon.Duplicate(true) as Texture2D; // Assuming Texture2D has a Duplicate method
		
		
		return instance;
	}
}
