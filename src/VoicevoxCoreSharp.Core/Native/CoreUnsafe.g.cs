// <auto-generated>
// This code is generated by csbindgen.
// DON'T CHANGE THIS DIRECTLY.
// </auto-generated>
#pragma warning disable CS8500
#pragma warning disable CS8981
using System;
using System.Runtime.InteropServices;


namespace VoicevoxCoreSharp.Core.Native
{
    internal static unsafe partial class CoreUnsafe
    {
#if UNITY_IOS && !UNITY_EDITOR
        const string __DllName = "__Internal";
#else
        const string __DllName = "voicevox_core";
#endif
        



        /// <summary>::OpenJtalkRc を&lt;b&gt;構築&lt;/b&gt;(_construct_)する。  解放は ::voicevox_open_jtalk_rc_delete で行う。  @param [in] open_jtalk_dic_dir 辞書ディレクトリを指すUTF-8のパス @param [out] out_open_jtalk 構築先  @returns 結果コード  \\example{ ```c OpenJtalkRc *open_jtalk; voicevox_open_jtalk_rc_new(\"./open_jtalk_dic_utf_8-1.11\", &amp;open_jtalk); ``` }  \\safety{ - `open_jtalk_dic_dir`はヌル終端文字列を指し、かつ&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 - `out_open_jtalk`は&lt;a href=\"#voicevox-core-safety\"&gt;書き込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_open_jtalk_rc_new", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_open_jtalk_rc_new(byte* open_jtalk_dic_dir, OpenJtalkRc** out_open_jtalk);

        /// <summary>OpenJtalkの使うユーザー辞書を設定する。  この関数を呼び出した後にユーザー辞書を変更した場合、再度この関数を呼び出す必要がある。  @param [in] open_jtalk Open JTalkのオブジェクト @param [in] user_dict ユーザー辞書  \\safety{ - `open_jtalk`は ::voicevox_open_jtalk_rc_new で得たものでなければならず、また ::voicevox_open_jtalk_rc_delete で解放されていてはいけない。 - `user_dict`は ::voicevox_user_dict_new で得たものでなければならず、また ::voicevox_user_dict_delete で解放されていてはいけない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_open_jtalk_rc_use_user_dict", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_open_jtalk_rc_use_user_dict(OpenJtalkRc* open_jtalk, VoicevoxUserDict* user_dict);

        /// <summary>::OpenJtalkRc を&lt;b&gt;破棄&lt;/b&gt;(_destruct_)する。  @param [in] open_jtalk 破棄対象  \\example{ ```c voicevox_open_jtalk_rc_delete(open_jtalk); ``` }  \\safety{ - `open_jtalk`は ::voicevox_open_jtalk_rc_new で得たものでなければならず、また既にこの関数で解放されていてはいけない。 - `open_jtalk`は以後&lt;b&gt;ダングリングポインタ&lt;/b&gt;(_dangling pointer_)として扱われなくてはならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_open_jtalk_rc_delete", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void voicevox_open_jtalk_rc_delete(OpenJtalkRc* open_jtalk);

        /// <summary>デフォルトの初期化オプションを生成する @return デフォルト値が設定された初期化オプション</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_make_default_initialize_options", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxInitializeOptions voicevox_make_default_initialize_options();

        /// <summary>voicevoxのバージョンを取得する。 @return SemVerでフォーマットされたバージョン。</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_get_version", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern byte* voicevox_get_version();

        /// <summary>VVMファイルから ::VoicevoxVoiceModel を&lt;b&gt;構築&lt;/b&gt;(_construct_)する。  @param [in] path vvmファイルへのUTF-8のファイルパス @param [out] out_model 構築先  @returns 結果コード  \\safety{ - `path`はヌル終端文字列を指し、かつ&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 - `out_model`は&lt;a href=\"#voicevox-core-safety\"&gt;書き込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_voice_model_new_from_path", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_voice_model_new_from_path(byte* path, VoicevoxVoiceModel** out_model);

        /// <summary>::VoicevoxVoiceModel からIDを取得する。  @param [in] model 音声モデル  @returns 音声モデルID  \\safety{ - `model`は ::voicevox_voice_model_new_from_path で得たものでなければならず、また ::voicevox_voice_model_delete で解放されていてはいけない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_voice_model_id", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern byte* voicevox_voice_model_id(VoicevoxVoiceModel* model);

        /// <summary>::VoicevoxVoiceModel からメタ情報を取得する。  @param [in] model 音声モデル  @returns メタ情報のJSON文字列  \\safety{ - `model`は ::voicevox_voice_model_new_from_path で得たものでなければならず、また ::voicevox_voice_model_delete で解放されていてはいけない。 - 戻り値の文字列の&lt;b&gt;生存期間&lt;/b&gt;(_lifetime_)は次にこの関数が呼ばれるか、`model`が破棄されるまでである。この生存期間を越えて文字列にアクセスしてはならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_voice_model_get_metas_json", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern byte* voicevox_voice_model_get_metas_json(VoicevoxVoiceModel* model);

        /// <summary>::VoicevoxVoiceModel を&lt;b&gt;破棄&lt;/b&gt;(_destruct_)する。  @param [in] model 破棄対象  \\safety{ - `model`は ::voicevox_voice_model_new_from_path で得たものでなければならず、また既にこの関数で解放されていてはいけない。 - `model`は以後&lt;b&gt;ダングリングポインタ&lt;/b&gt;(_dangling pointer_)として扱われなくてはならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_voice_model_delete", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void voicevox_voice_model_delete(VoicevoxVoiceModel* model);

        /// <summary>::VoicevoxSynthesizer を&lt;b&gt;構築&lt;/b&gt;(_construct_)する。  @param [in] open_jtalk Open JTalkのオブジェクト @param [in] options オプション @param [out] out_synthesizer 構築先  @returns 結果コード  \\safety{ - `open_jtalk`は ::voicevox_voice_model_new_from_path で得たものでなければならず、また ::voicevox_open_jtalk_rc_new で解放されていてはいけない。 - `out_synthesizer`は&lt;a href=\"#voicevox-core-safety\"&gt;書き込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_synthesizer_new", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_synthesizer_new(OpenJtalkRc* open_jtalk, VoicevoxInitializeOptions options, VoicevoxSynthesizer** out_synthesizer);

        /// <summary>::VoicevoxSynthesizer を&lt;b&gt;破棄&lt;/b&gt;(_destruct_)する。  @param [in] synthesizer 破棄対象  \\safety{ - `synthesizer`は ::voicevox_synthesizer_new で得たものでなければならず、また既にこの関数で解放されていてはいけない。 - `synthesizer`は以後&lt;b&gt;ダングリングポインタ&lt;/b&gt;(_dangling pointer_)として扱われなくてはならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_synthesizer_delete", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void voicevox_synthesizer_delete(VoicevoxSynthesizer* synthesizer);

        /// <summary>音声モデルを読み込む。  @param [in] synthesizer 音声シンセサイザ @param [in] model 音声モデル  @returns 結果コード  \\safety{ - `synthesizer`は ::voicevox_synthesizer_new で得たものでなければならず、また ::voicevox_synthesizer_delete で解放されていてはいけない。 - `model`は ::voicevox_voice_model_new_from_path で得たものでなければならず、また ::voicevox_voice_model_delete で解放されていてはいけない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_synthesizer_load_voice_model", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_synthesizer_load_voice_model(VoicevoxSynthesizer* synthesizer, VoicevoxVoiceModel* model);

        /// <summary>音声モデルの読み込みを解除する。  @param [in] synthesizer 音声シンセサイザ @param [in] model_id 音声モデルID  @returns 結果コード  \\safety{ - `synthesizer`は ::voicevox_synthesizer_new で得たものでなければならず、また ::voicevox_synthesizer_delete で解放されていてはいけない。 - `model_id`はヌル終端文字列を指し、かつ&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_synthesizer_unload_voice_model", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_synthesizer_unload_voice_model(VoicevoxSynthesizer* synthesizer, byte* model_id);

        /// <summary>ハードウェアアクセラレーションがGPUモードか判定する。  @param [in] synthesizer 音声シンセサイザ  @returns GPUモードかどうか  \\safety{ - `synthesizer`は ::voicevox_synthesizer_new で得たものでなければならず、また ::voicevox_synthesizer_delete で解放されていてはいけない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_synthesizer_is_gpu_mode", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool voicevox_synthesizer_is_gpu_mode(VoicevoxSynthesizer* synthesizer);

        /// <summary>指定したIDの音声モデルが読み込まれているか判定する。  @param [in] synthesizer 音声シンセサイザ @param [in] model_id 音声モデルID  @returns モデルが読み込まれているかどうか  \\safety{ - `synthesizer`は ::voicevox_synthesizer_new で得たものでなければならず、また ::voicevox_synthesizer_delete で解放されていてはいけない。 - `model_id`はヌル終端文字列を指し、かつ&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_synthesizer_is_loaded_voice_model", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool voicevox_synthesizer_is_loaded_voice_model(VoicevoxSynthesizer* synthesizer, byte* model_id);

        /// <summary>今読み込んでいる音声モデルのメタ情報を、JSONで取得する。  JSONの解放は ::voicevox_json_free で行う。  @param [in] synthesizer 音声シンセサイザ  @return メタ情報のJSON文字列  \\safety{ - `synthesizer`は ::voicevox_synthesizer_new で得たものでなければならず、また ::voicevox_synthesizer_delete で解放されていてはいけない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_synthesizer_create_metas_json", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern byte* voicevox_synthesizer_create_metas_json(VoicevoxSynthesizer* synthesizer);

        /// <summary>このライブラリで利用可能なデバイスの情報を、JSONで取得する。  JSONの解放は ::voicevox_json_free で行う。  あくまで本ライブラリが対応しているデバイスの情報であることに注意。GPUが使える環境ではなかったとしても`cuda`や`dml`は`true`を示しうる。  @param [out] output_supported_devices_json サポートデバイス情報のJSON文字列  @returns 結果コード  \\example{ ```c char *supported_devices; VoicevoxResultCode result = voicevox_create_supported_devices_json(&amp;supported_devices); ``` }  \\safety{ - `output_supported_devices_json`は&lt;a href=\"#voicevox-core-safety\"&gt;書き込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_create_supported_devices_json", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_create_supported_devices_json(byte** output_supported_devices_json);

        /// <summary>AquesTalk風記法から、AudioQueryをJSONとして生成する。  生成したJSON文字列を解放するには ::voicevox_json_free を使う。  @param [in] synthesizer 音声シンセサイザ @param [in] kana AquesTalk風記法 @param [in] style_id スタイルID @param [out] output_audio_query_json 生成先  @returns 結果コード  \\example{ ```c char *audio_query; voicevox_synthesizer_create_audio_query_from_kana(synthesizer, \"コンニチワ'\", 2, // \"四国めたん (ノーマル)\ &amp;audio_query); ``` }  \\safety{ - `synthesizer`は ::voicevox_synthesizer_new で得たものでなければならず、また ::voicevox_synthesizer_delete で解放されていてはいけない。 - `kana`はヌル終端文字列を指し、かつ&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 - `output_audio_query_json`は&lt;a href=\"#voicevox-core-safety\"&gt;書き込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_synthesizer_create_audio_query_from_kana", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_synthesizer_create_audio_query_from_kana(VoicevoxSynthesizer* synthesizer, byte* kana, uint style_id, byte** output_audio_query_json);

        /// <summary>日本語テキストから、AudioQueryをJSONとして生成する。  生成したJSON文字列を解放するには ::voicevox_json_free を使う。  @param [in] synthesizer 音声シンセサイザ @param [in] text UTF-8の日本語テキスト @param [in] style_id スタイルID @param [out] output_audio_query_json 生成先  @returns 結果コード  \\example{ ```c char *audio_query; voicevox_synthesizer_create_audio_query(synthesizer, \"こんにちは\", 2, // \"四国めたん (ノーマル)\ &amp;audio_query); ``` }  \\safety{ - `synthesizer`は ::voicevox_synthesizer_new で得たものでなければならず、また ::voicevox_synthesizer_delete で解放されていてはいけない。 - `text`はヌル終端文字列を指し、かつ&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 - `output_audio_query_json`は&lt;a href=\"#voicevox-core-safety\"&gt;書き込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_synthesizer_create_audio_query", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_synthesizer_create_audio_query(VoicevoxSynthesizer* synthesizer, byte* text, uint style_id, byte** output_audio_query_json);

        /// <summary>AquesTalk風記法から、AccentPhrase (アクセント句)の配列をJSON形式で生成する。  生成したJSON文字列を解放するには ::voicevox_json_free を使う。  @param [in] synthesizer 音声シンセサイザ @param [in] kana AquesTalk風記法 @param [in] style_id スタイルID @param [out] output_accent_phrases_json 生成先  @returns 結果コード  \\example{ ```c char *accent_phrases; voicevox_synthesizer_create_accent_phrases_from_kana( synthesizer, \"コンニチワ'\", 2, // \"四国めたん (ノーマル)\ &amp;accent_phrases); ``` }  \\safety{ - `synthesizer`は ::voicevox_synthesizer_new で得たものでなければならず、また ::voicevox_synthesizer_delete で解放されていてはいけない。 - `kana`はヌル終端文字列を指し、かつ&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 - `output_audio_query_json`は&lt;a href=\"#voicevox-core-safety\"&gt;書き込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_synthesizer_create_accent_phrases_from_kana", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_synthesizer_create_accent_phrases_from_kana(VoicevoxSynthesizer* synthesizer, byte* kana, uint style_id, byte** output_accent_phrases_json);

        /// <summary>日本語テキストから、AccentPhrase (アクセント句)の配列をJSON形式で生成する。  生成したJSON文字列を解放するには ::voicevox_json_free を使う。  @param [in] synthesizer 音声シンセサイザ @param [in] text UTF-8の日本語テキスト @param [in] style_id スタイルID @param [out] output_accent_phrases_json 生成先  @returns 結果コード  \\example{ ```c char *accent_phrases; voicevox_synthesizer_create_accent_phrases(synthesizer, \"こんにちは\", 2, // \"四国めたん (ノーマル)\ &amp;accent_phrases); ``` }  \\safety{ - `synthesizer`は ::voicevox_synthesizer_new で得たものでなければならず、また ::voicevox_synthesizer_delete で解放されていてはいけない。 - `text`はヌル終端文字列を指し、かつ&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 - `output_audio_query_json`は&lt;a href=\"#voicevox-core-safety\"&gt;書き込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_synthesizer_create_accent_phrases", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_synthesizer_create_accent_phrases(VoicevoxSynthesizer* synthesizer, byte* text, uint style_id, byte** output_accent_phrases_json);

        /// <summary>AccentPhraseの配列の音高・音素長を、特定の声で生成しなおす。  生成したJSON文字列を解放するには ::voicevox_json_free を使う。  @param [in] synthesizer 音声シンセサイザ @param [in] accent_phrases_json AccentPhraseの配列のJSON文字列 @param [in] style_id スタイルID @param [out] output_accent_phrases_json 生成先  @returns 結果コード  \\safety{ - `synthesizer`は ::voicevox_synthesizer_new で得たものでなければならず、また ::voicevox_synthesizer_delete で解放されていてはいけない。 - `accent_phrases_json`はヌル終端文字列を指し、かつ&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 - `output_audio_query_json`は&lt;a href=\"#voicevox-core-safety\"&gt;書き込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_synthesizer_replace_mora_data", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_synthesizer_replace_mora_data(VoicevoxSynthesizer* synthesizer, byte* accent_phrases_json, uint style_id, byte** output_accent_phrases_json);

        /// <summary>AccentPhraseの配列の音素長を、特定の声で生成しなおす。  生成したJSON文字列を解放するには ::voicevox_json_free を使う。  @param [in] synthesizer 音声シンセサイザ @param [in] accent_phrases_json AccentPhraseの配列のJSON文字列 @param [in] style_id スタイルID @param [out] output_accent_phrases_json 生成先  @returns 結果コード  \\safety{ - `synthesizer`は ::voicevox_synthesizer_new で得たものでなければならず、また ::voicevox_synthesizer_delete で解放されていてはいけない。 - `accent_phrases_json`はヌル終端文字列を指し、かつ&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 - `output_audio_query_json`は&lt;a href=\"#voicevox-core-safety\"&gt;書き込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_synthesizer_replace_phoneme_length", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_synthesizer_replace_phoneme_length(VoicevoxSynthesizer* synthesizer, byte* accent_phrases_json, uint style_id, byte** output_accent_phrases_json);

        /// <summary>AccentPhraseの配列の音高を、特定の声で生成しなおす。  生成したJSON文字列を解放するには ::voicevox_json_free を使う。  @param [in] synthesizer 音声シンセサイザ @param [in] accent_phrases_json AccentPhraseの配列のJSON文字列 @param [in] style_id スタイルID @param [out] output_accent_phrases_json 生成先  @returns 結果コード  \\safety{ - `synthesizer`は ::voicevox_synthesizer_new で得たものでなければならず、また ::voicevox_synthesizer_delete で解放されていてはいけない。 - `accent_phrases_json`はヌル終端文字列を指し、かつ&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 - `output_audio_query_json`は&lt;a href=\"#voicevox-core-safety\"&gt;書き込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_synthesizer_replace_mora_pitch", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_synthesizer_replace_mora_pitch(VoicevoxSynthesizer* synthesizer, byte* accent_phrases_json, uint style_id, byte** output_accent_phrases_json);

        /// <summary>デフォルトの `voicevox_synthesizer_synthesis` のオプションを生成する @return デフォルト値が設定された `voicevox_synthesizer_synthesis` のオプション</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_make_default_synthesis_options", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxSynthesisOptions voicevox_make_default_synthesis_options();

        /// <summary>AudioQueryから音声合成を行う。  生成したWAVデータを解放するには ::voicevox_wav_free を使う。  @param [in] synthesizer 音声シンセサイザ @param [in] audio_query_json AudioQueryのJSON文字列 @param [in] style_id スタイルID @param [in] options オプション @param [out] output_wav_length 出力のバイト長 @param [out] output_wav 出力先  @returns 結果コード  \\safety{ - `synthesizer`は ::voicevox_synthesizer_new で得たものでなければならず、また ::voicevox_synthesizer_delete で解放されていてはいけない。 - `audio_query_json`はヌル終端文字列を指し、かつ&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 - `output_wav_length`は&lt;a href=\"#voicevox-core-safety\"&gt;書き込みについて有効&lt;/a&gt;でなければならない。 - `output_wav`は&lt;a href=\"#voicevox-core-safety\"&gt;書き込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_synthesizer_synthesis", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_synthesizer_synthesis(VoicevoxSynthesizer* synthesizer, byte* audio_query_json, uint style_id, VoicevoxSynthesisOptions options, nuint* output_wav_length, byte** output_wav);

        /// <summary>デフォルトのテキスト音声合成オプションを生成する @return テキスト音声合成オプション</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_make_default_tts_options", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxTtsOptions voicevox_make_default_tts_options();

        /// <summary>AquesTalk風記法から音声合成を行う。  生成したWAVデータを解放するには ::voicevox_wav_free を使う。  @param [in] synthesizer @param [in] kana AquesTalk風記法 @param [in] style_id スタイルID @param [in] options オプション @param [out] output_wav_length 出力のバイト長 @param [out] output_wav 出力先  @returns 結果コード  \\safety{ - `synthesizer`は ::voicevox_synthesizer_new で得たものでなければならず、また ::voicevox_synthesizer_delete で解放されていてはいけない。 - `kana`はヌル終端文字列を指し、かつ&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 - `output_wav_length`は&lt;a href=\"#voicevox-core-safety\"&gt;書き込みについて有効&lt;/a&gt;でなければならない。 - `output_wav`は&lt;a href=\"#voicevox-core-safety\"&gt;書き込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_synthesizer_tts_from_kana", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_synthesizer_tts_from_kana(VoicevoxSynthesizer* synthesizer, byte* kana, uint style_id, VoicevoxTtsOptions options, nuint* output_wav_length, byte** output_wav);

        /// <summary>日本語テキストから音声合成を行う。  生成したWAVデータを解放するには ::voicevox_wav_free を使う。  @param [in] synthesizer @param [in] text UTF-8の日本語テキスト @param [in] style_id スタイルID @param [in] options オプション @param [out] output_wav_length 出力のバイト長 @param [out] output_wav 出力先  @returns 結果コード  \\safety{ - `synthesizer`は ::voicevox_synthesizer_new で得たものでなければならず、また ::voicevox_synthesizer_delete で解放されていてはいけない。 - `text`はヌル終端文字列を指し、かつ&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 - `output_wav_length`は&lt;a href=\"#voicevox-core-safety\"&gt;書き込みについて有効&lt;/a&gt;でなければならない。 - `output_wav`は&lt;a href=\"#voicevox-core-safety\"&gt;書き込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_synthesizer_tts", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_synthesizer_tts(VoicevoxSynthesizer* synthesizer, byte* text, uint style_id, VoicevoxTtsOptions options, nuint* output_wav_length, byte** output_wav);

        /// <summary>JSON文字列を解放する。  @param [in] json 解放するJSON文字列  \\safety{ - `json`は以下のAPIで得られたポインタでなくてはいけない。 - ::voicevox_create_supported_devices_json - ::voicevox_synthesizer_create_metas_json - ::voicevox_synthesizer_create_audio_query - ::voicevox_synthesizer_create_accent_phrases - ::voicevox_synthesizer_replace_mora_data - ::voicevox_synthesizer_replace_phoneme_length - ::voicevox_synthesizer_replace_mora_pitch - ::voicevox_user_dict_to_json - 文字列の長さは生成時より変更されていてはならない。 - `json`は&lt;a href=\"#voicevox-core-safety\"&gt;読み込みと書き込みについて有効&lt;/a&gt;でなければならない。 - `json`は以後&lt;b&gt;ダングリングポインタ&lt;/b&gt;(_dangling pointer_)として扱われなくてはならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_json_free", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void voicevox_json_free(byte* json);

        /// <summary>WAVデータを解放する。  @param [in] wav 解放するWAVデータ  \\safety{ - `wav`は以下のAPIで得られたポインタでなくてはいけない。 - ::voicevox_synthesizer_synthesis - ::voicevox_synthesizer_tts - `wav`は&lt;a href=\"#voicevox-core-safety\"&gt;読み込みと書き込みについて有効&lt;/a&gt;でなければならない。 - `wav`は以後&lt;b&gt;ダングリングポインタ&lt;/b&gt;(_dangling pointer_)として扱われなくてはならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_wav_free", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void voicevox_wav_free(byte* wav);

        /// <summary>結果コードに対応したメッセージ文字列を取得する。  @param [in] result_code 結果コード  @returns 結果コードに対応したメッセージ文字列  \\examples{ ```c const char *actual = voicevox_error_result_to_message(VOICEVOX_RESULT_OK); const char *EXPECTED = \"エラーが発生しませんでした\"; assert(strcmp(actual, EXPECTED) == 0); ```  ```c const char *actual voicevox_error_result_to_message(VOICEVOX_RESULT_LOAD_MODEL_ERROR); const char *EXPECTED = \"modelデータ読み込みに失敗しました\"; assert(strcmp(actual, EXPECTED) == 0); ``` }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_error_result_to_message", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern byte* voicevox_error_result_to_message(VoicevoxResultCode result_code);

        /// <summary>::VoicevoxUserDictWord を最低限のパラメータで作成する。  @param [in] surface 表記 @param [in] pronunciation 読み @returns ::VoicevoxUserDictWord</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_user_dict_word_make", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxUserDictWord voicevox_user_dict_word_make(byte* surface, byte* pronunciation);

        /// <summary>ユーザー辞書をb&gt;構築&lt;/b&gt;(_construct_)する。  @returns ::VoicevoxUserDict</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_user_dict_new", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxUserDict* voicevox_user_dict_new();

        /// <summary>ユーザー辞書にファイルを読み込ませる。  @param [in] user_dict ユーザー辞書 @param [in] dict_path 読み込む辞書ファイルのパス @returns 結果コード  \\safety{ - `user_dict`は ::voicevox_user_dict_new で得たものでなければならず、また ::voicevox_user_dict_delete で解放されていてはいけない。 - `dict_path`はヌル終端文字列を指し、かつ&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_user_dict_load", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_user_dict_load(VoicevoxUserDict* user_dict, byte* dict_path);

        /// <summary>ユーザー辞書に単語を追加する。  @param [in] ユーザー辞書 @param [in] word 追加する単語 @param [out] output_word_uuid 追加した単語のUUID @returns 結果コード  # Safety @param user_dict は有効な :VoicevoxUserDict のポインタであること  \\safety{ - `user_dict`は ::voicevox_user_dict_new で得たものでなければならず、また ::voicevox_user_dict_delete で解放されていてはいけない。 - `word-&gt;surface`と`word-&gt;pronunciation`はヌル終端文字列を指し、かつ&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 - `output_word_uuid`は&lt;a href=\"#voicevox-core-safety\"&gt;書き込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_user_dict_add_word", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_user_dict_add_word(VoicevoxUserDict* user_dict, VoicevoxUserDictWord* word, void/* byte[] */* output_word_uuid);

        /// <summary>ユーザー辞書の単語を更新する。  @param [in] user_dict ユーザー辞書 @param [in] word_uuid 更新する単語のUUID @param [in] word 新しい単語のデータ @returns 結果コード  \\safety{ - `user_dict`は ::voicevox_user_dict_new で得たものでなければならず、また ::voicevox_user_dict_delete で解放されていてはいけない。 - `word_uuid`は&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 - `word-&gt;surface`と`word-&gt;pronunciation`はヌル終端文字列を指し、かつ&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_user_dict_update_word", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_user_dict_update_word(VoicevoxUserDict* user_dict, void/* byte[] */* word_uuid, VoicevoxUserDictWord* word);

        /// <summary>ユーザー辞書から単語を削除する。  @param [in] user_dict ユーザー辞書 @param [in] word_uuid 削除する単語のUUID @returns 結果コード  \\safety{ - `user_dict`は ::voicevox_user_dict_new で得たものでなければならず、また ::voicevox_user_dict_delete で解放されていてはいけない。 - `word_uuid`は&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_user_dict_remove_word", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_user_dict_remove_word(VoicevoxUserDict* user_dict, void/* byte[] */* word_uuid);

        /// <summary>ユーザー辞書の単語をJSON形式で出力する。  生成したJSON文字列を解放するには ::voicevox_json_free を使う。  @param [in] user_dict ユーザー辞書 @param [out] output_json 出力先 @returns 結果コード  \\safety{ - `user_dict`は ::voicevox_user_dict_new で得たものでなければならず、また ::voicevox_user_dict_delete で解放されていてはいけない。 - `output_json`は&lt;a href=\"#voicevox-core-safety\"&gt;書き込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_user_dict_to_json", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_user_dict_to_json(VoicevoxUserDict* user_dict, byte** output_json);

        /// <summary>他のユーザー辞書をインポートする。  @param [in] user_dict ユーザー辞書 @param [in] other_dict インポートするユーザー辞書 @returns 結果コード  \\safety{ - `user_dict`と`other_dict`は ::voicevox_user_dict_new で得たものでなければならず、また ::voicevox_user_dict_delete で解放されていてはいけない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_user_dict_import", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_user_dict_import(VoicevoxUserDict* user_dict, VoicevoxUserDict* other_dict);

        /// <summary>ユーザー辞書をファイルに保存する。  @param [in] user_dict ユーザー辞書 @param [in] path 保存先のファイルパス  \\safety{ - `user_dict`は ::voicevox_user_dict_new で得たものでなければならず、また ::voicevox_user_dict_delete で解放されていてはいけない。 - `path`はヌル終端文字列を指し、かつ&lt;a href=\"#voicevox-core-safety\"&gt;読み込みについて有効&lt;/a&gt;でなければならない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_user_dict_save", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_user_dict_save(VoicevoxUserDict* user_dict, byte* path);

        /// <summary>ユーザー辞書を&lt;b&gt;破棄&lt;/b&gt;(_destruct_)する。  @param [in] user_dict 破棄対象  \\safety{ - `user_dict`は ::voicevox_user_dict_new で得たものでなければならず、また既にこの関数で解放されていてはいけない。 }</summary>
        [DllImport(__DllName, EntryPoint = "voicevox_user_dict_delete", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void voicevox_user_dict_delete(VoicevoxUserDict* user_dict);


    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe partial struct OpenJtalkRc
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe partial struct VoicevoxInitializeOptions
    {
        public VoicevoxAccelerationMode acceleration_mode;
        public ushort cpu_num_threads;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe partial struct VoicevoxVoiceModel
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe partial struct VoicevoxSynthesizer
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe partial struct VoicevoxSynthesisOptions
    {
        [MarshalAs(UnmanagedType.U1)] public bool enable_interrogative_upspeak;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe partial struct VoicevoxTtsOptions
    {
        [MarshalAs(UnmanagedType.U1)] public bool enable_interrogative_upspeak;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe partial struct VoicevoxUserDict
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe partial struct VoicevoxUserDictWord
    {
        public byte* surface;
        public byte* pronunciation;
        public nuint accent_type;
        public VoicevoxUserDictWordType word_type;
        public uint priority;
    }


    internal enum VoicevoxResultCode : int
    {
        VOICEVOX_RESULT_OK = 0,
        VOICEVOX_RESULT_NOT_LOADED_OPENJTALK_DICT_ERROR = 1,
        VOICEVOX_RESULT_GET_SUPPORTED_DEVICES_ERROR = 3,
        VOICEVOX_RESULT_GPU_SUPPORT_ERROR = 4,
        VOICEVOX_RESULT_STYLE_NOT_FOUND_ERROR = 6,
        VOICEVOX_RESULT_MODEL_NOT_FOUND_ERROR = 7,
        VOICEVOX_RESULT_INFERENCE_ERROR = 8,
        VOICEVOX_RESULT_EXTRACT_FULL_CONTEXT_LABEL_ERROR = 11,
        VOICEVOX_RESULT_INVALID_UTF8_INPUT_ERROR = 12,
        VOICEVOX_RESULT_PARSE_KANA_ERROR = 13,
        VOICEVOX_RESULT_INVALID_AUDIO_QUERY_ERROR = 14,
        VOICEVOX_RESULT_INVALID_ACCENT_PHRASE_ERROR = 15,
        VOICEVOX_RESULT_OPEN_ZIP_FILE_ERROR = 16,
        VOICEVOX_RESULT_READ_ZIP_ENTRY_ERROR = 17,
        VOICEVOX_RESULT_MODEL_ALREADY_LOADED_ERROR = 18,
        VOICEVOX_RESULT_STYLE_ALREADY_LOADED_ERROR = 26,
        VOICEVOX_RESULT_INVALID_MODEL_DATA_ERROR = 27,
        VOICEVOX_RESULT_LOAD_USER_DICT_ERROR = 20,
        VOICEVOX_RESULT_SAVE_USER_DICT_ERROR = 21,
        VOICEVOX_RESULT_USER_DICT_WORD_NOT_FOUND_ERROR = 22,
        VOICEVOX_RESULT_USE_USER_DICT_ERROR = 23,
        VOICEVOX_RESULT_INVALID_USER_DICT_WORD_ERROR = 24,
        VOICEVOX_RESULT_INVALID_UUID_ERROR = 25,
    }

    internal enum VoicevoxAccelerationMode : int
    {
        VOICEVOX_ACCELERATION_MODE_AUTO = 0,
        VOICEVOX_ACCELERATION_MODE_CPU = 1,
        VOICEVOX_ACCELERATION_MODE_GPU = 2,
    }

    internal enum VoicevoxUserDictWordType : int
    {
        VOICEVOX_USER_DICT_WORD_TYPE_PROPER_NOUN = 0,
        VOICEVOX_USER_DICT_WORD_TYPE_COMMON_NOUN = 1,
        VOICEVOX_USER_DICT_WORD_TYPE_VERB = 2,
        VOICEVOX_USER_DICT_WORD_TYPE_ADJECTIVE = 3,
        VOICEVOX_USER_DICT_WORD_TYPE_SUFFIX = 4,
    }


}
