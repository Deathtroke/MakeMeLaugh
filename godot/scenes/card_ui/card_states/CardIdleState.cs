using Godot;
using System;
using System.Diagnostics;
using Color = System.Drawing.Color;

public partial class CardIdleState : CardState
{
	public override void Enter()
	{
		c_ui.state.Text = "idle";
		c_ui.PivotOffset = Vector2.Zero;
		c_ui.panel.Set("theme_override_styles/panel", c_ui.default_style);
	}

	public override void on_gui_input(InputEvent e)
	{
		if (e.IsActionPressed("left_mouse") && (c_ui.hovered))
		{
			c_ui.PivotOffset = c_ui.GetGlobalMousePosition() - c_ui.GlobalPosition;
			EmitSignal(SignalName.Transition, this, (int)State.Clicked);
		}
	}
}
