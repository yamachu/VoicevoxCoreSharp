use std::error::Error;

fn main() -> Result<(), Box<dyn Error>> {
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

    // let ver = "v0_15";
    // csbindgen::Builder::default()
    //     .input_extern_file("./voicevox_core/crates/voicevox_core_c_api/src/result_code.rs")
    //     .input_extern_file("./voicevox_core/crates/voicevox_core_c_api/src/lib.rs")
    //     .csharp_dll_name("voicevox_core")
    //     .csharp_class_name("CoreUnsafe")
    //     .csharp_namespace(format!("VoicevoxCoreSharp.Core.Compat.{}.Native", ver))
    //     // TODO: MAUI iOS
    //     // see: https://github.com/xamarin/xamarin-macios/issues/17418
    //     .csharp_dll_name_if("UNITY_IOS && !UNITY_EDITOR", "__Internal")
    //     .csharp_emit_as("delegate")
    //     .csharp_use_function_pointer(false)
    //     .generate_csharp_file(format!("../src/VoicevoxCoreSharp.Core.Compat/{}/Native/CoreUnsafe.g.cs", ver))
    //     .unwrap();

    Ok(())
}
