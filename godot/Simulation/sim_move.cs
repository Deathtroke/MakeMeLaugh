using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

	async public void Simulate()
	{
		while(true)
		{
			if (CheckIfViablePlay())
			{
				PlayCard(ChooseCard(SimMode.ballanced));
				await Task.Delay(1000);
			}
			else
			{
				ingame_scene gamescene = GetNode<ingame_scene>("ingame_scene");
				await gamescene.OnEndTurn();
			}
			
		}
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
				
				card.play(card.get_tagets(new Godot.Collections.Array<Godot.Node> {target}), Player.Stats);
			}
		}
		else
		{
			card.play(card.get_tagets(new Godot.Collections.Array<Godot.Node>{}), Player.Stats);
		}
	}

	public bool CheckIfViablePlay()
	{
		Hand hand = Player._playerHandler.hand;
		foreach (var cardNode in hand.GetChildren())
		{
			if (cardNode is CardUI cardUI)
			{
				if (cardUI.card.Ap_cost <= Player._stats.Ap)
					return true;
			}
		}

		return false;
	}
}
