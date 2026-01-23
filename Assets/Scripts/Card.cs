using System.Collections.Generic;
using UnityEngine;

namespace AWSBuilderCards
{
    /// <summary>
    /// ゲームカードの基本抽象クラス
    /// IValuable: 価値評価機能（VP、クレジット）
    /// IPlayable: カード実行機能（OnPlay）
    /// IUIDisplayable: UI表示機能
    /// IEffectContainer: エフェクト管理機能
    /// </summary>
    public abstract class Card : IValuable, IPlayable, IUIDisplayable, IEffectContainer
    {
        public string Name { get; protected set; }
        public int Cost { get; protected set; } // 2nd Edition: 統一されたコスト
        public CardType Type { get; protected set; }
        public string ServiceCategory { get; protected set; }
        public string Description { get; protected set; }
        public string DocumentationUrl { get; protected set; }
        public List<Effect> Effects { get; protected set; } = new List<Effect>(); // エフェクトリスト

        public Card(string name, int cost, CardType type, string serviceCategory = "", string description = "", string documentationUrl = "")
        {
            Name = name;
            Cost = cost;
            Type = type;
            ServiceCategory = serviceCategory;
            Description = description;
            DocumentationUrl = documentationUrl;
        }

        public virtual void OnPlay(Player player, GameManager game) 
        {
            // エフェクトを実行
            ExecuteAllEffects(player, game);
        }

        public virtual int VictoryPoints(Player player) { return 0; }
        public virtual int CreditValue() { return 0; }

        // IUIDisplayable 実装
        public virtual string GetDisplayName() => $"{Name} ({Cost})";
        public virtual string GetDisplayDescription() => Description;
        public virtual CardType GetCardType() => Type;

        // IEffectContainer 実装
        public void AddEffect(Effect effect)
        {
            if (effect != null) Effects.Add(effect);
        }

        public void ExecuteAllEffects(Player player, GameManager game)
        {
            foreach (var effect in Effects)
            {
                effect.Execute(player, game);
            }
        }
    }

    // ----- Concrete cards -----
    public class StarterCard : Card
    {
        public int Value { get; private set; }
        public StarterCard(string name, int value, string serviceCategory) : base(name, 0, CardType.Starter, serviceCategory)
        {
            Value = value;
        }
        public override int CreditValue() { return Value; }
    }

    public class WellArchitectedCard : Card
    {
        int points;
        public WellArchitectedCard(string name, int cost, int points) : base(name, cost, CardType.WellArchitected)
        {
            this.points = points;
        }
        public override int VictoryPoints(Player player) { return points; }
    }

    public class BuilderCard : Card
    {
        public BuilderCard(string name, int cost, string serviceCategory, string description = "", string documentationUrl = "") 
            : base(name, cost, CardType.Builder, serviceCategory, description, documentationUrl) { }
        
        public override int CreditValue() { return 1; } // Base credit value
    }

    // AWS Service Cards
    public class EC2Card : BuilderCard
    {
        public EC2Card() : base("Amazon EC2", 0, "Computing", "Scalable virtual servers in the cloud", "https://aws.amazon.com/ec2/") { }
        public override int CreditValue() { return 2; }
    }

    public class LambdaCard : BuilderCard
    {
        public LambdaCard() : base("AWS Lambda", 3, "Computing", "Run code without thinking about servers", "https://aws.amazon.com/lambda/") 
        {
            Effects.Add(new DrawCardsEffect(1));
        }
    }

    public class EC2AutoScalingCard : BuilderCard
    {
        public EC2AutoScalingCard() : base("Amazon EC2 Auto Scaling", 3, "Computing", "Automatically scale EC2 instances", "https://aws.amazon.com/autoscaling/") { }
        public override int CreditValue() { return 2; }
    }

    public static class BasicCards
    {
        // Starter Cards
        public static Card BareMetalHost() => new StarterCard("Bare Metal Host", 0, "OnPremises");
        public static Card Networking() => new StarterCard("Networking", 0, "OnPremises");
        public static Card VirtualMachine() => new StarterCard("Virtual Machine", 0, "OnPremises");
        public static Card DatabaseServer() => new StarterCard("Database Server", 0, "OnPremises");

        // Well-Architected Cards
        public static Card WellArchitected1() => new WellArchitectedCard("Well-Architected 1pt", 3, 1);
        public static Card WellArchitected3() => new WellArchitectedCard("Well-Architected 3pt", 8, 3);

        // Builder Cards
        public static Card EC2() => new EC2Card();
        public static Card Lambda() => new LambdaCard();
        public static Card EC2AutoScaling() => new EC2AutoScalingCard();
        
        // 新しいAWSサービスカード（エフェクト付き）
        public static Card S3() => new S3Card();
        public static Card RDS() => new RDSCard();
    }
    
    public class S3Card : BuilderCard
    {
        public S3Card() : base("Amazon S3", 0, "Storage", "Object storage built to store and retrieve any amount of data", "https://aws.amazon.com/s3/") { }
        public override int CreditValue() { return 2; }
    }
    
    public class RDSCard : BuilderCard
    {
        public RDSCard() : base("Amazon RDS", 0, "Database", "Managed relational database service", "https://aws.amazon.com/rds/") { }
        public override int CreditValue() { return 2; }
    }
}