using Godot;
using System;

public partial class CardClickedState : CardState
{
	public override void Enter()
	{
		var hand = GetTree().GetFirstNodeInGroup("hand");
		if (hand != null)
		{
			Reparent(hand);
		}
		c_ui.state.Text = "clicked";
		
	}

	public override void on_gui_input(InputEvent e)
	{
		if (e is InputEventMouseMotion)
		{
			EmitSignal(SignalName.Transition, this, (int)State.Drag);
		}
	}
}
