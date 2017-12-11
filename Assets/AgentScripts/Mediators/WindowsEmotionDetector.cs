using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class WindowsEmotionDetector
{
    // recorded speech file name
    private readonly string _filename = "sample.wav";
    private readonly int _margin = 20;
    private readonly WindowsSpeechRecognizer _speechRecognizer;
    private List<short> _allData;

    public WindowsEmotionDetector(WindowsSpeechRecognizer speechRecognizer)
    {
        _speechRecognizer = speechRecognizer;
    }

    // emotion detection entry method
    public void DetectEmotion(List<short> audioData)
    {
        _allData = audioData;
        _speechRecognizer.StartCoroutine(DoDetection());
    }

    private IEnumerator DoDetection()
    {
        WriteWavFile(); // TA: this might take a long time. if it does, move it to a thread
        Process process = new Process();
        process.StartInfo.FileName = "emotion.exe";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.Start();
        while (!process.HasExited)
        {
            yield return new WaitForEndOfFrame();
        }
        ReadLog();
    }

    // read emotion detection output log
    private void ReadLog()
    {
        float[] output = {0, 0, 0};
        int n = 0;
        string line;
        StreamReader file = new StreamReader("log.txt");
        while (file.ReadLine() != null)
        {
            file.ReadLine();
            for (int i = 0; i < 3; i++)
            {
                line = file.ReadLine();
                string valueString = line.Substring(line.IndexOf(":") + 1);
                float val = valueString.ParseFloat();
                output[i] += val;
            }
            n++;
        }

        if (n == 0)
        {
            Globals.EventBus.Dispatch(new EmotionDataAvailableEvent(-1, -1, -1));
        }
        else
        {
            Globals.EventBus.Dispatch(new EmotionDataAvailableEvent(output[0]/n, output[1]/n, output[2]/n));
        }
    }

    // write speech data to wav file
    private void WriteWavFile()
    {
        UnityEngine.Debug.Log("In writeWavFile: " + _allData.Count);
        if (_allData.Count == 0)
        {
            UnityEngine.Debug.Log("alldata was 0");
            return;
        }
        const short NumberOfChannels = 1; // mono audio
        const short BytesPerSample = 2; // 16bit samples
        const int SamplingRate = 16000; // 16 kHz
        int totalBytes = checked(44 + _allData.Count*BytesPerSample*NumberOfChannels); // size of headers + data
        UnityEngine.Debug.Log("checkpoint 1");
        byte[] output = new byte[totalBytes];
        Buffer.BlockCopy(GetLEBytes(0x46464952), 0, output, 0, 4); // "RIFF"
        Buffer.BlockCopy(GetLEBytes(totalBytes - 8), 0, output, 4, 4); // RIFF chunk size
        Buffer.BlockCopy(GetLEBytes(0x45564157), 0, output, 8, 4); // "WAVE"
        Buffer.BlockCopy(GetLEBytes(0x20746D66), 0, output, 12, 4); // "fmt "
        Buffer.BlockCopy(GetLEBytes(16), 0, output, 16, 4); // fmt chunk size
        Buffer.BlockCopy(GetLEBytes((short) 1), 0, output, _margin, 2); // compression code (1 - PCM/Uncompressed)
        Buffer.BlockCopy(GetLEBytes(NumberOfChannels), 0, output, 22, 2); // number of channels
        Buffer.BlockCopy(GetLEBytes(SamplingRate), 0, output, 24, 4); // sampling rate
        Buffer.BlockCopy(GetLEBytes(SamplingRate*BytesPerSample*NumberOfChannels), 0, output, 28, 4); // bytes/second
        Buffer.BlockCopy(GetLEBytes((short) (BytesPerSample*NumberOfChannels)), 0, output, 32, 2); // block size
        Buffer.BlockCopy(GetLEBytes((short) (BytesPerSample*8)), 0, output, 34, 2); // bits per sample
        Buffer.BlockCopy(GetLEBytes(0x61746164), 0, output, 36, 4); // "data"
        Buffer.BlockCopy(GetLEBytes(totalBytes - 44), 0, output, 40, 4); // data chunk size
        UnityEngine.Debug.Log("checkpoint 2");
        for (int i = 0; i < _allData.Count; i++)
        {
            Buffer.BlockCopy(GetLEBytes(_allData[i]), 0, output, BytesPerSample*i*NumberOfChannels + 44, BytesPerSample);
        }
        UnityEngine.Debug.Log("About to write wav file");
        File.WriteAllBytes(_filename, output);
    }

    private byte[] GetLEBytes(short value)
    {
        if (BitConverter.IsLittleEndian)
        {
            return BitConverter.GetBytes(value);
        }
        return BitConverter.GetBytes((short) ((value & 0xFF) << 8 | (value & 0xFF00) >> 8));
    }

    private byte[] GetLEBytes(int value)
    {
        if (BitConverter.IsLittleEndian)
        {
            return BitConverter.GetBytes(value);
        }
        return BitConverter.GetBytes((value & 0xFF) << 24 | (value & 0xFF00) << 8
                                     | (value & 0xFF0000) >> 8 | (int) ((value & 0xFF000000) >> 24));
    }
}