using System.Collections.Generic;
using UnityEngine;

namespace AWSBuilderCards
{
    [System.Serializable]
    public class CardData
    {
        public string Name;
        public CardType Type;
        public int Cost; // 2nd Edition: 統一されたコスト
        public int CreditValue;
        public int VictoryPoints;
        public string ServiceCategory;
        public string Description;
        public string DocumentationUrl;
        public string EffectDescription; // エフェクトの説明
    }

    public static class CardLists
    {
        public static List<CardData> GetAllCards()
        {
            return new List<CardData>
            {
                // Starter Cards
                new CardData
                {
                    Name = "Bare Metal Host",
                    Type = CardType.Starter,
                    Cost = 0,
                    CreditValue = 0,
                    VictoryPoints = 0,
                    ServiceCategory = "OnPremises",
                    Description = "Pingは返ってくるが、サーバーがどこにあるかわからない。",
                    DocumentationUrl = ""
                },
                new CardData
                {
                    Name = "Networking",
                    Type = CardType.Starter,
                    Cost = 0,
                    CreditValue = 0,
                    VictoryPoints = 0,
                    ServiceCategory = "OnPremises",
                    Description = "まだ慌てる時間じゃない。ひとまずネットワークのせいにしよう。",
                    DocumentationUrl = ""
                },
                new CardData
                {
                    Name = "Virtual Machine",
                    Type = CardType.Starter,
                    Cost = 0,
                    CreditValue = 0,
                    VictoryPoints = 0,
                    ServiceCategory = "OnPremises",
                    Description = "アウトプットしないのは知的な便秘。",
                    EffectDescription = "Bare Metal Hostを既にデプロイしている場合、追加で1枚カードを引く。",
                    DocumentationUrl = ""
                },
                new CardData
                {
                    Name = "Database Server",
                    Type = CardType.Starter,
                    Cost = 0,
                    CreditValue = 0,
                    VictoryPoints = 0,
                    ServiceCategory = "OnPremises",
                    Description = "バックアップしてなかった？じゃあ、そのシステムは重要じゃなかったんだね",
                    DocumentationUrl = ""
                },

                // Well-Architected Cards
                new CardData
                {
                    Name = "Well-Architected 1pt",
                    Type = CardType.WellArchitected,
                    Cost = 3,
                    CreditValue = 0,
                    VictoryPoints = 1,
                    ServiceCategory = "WellArchitected",
                    Description = "Well-Architected Framework principles",
                    DocumentationUrl = "https://aws.amazon.com/architecture/well-architected/"
                },
                new CardData
                {
                    Name = "Well-Architected 3pt",
                    Type = CardType.WellArchitected,
                    Cost = 8,
                    CreditValue = 0,
                    VictoryPoints = 3,
                    ServiceCategory = "WellArchitected",
                    Description = "Advanced Well-Architected Framework principles",
                    DocumentationUrl = "https://aws.amazon.com/architecture/well-architected/"
                },

                // Builder Cards
                new CardData
                {
                    Name = "Amazon EC2",
                    Type = CardType.Builder,
                    Cost = 0,
                    CreditValue = 2,
                    VictoryPoints = 0,
                    ServiceCategory = "Computing",
                    Description = "Scalable virtual servers in the cloud",
                    DocumentationUrl = "https://aws.amazon.com/ec2/",
                    EffectDescription = "2枚目のAmazon EC2をデプロイした時、このカードの上にスタックして1クレジットを得る。3枚目のAmazon EC2をデプロイした時、このカードの上にスタックしてさらに1クレジットと1購入を得る。"
                },
                new CardData
                {
                    Name = "AWS Lambda",
                    Type = CardType.Builder,
                    Cost = 3,
                    CreditValue = 1,
                    VictoryPoints = 0,
                    ServiceCategory = "Computing",
                    Description = "Run code without thinking about servers",
                    DocumentationUrl = "https://aws.amazon.com/lambda/",
                    EffectDescription = "Draw 1 card"
                },
                new CardData
                {
                    Name = "Amazon EC2 Auto Scaling",
                    Type = CardType.Builder,
                    Cost = 3,
                    CreditValue = 2,
                    VictoryPoints = 0,
                    ServiceCategory = "Computing",
                    Description = "Automatically scale EC2 instances",
                    DocumentationUrl = "https://aws.amazon.com/autoscaling/",
                    EffectDescription = "既にAmazon EC2をデプロイしている場合、追加で2クレジットを得る。このカードのみをデプロイした場合、Computeのカテゴリを持たない。"
                },
                new CardData
                {
                    Name = "Amazon S3",
                    Type = CardType.Builder,
                    Cost = 0,
                    CreditValue = 2,
                    VictoryPoints = 0,
                    ServiceCategory = "Storage",
                    Description = "Object storage built to store and retrieve any amount of data",
                    DocumentationUrl = "https://aws.amazon.com/s3/",
                    EffectDescription = "AWS Lambdaを既にデプロイしている場合、追加で1枚カードを引く。"
                },
                new CardData
                {
                    Name = "Amazon RDS",
                    Type = CardType.Builder,
                    Cost = 0,
                    CreditValue = 2,
                    VictoryPoints = 0,
                    ServiceCategory = "Database",
                    Description = "Managed relational database service",
                    DocumentationUrl = "https://aws.amazon.com/rds/",
                    EffectDescription = "ComputeまたはContainersカテゴリのカードを既にデプロイしている場合、追加で2クレジットを得る。"
                }
            };
        }

        public static List<CardData> GetCardsByType(CardType type)
        {
            var allCards = GetAllCards();
            return allCards.FindAll(card => card.Type == type);
        }

        public static List<CardData> GetCardsByCategory(string category)
        {
            var allCards = GetAllCards();
            return allCards.FindAll(card => card.ServiceCategory == category);
        }

        public static CardData GetCardByName(string name)
        {
            var allCards = GetAllCards();
            return allCards.Find(card => card.Name == name);
        }
    }
}
