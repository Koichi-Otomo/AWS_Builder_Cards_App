# AWS Builder Cards Scripts 説明書

このフォルダには、AWS Builder Cardsアプリのコアロジックを実装するC#スクリプトが含まれています。

## ファイル構成

### インターフェース（Interface）

#### Interfaces.cs
- **用途**: ゲームシステムの契約定義（SOLID原則に基づく設計）
- **定義インターフェース**:
  - `IValuable`: カード価値評価（VictoryPoints、CreditValue）
  - `IPlayable`: カード実行機能（OnPlay）
  - `IUIDisplayable`: UI表示対応（DisplayName、Description、CardType取得）
  - `IEffectContainer`: エフェクト管理（Effects、AddEffect、ExecuteAllEffects）
  - `IExecutable`: エフェクト実行（Execute）
  - `IGainable`: カード取得機能（Gain）
  - `IGameStateManageable`: ゲーム状態管理（Setup、Cleanup）
- **使用方法**: キャストや型チェック時に使用可能
  ```csharp
  IValuable valuable = (Card)card;
  int vp = valuable.VictoryPoints(player);
  ```

### 基本クラス

#### Card.cs
- **用途**: カードシステムの基底クラスと具象クラス
- **実装インターフェース**: `IValuable`, `IPlayable`, `IUIDisplayable`, `IEffectContainer`
- **主要クラス**:
  - `Card`: 全カードの基底抽象クラス
    - `StarterCard`: スターターカード（オンプレミス系）
    - `WellArchitectedCard`: 勝利点カード
    - `BuilderCard`: AWSサービスカード
  - `BasicCards`: カード生成用の静的クラス
- **主要メソッド**:
  - `OnPlay()`: カードプレイ時の処理
  - `AddEffect()`: エフェクト追加
  - `ExecuteAllEffects()`: 全エフェクト実行
  - `GetDisplayName()`: UI表示名取得
- **使用方法**: `BasicCards.EC2()` でカードインスタンスを生成

#### CardType.cs
- **用途**: カードタイプの列挙型定義
- **定義値**: `Starter`, `WellArchitected`, `Builder`
- **使用方法**: `card.Type == CardType.Builder` で判定

#### CardLists.cs
- **用途**: カード図鑑機能用のデータ管理
- **主要機能**:
  - 全カードデータの取得
  - タイプ別・カテゴリ別検索
  - カード名による検索
- **構造**:
  - `CardData`: カード情報を保持する可視化可能なデータクラス
  - 統計情報：全11種類のカード
    - Starter Cards: 4種類（Cost: 0, Credit: 0）
    - Well-Architected: 2種類（Cost: 11, VP: 4）
    - Builder Cards: 5種類（Cost: 6, Credit: 9）
- **使用方法**: `CardLists.GetAllCards()` でカード一覧取得

### ゲームロジック

#### Player.cs
- **用途**: プレイヤーの状態管理とアクション処理
- **実装インターフェース**: `IGameStateManageable`
- **主要機能**:
  - 手札・デッキ管理
  - カードプレイ処理
  - クレジット計算
  - 勝利点計算
- **主要メソッド**:
  - `Setup()`: プレイヤー初期化
  - `Cleanup()`: ターン終了処理
  - `PlayCard()`: カード実行
  - `Draw()`: カード引く
- **使用方法**: `player.PlayCard(card, gameManager)` でカードをプレイ

#### Deck.cs
- **用途**: デッキ（山札・捨て札）の管理
- **実装インターフェース**: `IGainable`
- **主要機能**:
  - カードの追加・引く処理
  - シャッフル機能
  - 全カード取得
- **主要メソッド**:
  - `DrawTop()`: 山札から1枚引く
  - `Gain()`: 捨て札にカード追加
  - `Shuffle()`: デッキシャッフル
- **使用方法**: `deck.DrawTop()` でカードを1枚引く、`deck.Gain(card)` でカード獲得

#### Supply.cs
- **用途**: 場のカード供給管理（Marketplace）
- **主要機能**:
  - カードパイルの管理
  - カード購入処理
  - 残り枚数確認
- **主要メソッド**:
  - `AddPile()`: カード種類追加
  - `Take()`: カード購入
  - `Count()`: 残り枚数確認
- **使用方法**: `supply.Take("EC2")` でカードを購入

#### GameManager.cs
- **用途**: ゲーム全体の進行管理（MonoBehaviour）
- **主要機能**:
  - ゲーム初期化（SetupSupply, SetupPlayers）
  - ターン進行
  - 勝敗判定
- **主要プロパティ**:
  - `supply`: Supply インスタンス
  - `players`: プレイヤーリスト
  - `currentPlayerIndex`: 現在のプレイヤーインデックス
- **使用方法**: MonoBehaviourとしてGameObjectにアタッチ

### エフェクト

#### Effect.cs
- **用途**: ゲームエフェクト（カード効果）の基底クラス
- **実装インターフェース**: `IExecutable`
- **主要クラス**:
  - `Effect`: 抽象基底クラス
  - `DrawCardsEffect`: カード引く効果
  - `GainCreditsEffect`: クレジット獲得効果
  - `GainActionsEffect`: アクション獲得効果
  - `GainBuysEffect`: 購入獲得効果
- **使用方法**: `card.Effects.Add(new DrawCardsEffect(1))` でエフェクト追加

#### AdvancedEffects.cs
- **用途**: 高度なエフェクト実装
- **主要クラス**:
  - `ConditionalEffect`: 条件付きエフェクト
  - `MultipleEffect`: 複数エフェクト組み合わせ
  - `ScalingEffect`: スケーリングエフェクト
- **使用方法**: より複雑なカード効果を定義する際に使用

### UI関連

#### UIManager.cs
- **用途**: ゲーム画面のUI管理（MonoBehaviour）
- **主要機能**:
  - 手札表示
  - カードボタン生成
  - プレイヤー操作処理
- **主要メソッド**:
  - `RefreshHand()`: 手札表示更新
  - `CreateCardButton()`: カードボタン生成
  - `OnCardClicked()`: カードクリック処理
- **使用方法**: MonoBehaviourとしてGameObjectにアタッチ

## アーキテクチャ設計

### SOLID 原則への準拠

- **Single Responsibility**: 各クラスが単一の責任を持つ
- **Open/Closed**: インターフェース実装により拡張に開く
- **Liskov Substitution**: インターフェース実装により置換可能性を確保
- **Interface Segregation**: 小さく特化したインターフェース設計
- **Dependency Inversion**: 高レベルモジュールが低レベルモジュールに依存しない

### クラス図とオブジェクト図

詳細なクラス構造とカード定義は以下のドキュメントを参照：
- [ClassDiagram.drawio](../../Documents/ClassDiagram.drawio): クラス構造とインターフェース関係
- [ObjectDiagram_CardLists.drawio](../../Documents/ObjectDiagram_CardLists.drawio): カードオブジェクト実装例

## 2nd Edition対応

このプロジェクトはAWS Builder Cards 2nd Editionに対応しています：

- **統一コストシステム**: TCOとAWSome Creditから単一のCostに統一
- **新カード追加**: Amazon EC2 Auto Scalingなど
- **バランス調整**: ゲームバランスの最適化
- **インターフェース設計**: より保守性の高い拡張可能な設計

## 使用例

```csharp
// ゲーム初期化
var gameManager = new GameManager();

// プレイヤー作成
var player = new Player("Alice");
player.Setup();  // IGameStateManageable.Setup() を実行

// カードプレイ
var ec2Card = BasicCards.EC2();
if (player.PlayCard(ec2Card, gameManager))
{
    Debug.Log($"{player.Name} played {ec2Card.Name}");
}

// エフェクト追加例
var lambdaCard = BasicCards.Lambda();
lambdaCard.AddEffect(new DrawCardsEffect(1));

// カード購入
var supply = new Supply();
supply.AddPile("EC2", BasicCards.EC2(), 10);
var purchasedCard = supply.Take("EC2");
player.Deck.Gain(purchasedCard);

// UI利用時のインターフェース活用
IUIDisplayable displayCard = (Card)ec2Card;
Debug.Log(displayCard.GetDisplayName());  // "Amazon EC2 (0)" と表示
```
supply.AddPile("EC2", BasicCards.EC2(), 10);
var purchasedCard = supply.Take("EC2");
```

## 開発時の注意点

- 新しいカードを追加する場合は、Card.cs、Supply.cs、CardLists.csを更新
- ゲームルール変更時は、Player.csとGameManager.csを確認
- UI変更時は、UIManager.csを修正