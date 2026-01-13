ドミニオン風ミニゲーム（Unity用）

概要
- Unityプロジェクト内で動作する簡易的なドミニオン風ロジックを実装しています。サンプルは自動で数ターン進行し、コンソールにログを出力します。

設置方法
1. Unityでプロジェクトを開く。
2. 空のGameObjectを作成し、コンポーネントとして`GameManager`スクリプトを追加する。
3. Playボタンを押すと、コンソールにデモゲームのログが表示されます。

UIで手動プレイする方法
- 同じシーンに空のGameObjectを作成し、`UIManager`スクリプトを追加してください（`GameManager`がシーンに存在する必要があります）。
- `UIManager`は自動で Canvas と手札パネルを生成し、現在のプレイヤーの手札をボタンとして表示します。
- カードボタンをクリックすると、そのカードをプレイします（アクション/トレジャーをサポート）。

簡単な使用手順
1. シーンに `GameManager` を追加。
2. 別の空GameObjectを作成して `UIManager` をアタッチ。
3. Play を押すと画面下部に手札が表示され、カードをクリックするとプレイされます。

注意
- このUIはランタイムで簡易的に生成されるもので、見た目は最小限です。Inspectorでレイアウトやスタイルを編集する、あるいはPrefab化して差し替えることをおすすめします。

ファイル
- Assets/Scripts/Dominion/CardType.cs
- Assets/Scripts/Dominion/Card.cs
- Assets/Scripts/Dominion/Deck.cs
- Assets/Scripts/Dominion/Player.cs
- Assets/Scripts/Dominion/Supply.cs
- Assets/Scripts/Dominion/GameManager.cs
- Assets/Scripts/Dominion/UIManager.cs

拡張案
- カードのシリアライズ、Inspectorからカードセット編集（ScriptableObject化）
- UIを整えてドラッグ&ドロップでプレイ可能にする
- 得点計算、勝利条件判定、AIプレイヤー
ドミニオン風ミニゲーム（Unity用）

概要
- Unityプロジェクト内で動作する簡易的なドミニオン風ロジックを実装しています。サンプルは自動で数ターン進行し、コンソールにログを出力します。

設置方法
1. Unityでプロジェクトを開く。
2. 空のGameObjectを作成し、コンポーネントとして`GameManager`スクリプトを追加する。
3. Playボタンを押すと、コンソールにデモゲームのログが表示されます。

ファイル
- Assets/Scripts/Dominion/CardType.cs
- Assets/Scripts/Dominion/Card.cs
- Assets/Scripts/Dominion/Deck.cs
- Assets/Scripts/Dominion/Player.cs
- Assets/Scripts/Dominion/Supply.cs
- Assets/Scripts/Dominion/GameManager.cs

拡張案
- カードのシリアライズ、Inspectorからカードセット編集
- UIを用意してプレイヤー操作（カードの選択・プレイ）を可能にする
- 得点計算、勝利条件判定、AIプレイヤー
