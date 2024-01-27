using Godot;
using System;
using System.Collections.Generic;

public partial class CardAimingState : CardState
{
	private const int mouse_y_cancel = 700;

	public override void Enter()
	{
		c_ui.color.Color = Godot.Color.Color8(0, 0, 155);
		c_ui.state.Text = "aiming";

		c_ui.targets = new List<Node>();

		var offset = new Vector2(c_ui.parent.Size.X / 2, -c_ui.Size.Y / 2);

		offset.X -= c_ui.Size.X / 2;
		c_ui.animate_to_position(c_ui.parent.Position + offset, 0.2f);

		//EmitSignal(Events.AimStart, c_ui);
	}

	public override void Exit()
	{
		//EmitSignal(Events.AimEnd, c_ui);
	}

	public override void on_gui_input(InputEvent e)
	{
		bool mouse_motion = e is InputEventMouseMotion;
		var mous_to_low = c_ui.GetGlobalMousePosition().Y > mouse_y_cancel;
		
		if ((mouse_motion && mous_to_low) || e.IsActionPressed("right_mouse"))
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
		else
		{
			GetViewport().SetInputAsHandled();
			c_ui.hovered = false;
			EmitSignal(SignalName.Transition, this, (int)State.Released);
		}
	}
}
