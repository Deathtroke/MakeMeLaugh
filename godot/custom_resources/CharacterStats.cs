using Godot;
using System;


[GlobalClass]
public partial class CharacterStats : Stats
{
    [Export] CardPile Starting_deck;
    [Export] int Draw_per_turn = 5;
    [Export] int Max_ap = 2;

    private int _ap;
    private CardPile _deck;
    private CardPile _discard;
    private CardPile _draw_pile;
    
    public int Ap
    {
        get { return _ap; }
        set { setAp(value); }
    }
    
    private void setAp(int value)
    {
        _ap = value;
        On_stats_changed();
    }
    
    private void reset_ap()
    {
        Ap = Max_ap;
    }
    
    private bool can_play_card(Card card)
    {
        return Ap >= card.Ap_cost;
    }
    
    private Resource create_instance()
    {
        var instance = new CharacterStats();
        instance.MaxHp = MaxHp;
        instance.Art = Art;
        instance.Health = MaxHp;
        instance.Block = 0;
        instance.Starting_deck = Starting_deck;
        instance.Draw_per_turn = Draw_per_turn;
        instance.Max_ap = Max_ap;
        instance.reset_ap();
        instance._deck = instance.Starting_deck.Duplicate();
        instance._discard = new CardPile();
        instance._draw_pile = new CardPile();
        return instance;
    }
}
