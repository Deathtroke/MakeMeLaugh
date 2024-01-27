using Godot;
using System;
using System.Diagnostics;
using Color = System.Drawing.Color;

public partial class CardIdleState : CardState
{
	public override void Enter()
	{
		c_ui.color.Color = Godot.Color.Color8(0, 255, 0);
		c_ui.state.Text = "idle";
		c_ui.PivotOffset = Vector2.Zero;
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
