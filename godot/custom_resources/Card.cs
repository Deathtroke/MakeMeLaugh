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
	[Export] public int Ap_cost = 1;

	[Export] public Texture2D icon;
	
	public bool is_single_target()
	{
		return Target == TargetType.Single;
	}
}
