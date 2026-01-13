using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dominion
{
    public class GameManager : MonoBehaviour
    {
        public Supply supply = new Supply();
        public List<Player> players = new List<Player>();
        int currentPlayer = 0;
        // expose current player index for UI
        public int currentPlayerIndex { get { return currentPlayer; } }
    void Start()
    {
        SetupSupply();
        SetupPlayers();
        StartCoroutine(RunDemoGame());
    }

    void SetupSupply()
    {
        supply.AddPile("Copper", BasicCards.Copper(), 60);
        supply.AddPile("Silver", BasicCards.Silver(), 40);
        supply.AddPile("Gold", BasicCards.Gold(), 30);

        supply.AddPile("Estate", BasicCards.Estate(), 12);
        supply.AddPile("Duchy", BasicCards.Duchy(), 12);
        supply.AddPile("Province", BasicCards.Province(), 12);

        supply.AddPile("Smithy", BasicCards.Smithy(), 10);
        supply.AddPile("Village", BasicCards.Village(), 10);
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
                p.Actions = 1; p.Buys = 1; p.Coins = 0;
                // Action phase: play first action if any
                p.PlayFirstAction(this);
                // Treasure phase
                p.PlayAllTreasures();
                // Buy phase (simple): buy Province if enough, else Silver if >=3
                if (p.Coins >= 8 && supply.Count("Province") > 0)
                {
                    var card = supply.Take("Province");
                    p.Deck.Gain(card);
                    Debug.Log($"{p.Name} bought Province");
                }
                else if (p.Coins >= 3 && supply.Count("Silver") > 0)
                {
                    var card = supply.Take("Silver");
                    p.Deck.Gain(card);
                    Debug.Log($"{p.Name} bought Silver");
                }
                // Cleanup
                p.Cleanup();
                yield return new WaitForSeconds(0.2f);
            }
        }
        Debug.Log("Demo finished. Check console for logs.");
    }
}

}
