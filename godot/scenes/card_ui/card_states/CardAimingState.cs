using Godot;
using System;
using System.Collections.Generic;

public partial class CardAimingState : CardState
{
	private const int mouse_y_cancel = 700;
	const float drag_min_threshold = 0.05f;
	private bool drag_time_passed = false;
	
	public override void Enter()
	{
		var ui_layer = GetTree().GetFirstNodeInGroup("ui_layer");
		if (ui_layer != null)
		{
			c_ui.GetParent().RemoveChild(c_ui);
			ui_layer.AddChild(c_ui);
		}
		

		
		drag_time_passed = false;

		var timer = GetTree().CreateTimer(drag_min_threshold, false);
		timer.Timeout += () => drag_time_passed = true;
	}

	public override void Exit()
	{
		//EmitSignal(Events.AimEnd, c_ui);
	}

	public override void on_gui_input(InputEvent e)
	{
		var mouse_motion = e is InputEventMouseMotion;
		var cancel = e.IsActionPressed("right_mouse");
		var confirm = e.IsActionPressed("left_mouse") || e.IsActionReleased("left_mouse");
	
		float min_distance = 10000;
		Area2D closest_enemy = new Area2D();
		foreach (Area2D enemy in GetTree().GetNodesInGroup("enemy"))
		{
			//var size = enemy.GetChild<CollisionShape2D>(3);
			var dist = c_ui.GetGlobalMousePosition().DistanceTo(enemy.Position);
			if (dist < min_distance)
			{
				closest_enemy = enemy;
				min_distance = dist;
			}
			else
			{
				c_ui.targets.Remove(enemy);
			}
		}

		if (min_distance < 200)
		{
			if (closest_enemy is enemy en)
			{
				c_ui.targets.Add(en);
				en.hover();
			}
		}
		else
		{
			if (closest_enemy is enemy en)
			{
				foreach (Area2D p in c_ui.targets)
				{
					if (p.Position == en.Position)
					{
						c_ui.targets.Remove(p);
					}
				}
				en.hoverEnd();
			}
		}
	
		if (mouse_motion)
			c_ui.GlobalPosition = c_ui.GetGlobalMousePosition() - c_ui.PivotOffset;

		if (cancel)
		{
			var hand = GetTree().GetFirstNodeInGroup("hand");
			if (hand is BoxContainer box)
			{
				c_ui.GetParent().RemoveChild(c_ui);
				box.AddChild(c_ui);
				c_ui.PivotOffset = Vector2.Zero;
			}
			EmitSignal(SignalName.Transition, this, (int)State.Idle);
		}
		else if (confirm && drag_time_passed)
		{
			if (c_ui.GetGlobalMousePosition().Y < 700 && min_distance < 250)
			{
				GetViewport().SetInputAsHandled();
				c_ui.hovered = false;
				if (closest_enemy is enemy en)
				{
					en._arrow.Hide();
				}
				EmitSignal(SignalName.Transition, this, (int)State.Released);
			}
			else
			{
				var hand = GetTree().GetFirstNodeInGroup("hand");
				if (hand is BoxContainer box)
				{
					c_ui.GetParent().RemoveChild(c_ui);
					box.AddChild(c_ui);
					c_ui.PivotOffset = Vector2.Zero;
				}
				EmitSignal(SignalName.Transition, this, (int)State.Idle);
			}
		}
	}
}
