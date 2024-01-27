using Godot;
using System;
using System.Diagnostics;

[GlobalClass]
public partial class Card : Resource
{
	
	public enum EffectType {Atk, Def, Util}
	public enum TargetType {Self, Single, AOE}
	
	
	
	[Export] public String id;
	[Export] public EffectType Effect;
	[Export] public TargetType Target;
	[Export] public int Ap_cost = 1;

	[Export] public Texture2D icon;
	
	public bool is_single_target()
	{
		return Target == TargetType.Single;
	}

	public Godot.Collections.Array<Godot.Node> get_tagets(Godot.Collections.Array<Godot.Node> targets)
	{
		Debug.Print("cards.cs - " + targets.Count);
		
		var tree = targets[0].GetTree();

		switch (Target)
		{
			case TargetType.Self:
				return tree.GetNodesInGroup("player");
				break;
			case TargetType.AOE:
				return tree.GetNodesInGroup("enemy");
				break; 
			default:
				return new Godot.Collections.Array<Godot.Node>();
			break;
		}
	}

	public void play(Godot.Collections.Array<Godot.Node> targets, CharacterStats char_stats)
	{
		
		char_stats.Ap -= Ap_cost;
		
		if (is_single_target())
		{
			apply_effects(targets[0..1]);
		}
		else
		{
			apply_effects(get_tagets(targets));
		}
	}

	public virtual void apply_effects(Godot.Collections.Array<Godot.Node> targets)
	{
		
	}
}
