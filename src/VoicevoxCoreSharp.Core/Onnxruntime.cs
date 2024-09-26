using System;
using System.Runtime.InteropServices;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Native;
using VoicevoxCoreSharp.Core.Struct;

namespace VoicevoxCoreSharp.Core
{
    internal class OnnxruntimeHandle : SafeHandle
    {
        public OnnxruntimeHandle(IntPtr intPtr) : base(IntPtr.Zero, true)
        {
            this.SetHandle(intPtr);
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            unsafe
            {
                handle = IntPtr.Zero;
            }
            return true;
        }


        public static unsafe implicit operator VoicevoxOnnxruntime*(OnnxruntimeHandle handle) => (VoicevoxOnnxruntime*)handle.handle.ToPointer();
    }

    public class Onnxruntime : IDisposable
    {
        internal OnnxruntimeHandle Handle { get; private set; }
        private bool _disposed = false;

        internal unsafe Onnxruntime(VoicevoxOnnxruntime* onnxruntimeHandle)
        {
            Handle = new OnnxruntimeHandle(new IntPtr(onnxruntimeHandle));
        }

        public static string GetVersionedFilename()
        {
            unsafe
            {
                return StringConvertCompat.ToUTF8String(CoreUnsafe.voicevox_get_onnxruntime_lib_versioned_filename());
            }
        }

        public static string GetUnversionedFilename()
        {
            unsafe
            {
                return StringConvertCompat.ToUTF8String(CoreUnsafe.voicevox_get_onnxruntime_lib_unversioned_filename());
            }
        }

        public static bool TryGet(out Onnxruntime? Onnxruntime)
        {
            unsafe
            {
                var p = CoreUnsafe.voicevox_onnxruntime_get();
                if ((IntPtr)p == IntPtr.Zero)
                {
                    Onnxruntime = null;
                    return false;
                }
                Onnxruntime = new Onnxruntime(p);
                return true;
            }
        }

        public static ResultCode LoadOnce(LoadOnnxruntimeOptions options, out Onnxruntime Onnxruntime)
        {
            unsafe
            {
                var p = (VoicevoxOnnxruntime*)IntPtr.Zero.ToPointer();
                var result = CoreUnsafe.voicevox_onnxruntime_load_once(options.ToNative(), &p).FromNative();
                if (result == ResultCode.RESULT_OK)
                {
                    Onnxruntime = new Onnxruntime(p);
                }
                else
                {
                    Onnxruntime = new Onnxruntime(null);
                }

                return result;
            }
        }

        /// <summary>
        /// ONNX Runtimeを初期化する。
        /// 一度成功したら以後は同じ参照を返す。
        /// iOSでのみ利用可能。
        /// </summary>
        /// <param name="Onnxruntime"></param>
        /// <returns></returns>
        public static ResultCode InitOnce(out Onnxruntime Onnxruntime)
        {
            unsafe
            {
                var p = (VoicevoxOnnxruntime*)IntPtr.Zero.ToPointer();
                var result = CoreUnsafe.voicevox_onnxruntime_init_once(&p).FromNative();
                if (result == ResultCode.RESULT_OK)
                {
                    Onnxruntime = new Onnxruntime(p);
                }
                else
                {
                    Onnxruntime = new Onnxruntime(null);
                }

                return result;
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
