using Godot;
using System;

public partial class ap_ui : Panel
{
	Label ap_Label;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ap_Label = GetNode<Label>("APLabel");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	void on_char_stats()
	{
		ap_Label.Text = "2/2";
	}
}
