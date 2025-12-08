using System;
using VoicevoxCoreSharp.Core.Native;

namespace VoicevoxCoreSharp.Core.Enum
{
    /// <summary>
    /// 処理結果を示す結果コード。
    /// </summary>
    public enum ResultCode : int
    {
        /// <summary>
        /// 成功
        /// </summary>
        RESULT_OK = 0,
        /// <summary>
        /// open_jtalk辞書ファイルが読み込まれていない
        /// </summary>
        RESULT_NOT_LOADED_OPENJTALK_DICT_ERROR = 1,
        /// <summary>
        /// サポートされているデバイス情報取得に失敗した
        /// </summary>
        RESULT_GET_SUPPORTED_DEVICES_ERROR = 3,
        /// <summary>
        /// GPUモードがサポートされていない
        /// </summary>
        RESULT_GPU_SUPPORT_ERROR = 4,
        /// <summary>
        /// 推論ライブラリのロードまたは初期化ができなかった
        /// </summary>
        RESULT_INIT_INFERENCE_RUNTIME_ERROR = 29,
        /// <summary>
        /// スタイルIDに対するスタイルが見つからなかった
        /// </summary>
        RESULT_STYLE_NOT_FOUND_ERROR = 6,
        /// <summary>
        /// 音声モデルIDに対する音声モデルが見つからなかった
        /// </summary>
        RESULT_MODEL_NOT_FOUND_ERROR = 7,
        /// <summary>
        /// 推論に失敗した
        /// </summary>
        RESULT_RUN_MODEL_ERROR = 8,
        /// <summary>
        /// 入力テキストの解析に失敗した
        /// </summary>
        RESULT_ANALYZE_TEXT_ERROR = 11,
        /// <summary>
        /// 無効なutf8文字列が入力された
        /// </summary>
        RESULT_INVALID_UTF8_INPUT_ERROR = 12,
        /// <summary>
        /// AquesTalk風記法のテキストの解析に失敗した
        /// </summary>
        RESULT_PARSE_KANA_ERROR = 13,
        /// <summary>
        /// 無効なAudioQuery
        /// </summary>
        RESULT_INVALID_AUDIO_QUERY_ERROR = 14,
        /// <summary>
        /// 無効なAccentPhrase
        /// </summary>
        RESULT_INVALID_ACCENT_PHRASE_ERROR = 15,
        /// <summary>
        /// ZIPファイルを開くことに失敗した
        /// </summary>
        RESULT_OPEN_ZIP_FILE_ERROR = 16,
        /// <summary>
        /// ZIP内のファイルが読めなかった
        /// </summary>
        RESULT_READ_ZIP_ENTRY_ERROR = 17,
        /// <summary>
        /// モデルの形式が不正
        /// </summary>
        RESULT_INVALID_MODEL_HEADER_ERROR = 28,
        /// <summary>
        /// すでに読み込まれている音声モデルを読み込もうとした
        /// </summary>
        RESULT_MODEL_ALREADY_LOADED_ERROR = 18,
        /// <summary>
        /// すでに読み込まれているスタイルを読み込もうとした
        /// </summary>
        RESULT_STYLE_ALREADY_LOADED_ERROR = 26,
        /// <summary>
        /// 無効なモデルデータ
        /// </summary>
        RESULT_INVALID_MODEL_DATA_ERROR = 27,
        /// <summary>
        /// ユーザー辞書を読み込めなかった
        /// </summary>
        RESULT_LOAD_USER_DICT_ERROR = 20,
        /// <summary>
        /// ユーザー辞書を書き込めなかった
        /// </summary>
        RESULT_SAVE_USER_DICT_ERROR = 21,
        /// <summary>
        /// ユーザー辞書に単語が見つからなかった
        /// </summary>
        RESULT_USER_DICT_WORD_NOT_FOUND_ERROR = 22,
        /// <summary>
        /// OpenJTalkのユーザー辞書の設定に失敗した
        /// </summary>
        RESULT_USE_USER_DICT_ERROR = 23,
        /// <summary>
        /// ユーザー辞書の単語のバリデーションに失敗した
        /// </summary>
        RESULT_INVALID_USER_DICT_WORD_ERROR = 24,
        /// <summary>
        /// UUIDの変換に失敗した
        /// </summary>
        RESULT_INVALID_UUID_ERROR = 25,
        /// <summary>
        /// 無効なMora
        /// </summary>
        RESULT_INVALID_MORA_ERROR = 30,
    }

    public static class ResultCodeExt
    {
        internal static ResultCode FromNative(this VoicevoxResultCode code)
        {
#pragma warning disable CS8524
            return code switch
            {
                VoicevoxResultCode.VOICEVOX_RESULT_OK => ResultCode.RESULT_OK,
                VoicevoxResultCode.VOICEVOX_RESULT_NOT_LOADED_OPENJTALK_DICT_ERROR => ResultCode.RESULT_NOT_LOADED_OPENJTALK_DICT_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_GET_SUPPORTED_DEVICES_ERROR => ResultCode.RESULT_GET_SUPPORTED_DEVICES_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_GPU_SUPPORT_ERROR => ResultCode.RESULT_GPU_SUPPORT_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_INIT_INFERENCE_RUNTIME_ERROR => ResultCode.RESULT_INIT_INFERENCE_RUNTIME_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_STYLE_NOT_FOUND_ERROR => ResultCode.RESULT_STYLE_NOT_FOUND_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_MODEL_NOT_FOUND_ERROR => ResultCode.RESULT_MODEL_NOT_FOUND_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_RUN_MODEL_ERROR => ResultCode.RESULT_RUN_MODEL_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_ANALYZE_TEXT_ERROR => ResultCode.RESULT_ANALYZE_TEXT_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_INVALID_UTF8_INPUT_ERROR => ResultCode.RESULT_INVALID_UTF8_INPUT_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_PARSE_KANA_ERROR => ResultCode.RESULT_PARSE_KANA_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_INVALID_AUDIO_QUERY_ERROR => ResultCode.RESULT_INVALID_AUDIO_QUERY_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_INVALID_ACCENT_PHRASE_ERROR => ResultCode.RESULT_INVALID_ACCENT_PHRASE_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_OPEN_ZIP_FILE_ERROR => ResultCode.RESULT_OPEN_ZIP_FILE_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_READ_ZIP_ENTRY_ERROR => ResultCode.RESULT_READ_ZIP_ENTRY_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_INVALID_MODEL_HEADER_ERROR => ResultCode.RESULT_INVALID_MODEL_HEADER_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_MODEL_ALREADY_LOADED_ERROR => ResultCode.RESULT_MODEL_ALREADY_LOADED_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_STYLE_ALREADY_LOADED_ERROR => ResultCode.RESULT_STYLE_ALREADY_LOADED_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_INVALID_MODEL_DATA_ERROR => ResultCode.RESULT_INVALID_MODEL_DATA_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_LOAD_USER_DICT_ERROR => ResultCode.RESULT_LOAD_USER_DICT_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_SAVE_USER_DICT_ERROR => ResultCode.RESULT_SAVE_USER_DICT_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_USER_DICT_WORD_NOT_FOUND_ERROR => ResultCode.RESULT_USER_DICT_WORD_NOT_FOUND_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_USE_USER_DICT_ERROR => ResultCode.RESULT_USE_USER_DICT_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_INVALID_USER_DICT_WORD_ERROR => ResultCode.RESULT_INVALID_USER_DICT_WORD_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_INVALID_UUID_ERROR => ResultCode.RESULT_INVALID_UUID_ERROR,
                VoicevoxResultCode.VOICEVOX_RESULT_INVALID_MORA_ERROR => ResultCode.RESULT_INVALID_MORA_ERROR,
            };
#pragma warning restore CS8524
        }

        internal static VoicevoxResultCode ToNative(this ResultCode code)
        {
#pragma warning disable CS8524
            return code switch
            {
                ResultCode.RESULT_OK => VoicevoxResultCode.VOICEVOX_RESULT_OK,
                ResultCode.RESULT_NOT_LOADED_OPENJTALK_DICT_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_NOT_LOADED_OPENJTALK_DICT_ERROR,
                ResultCode.RESULT_GET_SUPPORTED_DEVICES_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_GET_SUPPORTED_DEVICES_ERROR,
                ResultCode.RESULT_GPU_SUPPORT_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_GPU_SUPPORT_ERROR,
                ResultCode.RESULT_INIT_INFERENCE_RUNTIME_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_INIT_INFERENCE_RUNTIME_ERROR,
                ResultCode.RESULT_STYLE_NOT_FOUND_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_STYLE_NOT_FOUND_ERROR,
                ResultCode.RESULT_MODEL_NOT_FOUND_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_MODEL_NOT_FOUND_ERROR,
                ResultCode.RESULT_RUN_MODEL_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_RUN_MODEL_ERROR,
                ResultCode.RESULT_ANALYZE_TEXT_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_ANALYZE_TEXT_ERROR,
                ResultCode.RESULT_INVALID_UTF8_INPUT_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_INVALID_UTF8_INPUT_ERROR,
                ResultCode.RESULT_PARSE_KANA_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_PARSE_KANA_ERROR,
                ResultCode.RESULT_INVALID_AUDIO_QUERY_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_INVALID_AUDIO_QUERY_ERROR,
                ResultCode.RESULT_INVALID_ACCENT_PHRASE_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_INVALID_ACCENT_PHRASE_ERROR,
                ResultCode.RESULT_OPEN_ZIP_FILE_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_OPEN_ZIP_FILE_ERROR,
                ResultCode.RESULT_READ_ZIP_ENTRY_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_READ_ZIP_ENTRY_ERROR,
                ResultCode.RESULT_INVALID_MODEL_HEADER_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_INVALID_MODEL_HEADER_ERROR,
                ResultCode.RESULT_MODEL_ALREADY_LOADED_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_MODEL_ALREADY_LOADED_ERROR,
                ResultCode.RESULT_STYLE_ALREADY_LOADED_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_STYLE_ALREADY_LOADED_ERROR,
                ResultCode.RESULT_INVALID_MODEL_DATA_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_INVALID_MODEL_DATA_ERROR,
                ResultCode.RESULT_LOAD_USER_DICT_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_LOAD_USER_DICT_ERROR,
                ResultCode.RESULT_SAVE_USER_DICT_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_SAVE_USER_DICT_ERROR,
                ResultCode.RESULT_USER_DICT_WORD_NOT_FOUND_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_USER_DICT_WORD_NOT_FOUND_ERROR,
                ResultCode.RESULT_USE_USER_DICT_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_USE_USER_DICT_ERROR,
                ResultCode.RESULT_INVALID_USER_DICT_WORD_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_INVALID_USER_DICT_WORD_ERROR,
                ResultCode.RESULT_INVALID_UUID_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_INVALID_UUID_ERROR,
                ResultCode.RESULT_INVALID_MORA_ERROR => VoicevoxResultCode.VOICEVOX_RESULT_INVALID_MORA_ERROR,
            };
#pragma warning restore CS8524
        }

        public static unsafe string ToMessage(this ResultCode code)
        {
            return StringConvertCompat.ToUTF8String(CoreUnsafe.voicevox_error_result_to_message(code.ToNative()));
        }
    }
}
