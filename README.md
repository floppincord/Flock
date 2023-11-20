# Flock
クラウドソーシングサイトから新着案件を通知するアプリ
30秒に一回ごとに、LeafX.Net + AngleSharpを使い新着案件をスクレイプします。

## ビルド
VisualStudioなどのIDEソフトでこのプロジェクトを開き、DebugもしくはReleaseを選びビルドをしてください。
なお、実行する際には同じディレクトリに`config.json`を作成し、以下のようなフォーマットでの入力をしてください
```json
{
	"queries": ["スクレイピング", "Python"]
}
```

## 注意
このプロジェクトは自己責任で使用してください！

## ライセンス
MIT
