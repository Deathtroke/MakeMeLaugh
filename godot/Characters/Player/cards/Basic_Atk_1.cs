using Godot;
using System;
using Godot.Collections;

public partial class Basic_Atk_1 : Card
{
	public override void apply_effects(Array<Node> targets)
	{
		var damage_effect = new Damage_Effect();
		damage_effect.amount = 6;
		damage_effect.execute(targets);
	}
}
