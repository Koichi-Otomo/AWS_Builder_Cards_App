using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AWSBuilderCards
{
    public class Player
    {
        public string Name { get; private set; }
        public Deck Deck { get; private set; } = new Deck();
        public List<Card> Hand { get; private set; } = new List<Card>();
        public List<Card> InPlay { get; private set; } = new List<Card>();

        public int Actions { get; set; }
        public int Buys { get; set; }
        public int Credits { get; set; } // 2nd Edition: 統一されたクレジット

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
            // 10 starter cards of same color
            for (int i = 0; i < 3; i++) Deck.AddToDraw(BasicCards.BareMetalHost());
            for (int i = 0; i < 3; i++) Deck.AddToDraw(BasicCards.Networking());
            for (int i = 0; i < 2; i++) Deck.AddToDraw(BasicCards.VirtualMachine());
            for (int i = 0; i < 2; i++) Deck.AddToDraw(BasicCards.DatabaseServer());
            Deck.Shuffle();
            Draw(5);
        }

        public void PlayAllStarters()
        {
            var starters = Hand.Where(h => h.Type == CardType.Starter).ToList();
            foreach (var s in starters)
            {
                Credits += s.CreditValue();
                InPlay.Add(s);
                Hand.Remove(s);
                Debug.Log($"{Name} played starter {s.Name} -> +{s.CreditValue()} credits");
            }
        }

        public bool PlayCard(Card card, GameManager game)
        {
            if (card == null || !Hand.Contains(card)) return false;
            
            if (card.Type == CardType.Builder)
            {
                if (Actions <= 0)
                {
                    Debug.Log($"{Name} has no actions to play {card.Name}");
                    return false;
                }
                Actions -= 1;
                Hand.Remove(card);
                InPlay.Add(card);
                Credits += card.CreditValue();
                Debug.Log($"{Name} played builder {card.Name} -> +{card.CreditValue()} credits");
                card.OnPlay(this, game);
                return true;
            }
            else if (card.Type == CardType.Starter)
            {
                Hand.Remove(card);
                InPlay.Add(card);
                Credits += card.CreditValue();
                Debug.Log($"{Name} played starter {card.Name} -> +{card.CreditValue()} credits");
                return true;
            }
            else
            {
                Debug.Log($"{card.Name} is not playable");
                return false;
            }
        }

        public bool PlayFirstBuilder(GameManager game)
        {
            if (Actions <= 0) return false;
            var builder = Hand.FirstOrDefault(h => h.Type == CardType.Builder);
            if (builder == null) return false;
            
            Actions -= 1;
            Hand.Remove(builder);
            InPlay.Add(builder);
            Credits += builder.CreditValue();
            Debug.Log($"{Name} played builder {builder.Name} -> +{builder.CreditValue()} credits");
            builder.OnPlay(this, game);
            return true;
        }

        public void Cleanup()
        {
            foreach (var c in Hand) Deck.AddToDiscard(c);
            Hand.Clear();
            foreach (var c in InPlay) Deck.AddToDiscard(c);
            InPlay.Clear();
            Credits = 0; Actions = 0; Buys = 0;
            Draw(5);
        }

        public int TotalVictoryPoints()
        {
            int points = 0;
            // Count all Well-Architected cards in deck, discard, and hand
            foreach (var card in Deck.AllCards())
            {
                if (card.Type == CardType.WellArchitected)
                    points += card.VictoryPoints(this);
            }
            foreach (var card in Hand)
            {
                if (card.Type == CardType.WellArchitected)
                    points += card.VictoryPoints(this);
            }
            return points;
        }
    }
}