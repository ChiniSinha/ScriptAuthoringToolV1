#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using AOT;
using UnityEngine;

#endregion

public class WindowsSpeechRecognizer : MonoBehaviour
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void listenCallback(IntPtr output, int size, int option);

    private readonly List<short> _allAudioData = new List<short>();

    private readonly double _vadThreshold = 3; //Threshhold Speech vs Noise

    private readonly double _waitTime = 1; //In seconds
    private WindowsEmotionDetector _emotionDetector;

    private bool _highlightFlag;

    // temporary speech data 
    private short[] _inputData;

    private WindowsSpeechRecognitionMediator _mediator;

    private string[] _menuOptions;
    private bool _recognitionFlag;

    private int _recognizedIdx = -1;
    private bool _resultConfirmed;

    private int _sentenceSensitivity = 50;
    private Thread _speechRecognitionThread;

    private bool _useKeywords;
    private int _wordSensitivity = 1;

    [DllImport("speech")]
    private static extern int run(string options, bool kws, int word, int sntc, double wait, double vad,
        listenCallback lCallback);

    [DllImport("speech")]
    private static extern void stop();

    public void Start()
    {
        _mediator = new WindowsSpeechRecognitionMediator(this);
        _mediator.Setup();

        _useKeywords = Globals.Config.SpeechRecognition.UseKeywordSearch;
        _wordSensitivity = Globals.Config.SpeechRecognition.WordSensitivity;
        _sentenceSensitivity = Globals.Config.SpeechRecognition.SentenceSensitivity;
        if (Globals.Config.SpeechRecognition.EnableEmotionRecognition)
        {
            _emotionDetector = new WindowsEmotionDetector(this);
        }
    }

    public void OnDestroy()
    {
        _mediator.Cleanup();
    }

    public void StartListen(string[] menuOptions)
    {
        StartCoroutine(DoListen(menuOptions));
    }

    public void RestartListener()
    {
        StartCoroutine(DoListen(_menuOptions));
    }

    private IEnumerator DoListen(string[] menuOptions)
    {
        while (_speechRecognitionThread != null && _speechRecognitionThread.IsAlive)
        {
            yield return new WaitForEndOfFrame();
        }
        _menuOptions = menuOptions;
        _speechRecognitionThread = new Thread(RunSpeechRecognizer);
        _speechRecognitionThread.Start();

        yield return WaitForPossibleRecognition();
        yield return WaitForRecognitionConfirmation();

        StopListener();
    }

    private IEnumerator WaitForRecognitionConfirmation()
    {
        while (!_recognitionFlag)
        {
            yield return new WaitForEndOfFrame();
        }

        _recognitionFlag = false;

        if (_resultConfirmed)
        {
            if (Globals.Config.SpeechRecognition.EnableEmotionRecognition)
            {
                for (int i = 0; i < _inputData.Length; i++)
                {
                    _allAudioData.Add(_inputData[i]);
                }
            }
            _mediator.OnRecognitionSuccessful(_recognizedIdx);
        }
        else
        {
            _mediator.OnRecognitionCancelled();
        }
    }

    private IEnumerator WaitForPossibleRecognition()
    {
        while (!_highlightFlag)
        {
            yield return new WaitForEndOfFrame();
        }

        _highlightFlag = false;
        _mediator.OnPossibleRecognition(_recognizedIdx);
    }

    private void RunSpeechRecognizer()
    {
        string options = string.Join("\n", _menuOptions);

        options = options.Replace(",", "").Replace("?", "").Replace(".", "");
        options = options.ToLower();

        int result = run(options, _useKeywords, _wordSensitivity, _sentenceSensitivity, _waitTime, _vadThreshold,
            OnPossibleRecognition);

        if (result < 0)
        {
            Debug.LogError("Windows Speech Recognizer returned exit code: " + result);
        }
        else
        {
            _resultConfirmed = result == 1;
            _recognitionFlag = true;
        }
    }

    // highlight callback 
    [MonoPInvokeCallback(typeof(listenCallback))]
    private void OnPossibleRecognition(IntPtr dataPtr, int size, int option)
    {
        _inputData = new short[size];
        Marshal.Copy(dataPtr, _inputData, 0, size);

        Debug.Log("[WindowsSpeechRecognizer] Detected option : " + option);
        _recognizedIdx = option;
        _highlightFlag = true;
    }

    public void StopListener()
    {
        if (_speechRecognitionThread.IsAlive)
        {
            stop();
        }
    }

    //TA: for future use
    public void AnalyzeEmotion()
    {
        _emotionDetector.DetectEmotion(_allAudioData);
        _allAudioData.Clear();
    }

    private void OnApplicationQuit()
    {
        StopListener();
    }
}