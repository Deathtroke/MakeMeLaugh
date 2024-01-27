using Godot;
using System;

public partial class Card : Resource
{
	
	public enum EffectType {Atk, Def, Util}
	public enum TargetType {Self, Single, AOE}

	public String id;
	public EffectType effectType;
	public TargetType targetType;
			
	bool is_single_target()
	{
		return targetType == TargetType.Single;
	}
	
}
