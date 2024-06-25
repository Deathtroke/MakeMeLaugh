using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class sim_move : Node
{
	public enum SimMode
	{
		offensive,
		defensive,
		ballanced
	}

	private player Player;
    private EnemyHandler enemyHandler;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = GetNode<player>("player");
        enemyHandler = GetNode<EnemyHandler>("EnemyHandler");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public Card ChooseCard(SimMode simMode)
	{
		Hand hand = Player._playerHandler.hand;
		List<Card> possibleChoices = getPossibleChoices(simMode);

		if (!possibleChoices.Any())
		{
			if (hand.GetChildren().Any())
			{
				possibleChoices = getPossibleChoices(SimMode.ballanced);
			}
			else
			{
				return null;
			}
		}

		Card pick = possibleChoices.First();
		
		if (simMode == SimMode.ballanced)
		{
			int expectedDamage = 0;
			foreach (enemy e in enemyHandler.GetChildren())
            {
	            if (e.curren_action.damage != null)
	            {
		            expectedDamage += e.curren_action.damage;
	            }

	            if (expectedDamage >= Player.Stats.Block)
	            {
		            return ChooseCard(SimMode.defensive);
	            }
	            else
	            {
		            return ChooseCard(SimMode.offensive);
	            }
            }
		}
		else
		{
			int bestEffect = 0;
			
			foreach (var card in possibleChoices)
			{
				if (bestEffect < card.Effect_Amount)
				{
					bestEffect = card.Effect_Amount;
					pick = card;
				}
			}
		}
		
		return pick;
	}

	public List<Card> getPossibleChoices(SimMode simMode)
	{
		Hand hand = Player._playerHandler.hand;
		List<Card> possibleChoices = new List<Card>();
		foreach(var cardNode in hand.GetChildren())
		{
			if (cardNode is CardUI cardUI)
			{
				Card card = cardUI.card;
				if (card.Ap_cost < Player._stats.Ap) continue;
				
				
				switch (simMode)
				{
					case SimMode.offensive:
						if (card.Effect == Card.EffectType.Atk)
						{
							possibleChoices.Add(card);	
						}
						break;
					case SimMode.defensive:
						if (card.Effect == Card.EffectType.Def)
						{
							possibleChoices.Add(card);	
						}
						break;
					case SimMode.ballanced:
						possibleChoices.Add(card);	
						break;
				}
			}
		}

		return possibleChoices;
	}

	public void PlayCard(Card card)
	{
		if (card.Effect == Card.EffectType.Atk)
		{
			if (enemyHandler.GetChildren().Any())
			{
				Node target = enemyHandler.GetChildren().First();
				int minHealth = 9999;
				foreach (Node e in enemyHandler.GetChildren())
				{
					if ((e as enemy).Stats.Health < minHealth)
					{
						minHealth = (e as enemy).Stats.Health;
						target = e;
					}
				}
				
				card.play(new Godot.Collections.Array<Godot.Node> {target}, Player.Stats);
			}
		}
	}
}
