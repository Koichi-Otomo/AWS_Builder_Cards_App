using System.Collections.Generic;
using UnityEngine;

namespace AWSBuilderCards
{
    /// <summary>
    /// ゲームエフェクトの基本抽象クラス
    /// IExecutable インターフェースを実装
    /// </summary>
    public abstract class Effect : IExecutable
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public Effect(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public abstract void Execute(Player player, GameManager game);
    }

    // 具体的なエフェクト実装
    public class DrawCardsEffect : Effect
    {
        int cardCount;
        
        public DrawCardsEffect(int count) : base("Draw Cards", $"Draw {count} cards")
        {
            cardCount = count;
        }

        public override void Execute(Player player, GameManager game)
        {
            player.Draw(cardCount);
            Debug.Log($"{player.Name} drew {cardCount} cards");
        }
    }

    public class GainCreditsEffect : Effect
    {
        int creditAmount;
        
        public GainCreditsEffect(int amount) : base("Gain Credits", $"Gain {amount} credits")
        {
            creditAmount = amount;
        }

        public override void Execute(Player player, GameManager game)
        {
            player.Credits += creditAmount;
            Debug.Log($"{player.Name} gained {creditAmount} credits");
        }
    }

    public class GainActionsEffect : Effect
    {
        int actionCount;
        
        public GainActionsEffect(int count) : base("Gain Actions", $"Gain {count} actions")
        {
            actionCount = count;
        }

        public override void Execute(Player player, GameManager game)
        {
            player.Actions += actionCount;
            Debug.Log($"{player.Name} gained {actionCount} actions");
        }
    }

    public class GainBuysEffect : Effect
    {
        int buyCount;
        
        public GainBuysEffect(int count) : base("Gain Buys", $"Gain {count} buys")
        {
            buyCount = count;
        }

        public override void Execute(Player player, GameManager game)
        {
            player.Buys += buyCount;
            Debug.Log($"{player.Name} gained {buyCount} buys");
        }
    }
}