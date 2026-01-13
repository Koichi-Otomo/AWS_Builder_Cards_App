using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AWSBuilderCards
{
    public class GameManager : MonoBehaviour
    {
        public Supply supply = new Supply();
        public List<Player> players = new List<Player>();
        int currentPlayer = 0;
        public int currentPlayerIndex { get { return currentPlayer; } }
        
        void Start()
        {
            SetupSupply();
            SetupPlayers();
            StartCoroutine(RunDemoGame());
        }

        void SetupSupply()
        {
            // Well-Architected Cards
            supply.AddPile("WellArchitected1", BasicCards.WellArchitected1(), 12);
            supply.AddPile("WellArchitected3", BasicCards.WellArchitected3(), 12);

            // Builder Cards (コストアイコンなし)
            supply.AddPile("EC2", BasicCards.EC2(), 10);
            supply.AddPile("Lambda", BasicCards.Lambda(), 10);
            supply.AddPile("S3", BasicCards.S3(), 10);
            
            // Builder Cards (コストアイコンあり)
            supply.AddPile("EC2AutoScaling", BasicCards.EC2AutoScaling(), 10);
            supply.AddPile("RDS", BasicCards.RDS(), 10);
        }

        void SetupPlayers()
        {
            players.Add(new Player("Alice"));
            players.Add(new Player("Bob"));
            foreach (var p in players) p.SetupStartingDeck();
        }

        IEnumerator RunDemoGame()
        {
            for (int turn = 1; turn <= 6; turn++)
            {
                Debug.Log($"--- Turn {turn} ---");
                foreach (var p in players)
                {
                    Debug.Log($"{p.Name} start of turn");
                    p.Actions = 1; p.Buys = 1; p.Credits = 0;
                    
                    // Build phase: play first builder if any
                    p.PlayFirstBuilder(this);
                    
                    // Play all starters for credits
                    p.PlayAllStarters();
                    
                    // Buy phase: buy Well-Architected if enough credits
                    if (p.Credits >= 8 && supply.Count("WellArchitected3") > 0)
                    {
                        var card = supply.Take("WellArchitected3");
                        p.Deck.Gain(card);
                        Debug.Log($"{p.Name} bought Well-Architected 3pt");
                    }
                    else if (p.Credits >= 3 && supply.Count("WellArchitected1") > 0)
                    {
                        var card = supply.Take("WellArchitected1");
                        p.Deck.Gain(card);
                        Debug.Log($"{p.Name} bought Well-Architected 1pt");
                    }
                    else if (p.Credits >= 3 && supply.Count("Lambda") > 0)
                    {
                        var card = supply.Take("Lambda");
                        p.Deck.Gain(card);
                        Debug.Log($"{p.Name} bought Lambda");
                    }
                    
                    // Cleanup
                    p.Cleanup();
                    yield return new WaitForSeconds(0.2f);
                }
            }
            
            // Game end - check victory points
            Debug.Log("Demo finished. Victory Points:");
            foreach (var p in players)
            {
                Debug.Log($"{p.Name}: {p.TotalVictoryPoints()} points");
            }
        }
    }
}