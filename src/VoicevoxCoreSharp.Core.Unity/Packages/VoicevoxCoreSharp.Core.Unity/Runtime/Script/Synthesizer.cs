using System;
using System.Runtime.InteropServices;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Native;
using VoicevoxCoreSharp.Core.Struct;

namespace VoicevoxCoreSharp.Core
{
    internal class SynthesizerHandle : SafeHandle
    {
        public SynthesizerHandle(IntPtr intPtr) : base(IntPtr.Zero, true)
        {
            this.SetHandle(intPtr);
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            unsafe
            {
                CoreUnsafe.voicevox_synthesizer_delete((VoicevoxSynthesizer*)handle.ToPointer());
                handle = IntPtr.Zero;
            }
            return true;
        }


        public static unsafe implicit operator VoicevoxSynthesizer*(SynthesizerHandle handle) => (VoicevoxSynthesizer*)handle.handle.ToPointer();
    }

    public class Synthesizer : IDisposable
    {
        internal SynthesizerHandle Handle { get; private set; }
        private bool _disposed = false;

        private unsafe Synthesizer(VoicevoxSynthesizer* synthesizerHandle)
        {
            Handle = new SynthesizerHandle(new IntPtr(synthesizerHandle));
        }

        public static ResultCode New(Onnxruntime onnxruntime, OpenJtalk openJtalk, InitializeOptions options, out Synthesizer synthesizer)
        {
            unsafe
            {
                var nativeOptions = options.ToNative();

                var p = (VoicevoxSynthesizer*)IntPtr.Zero.ToPointer();
                var result = CoreUnsafe.voicevox_synthesizer_new((VoicevoxOnnxruntime*)onnxruntime.Handle, (OpenJtalkRc*)openJtalk.Handle, nativeOptions, &p).FromNative();
                if (result == ResultCode.RESULT_OK)
                {
                    synthesizer = new Synthesizer(p);
                }
                else
                {
                    synthesizer = new Synthesizer(null);
                }

                return result;
            }
        }

        public ResultCode LoadVoiceModel(VoiceModel voiceModel)
        {
            unsafe
            {
                return CoreUnsafe.voicevox_synthesizer_load_voice_model((VoicevoxSynthesizer*)Handle, (VoicevoxVoiceModel*)voiceModel.Handle).FromNative();
            }
        }

        public ResultCode UnloadVoiceModel(string modelId)
        {
            unsafe
            {
                fixed (byte* ptr = System.Text.Encoding.UTF8.GetBytes(modelId))
                {
                    return CoreUnsafe.voicevox_synthesizer_unload_voice_model((VoicevoxSynthesizer*)Handle, ptr).FromNative();
                }
            }
        }

        public bool IsGpuMode
        {
            get
            {
                unsafe
                {
                    return CoreUnsafe.voicevox_synthesizer_is_gpu_mode((VoicevoxSynthesizer*)Handle);
                }
            }
        }

        public bool IsLoadedVoiceModel(string modelId)
        {
            unsafe
            {
                fixed (byte* ptr = System.Text.Encoding.UTF8.GetBytes(modelId))
                {
                    return CoreUnsafe.voicevox_synthesizer_is_loaded_voice_model((VoicevoxSynthesizer*)Handle, ptr);
                }
            }
        }

        public string MetasJson
        {
            get
            {
                unsafe
                {
                    var ptr = CoreUnsafe.voicevox_synthesizer_create_metas_json((VoicevoxSynthesizer*)Handle);
                    return StringConvertCompat.ToUTF8String(ptr);
                }
            }
        }

        public ResultCode CreateAudioQuery(string text, uint styleId, out string? audioQueryJson)
        {
            unsafe
            {
                fixed (byte* ptr = System.Text.Encoding.UTF8.GetBytes(text))
                {
                    byte* resultJsonPtr;

                    var result = CoreUnsafe.voicevox_synthesizer_create_audio_query((VoicevoxSynthesizer*)Handle, ptr, styleId, &resultJsonPtr).FromNative();
                    if (result == ResultCode.RESULT_OK)
                    {
                        audioQueryJson = StringConvertCompat.ToUTF8String(resultJsonPtr);
                        CoreUnsafe.voicevox_json_free(resultJsonPtr);
                    }
                    else
                    {
                        audioQueryJson = null;
                    }

                    return result;
                }
            }
        }

        public ResultCode CreateAudioQueryFromKana(string kana, uint styleId, out string? audioQueryJson)
        {
            unsafe
            {
                fixed (byte* ptr = System.Text.Encoding.UTF8.GetBytes(kana))
                {
                    byte* resultJsonPtr;

                    var result = CoreUnsafe.voicevox_synthesizer_create_audio_query_from_kana((VoicevoxSynthesizer*)Handle, ptr, styleId, &resultJsonPtr).FromNative();
                    if (result == ResultCode.RESULT_OK)
                    {
                        audioQueryJson = StringConvertCompat.ToUTF8String(resultJsonPtr);
                        CoreUnsafe.voicevox_json_free(resultJsonPtr);
                    }
                    else
                    {
                        audioQueryJson = null;
                    }

                    return result;
                }
            }
        }

        public ResultCode CreateAccentPhrases(string text, uint styleId, out string? accentPhrasesJson)
        {
            unsafe
            {
                fixed (byte* ptr = System.Text.Encoding.UTF8.GetBytes(text))
                {
                    byte* resultJsonPtr;

                    var result = CoreUnsafe.voicevox_synthesizer_create_accent_phrases((VoicevoxSynthesizer*)Handle, ptr, styleId, &resultJsonPtr).FromNative();
                    if (result == ResultCode.RESULT_OK)
                    {
                        accentPhrasesJson = StringConvertCompat.ToUTF8String(resultJsonPtr);
                        CoreUnsafe.voicevox_json_free(resultJsonPtr);
                    }
                    else
                    {
                        accentPhrasesJson = null;
                    }

                    return result;
                }
            }
        }

        public ResultCode CreateAccentPhrasesFromKana(string kana, uint styleId, out string? accentPhrasesJson)
        {
            unsafe
            {
                fixed (byte* ptr = System.Text.Encoding.UTF8.GetBytes(kana))
                {
                    byte* resultJsonPtr;

                    var result = CoreUnsafe.voicevox_synthesizer_create_accent_phrases_from_kana((VoicevoxSynthesizer*)Handle, ptr, styleId, &resultJsonPtr).FromNative();
                    if (result == ResultCode.RESULT_OK)
                    {
                        accentPhrasesJson = StringConvertCompat.ToUTF8String(resultJsonPtr);
                        CoreUnsafe.voicevox_json_free(resultJsonPtr);
                    }
                    else
                    {
                        accentPhrasesJson = null;
                    }

                    return result;
                }
            }
        }

        public ResultCode ReplaceMoraData(string accentPhrasesJson, uint styleId, out string? outputAccentPhrasesJson)
        {
            unsafe
            {
                fixed (byte* ptr = System.Text.Encoding.UTF8.GetBytes(accentPhrasesJson))
                {
                    byte* resultJsonPtr;

                    var result = CoreUnsafe.voicevox_synthesizer_replace_mora_data((VoicevoxSynthesizer*)Handle, ptr, styleId, &resultJsonPtr).FromNative();
                    if (result == ResultCode.RESULT_OK)
                    {
                        outputAccentPhrasesJson = StringConvertCompat.ToUTF8String(resultJsonPtr);
                        CoreUnsafe.voicevox_json_free(resultJsonPtr);
                    }
                    else
                    {
                        outputAccentPhrasesJson = null;
                    }

                    return result;
                }
            }
        }

        public ResultCode ReplacePhonemeLength(string accentPhrasesJson, uint styleId, out string? outputAccentPhrasesJson)
        {
            unsafe
            {
                fixed (byte* ptr = System.Text.Encoding.UTF8.GetBytes(accentPhrasesJson))
                {
                    byte* resultJsonPtr;

                    var result = CoreUnsafe.voicevox_synthesizer_replace_phoneme_length((VoicevoxSynthesizer*)Handle, ptr, styleId, &resultJsonPtr).FromNative();
                    if (result == ResultCode.RESULT_OK)
                    {
                        outputAccentPhrasesJson = StringConvertCompat.ToUTF8String(resultJsonPtr);
                        CoreUnsafe.voicevox_json_free(resultJsonPtr);
                    }
                    else
                    {
                        outputAccentPhrasesJson = null;
                    }

                    return result;
                }
            }
        }

        public ResultCode ReplaceMoraPitch(string accentPhrasesJson, uint styleId, out string? outputAccentPhrasesJson)
        {
            unsafe
            {
                fixed (byte* ptr = System.Text.Encoding.UTF8.GetBytes(accentPhrasesJson))
                {
                    byte* resultJsonPtr;

                    var result = CoreUnsafe.voicevox_synthesizer_replace_mora_pitch((VoicevoxSynthesizer*)Handle, ptr, styleId, &resultJsonPtr).FromNative();
                    if (result == ResultCode.RESULT_OK)
                    {
                        outputAccentPhrasesJson = StringConvertCompat.ToUTF8String(resultJsonPtr);
                        CoreUnsafe.voicevox_json_free(resultJsonPtr);
                    }
                    else
                    {
                        outputAccentPhrasesJson = null;
                    }

                    return result;
                }
            }
        }

        public ResultCode Synthesis(string audioQueryJson, uint styleId, SynthesisOptions options, out nuint outputWavLength, out byte[]? outputWav)
        {
            unsafe
            {
                fixed (byte* ptr = System.Text.Encoding.UTF8.GetBytes(audioQueryJson))
                {
                    fixed (nuint* ptr2 = &outputWavLength)
                    {
                        var nativeOptions = options.ToNative();
                        byte* resultWavPtr;

                        var result = CoreUnsafe.voicevox_synthesizer_synthesis((VoicevoxSynthesizer*)Handle, ptr, styleId, nativeOptions, ptr2, &resultWavPtr);
                        if (result == VoicevoxResultCode.VOICEVOX_RESULT_OK)
                        {
                            var i = 0;
                            var outputWavLengthInt = (int)outputWavLength;
                            var outputWavTmp = new byte[outputWavLength];
                            while (i < outputWavLengthInt)
                            {
                                outputWavTmp[i] = resultWavPtr[i];
                                i++;
                            }
                            outputWav = outputWavTmp;
                            CoreUnsafe.voicevox_wav_free(resultWavPtr);
                        }
                        else
                        {
                            outputWav = null;
                        }

                        return result.FromNative();
                    }

                }
            }
        }

        public ResultCode Tts(string text, uint styleId, TtsOptions options, out nuint outputWavLength, out byte[]? outputWav)
        {
            unsafe
            {
                fixed (byte* ptr = System.Text.Encoding.UTF8.GetBytes(text))
                {
                    fixed (nuint* ptr2 = &outputWavLength)
                    {
                        var nativeOptions = options.ToNative();
                        byte* resultWavPtr;

                        var result = CoreUnsafe.voicevox_synthesizer_tts((VoicevoxSynthesizer*)Handle, ptr, styleId, nativeOptions, ptr2, &resultWavPtr);
                        if (result == VoicevoxResultCode.VOICEVOX_RESULT_OK)
                        {
                            var i = 0;
                            var outputWavLengthInt = (int)outputWavLength;
                            var outputWavTmp = new byte[outputWavLength];
                            while (i < outputWavLengthInt)
                            {
                                outputWavTmp[i] = resultWavPtr[i];
                                i++;
                            }
                            outputWav = outputWavTmp;
                            CoreUnsafe.voicevox_wav_free(resultWavPtr);
                        }
                        else
                        {
                            outputWav = null;
                        }

                        return result.FromNative();
                    }

                }
            }
        }

        public ResultCode TtsFromKana(string kana, uint styleId, TtsOptions options, out nuint outputWavLength, out byte[]? outputWav)
        {
            unsafe
            {
                fixed (byte* ptr = System.Text.Encoding.UTF8.GetBytes(kana))
                {
                    fixed (nuint* ptr2 = &outputWavLength)
                    {
                        var nativeOptions = options.ToNative();
                        byte* resultWavPtr;

                        var result = CoreUnsafe.voicevox_synthesizer_tts_from_kana((VoicevoxSynthesizer*)Handle, ptr, styleId, nativeOptions, ptr2, &resultWavPtr);
                        if (result == VoicevoxResultCode.VOICEVOX_RESULT_OK)
                        {
                            var i = 0;
                            var outputWavLengthInt = (int)outputWavLength;
                            var outputWavTmp = new byte[outputWavLength];
                            while (i < outputWavLengthInt)
                            {
                                outputWavTmp[i] = resultWavPtr[i];
                                i++;
                            }
                            outputWav = outputWavTmp;
                            CoreUnsafe.voicevox_wav_free(resultWavPtr);
                        }
                        else
                        {
                            outputWav = null;
                        }

                        return result.FromNative();
                    }

                }
            }
        }

        public Onnxruntime? Onnxruntime()
        {
            unsafe
            {
                var p = CoreUnsafe.voicevox_synthesizer_get_onnxruntime((VoicevoxSynthesizer*)Handle);
                if ((IntPtr)p == IntPtr.Zero)
                {
                    return null;
                }
                return new Onnxruntime(p);
            }

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (Handle != null && !Handle.IsInvalid)
                    {
                        Handle.Dispose();
                    }
                }

                _disposed = true;
            }
        }
    }
}
