using Godot;
using System;

public partial class Hand : HBoxContainer
{
	private static PackedScene CARD_UI_SCENE = (PackedScene)ResourceLoader.Load("res://scenes/card_ui/card_ui.tscn");
	[Export] public CharacterStats Character_Stats;
	
	private void add_card(Card card)
	{
		CardUI _new_card_UI = (CardUI)CARD_UI_SCENE.Instantiate();
		AddChild(_new_card_UI);
		_new_card_UI.card = card;
		_new_card_UI.parent = this;
		_new_card_UI.Char_stats = Character_Stats;
	}
	
	private void discard_card(CardUI card)
	{
		RemoveChild(card);
		card.QueueFree();
	}

	private void disable_hand()
	{
		
	}
}
