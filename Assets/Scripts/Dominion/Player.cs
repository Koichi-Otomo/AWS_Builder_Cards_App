using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dominion
{
    public class Player
    {
    public string Name { get; private set; }
    public Deck Deck { get; private set; } = new Deck();
    public List<Card> Hand { get; private set; } = new List<Card>();
    public List<Card> InPlay { get; private set; } = new List<Card>();

    public int Actions { get; set; }
    public int Buys { get; set; }
    public int Coins { get; set; }

    public Player(string name)
    {
        Name = name;
    }

    public void Draw(int n)
    {
        for (int i = 0; i < n; i++)
        {
            var c = Deck.DrawTop();
            if (c != null) Hand.Add(c);
        }
    }

    public void SetupStartingDeck()
    {
        for (int i = 0; i < 7; i++) Deck.AddToDraw(BasicCards.Copper());
        for (int i = 0; i < 3; i++) Deck.AddToDraw(BasicCards.Estate());
        Deck.Shuffle();
        Draw(5);
    }

    public void PlayAllTreasures()
    {
        var treasures = Hand.Where(h => h.Type == CardType.Treasure).ToList();
        foreach (var t in treasures)
        {
            Coins += t.TreasureValue();
            InPlay.Add(t);
            Hand.Remove(t);
            Debug.Log($"{Name} played treasure {t.Name} -> +{t.TreasureValue()} coins");
        }
    }

    // Play a specific card from hand (used by UI)
    public bool PlayCard(Card card, GameManager game)
    {
        if (card == null || !Hand.Contains(card)) return false;
        if (card.Type == CardType.Action)
        {
            if (Actions <= 0)
            {
                Debug.Log($"{Name} has no actions to play {card.Name}");
                return false;
            }
            Actions -= 1;
            Hand.Remove(card);
            InPlay.Add(card);
            Debug.Log($"{Name} played action {card.Name}");
            card.OnPlay(this, game);
            return true;
        }
        else if (card.Type == CardType.Treasure)
        {
            Hand.Remove(card);
            InPlay.Add(card);
            Coins += card.TreasureValue();
            Debug.Log($"{Name} played treasure {card.Name} -> +{card.TreasureValue()} coins");
            return true;
        }
        else
        {
            Debug.Log($"{card.Name} is not playable");
            return false;
        }
    }

    public bool PlayFirstAction(GameManager game)
    {
        if (Actions <= 0) return false;
        var action = Hand.FirstOrDefault(h => h.Type == CardType.Action);
        if (action == null) return false;
        Actions -= 1;
        Hand.Remove(action);
        InPlay.Add(action);
        Debug.Log($"{Name} played action {action.Name}");
        action.OnPlay(this, game);
        return true;
    }

    public void Cleanup()
    {
        foreach (var c in Hand) Deck.AddToDiscard(c);
        Hand.Clear();
        foreach (var c in InPlay) Deck.AddToDiscard(c);
        InPlay.Clear();
        Coins = 0; Actions = 0; Buys = 0;
        Draw(5);
    }

    public int TotalVictoryPoints()
    {
        // naive: count victory cards in deck/discard/hand
        return 0; // for now
    }
}

}
