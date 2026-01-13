using System.Collections.Generic;
using UnityEngine;

namespace Dominion
{
    public abstract class Card
    {
        public string Name { get; protected set; }
        public int Cost { get; protected set; }
        public CardType Type { get; protected set; }

        public Card(string name, int cost, CardType type)
        {
            Name = name;
            Cost = cost;
            Type = type;
        }

        public virtual void OnPlay(Player player, GameManager game) { }
        public virtual int VictoryPoints(Player player) { return 0; }
        public virtual int TreasureValue() { return 0; }
    }

    // ----- Concrete cards -----
    public class TreasureCard : Card
    {
        public int Value { get; private set; }
        public TreasureCard(string name, int cost, int value) : base(name, cost, CardType.Treasure)
        {
            Value = value;
        }
        public override int TreasureValue() { return Value; }
    }

    public class VictoryCard : Card
    {
        int points;
        public VictoryCard(string name, int cost, int points) : base(name, cost, CardType.Victory)
        {
            this.points = points;
        }
        public override int VictoryPoints(Player player) { return points; }
    }

    public class SmithyCard : Card
    {
        public SmithyCard() : base("Smithy", 4, CardType.Action) { }
        public override void OnPlay(Player player, GameManager game)
        {
            for (int i = 0; i < 3; i++) player.Draw(1);
        }
    }

    public class VillageCard : Card
    {
        public VillageCard() : base("Village", 3, CardType.Action) { }
        public override void OnPlay(Player player, GameManager game)
        {
            player.Draw(1);
            player.Actions += 2;
        }
    }

    public static class BasicCards
    {
        public static Card Copper() => new TreasureCard("Copper", 0, 1);
        public static Card Silver() => new TreasureCard("Silver", 3, 2);
        public static Card Gold() => new TreasureCard("Gold", 6, 3);

        public static Card Estate() => new VictoryCard("Estate", 2, 1);
        public static Card Duchy() => new VictoryCard("Duchy", 5, 3);
        public static Card Province() => new VictoryCard("Province", 8, 6);

        public static Card Smithy() => new SmithyCard();
        public static Card Village() => new VillageCard();
    }
}
