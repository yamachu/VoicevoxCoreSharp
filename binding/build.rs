use std::error::Error;

fn main() -> Result<(), Box<dyn Error>> {
    // bindgen::Builder::default()
    //     .clang_args(&["-x", "c++"])
    //     .header("voicevox_core/crates/voicevox_core_c_api/include/voicevox_core.h")
    //     .default_enum_style(bindgen::EnumVariation::Rust {
    //         non_exhaustive: false,
    //     })
    //     .generate()
    //     .unwrap()
    //     .write_to_file("./generated/voicevox_core.g.rs")
    //     .unwrap();

    // csbindgen::Builder::default()
    //     .input_bindgen_file("generated/voicevox_core.g.rs")
    //     .csharp_dll_name("voicevox_core")
    //     .csharp_class_name("CoreUnsafe")
    //     .csharp_namespace("VoicevoxCoreSharp.Core.Native")
    //     // abortっていうのが勝手に足されてしまうので、スキップしている
    //     .method_filter(|method| !method.starts_with("abort"))
    //     // TODO: MAUI iOS
    //     // see: https://github.com/xamarin/xamarin-macios/issues/17418
    //     .csharp_dll_name_if("UNITY_IOS && !UNITY_EDITOR", "__Internal")
    //     .generate_to_file(
    //         "generated/voicevox_core_ffi.g.rs",
    //         "../src/VoicevoxCoreSharp.Core/Native/CoreUnsafe.g.cs",
    //     )
    //     .unwrap();

    csbindgen::Builder::default()
        .input_extern_file("./voicevox_core/crates/voicevox_core_c_api/src/result_code.rs")
        .input_extern_file("./voicevox_core/crates/voicevox_core_c_api/src/lib.rs")
        // 中身を C側 で触らない struct をここに列挙する
        .rust_as_empty_struct("OpenJtalkRc")
        .rust_as_empty_struct("VoicevoxSynthesizer")
        .rust_as_empty_struct("VoicevoxUserDict")
        .rust_as_empty_struct("VoicevoxVoiceModel")
        .csharp_dll_name("voicevox_core")
        .csharp_class_name("CoreUnsafe")
        .csharp_namespace("VoicevoxCoreSharp.Core.Native")
        // abortっていうのが勝手に足されてしまうので、スキップしている
        .method_filter(|method| !method.starts_with("abort"))
        // TODO: MAUI iOS
        // see: https://github.com/xamarin/xamarin-macios/issues/17418
        .csharp_dll_name_if("UNITY_IOS && !UNITY_EDITOR", "__Internal")
        .generate_csharp_file("../src/VoicevoxCoreSharp.Core/Native/CoreUnsafe.g.cs")
        .unwrap();

    Ok(())
}
