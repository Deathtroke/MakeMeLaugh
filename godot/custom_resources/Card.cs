using Godot;
using System;

public partial class Card : Resource
{
	
	public enum EffectType {Atk, Def, Util}
	public enum TargetType {Self, Single, AOE}

	public String id;
	public EffectType effectType;
	public TargetType targetType;
	
	//TEMPORARY!
	public int Ap_cost = 1;

	public Card(String new_id, EffectType new_effectType, TargetType new_targetType)
	{
		id = new_id;
		effectType = new_effectType;
		targetType = new_targetType;
	}

	
	public bool is_single_target()
	{
		return targetType == TargetType.Single;
	}
	
}
