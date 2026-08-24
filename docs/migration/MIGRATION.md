# Migration Guide

各バージョン間の移行手順をまとめています。

## 目次

- [0.17.0 → next](#0170--next)
- [0.16.3 → 0.17.0](#0163--0170)

---

## 0.17.0 → next

本セクションでは、`main` ブランチに導入された **Newtype パターン** への移行方法を説明します。  
現時点では後方互換 API（旧シグネチャ）に `[Obsolete]` が付与されており、コンパイル警告で確認できます。  
**次のリリースでは旧シグネチャが削除される予定**のため、今のうちに移行をお願いします。

### Newtype パターンとは

従来、スタイルID やボイスモデルID は `uint` / `string` といったプリミティブ型で渡していました。  
新しい設計では **専用の値型（newtype）** を使用します：

| 旧型 | 新型 | 用途 |
|------|------|------|
| `uint` | `StyleId` | スタイル ID |
| `string` (UUID) | `VoiceModelId` | ボイスモデル ID |

これにより、型の取り違えによるバグを防ぎ、意図を明確にコードで表現できます。

### `StyleId` への移行

`Synthesizer` クラス（および `Experimental` 拡張メソッド）の全メソッドで `uint styleId` が `StyleId styleId` に変わりました。

```csharp
// 変更前（警告が出る旧シグネチャ）
synthesizer.CreateAudioQuery(text, 1u, out var audioQueryJson);
synthesizer.Synthesis(audioQueryJson, 1u, options, out var wavLen, out var wav);

// 変更後
var styleId = new StyleId(1u);
// または明示的キャスト: var styleId = (StyleId)1u;

synthesizer.CreateAudioQuery(text, styleId, out var audioQueryJson);
synthesizer.Synthesis(audioQueryJson, styleId, options, out var wavLen, out var wav);
```

影響を受けるすべてのメソッドは以下のとおりです（`Synthesizer` クラス）：

- `CreateAudioQuery`
- `CreateAudioQueryFromKana`
- `CreateAccentPhrases`
- `CreateAccentPhrasesFromKana`
- `ReplaceMoraData`
- `ReplacePhonemeLength`
- `ReplaceMoraPitch`
- `Synthesis`
- `Tts`
- `TtsFromKana`
- `CreateSingFrameAudioQuery`
- `CreateSingFrameF0`
- `CreateSingFrameVolume`
- `FrameSynthesis`
- `UnloadVoiceModel`
- `IsLoadedVoiceModel`

`Experimental` パッケージの対応する非同期拡張メソッドも同様です。

#### `uint` との相互変換

`StyleId` は `uint` との明示的キャストをサポートしています：

```csharp
// uint → StyleId
StyleId styleId = (StyleId)1u;

// StyleId → uint
uint value = (uint)styleId;
// または
uint value = styleId.Value;
```

### `VoiceModelId` への移行

`Synthesizer.UnloadVoiceModel` と `Synthesizer.IsLoadedVoiceModel` では `string modelId` が `VoiceModelId modelId` に変わりました。  
また `VoiceModelFile.Id` プロパティの型も `string` から `VoiceModelId` になりました。

```csharp
// 変更前（警告が出る旧シグネチャ）
synthesizer.UnloadVoiceModel(voiceModel.Id);         // voiceModel.Id は string だった
synthesizer.IsLoadedVoiceModel(voiceModel.Id);

// 変更後
synthesizer.UnloadVoiceModel(voiceModel.Id);         // voiceModel.Id は VoiceModelId
synthesizer.IsLoadedVoiceModel(voiceModel.Id);
```

`voiceModel.Id` 経由でそのまま渡している場合はコード変更不要です。  
UUID 文字列から生成する場合は以下のとおりです：

```csharp
// string → VoiceModelId
var modelId = new VoiceModelId("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx");
// または明示的キャスト
var modelId = (VoiceModelId)"xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx";

// VoiceModelId → string
string value = modelId.ToString();          // "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
// または明示的キャスト
string value = (string)modelId;

// VoiceModelId → Guid
Guid guid = (Guid)modelId;
// または
Guid guid = modelId.Value;
```

### 移行チェックリスト

- [ ] `uint styleId` を受け取っていた箇所を `StyleId styleId` に変更する
  - `(StyleId)value` または `new StyleId(value)` で生成する
- [ ] `string modelId` を受け取っていた箇所を `VoiceModelId modelId` に変更する
  - `new VoiceModelId(uuid)` または `(VoiceModelId)uuid` で生成する
- [ ] コンパイル警告（`[Obsolete]`）がなくなっていることを確認する

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
