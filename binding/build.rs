use std::error::Error;

fn main() -> Result<(), Box<dyn Error>> {
    // latest
    csbindgen::Builder::default()
        .input_extern_file("./voicevox_core/crates/voicevox_core_c_api/src/result_code.rs")
        .input_extern_file("./voicevox_core/crates/voicevox_core_c_api/src/lib.rs")
        .csharp_dll_name("voicevox_core")
        .csharp_class_name("CoreUnsafe")
        .csharp_namespace("VoicevoxCoreSharp.Core.Native")
        // TODO: MAUI iOS
        // see: https://github.com/xamarin/xamarin-macios/issues/17418
        .csharp_dll_name_if("UNITY_IOS && !UNITY_EDITOR", "__Internal")
        .generate_csharp_file("../src/VoicevoxCoreSharp.Core/Native/CoreUnsafe.g.cs")
        .unwrap();

    // legacy engine
    // 0.12 ~ 0.14
    csbindgen::Builder::default()
        .input_extern_file("./voicevox_core/crates/voicevox_core_c_api/src/compatible_engine.rs")
        .csharp_dll_name("voicevox_core")
        .csharp_class_name("CoreUnsafe")
        .csharp_namespace("VoicevoxCoreSharp.Core.Legacy.Native")
        // TODO: MAUI iOS
        // see: https://github.com/xamarin/xamarin-macios/issues/17418
        .csharp_dll_name_if("UNITY_IOS && !UNITY_EDITOR", "__Internal")
        .generate_csharp_file("../src/VoicevoxCoreSharp.Core.Legacy/Native/CoreUnsafe.g.cs")
        .unwrap();

    Ok(())
}
