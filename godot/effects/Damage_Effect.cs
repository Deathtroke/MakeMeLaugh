using Godot;
using System;
using System.Diagnostics;
using Godot.Collections;

public partial class Damage_Effect : Effects
{
	public int amount = 0;

	public override void execute(Array<Node> targets)
	{
		foreach (var target in targets)
		{
			if (target is player p)
			{
				p.Stats.take_damage(amount);
			}
			else if (target is enemy e)
			{
				Debug.Print("" + e.Stats);
				e.take_damage(amount);
			}
		}
	}
}
