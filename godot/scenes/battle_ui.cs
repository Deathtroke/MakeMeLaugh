using Godot;
using System;

public partial class battle_ui : CanvasLayer
{
	private Hand _hand;
	private ap_ui _ap_ui;

	private void _ready()
	{
		_hand = GetNode<Hand>("Hand");
		_ap_ui = GetNode<ap_ui>("APUI");
	}
 
	[Export] public CharacterStats Character_stats;
	
	[Export] public CharacterStats Stats
	{
		get { return Character_stats; }
		set { setStats(value); }
	}
	
	private void setStats(CharacterStats value)
	{
		Character_stats = value;
		_ap_ui.char_stats = Character_stats;
		_hand.Character_Stats = Character_stats;
	}
}
