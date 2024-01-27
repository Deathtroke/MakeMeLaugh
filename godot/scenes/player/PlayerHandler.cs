using Godot;
using System;

public partial class PlayerHandler : Node
{

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
		reshuffle_deck_from_discard();
		hand.add_card(_characterStats._draw_pile.drawcard());
		reshuffle_deck_from_discard();
		
	}

	private void reshuffle_deck_from_discard()
	{
		if (!_characterStats._draw_pile.empty()) return;
		while (!_characterStats._discard.empty())
		{
			_characterStats._draw_pile.addcard(_characterStats._discard.drawcard());
		}
		_characterStats._draw_pile.shuffle();
	}
	public delegate void DiscardFinishedHandler();
	public event DiscardFinishedHandler DiscardFinished;
	public void _end_turn()
	{
		GD.Print("[PlayerHandler] end_turn");
		discard_cards();
		DiscardFinished?.Invoke();
	}

	private void discard_cards()
	{
		foreach (var card_ui in hand.GetChildren())
		{
			GD.Print("Type:" + card_ui.GetType().Name);
		}

		foreach (var card_ui in hand.GetChildren())
		{
			
			if (card_ui.GetType().Name == "CardUI")
			{
				GD.Print("[PlayerHandler] discard_cards: "+((CardUI)card_ui).card + " to " + _characterStats._discard);

				_characterStats._discard.addcard(((CardUI)card_ui).card);
				hand.discard_card((CardUI)card_ui);
			}
			else
			{
				GD.Print(card_ui + " is an Imposter!");
				card_ui.QueueFree();
			}
		}
		
	}
}
