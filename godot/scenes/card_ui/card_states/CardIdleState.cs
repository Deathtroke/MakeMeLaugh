using Godot;
using System;
using Color = System.Drawing.Color;

public partial class CardIdleState : CardState
{
	void enter()
	{
		if (!c_ui.IsNodeReady())
		{
			c_ui._Ready();
		}

		EmitSignal(CardUI.SignalName.Reparent, c_ui);
		c_ui.color.Color = Godot.Color.FromHtml("WEB_GREEN");
		c_ui.state.Text = "idle";
		c_ui.PivotOffset = Vector2.Zero;
	}

	void on_gui_input(InputEvent e)
	{
		if (e.IsActionPressed("left_mouse"))
		{
			c_ui.PivotOffset = c_ui.GetGlobalMousePosition() - c_ui.GlobalPosition;
			EmitSignal(SignalName.Transition, this, (int)State.Clicked);
		}
	}
}
