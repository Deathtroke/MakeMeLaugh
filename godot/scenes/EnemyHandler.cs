using Godot;
using System;
using System.Threading.Tasks;

public partial class EnemyHandler : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	void reset_enemy_actions()
	{
		enemy e;
		foreach (var child in GetChildren())
		{
			e = child as enemy;
			e.curren_action = null;
			e.Stats.On_stats_changed();
		}
	}

	public async void stat_turn()
	{
		if (GetChildCount() == 0)
		{
			return;
		}


		foreach (enemy e in GetChildren())
		{
			e.do_turn();

			await Task.Delay(200);
		}
	}
}
