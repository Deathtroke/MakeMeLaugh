using Godot;
using System;

public partial class Enemy_Action : Node
{
	public enum Type
	{
		conditional, chance
	}

	[Export] public Type type;
	[Export] public float chance_weigth = 0.0f;

	public float acc_weight = 0.0f;

	public enemy _enemy;
	public Node target;
	
	public virtual bool is_performable()
	{
		return false;
	}

	public virtual void perform_action()
	{
		
	}
}
