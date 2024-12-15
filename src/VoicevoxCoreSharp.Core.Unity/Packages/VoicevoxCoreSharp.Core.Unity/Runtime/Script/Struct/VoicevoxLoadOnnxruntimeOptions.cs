using System;
using System.Text;
using VoicevoxCoreSharp.Core.Native;

namespace VoicevoxCoreSharp.Core.Struct
{
    /// <summary>
    /// voicevox_onnxruntime_load_once のオプション。
    /// </summary>
    public struct LoadOnnxruntimeOptions
    {
        public LoadOnnxruntimeOptions(string filename)
        {
            Filename = filename;
        }

        /// <summary>
        /// `dlopen`/[`LoadLibraryExW`](https://learn.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-loadlibraryexw)の引数に使われる。デフォルトは ::voicevox_get_onnxruntime_lib_versioned_filename と同じ。
        /// </summary>
        public string Filename { get; }

        public static LoadOnnxruntimeOptions Default()
        {
            return LoadOnnxruntimeOptionsDefault.Value;
        }
    }

    internal static class LoadOnnxruntimeOptionsDefault
    {
        public static readonly LoadOnnxruntimeOptions Value = CoreUnsafe.voicevox_make_default_load_onnxruntime_options().FromNative();
    }

    internal static class LoadOnnxruntimeOptionsExt
    {
        internal static LoadOnnxruntimeOptions FromNative(this VoicevoxLoadOnnxruntimeOptions loadOnnxruntimeOptions)
        {
            unsafe
            {
                return new LoadOnnxruntimeOptions(
                    StringConvertCompat.ToUTF8String(loadOnnxruntimeOptions.filename)
                );
            }
        }

        internal static VoicevoxLoadOnnxruntimeOptions ToNative(this LoadOnnxruntimeOptions loadOnnxruntimeOptions)
        {
            unsafe
            {
                fixed (byte* ptrFilename = Encoding.UTF8.GetBytes(loadOnnxruntimeOptions.Filename))
                {
                    return new VoicevoxLoadOnnxruntimeOptions
                    {
                        filename = ptrFilename
                    };
                }
            }
        }
    }
}
