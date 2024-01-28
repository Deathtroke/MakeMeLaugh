using Godot;
using System;
using System.Threading.Tasks;

public partial class EnemyHandler : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void reset_enemy_actions()
	{
	foreach (enemy e in GetChildren())
		{
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
			GD.Print("e turn" + e.Name);
			e.do_turn();

			await Task.Delay(200);
		}
	}
}
