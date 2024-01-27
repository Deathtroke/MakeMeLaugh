using Godot;
using System;
using Godot.Collections;

public partial class Basic_Def_1 : Card
{
    public override void apply_effects(Array<Node> targets)
    {
        var damage_effect = new Block_Effect();
        damage_effect.amount = 6;
        damage_effect.execute(targets);
    }
}
