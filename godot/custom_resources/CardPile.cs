using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

[GlobalClass]
public partial class CardPile : Resource
{
    // Define a delegate that describes the signature of the methods that can respond to the event
    public delegate void CardPileSizeChangedHandler(int cardsamount);
    
    // Define an event based on that delegate
    public event CardPileSizeChangedHandler CardPileSizeChanged;
    
    private void notify(int cardsamount)
    {
        // Check if there are any Subscribers
        if (CardPileSizeChanged != null)
        {
            // Call the Event 
            CardPileSizeChanged(cardsamount);
        }
    }

    [Export] public Godot.Collections.Array<Card> Cards;
    
    private bool empty()
    {
        return Cards.Count == 0;
    }

    private Card drawcard()
    {
        if (Cards.Count == 0)
        {
            throw new InvalidOperationException("Cannot pop from an empty list.");
        }

        var top = Cards[0];
        Cards.RemoveAt(0);
        CardPileSizeChanged?.Invoke(Cards.Count);
        return top;
    }
    
    private void addcard(Card card)
    {
        Cards.Insert(0, card);
        CardPileSizeChanged?.Invoke(Cards.Count);
    }
    
    private void shuffle()
    {
        Cards.Shuffle();
    }
    
    private void clear()
    {
        Cards.Clear();
        CardPileSizeChanged?.Invoke(Cards.Count);
    }

    private String _to_string()
    {
        var _card_strings = Cards.Select((t, i) => $"{i + 1}: {t.id}").ToList();
        return String.Join("\n", _card_strings);
    }
    
    public CardPile Duplicate()
    {
        var instance = new CardPile();
        instance.Cards = Cards;
        return instance;
    }
}