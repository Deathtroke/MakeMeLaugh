using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class card_pile : Resource
{
    // Define a delegate that describes the signature of the methods that can respond to the event
    public delegate void CardPileSizeChangedHandler(int cardsamount);
    
    // Define an event based on that delegate
    public event CardPileSizeChangedHandler CardPileSizeChanged;
    
    public void Notify(int cardsamount)
    {
        // Check if there are any Subscribers
        if (CardPileSizeChanged != null)
        {
            // Call the Event
            CardPileSizeChanged(cardsamount);
        }
    }

    private List<Card> cards = new List<Card>();
    
    private bool Empty()
    {
        return cards.Count == 0;
    }

    private Card DrawCard()
    {
        if (cards.Count == 0)
        {
            throw new InvalidOperationException("Cannot pop from an empty list.");
        }

        var top = cards[0];
        cards.RemoveAt(0);
        CardPileSizeChanged?.Invoke(cards.Count);
        return top;
    }
    
    private void AddCard(Card card)
    {
        cards.Insert(0, card);
        CardPileSizeChanged?.Invoke(cards.Count);
    }
    
    private void Shuffle()
    {
        var random = new Random();
        cards = cards.OrderBy(x => random.Next()).ToList();
    }
    
    private void Clear()
    {
        cards.Clear();
        CardPileSizeChanged?.Invoke(cards.Count);
    }

    private String _to_string()
    {
        var _card_strings = cards.Select((t, i) => $"{i + 1}: {t.id}").ToList();
        return String.Join("\n", _card_strings);
    }
}
