using Godot;
using System;
using System.Diagnostics;

[GlobalClass]
public partial class Card : Resource
{
	
	public enum EffectType {Atk, Def, Util}
	public enum TargetType {Self, Single, AOE}
	
	
	
	[ExportGroup("Stats")]
	[Export] public EffectType Effect;
	[Export] public TargetType Target;
	[Export] public int Ap_cost = 1;

	[ExportGroup("Info")]
	[Export] public String id;
	[Export] public Texture2D icon;
	[Export] public String description;
	
	public bool is_single_target()
	{
		return Target == TargetType.Single;
	}

	public Godot.Collections.Array<Godot.Node> get_tagets(Godot.Collections.Array<Godot.Node> targets)
	{
		var tree = targets[0].GetTree();

		switch (Target)
		{
			case TargetType.Self:
				return tree.GetNodesInGroup("player");
			case TargetType.AOE:
				return tree.GetNodesInGroup("enemy");
			default:
				return new Godot.Collections.Array<Godot.Node>();
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
	public virtual Card Duplicate()
	{
		var instance = new Card();
		
		instance.id = this.id;
		instance.description = this.description;
		instance.Effect = this.Effect;
		instance.Target = this.Target;
		instance.Ap_cost = this.Ap_cost;
		instance.icon = this.icon.Duplicate(true) as Texture2D; // Assuming Texture2D has a Duplicate method
		
		
		return instance;
	
	}
}
