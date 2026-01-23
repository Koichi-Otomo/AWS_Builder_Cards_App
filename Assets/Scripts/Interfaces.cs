using System.Collections.Generic;
using UnityEngine;

namespace AWSBuilderCards
{
    /// <summary>
    /// カードの価値評価インターフェース
    /// </summary>
    public interface IValuable
    {
        int VictoryPoints(Player player);
        int CreditValue();
    }

    /// <summary>
    /// カード実行インターフェース
    /// </summary>
    public interface IPlayable
    {
        void OnPlay(Player player, GameManager game);
    }

    /// <summary>
    /// エフェクト実行インターフェース
    /// </summary>
    public interface IExecutable
    {
        void Execute(Player player, GameManager game);
        string Name { get; }
        string Description { get; }
    }

    /// <summary>
    /// UI表示対応インターフェース
    /// </summary>
    public interface IUIDisplayable
    {
        string GetDisplayName();
        string GetDisplayDescription();
        CardType GetCardType();
    }

    /// <summary>
    /// カード取得インターフェース
    /// </summary>
    public interface IGainable
    {
        void Gain(Card card);
    }

    /// <summary>
    /// ゲーム状態管理インターフェース
    /// </summary>
    public interface IGameStateManageable
    {
        void Setup();
        void Cleanup();
    }

    /// <summary>
    /// エフェクト管理インターフェース
    /// </summary>
    public interface IEffectContainer
    {
        List<Effect> Effects { get; }
        void AddEffect(Effect effect);
        void ExecuteAllEffects(Player player, GameManager game);
    }
}
