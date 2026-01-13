using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AWSBuilderCards
{
    public class Deck
    {
        List<Card> draw = new List<Card>();
        List<Card> discard = new List<Card>();

        System.Random rng = new System.Random();

        public Deck() { }

        public void AddToDraw(Card c) { draw.Add(c); }
        public void AddToDiscard(Card c) { discard.Add(c); }

        public void Shuffle()
        {
            var list = draw.Concat(discard).ToList();
            draw.Clear();
            discard.Clear();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                int j = rng.Next(i + 1);
                var tmp = list[i]; list[i] = list[j]; list[j] = tmp;
            }
            draw.AddRange(list);
        }

        public Card DrawTop()
        {
            if (draw.Count == 0)
            {
                if (discard.Count == 0) return null;
                draw.AddRange(discard);
                discard.Clear();
                Shuffle();
            }
            var c = draw[0];
            draw.RemoveAt(0);
            return c;
        }

        public void Gain(Card c)
        {
            discard.Add(c);
        }

        public int CountDraw() => draw.Count + discard.Count;
        
        public List<Card> AllCards()
        {
            return draw.Concat(discard).ToList();
        }
    }
}