using Godot;
using System;

public partial class CardReleaseState : CardState
{
	// Called when the node enters the scene tree for the first time.
	public override void Enter()
	{
		c_ui.color.Color = Godot.Color.Color8(150, 0, 150);;
		c_ui.state.Text = "released";
		c_ui.PivotOffset = Vector2.Zero;
	}


}
