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

	public int level;

	[Export] public CharacterStats Char_stats;
	private void _ready()
	{
		fade_overlay = GetNode<ColorRect>("UI/FadeOverlay");
		pause_overlay = GetNode<CenterContainer>("UI/PauseOverlay");
		_battle_ui = GetNode<battle_ui>("BattleUI");
		_player_handler = GetNode<PlayerHandler>("PlayerHandler");

		level = 0;
		
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
		new_level();
		_player_handler.start_battle(stats);
		enemyHandler.reset_enemy_actions();
	}

	public void on_enemy_change()
	{
		GD.Print("xxxx" + enemyHandler.GetChildCount());
		if (enemyHandler.GetChildCount() == 0)
		{
			level += 1;
			new_level();
			
			CharacterStats _new_stats = Char_stats.create_instance();
			_battle_ui.Character_stats = _new_stats;
			_player._stats = _new_stats;
		
			_player_handler.discard_cards();
			
			start_battle(_new_stats);
		}
	}

	public void new_level()
	{
		Sprite2D background = GetNode<Sprite2D>("Background");

		switch (level)
		{
			case 0:
				background.Texture = GD.Load<Texture2D>("res://Art/Backgrounds/Keller_v01.png");
				enemyHandler = GetNode<EnemyHandler>("EnemyHandler1");
				break;
			case 1:
				background.Texture = GD.Load<Texture2D>("res://Art/Backgrounds/Wald_v01.png");
				enemyHandler = GetNode<EnemyHandler>("EnemyHandler2");
				break;
			case 2:
				background.Texture = GD.Load<Texture2D>("res://Art/Backgrounds/Wald_v01.png");
				enemyHandler = GetNode<EnemyHandler>("EnemyHandler3");
				break;
			case 3:
				background.Texture = GD.Load<Texture2D>("res://Art/Backgrounds/Drachenhort_v01.png");
				enemyHandler = GetNode<EnemyHandler>("EnemyHandler4");
				break;
			case 4:
				background.Texture = GD.Load<Texture2D>("res://Art/Backgrounds/Drachenhort_v01.png");
				enemyHandler = GetNode<EnemyHandler>("EnemyHandler5");
				break;
			default:
				break;
		}

		enemyHandler.Visible = true;
		enemyHandler.ChildOrderChanged += on_enemy_change;
		foreach (var enemy_node in enemyHandler.GetChildren())
		{
			enemy_node.AddToGroup("enemy");
			enemy_node.AddToGroup("enemys");
		}
		
	}
	
}
