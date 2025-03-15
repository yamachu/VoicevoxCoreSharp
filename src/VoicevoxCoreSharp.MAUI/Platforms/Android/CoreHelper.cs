using System.Reflection;
using System.Runtime.InteropServices;

namespace VoicevoxCoreSharp.MAUI;

public static class CoreHelper
{
    private static bool _isInitialized = false;

    /// <summary>
    /// Initialize VoicevoxCore for loading voicevox_core.framework
    /// </summary>
    /// <param name="overrideVoicevoxCoreDynamicLibraryPath">if pass `null`, using iOS default framework path.</param>
    /// <exception cref="DllNotFoundException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public static void Initialize(string? overrideVoicevoxCoreDynamicLibraryPath)
    {
        if (_isInitialized)
        {
            return;
        }

        var voicevoxAssembly = AppDomain.CurrentDomain.GetAssemblies()
            .FirstOrDefault(a => a.GetName().Name == "VoicevoxCoreSharp.Core");

        if (voicevoxAssembly is null)
        {
            throw new DllNotFoundException("Cannot find assembly, VoicevoxCoreSharp.Core");
        }

        NativeLibrary.SetDllImportResolver(voicevoxAssembly, ResolveLibrary(overrideVoicevoxCoreDynamicLibraryPath));

        _isInitialized = true;
    }

    private static DllImportResolver ResolveLibrary(string? overrideVoicevoxCoreDynamicLibraryPath)
    {
        return ResolveLibraryImpl;

        IntPtr ResolveLibraryImpl(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (libraryName != "voicevox_core")
            {
                return IntPtr.Zero;
            }

            try
            {
                var nativeLibDir = Android.App.Application.Context.ApplicationInfo?.NativeLibraryDir ?? string.Empty;
                var corePath = overrideVoicevoxCoreDynamicLibraryPath ?? Path.Combine(nativeLibDir, "libvoicevox_core.so");

                if (NativeLibrary.TryLoad(corePath, out var handle))
                {
                    return handle;
                }
            }
            catch (Exception ex)
            {
                // Java.Lang.JavaSystem.LoadLibrary("voicevox_core");
                throw;
            }

            return IntPtr.Zero;
        }
    }
}
