using Godot;
using System;
using System.Diagnostics;

public partial class CardDragState : CardState
{
  
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
		

		c_ui.color.Color = Godot.Color.Color8(0, 0, 255);
		c_ui.state.Text = "drag";
		
		drag_time_passed = false;

		var timer = GetTree().CreateTimer(drag_min_threshold, false);
		timer.Timeout += () => drag_time_passed = true;
	}

	public override void on_gui_input(InputEvent e)
	{
		var mouse_motion = e is InputEventMouseMotion;
		var cancel = e.IsActionPressed("right_mouse");
		var confirm = e.IsActionPressed("left_mouse") || e.IsActionReleased("left_mouse");

		Debug.Print(c_ui.card.id);
		
		if (c_ui.card.is_single_target() && mouse_motion && c_ui.targets.Count > 0)
		{
			EmitSignal(SignalName.Transition, this, (int)State.Aiming);
			return;
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
			if (c_ui.GetGlobalMousePosition().Y < 700)
			{
				GetViewport().SetInputAsHandled();
				c_ui.hovered = false;
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
