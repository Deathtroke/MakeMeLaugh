using Godot;
using System;

public partial class CardReleaseState : CardState
{
	// Called when the node enters the scene tree for the first time.
	void enter()
	{
		c_ui.color.Color = Godot.Color.FromHtml("DARK_VIOLET");
		c_ui.state.Text = "released";
	}


}
