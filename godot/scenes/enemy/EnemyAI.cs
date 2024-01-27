using Godot;
using System;

public partial class EnemyAI : Node
{
	[Export]
	public enemy _enemy
	{
		set
		{
			_enemy = value;

			foreach (Enemy_Action action in GetChildren())
			{
				action._enemy = _enemy;
			}
		}
		get
		{
			return _enemy;
		}
	}

	[Export]
	public Node target
	{
		set
		{
			target = value;
			
			foreach (Enemy_Action action in GetChildren())
			{
				action.target = target;
			}
		}
		get
		{
			return target;
		}
	}

	private float total_weight = 0.0f;

	public override void _Ready()
	{
		target = GetTree().GetFirstNodeInGroup("player");
		setup_chance();
	}

	public Enemy_Action get_action()
	{
		var action = get_first_con_action();

		if (action != null)
		{
			return action;
		}
		
		return null;
	}

	Enemy_Action get_first_con_action()
	{
		Enemy_Action action;

		foreach (var child in GetChildren())
		{
			action = child as Enemy_Action;
			if (action == null || action.type != Enemy_Action.Type.conditional)
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
		Enemy_Action action;
		Random rng = new Random();
		float roll = rng.NextSingle() * total_weight;
		
		foreach (var child in GetChildren())
		{
			action = child as Enemy_Action;
			if (action == null || action.type != Enemy_Action.Type.chance)
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
		Enemy_Action action;

		foreach (var child in GetChildren())
		{
			action = child as Enemy_Action;
			if (action == null || action.type != Enemy_Action.Type.chance)
			{
				continue;
			}

			total_weight += action.chance_weigth;
			action.acc_weight = total_weight;
		}

	}
}
