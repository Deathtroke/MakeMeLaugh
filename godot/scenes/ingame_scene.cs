using Godot;
using System;

public partial class ingame_scene : Node2D
{
	private ColorRect fade_overlay;
	private CenterContainer pause_overlay;
	private battle_ui _battle_ui;

	[Export] public CharacterStats Char_stats;
	private void _ready()
	{
		fade_overlay = GetNode<ColorRect>("UI/FadeOverlay");
		pause_overlay = GetNode<CenterContainer>("UI/PauseOverlay");
		_battle_ui = GetNode<battle_ui>("BattleUI");

		fade_overlay.Visible = true;
		
		CharacterStats _new_stats = Char_stats.create_instance();
		_battle_ui._character_stats = _new_stats;
		
		start_battle();
	}
	
	private void _input(InputEvent @event)
	{
		if (@event.IsActionPressed("pause") && !pause_overlay.Visible)
		{
			GetViewport().SetInputAsHandled();
			GetTree().Paused = true;
			pause_overlay.GrabFocus();
			pause_overlay.Visible = true;
		}
	}
	
	private void start_battle()
	{
		GD.Print("[ingame_scene.cs] Battle started");
	}
}
