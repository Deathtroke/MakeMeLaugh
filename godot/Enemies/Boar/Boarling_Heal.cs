using Godot;
using System;
using Godot.Collections;

public partial class Boarling_Heal : Enemy_Action
{
	[Export] public int block = 2;

	public override void perform_action()
	{
		GD.Print(_enemy);

		if (_enemy == null)
		{
			return;
		}

		Heal_Effect block_eff = new Heal_Effect();
		block_eff.amount = block;
		Array<Node> targets = new Array<Node>();
		targets.Add(_enemy);
		block_eff.execute(targets);
	}
}
