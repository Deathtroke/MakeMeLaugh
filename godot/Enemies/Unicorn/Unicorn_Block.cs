using Godot;
using System;
using Godot.Collections;

public partial class Unicorn_Block : Enemy_Action
{
	[Export] public int block = 6;

	public override void perform_action()
	{
		GD.Print(_enemy);

		if (_enemy == null)
		{
			return;
		}

		Block_Effect block_eff = new Block_Effect();
		block_eff.amount = block;
		Array<Node> targets = new Array<Node>();
		targets.Add(_enemy);
		block_eff.execute(targets);
	}
}
