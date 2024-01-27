using Godot;
using System;

[GlobalClass]
public partial class Card : Resource
{
	
	public enum EffectType {Atk, Def, Util}
	public enum TargetType {Self, Single, AOE}

	[Export] public String id;
	[Export] public EffectType Effect;
	[Export] public TargetType Target;
	
	public bool is_single_target()
	{
		return Target == TargetType.Single;
	}
}
