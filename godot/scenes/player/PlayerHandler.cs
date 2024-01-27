using Godot;
using System;

public partial class PlayerHandler : Node
{
	const float HAND_DRAW_INTERVAL = 0.25f;

	[Export] public Hand hand;
	private CharacterStats _characterStats;

	public void start_battle(CharacterStats character)
	{
		GD.Print(character._deck.Cards);
		_characterStats = character;
		character._draw_pile = character._deck.Duplicate();
		character._draw_pile.shuffle();
		character._discard = new CardPile();
		start_turn();

	}

	private void start_turn()
	{
		_characterStats.Block = 0;
		_characterStats.reset_ap();
		draw_cards(_characterStats.Draw_per_turn);
	}

	private void draw_cards(int amount)
	{
		for(int i = 0; i<amount; i++)
		{
			draw_card();
		}
		
	}

	private void draw_card()
	{
		hand.add_card(_characterStats._draw_pile.drawcard());
		
	}
}
