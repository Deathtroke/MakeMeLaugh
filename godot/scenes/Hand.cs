using Godot;
using System;

public partial class Hand : HBoxContainer
{
	private static PackedScene CARD_UI_SCENE = (PackedScene)ResourceLoader.Load("res://scenes/card_ui/card_ui.tscn");
	[Export] public CharacterStats Character_Stats;
	
	[Export] public StyleBox default_style;
	[Export] public StyleBox hover_style;
	[Export] public StyleBox drag_style;
	public void add_card(Card card)
	{
		CardUI _new_card_UI = (CardUI)CARD_UI_SCENE.Instantiate();
		_new_card_UI.card = card;
		_new_card_UI.parent = this;
		_new_card_UI.Char_stats = Character_Stats;
		_new_card_UI.default_style = default_style;
		_new_card_UI.hover_style = hover_style;
		_new_card_UI.drag_style = drag_style;
		AddChild(_new_card_UI);
	}

	public void discard_card(CardUI card)
	{
		RemoveChild(card);
		card.QueueFree();
	}

	private void disable_hand()
	{
		
	}
}
