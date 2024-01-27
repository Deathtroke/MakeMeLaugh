using Godot;
using System;

public partial class battle_ui : CanvasLayer
{
	//private Hand _hand;
	private ap_ui _ap_ui;
	
	private void _ready()
	{
		//_hand = GetNode<Hand>("Hand");
		_ap_ui = GetNode<ap_ui>("APUI");
	}
	
	public CharacterStats _character_stats;
	
	[Export] public CharacterStats Stats
	{
		get { return _character_stats; }
		set { setStats(value); }
	}
	
	private void setStats(CharacterStats value)
	{
		_character_stats = value;
		//_ap_ui.char_stats = char_stats;
	}
}
