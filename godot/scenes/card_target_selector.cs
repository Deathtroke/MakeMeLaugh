using Godot;
using System;

public partial class card_target_selector : Node2D
{
	private const int arc_points = 8;
	private Area2D area2D;
	private Line2D arc;

	private CardUI current_card;
	private bool targeting = false;

	public override void _Ready()
	{
		area2D = GetNode<Area2D>("Area2D");
		arc = GetNode<Line2D>("CanvasLayer/arch");

		area2D.AreaEntered += _on_area_2d_area_shape_entered;
		area2D.AreaExited += _on_area_2d_area_shape_exited;
	}

	public override void _Process(double delta)
	{
		if (!targeting)
		{
			return;
		}

		area2D.Position = GetLocalMousePosition();
		arc.Points = getPoints();
	}

	Vector2[] getPoints()
	{
		Vector2[] points = new Vector2[arc_points+1];
		var start = current_card.GlobalPosition;
		start.X += (current_card.Size.X / 2);
		var target = GetLocalMousePosition();
		var distance = target - start;

		for (int i = 0; i < arc_points; i++)
		{
			var t = (1.0f / arc_points) * i;
			var x = start.X + (distance.X / arc_points) * i;
			var y = start.Y + ease_out_cubic(t) * distance.Y;
			points[i] = new Vector2(x, y);




		}
		points[arc_points] = target;
		return points;
	}
	
	float ease_out_cubic(float number)
	{
		return 1.0f - (float)Math.Pow(1.0 - number, 3.0);
	}
	
	void _on_card_aim_started(CardUI card)
	{
		if (!card.card.is_single_target())
		{
			return;
		}


		targeting = true;
		area2D.Monitoring = true;
		area2D.Monitorable = true;
		current_card = card;
	}

	void _on_card_aim_ended(CardUI card)
	{
		if (!card.card.is_single_target())
		{
			return;
		}


		targeting = false;
		arc.ClearPoints();
		area2D.Position = Vector2.Zero;
		area2D.Monitoring = false;
		area2D.Monitorable = false;
		current_card = null;
	}
	
	
	private void _on_area_2d_area_shape_entered(Area2D area)
	{
		if (current_card == null || !targeting)
		{
			return;
		}

		if (!current_card.targets.Contains(area))
		{
			current_card.targets.Insert(-1, area);
		}
	}
	
	private void _on_area_2d_area_shape_exited(Area2D area)
	{
		if (current_card == null || !targeting)
		{
			return;
		}
	
		current_card.targets.Remove(area);
	}
}



