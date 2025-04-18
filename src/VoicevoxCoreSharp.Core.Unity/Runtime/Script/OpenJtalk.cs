﻿using System;
using System.Runtime.InteropServices;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Native;
using VoicevoxCoreSharp.Core.Struct;

namespace VoicevoxCoreSharp.Core
{
    internal class OpenJtalkRcHandle : SafeHandle
    {
        public OpenJtalkRcHandle(IntPtr intPtr) : base(IntPtr.Zero, true)
        {
            this.SetHandle(intPtr);
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            unsafe
            {
                CoreUnsafe.voicevox_open_jtalk_rc_delete((OpenJtalkRc*)handle.ToPointer());
                handle = IntPtr.Zero;
            }
            return true;
        }


        public static unsafe implicit operator OpenJtalkRc*(OpenJtalkRcHandle handle) => (OpenJtalkRc*)handle.handle.ToPointer();
    }

    public class OpenJtalk : IDisposable
    {
        internal OpenJtalkRcHandle Handle { get; private set; }
        private bool _disposed = false;

        private unsafe OpenJtalk(OpenJtalkRc* rcHandle)
        {
            Handle = new OpenJtalkRcHandle(new IntPtr(rcHandle));
        }

        public static ResultCode New(string openJtalkDicDir, out OpenJtalk openJtalk)
        {
            unsafe
            {
                var p = (OpenJtalkRc*)IntPtr.Zero.ToPointer();
                fixed (byte* ptr = System.Text.Encoding.UTF8.GetBytes(openJtalkDicDir))
                {
                    var result = CoreUnsafe.voicevox_open_jtalk_rc_new(ptr, &p).FromNative();
                    if (result == ResultCode.RESULT_OK)
                    {
                        openJtalk = new OpenJtalk(p);
                    }
                    else
                    {
                        openJtalk = new OpenJtalk(null);
                    }

                    return result;
                }
            }
        }

        public ResultCode Analyze(string text, out string? outputAccentPhrasesJson)
        {
            unsafe
            {
                byte* output;
                fixed (byte* ptr = System.Text.Encoding.UTF8.GetBytes(text))
                {
                    var result = CoreUnsafe.voicevox_open_jtalk_rc_analyze((OpenJtalkRc*)Handle, ptr, &output);
                    if (result == VoicevoxResultCode.VOICEVOX_RESULT_OK)
                    {
                        outputAccentPhrasesJson = StringConvertCompat.ToUTF8String(output);
                        CoreUnsafe.voicevox_json_free(output);
                    }
                    else
                    {
                        outputAccentPhrasesJson = null;
                    }
                    return result.FromNative();
                }
            }
        }

        public ResultCode UseUserDict(UserDict userDict)
        {
            unsafe
            {
                return CoreUnsafe.voicevox_open_jtalk_rc_use_user_dict((OpenJtalkRc*)Handle, (VoicevoxUserDict*)userDict.Handle).FromNative();
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
