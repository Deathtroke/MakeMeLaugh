using Godot;
using System;
using Godot.Collections;

public partial class Rat_Attack2 : Enemy_Action
{
	[Export] public int damage = 6;

	public override void perform_action()
	{
		if (_enemy == null || target == null)
		{
			return;
		}
		
		Damage_Effect dmg_eff = new Damage_Effect();
		dmg_eff.amount = damage;
		Array<Node> targets = new Array<Node>();
		targets.Add(target);
		dmg_eff.execute(targets);
	}
}
