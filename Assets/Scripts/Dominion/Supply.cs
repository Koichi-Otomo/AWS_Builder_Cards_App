using System.Collections.Generic;
using UnityEngine;

namespace Dominion
{
    public class Supply
    {
        Dictionary<string, (Card prototype, int count)> piles = new Dictionary<string, (Card, int)>();

        public void AddPile(string name, Card proto, int count)
        {
            piles[name] = (proto, count);
        }

        public Card Take(string name)
        {
            if (!piles.ContainsKey(name)) return null;
            var entry = piles[name];
            if (entry.count <= 0) return null;
            entry.count -= 1;
            piles[name] = entry;
            // create a new instance: simple factory by name
            switch (name)
            {
                case "Copper": return BasicCards.Copper();
                case "Silver": return BasicCards.Silver();
                case "Gold": return BasicCards.Gold();
                case "Estate": return BasicCards.Estate();
                case "Duchy": return BasicCards.Duchy();
                case "Province": return BasicCards.Province();
                case "Smithy": return BasicCards.Smithy();
                case "Village": return BasicCards.Village();
                default: return null;
            }
        }

        public int Count(string name) => piles.ContainsKey(name) ? piles[name].count : 0;
    }

}
