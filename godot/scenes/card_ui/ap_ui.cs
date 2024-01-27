using Godot;
using System;

public partial class ap_ui : Panel
{
	Label ap_Label;

	[Export] private CharacterStats char_stats;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ap_Label = GetNode<Label>("APLabel");
		set_char_stats(char_stats);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	void set_char_stats(CharacterStats value)
	{
		char_stats = value;
		char_stats.StatsChanged += on_char_stats;
	}

	void on_char_stats(object sender, EventArgs e)
	{
		ap_Label.Text = char_stats.Ap + "/" + char_stats.Max_ap;
	}
}
