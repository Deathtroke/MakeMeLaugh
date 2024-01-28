using Godot;
using System;
using Godot.Collections;

public partial class Dragon_Attack1 : Enemy_Action
{
	[Export] public int Damage = 6;

	public override void perform_action()
	{
		GD.Print(target);
		if (target == null)
		{
			return;
		}
		
		Damage_Effect dmg_eff = new Damage_Effect();
		dmg_eff.amount = Damage;
		Array<Node> targets = new Array<Node>();
		targets.Add(target);
		dmg_eff.execute(targets);
	}
}