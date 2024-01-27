using Godot;
using System;
using Godot.Collections;

public partial class Block_Effect : Effects
{
    public int amount = 0;

    public override void execute(Array<Node> targets)
    {
        foreach (var target in targets)
        {
            if (target is player p)
            {
                p.Stats.Block += amount;
            }
            else if (target is enemy e)
            {
                e.Stats.Block += amount;
            }
        }
    }
}
