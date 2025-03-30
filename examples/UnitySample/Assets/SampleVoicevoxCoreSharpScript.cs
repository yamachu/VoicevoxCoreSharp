using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using VoicevoxCoreSharp.Core;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core.Struct;

public class SampleVoicevoxCoreSharpScript : MonoBehaviour
{
    public string Text = "こんにちは";
    public uint StyleId = 1;

    // Start is called before the first frame update
    void Start()
    {
        var openJTalkDictPath = System.IO.Path.Combine(Application.streamingAssetsPath, "voicevox_core/dict/open_jtalk_dic_utf_8-1.11");

        Debug.Log("coreの初期化中");

        var initializeOptions = InitializeOptions.Default();
        var result = OpenJtalk.New(openJTalkDictPath, out var openJtalk);
        if (result != ResultCode.RESULT_OK)
        {
            Debug.LogError(result.ToMessage());
            return;
        }

        // TODO: This is platform dependent code, FIXME
        var loadOnnxruntimeOptions = new LoadOnnxruntimeOptions(Path.Combine(Application.dataPath, "Plugins", "runtimes", "osx.10.14-arm64", "native", "libvoicevox_onnxruntime.1.17.3.dylib"));
        if (Onnxruntime.LoadOnce(loadOnnxruntimeOptions, out var onnxruntime) != ResultCode.RESULT_OK)
        {
            Debug.LogError("Failed to initialize onnxruntime");
            return;
        }
        result = Synthesizer.New(onnxruntime, openJtalk, initializeOptions, out var synthesizer);
        if (result != ResultCode.RESULT_OK)
        {
            Debug.LogError(result.ToMessage());
            return;
        }

        using (openJtalk) { }

        result = VoiceModelFile.New(System.IO.Path.Combine(Application.streamingAssetsPath, "voicevox_core/models/vvms/0.vvm"), out var voiceModel);
        if (result != ResultCode.RESULT_OK)
        {
            Debug.LogError(result.ToMessage());
            return;
        }

        result = synthesizer.LoadVoiceModel(voiceModel);
        if (result != ResultCode.RESULT_OK)
        {
            Debug.LogError(result.ToMessage());
            return;
        }

        using (voiceModel) { }

        Debug.Log("音声生成中...");

        result = synthesizer.Tts(Text, StyleId, TtsOptions.Default(), out var outputWavSize, out var outputWav);
        if (result != ResultCode.RESULT_OK)
        {
            Debug.LogError(result.ToMessage());
            return;
        }

        using (synthesizer) { }

        var clip = AudioClip.Create(Text, (int)outputWavSize, 1, 24000, false);
        // WAVのヘッダとかが残ってるため、音声はプチっとかする
        // ちゃんとした音声にするならいい感じにしてください
        clip.SetData(ToWavHeaderSkippedFloatWavData(outputWav), 0);
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private float[] ToWavHeaderSkippedFloatWavData(byte[] original)
    {
        var wavData = new float[original.Length / 2];
        for (var i = 0; i < wavData.Length; i++)
        {
            wavData[i] = (float)BitConverter.ToInt16(original, i * 2) / 32768.0f;
        }
        return wavData;
    }
}
