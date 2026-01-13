using System.Collections.Generic;
using UnityEngine;

namespace AWSBuilderCards
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
            
            // Create new instance based on card name
            switch (name)
            {
                case "WellArchitected1": return BasicCards.WellArchitected1();
                case "WellArchitected3": return BasicCards.WellArchitected3();
                case "EC2": return BasicCards.EC2();
                case "Lambda": return BasicCards.Lambda();
                case "EC2AutoScaling": return BasicCards.EC2AutoScaling();
                case "S3": return BasicCards.S3();
                case "RDS": return BasicCards.RDS();
                default: return null;
            }
        }

        public int Count(string name) => piles.ContainsKey(name) ? piles[name].count : 0;
    }
}