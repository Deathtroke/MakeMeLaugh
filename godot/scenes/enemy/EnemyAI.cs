using Godot;
using System;

public partial class EnemyAI : Node
{
	[Export] public enemy _enemy;

	[Export] public Node target;

	private float total_weight = 0.0f;

	public override void _Ready()
	{
		set_target(GetTree().GetFirstNodeInGroup("player"));
		setup_chance();
	}

	public void set_enemy(enemy e)
	{
		_enemy = e;

		foreach (Enemy_Action action in GetChildren())
		{
			action._enemy = _enemy;
		}
	}

	public void set_target(Node t)
	{
		target = t;

		foreach (Enemy_Action action in GetChildren())
		{
			action.target = target;
		}
	}
	
	public Enemy_Action get_action()
	{
		var action = get_first_con_action();
		
		GD.Print(action);

		if (action != null)
		{
			return action;
		}
		
		return get_chance_action();
	}

	Enemy_Action get_first_con_action()
	{
		foreach (Enemy_Action action in GetChildren())
		{
			if (action.type != Enemy_Action.Type.conditional)
			{
				continue;
			}

			if (action.is_performable())
			{
				return action;
			}
		}

		return null;
	}

	Enemy_Action get_chance_action()
	{
		Random rng = new Random();
		float roll = rng.NextSingle() * total_weight;
		
		foreach (Enemy_Action action in GetChildren())
		{
			if (action.type != Enemy_Action.Type.chance)
			{
				continue;
			}

			if (action.acc_weight > roll)
			{
				return action;
			}
		}
		return null;
	}
	
	void setup_chance()
	{
		foreach (Enemy_Action action in GetChildren())
		{
			if (action.type != Enemy_Action.Type.chance)
			{
				continue;
			}

			total_weight += action.chance_weigth;
			action.acc_weight = total_weight;
		}

	}
}
