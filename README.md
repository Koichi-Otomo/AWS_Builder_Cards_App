# AWS Builder Cards App

AWS Builder Cardsをデジタル化したUnityアプリケーションです。

## プロジェクト概要

このプロジェクトは、[AWS Builder Cards](https://github.com/jaws-ug/AWS-BuilderCards-Japanese)をベースにしたデジタルカードゲームアプリです。UnityでiOS/Android/Steam向けに開発しています。

## 開発環境

### 必要なソフトウェア

- **Unity**: 2022.3 LTS以上推奨
- **Git**: バージョン管理
- **IDE**: Visual Studio Code または Visual Studio

### 対応プラットフォーム

- iOS
- Android  
- Steam（将来的に）

## プロジェクトへの参加方法

### 1. リポジトリのクローン

```bash
git clone https://github.com/Koichi-Otomo/AWS_Builder_Cards_App.git
cd AWS_Builder_Cards_App
```

### 2. Unityでプロジェクトを開く

1. Unity Hubを起動
2. 「開く」をクリック
3. クローンしたフォルダを選択
4. Unityエディターでプロジェクトが開かれます

### 3. 初回セットアップ

プロジェクトを開いた後、以下を確認してください：

- Package Managerで必要なパッケージが自動インストールされること
- コンソールにエラーが出ていないこと
- Assets/Scenes/SampleScene.unityが正常に開けること

### 4. 開発フロー

1. **ブランチ作成**
   ```bash
   git checkout -b feature/your-feature-name
   ```

2. **開発作業**
   - コードの変更
   - テスト実行

3. **コミット**
   ```bash
   git add .
   git commit -m "Add: 機能の説明"
   ```

4. **プッシュ**
   ```bash
   git push origin feature/your-feature-name
   ```

5. **プルリクエスト作成**
   - GitHubでプルリクエストを作成
   - レビュー後にマージ

## プロジェクト構成

```
AWS_Builder_Cards_App/
├── Assets/
│   ├── Scripts/          # C#スクリプト
│   ├── Scenes/           # Unityシーン
│   └── Prefabs/          # プレハブファイル
├── Documents/
│   └── GDD.md           # ゲームデザインドキュメント
├── ProjectSettings/      # Unityプロジェクト設定
├── Packages/            # パッケージ管理
└── README.md
```

## 開発ルール

### コーディング規約

- C#の命名規則に従う
- クラス名：PascalCase
- メソッド名：PascalCase
- 変数名：camelCase
- 定数：UPPER_SNAKE_CASE

### コミットメッセージ

```
Add: 新機能追加
Fix: バグ修正
Update: 既存機能の更新
Remove: 不要なコードの削除
Docs: ドキュメント更新
```

### ブランチ命名

- `feature/機能名`: 新機能開発
- `fix/バグ名`: バグ修正
- `docs/ドキュメント名`: ドキュメント更新

## トラブルシューティング

### よくある問題

1. **Unityでプロジェクトが開けない**
   - Unityのバージョンを確認
   - Library/フォルダを削除して再生成

2. **パッケージエラー**
   - Window > Package Manager でパッケージを再インストール

3. **ビルドエラー**
   - ProjectSettings/が正しく同期されているか確認

## 参考資料

- [AWS Builder Cards 公式リポジトリ](https://github.com/jaws-ug/AWS-BuilderCards-Japanese)
- [Unity公式ドキュメント](https://docs.unity3d.com/)
- [ゲームデザインドキュメント](Documents/GDD.md)

## ライセンス

このプロジェクトは、AWS Builder Cardsの利用規約に準拠します。

## 連絡先

プロジェクトに関する質問や提案は、GitHubのIssuesまたはDiscussionsをご利用ください。
