# AWS Builder Cards Scripts 説明書

このフォルダには、AWS Builder Cardsアプリのコアロジックを実装するC#スクリプトが含まれています。

## ファイル構成

### 基本クラス

#### Card.cs
- **用途**: カードシステムの基底クラスと具象クラス
- **主要クラス**:
  - `Card`: 全カードの基底抽象クラス
  - `StarterCard`: スターターカード（オンプレミス系）
  - `WellArchitectedCard`: 勝利点カード
  - `BuilderCard`: AWSサービスカード
  - `BasicCards`: カード生成用の静的クラス
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
- **使用方法**: `CardLists.GetAllCards()` でカード一覧取得

### ゲームロジック

#### Player.cs
- **用途**: プレイヤーの状態管理とアクション処理
- **主要機能**:
  - 手札・デッキ管理
  - カードプレイ処理
  - クレジット計算
  - 勝利点計算
- **使用方法**: `player.PlayCard(card, gameManager)` でカードをプレイ

#### Deck.cs
- **用途**: デッキ（山札・捨て札）の管理
- **主要機能**:
  - カードの追加・引く処理
  - シャッフル機能
  - 全カード取得
- **使用方法**: `deck.DrawTop()` でカードを1枚引く

#### Supply.cs
- **用途**: 場のカード供給管理（Marketplace）
- **主要機能**:
  - カードパイルの管理
  - カード購入処理
  - 残り枚数確認
- **使用方法**: `supply.Take("EC2")` でカードを購入

#### GameManager.cs
- **用途**: ゲーム全体の進行管理
- **主要機能**:
  - ゲーム初期化
  - ターン進行
  - 勝敗判定
- **使用方法**: MonoBehaviourとしてGameObjectにアタッチ

### UI関連

#### UIManager.cs
- **用途**: ゲーム画面のUI管理
- **主要機能**:
  - 手札表示
  - カードボタン生成
  - プレイヤー操作処理
- **使用方法**: MonoBehaviourとしてGameObjectにアタッチ

## 2nd Edition対応

このプロジェクトはAWS Builder Cards 2nd Editionに対応しています：

- **統一コストシステム**: TCOとAWSome Creditから単一のCostに統一
- **新カード追加**: Amazon EC2 Auto Scalingなど
- **バランス調整**: ゲームバランスの最適化

## 使用例

```csharp
// ゲーム初期化
var gameManager = new GameManager();

// プレイヤー作成
var player = new Player("Alice");
player.SetupStartingDeck();

// カードプレイ
var ec2Card = BasicCards.EC2();
player.PlayCard(ec2Card, gameManager);

// カード購入
var supply = new Supply();
supply.AddPile("EC2", BasicCards.EC2(), 10);
var purchasedCard = supply.Take("EC2");
```

## 開発時の注意点

- 新しいカードを追加する場合は、Card.cs、Supply.cs、CardLists.csを更新
- ゲームルール変更時は、Player.csとGameManager.csを確認
- UI変更時は、UIManager.csを修正