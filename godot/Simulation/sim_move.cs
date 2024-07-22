using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

public partial class sim_move : Node
{
	public enum SimMode
	{
		offensive,
		defensive,
		any
	}

	public player Player;
	public EnemyHandler enemyHandler;
	public ingame_scene Gamescene;
	public battle_ui BattleUi;
	public bool isSimulating = false;
	
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
		
		while(isSimulating)
		{
			await SimulateStep();
		}
	}
	
	async public Task SimulateStep()
	{
		if (CheckIfViablePlay())
		{
			var card = ChooseCard(SimMode.defensive);
			if (card != null)
			{
				PlayCard(card);
			}
			await Task.Delay(1000);
		}
		else
		{
			await Gamescene.OnEndTurn();
		}
	}
	
	public CardUI ChooseCard(SimMode simMode)
	{
		Hand hand = Player._playerHandler.hand;
		List<CardUI> possibleChoices = getPossibleChoices(simMode);

		if (!possibleChoices.Any())
		{
			if (CheckIfViablePlay())
			{
				if (simMode == SimMode.defensive)
				{
					return ChooseCard(SimMode.offensive);
				}
				else
				{
					return ChooseCard(SimMode.any);
				}
			}
			else
			{
				return null;
			}
		}

		CardUI pick = possibleChoices.First();

		//check if more block is needed otherwise switch to offensive
		if (simMode == SimMode.defensive)
		{
			int expectedDamage = 0;
			foreach (Node e in enemyHandler.GetChildren())
			{
				if (e.Name.ToString().Contains("Boarling"))
				{
					expectedDamage += 2;
				}
				else if (e.Name.ToString().Contains("Boar"))
				{
					expectedDamage += 6;
				}
				else if (e.Name.ToString().Contains("Unicorn"))
				{
					expectedDamage += 8;
				}
				else if (e.Name.ToString().Contains("Rat"))
				{
					expectedDamage += 4;
				}
				else if (e.Name.ToString().Contains("Dragon"))
				{
					expectedDamage += 30;
				}
			}

			if (expectedDamage <= Player.Stats.Block)
			{
				return ChooseCard(SimMode.offensive);
			}

		}

		int bestEffect = 0;
		
		foreach (var card in possibleChoices)
		{
			var effectPerAP = 0;

			if (card.card.Ap_cost != 0)
			{
				effectPerAP = card.card.Effect_Amount / card.card.Ap_cost;
			}
			else
			{
				effectPerAP = card.card.Effect_Amount * 10;
			}

			if (card.card.Target == Card.TargetType.AOE)
			{
				effectPerAP *= enemyHandler.GetChildren().Count;
			}
			
			if (bestEffect < effectPerAP)
			{
				bestEffect = effectPerAP;
				pick = card;
			}
		}

		
		return pick;
	}

	public List<CardUI> getPossibleChoices(SimMode simMode)
	{
		Hand hand = Player._playerHandler.hand;
		List<CardUI> possibleChoices = new List<CardUI>();
		foreach(var cardNode in hand.GetChildren())
		{
			if (cardNode is CardUI cardUI)
			{
				Card card = cardUI.card;
				if (card.Ap_cost > Player._stats.Ap) continue;
				
				switch (simMode)
				{
					case SimMode.offensive:
						if (card.Effect == Card.EffectType.Atk)
						{
							possibleChoices.Add(cardUI);	
						}
						break;
					case SimMode.defensive:
						if (card.Effect == Card.EffectType.Def)
						{
							possibleChoices.Add(cardUI);	
						}
						break;
					case SimMode.any:
						possibleChoices.Add(cardUI);	
						break;
				}
			}
		}
		
		GD.Print(possibleChoices.Count);
		return possibleChoices;
	}

	public void PlayCard(CardUI card)
	{
		card.Char_stats = Player._stats;
		if (card.card.Effect == Card.EffectType.Atk)
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
				
				card.targets.Add(target);
				card.play();
			}
		}
		else
		{
			card.play();
		}
		GD.Print("ap" + Player._stats.Ap);
		Player._stats._discard.addcard(card.card);
		BattleUi._ap_ui.ap_Label.Text = Player._stats.Ap + "/" + Player._stats.Max_ap;
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
