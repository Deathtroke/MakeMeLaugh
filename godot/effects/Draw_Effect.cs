using Godot;
using System;
using System.Diagnostics;
using Godot.Collections;

public partial class Draw_Effect : Effects
{
	public int amount = 0;

	public override void execute(Array<Node> targets)
	{
		foreach (var target in targets)
		{
			if (target is player p)
			{
				p._playerHandler.draw_cards(amount);
			}
		}
	}
}
