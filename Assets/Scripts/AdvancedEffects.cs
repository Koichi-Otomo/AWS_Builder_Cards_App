using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AWSBuilderCards
{
    // より高度なエフェクト実装
    public class ConditionalEffect : Effect
    {
        Effect conditionalEffect;
        System.Func<Player, GameManager, bool> condition;

        public ConditionalEffect(string name, string description, Effect effect, System.Func<Player, GameManager, bool> condition) 
            : base(name, description)
        {
            conditionalEffect = effect;
            this.condition = condition;
        }

        public override void Execute(Player player, GameManager game)
        {
            if (condition(player, game))
            {
                conditionalEffect.Execute(player, game);
            }
        }
    }

    public class MultipleEffect : Effect
    {
        List<Effect> effects;

        public MultipleEffect(string name, string description, params Effect[] effects) : base(name, description)
        {
            this.effects = new List<Effect>(effects);
        }

        public override void Execute(Player player, GameManager game)
        {
            foreach (var effect in effects)
            {
                effect.Execute(player, game);
            }
        }
    }

    public class ScalingEffect : Effect
    {
        Effect baseEffect;
        System.Func<Player, GameManager, int> scalingFunction;

        public ScalingEffect(string name, string description, Effect baseEffect, System.Func<Player, GameManager, int> scalingFunction) 
            : base(name, description)
        {
            this.baseEffect = baseEffect;
            this.scalingFunction = scalingFunction;
        }

        public override void Execute(Player player, GameManager game)
        {
            int multiplier = scalingFunction(player, game);
            for (int i = 0; i < multiplier; i++)
            {
                baseEffect.Execute(player, game);
            }
        }
    }

    // AWS特有のエフェクト
    public class CloudMigrationEffect : Effect
    {
        public CloudMigrationEffect() : base("Cloud Migration", "Retire a Starter card to gain 2 credits")
        {
        }

        public override void Execute(Player player, GameManager game)
        {
            var starterCards = player.Hand.Where(c => c.Type == CardType.Starter).ToList();
            if (starterCards.Count > 0)
            {
                var cardToRetire = starterCards.First();
                player.Hand.Remove(cardToRetire);
                player.Credits += 2;
                Debug.Log($"{player.Name} retired {cardToRetire.Name} and gained 2 credits");
            }
        }
    }

    public class WellArchitectedBonusEffect : Effect
    {
        public WellArchitectedBonusEffect() : base("Well-Architected Bonus", "Gain 1 credit for each Well-Architected card you own")
        {
        }

        public override void Execute(Player player, GameManager game)
        {
            int wellArchitectedCount = 0;
            
            // 手札をチェック
            wellArchitectedCount += player.Hand.Count(c => c.Type == CardType.WellArchitected);
            
            // デッキをチェック
            wellArchitectedCount += player.Deck.AllCards().Count(c => c.Type == CardType.WellArchitected);

            if (wellArchitectedCount > 0)
            {
                player.Credits += wellArchitectedCount;
                Debug.Log($"{player.Name} gained {wellArchitectedCount} credits from Well-Architected bonus");
            }
        }
    }
}