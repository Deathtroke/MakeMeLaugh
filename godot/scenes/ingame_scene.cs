using Godot;
using System;

public partial class ingame_scene : Node2D
{
	private ColorRect fade_overlay;
	private CenterContainer pause_overlay;
	private battle_ui _battle_ui;
	private PlayerHandler _player_handler;
	private EnemyHandler enemyHandler;
	private player _player;

	[Export] public CharacterStats Char_stats;
	private void _ready()
	{
		fade_overlay = GetNode<ColorRect>("UI/FadeOverlay");
		pause_overlay = GetNode<CenterContainer>("UI/PauseOverlay");
		_battle_ui = GetNode<battle_ui>("BattleUI");
		_player_handler = GetNode<PlayerHandler>("PlayerHandler");
		enemyHandler = GetNode<EnemyHandler>("EnemyHandler");
		_player = GetNode<player>("Player");
		_battle_ui = GetNode<battle_ui>("BattleUI");
 		
		_player_handler.DiscardFinished += OnDiscardFinished;
		_battle_ui.EndTurn += OnEndTurn;

		fade_overlay.Visible = true;

		CharacterStats _new_stats = Char_stats.create_instance();
		_battle_ui.Character_stats = _new_stats;
		_player._stats = _new_stats;
		
		start_battle(_new_stats);
	}

	private void OnEndTurn()
	{
		GD.Print("Turn ending");
		_player_handler._end_turn();
		GD.Print("Turn ended.");
		enemyHandler.stat_turn();
		enemyHandler.reset_enemy_actions();
		
		_player_handler.start_turn();
	}

	private void OnDiscardFinished()
	{
		GD.Print("Discard finished.");
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
	
	private void start_battle(CharacterStats stats)
	{
		
		_player_handler.start_battle(stats);
		enemyHandler.reset_enemy_actions();
	}
	
}
