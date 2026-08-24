# Migration Guide

各バージョン間の移行手順をまとめています。

## 目次

- [0.16.3 → 0.17.0](#0163--0170)

---

## 0.16.3 → 0.17.0

voicevox_core の 0.16.4 → 0.17.0 アップグレードに伴う更新です。  
本リリースには**破壊的変更**が含まれます。

### 必須ランタイム・サポート範囲の変更

#### ONNX Runtime バージョンの引き上げ

| | 0.16.3 | 0.17.0 |
|---|---|---|
| 推奨 ONNX Runtime バージョン | 1.17.3 | **1.23.2** |

利用している `voicevox_onnxruntime` の動的ライブラリ（`.so` / `.dylib` / `.dll`）を **1.23.2** に差し替えてください。

```
# 変更前
libvoicevox_onnxruntime.1.17.3.dylib

# 変更後
libvoicevox_onnxruntime.1.23.2.dylib
```

また、ONNX Runtime のロード時にバージョン検証が強化されました。

- マイナーバージョンが最小要件 (`GetMinRequiredMinorVersion()`) 未満の場合はエラー
- 最大サポートバージョン (`GetMaxSupportedMinorVersion()`) を超える場合は警告ログ

#### Unity サポートバージョンの引き上げ

| | 0.16.3 | 0.17.0 |
|---|---|---|
| 最低サポート Unity バージョン | Unity 2021.3 (LTS) | **Unity 2022.3 (LTS)** |

Unity 2021.3 はサポート対象外となりました。Unity 2022.3 以降へアップグレードしてください。

#### .NET サポートの変更（MAUI パッケージ）

`VoicevoxCoreSharp.MAUI` パッケージにおいて、**net8** がターゲットから除外されました。  
.NET 9 以降を使用してください。

### C# API の破壊的変更

#### `Synthesizer.LoadVoiceModel` のシグネチャ変更

`LoadVoiceModel` メソッドに `LoadVoiceModelOptions` 引数が追加されました。

```csharp
// 変更前 (0.16.3)
ResultCode result = synthesizer.LoadVoiceModel(voiceModel);

// 変更後 (0.17.0)
ResultCode result = synthesizer.LoadVoiceModel(voiceModel, LoadVoiceModelOptions.Default());
```

デフォルト動作（同じIDのモデルが既に読み込まれているとエラー）を維持したい場合は `LoadVoiceModelOptions.Default()` を渡してください。

新しい `LoadVoiceModelOptions` 構造体（`VoicevoxCoreSharp.Core.Struct` 名前空間）により、同じIDのモデルが既に読み込まれていたときの挙動を `OnExistingVoiceModelId` 列挙値で制御できます。

| 値 | 説明 |
|---|---|
| `VOICEVOX_ON_EXISTING_VOICE_MODEL_ID_ERROR` | エラーにする（デフォルト） |
| `VOICEVOX_ON_EXISTING_VOICE_MODEL_ID_RELOAD` | 再読み込みする（長文合成後のメモリ解放に有効） |
| `VOICEVOX_ON_EXISTING_VOICE_MODEL_ID_SKIP` | 何もしない |

```csharp
using VoicevoxCoreSharp.Core.Struct;
using VoicevoxCoreSharp.Core.Enum;

// 再読み込みする例（長文テキスト合成後のメモリ使用量をリセットする場合）
var options = new LoadVoiceModelOptions
{
    OnExisting = OnExistingVoiceModelId.VOICEVOX_ON_EXISTING_VOICE_MODEL_ID_RELOAD
};
synthesizer.LoadVoiceModel(voiceModel, options);
```

#### `Onnxruntime` クラスのメソッド名変更

ファイル名取得系メソッドが削除・改名され、バージョン範囲取得メソッドが追加されました。

| 変更前 (0.16.3) | 変更後 (0.17.0) |
|---|---|
| `Onnxruntime.GetVersionedFilename()` | `Onnxruntime.GetRecommendedVersionedFilename()` |
| `Onnxruntime.GetUnversionedFilename()` | `Onnxruntime.GetRecommendedUnversionedFilename()` |
| *(なし)* | `Onnxruntime.GetMinRequiredMinorVersion()` → `uint` |
| *(なし)* | `Onnxruntime.GetMaxSupportedMinorVersion()` → `uint` |

```csharp
// 変更前
string filename = Onnxruntime.GetVersionedFilename();

// 変更後
string filename = Onnxruntime.GetRecommendedVersionedFilename();
```

#### `ResultCode` 列挙値のリネーム

| 変更前 (0.16.3) | 変更後 (0.17.0) |
|---|---|
| `ResultCode.RESULT_INVALID_MODEL_HEADER_ERROR` | `ResultCode.RESULT_INVALID_MODEL_FORMAT_ERROR` |

```csharp
// 変更前
if (result == ResultCode.RESULT_INVALID_MODEL_HEADER_ERROR) { ... }

// 変更後
if (result == ResultCode.RESULT_INVALID_MODEL_FORMAT_ERROR) { ... }
```

#### `UserDictWord.Priority` の型変更

`UserDictWord.Priority` プロパティの型が `uint` から `byte` に変更されました。  
値の範囲は `0`〜`10` であるため、実質的なデータの損失はありません。

```csharp
// 変更前
uint priority = word.Priority;

// 変更後
byte priority = word.Priority;
```

#### `Experimental` パッケージ: `LoadVoiceModelAsync` のシグネチャ変更

`VoicevoxCoreSharp.Experimental` を使用している場合も同様の変更が必要です。

```csharp
// 変更前 (0.16.3)
await synthesizer.LoadVoiceModelAsync(voiceModel);

// 変更後 (0.17.0)
await synthesizer.LoadVoiceModelAsync(voiceModel, LoadVoiceModelOptions.Default());
```

### 移行チェックリスト

- [ ] `voicevox_onnxruntime` を **1.23.2** に更新する
- [ ] `voicevox_core` を **0.17.0** に更新する
- [ ] `Synthesizer.LoadVoiceModel(voiceModel)` → `Synthesizer.LoadVoiceModel(voiceModel, LoadVoiceModelOptions.Default())` に修正する
- [ ] `synthesizer.LoadVoiceModelAsync(voiceModel)` → `synthesizer.LoadVoiceModelAsync(voiceModel, LoadVoiceModelOptions.Default())` に修正する（`Experimental` 利用者）
- [ ] `Onnxruntime.GetVersionedFilename()` → `GetRecommendedVersionedFilename()` に修正する（使用している場合）
- [ ] `Onnxruntime.GetUnversionedFilename()` → `GetRecommendedUnversionedFilename()` に修正する（使用している場合）
- [ ] `ResultCode.RESULT_INVALID_MODEL_HEADER_ERROR` → `RESULT_INVALID_MODEL_FORMAT_ERROR` に修正する（使用している場合）
- [ ] `UserDictWord.Priority` の受け取り型を `uint` → `byte` に変更する（使用している場合）
- [ ] Unity 利用者: Unity **2022.3** 以降にアップグレードする
- [ ] MAUI 利用者: **.NET 9** 以降を使用する
