using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using AOT;
using UnityEngine;


public class MacTTS : TTSController
{
	/*
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
	static bool _pendingPhoneme = false;
//	static string pendingPhonemeString = "";
	protected static int _pendingPhonemeId;
	protected static int _pendingPhonemeDuration;

	static Queue<int> PendingSyncQueue = new Queue<int>();
	
	private static int minRandomDuration = 40;
	private static int maxRandomDuration = 120;

	protected static bool _endSpeechFlag;

	static int currentSync = 0;

//Define callbacks
	public delegate void phonemeCallback(string message);
	public delegate void syncCallback(string message);

//Import DLL Functions
	[DllImport ("osxTTS")]
	private static extern int createTTS(phonemeCallback pCallback, syncCallback sCallback);

	[DllImport ("osxTTS")]
	private static extern void speakText(string text);

//Create callback methods
 
 	[MonoPInvokeCallback(typeof(phonemeCallback))]
	 static void PrintPhoneme(string phoneme) {
		_pendingPhoneme = true;
		if (phoneme != "endspeak") {
			_endSpeechFlag = false;
			int duration = UnityEngine.Random.Range (minRandomDuration, maxRandomDuration);
			_pendingPhonemeId = int.Parse(phoneme);
			if (phoneme == "0")
				_pendingPhonemeDuration = 20;
			_pendingPhonemeDuration = duration;
		} else {
			_endSpeechFlag = true;
		}

	 }
		
 
 	[MonoPInvokeCallback(typeof(syncCallback))]
	 static void PrintSync(string sync) {
		sync = sync.Substring(0,sync.Length - 1);
		//TODO: FIGURE OUT WHY THIS IS NEEDED
		sync = "" + currentSync;
		currentSync++;
		//RagLog.Log("sync:" + sync);
		//END OF SYNC-HACK
		PendingSyncQueue.Enqueue(int.Parse(sync));
	 }
	 
	public override void InitTts(){
		int result = createTTS(PrintPhoneme,PrintSync);
		if (result == 1) {
			RagLog.Log("MacTTS created");
		} else {
			RagLog.Log("MacTTS init failed!");
		}
	}
		
		
	private string[] syncMessages = new string[100];
	public override void SpeakBlock(XmlNode inputNode){
		string input = inputNode.InnerXml;
		SpeakText (input);
	}

	public override void SpeakText(string input){
		IsSpeaking = true;
		
		//SYNC-HACK
		currentSync = 1;
		//END OF HACK
		//NOTE: hack to make sure we remove ssml
		//TODO: make this hack more robust
		input = input.Replace ("<speak>", "");
		input = input.Replace ("</speak>", "");
		input = input.Replace ("<speech>", "");
		input = input.Replace ("</speech>", "");
		//Add sync blocks
		RagLog.Log("MacTTS: Orginal Input:" + input);
		int syncCount = 1;
		try {
		    Regex regex = new Regex(@"<[^>]+>");
		    while(regex.IsMatch(input) ) {
				syncMessages[syncCount] = regex.Match(input).Value;
				syncMessages[syncCount] = syncMessages[syncCount].Substring(1,syncMessages[syncCount].Length - 3);
				input = ReplaceFirst(input,regex.Match(input).Value,"[[sync R" + ReverseNumber(syncCount) + "]]");
				//RagLog.Log("[sync R" + ReverseNumber(syncCount) + "]]" + " | " + syncMessages[syncCount]);
				syncCount++;
		    }
		} catch (Exception e) {
			RagLog.Log("Problem cleaning speak input: " + e);
		    // Syntax error in the regular expression
		}
		
		//RagLog.Log("sync ready input = " + input);
		
		speakText (input);
	}
	
	public static string ReverseNumber(int org)
    {
		string s = org + "";
		if(org < 10){
			s = "00" + s;
		} else if(org >= 10 && org < 100){
			s = "0" + s;
		} else {
			RagLog.Log("Error flipping for sync message, org count is :" + org);
		}
		char[] arr = s.ToCharArray();
		Array.Reverse(arr);
		return new string(arr);
    }
	
	public void Update(){
		if (_pendingPhoneme)
		{
			_pendingPhoneme = false;
			if (_endSpeechFlag)
			{
				//check for end of speaking
				RagLog.Log("MacTTS: Endspeak received!");
				Agent.AnimationController.PlayViseme(0, 0);
				SpeakComplete();
			}
			else
			{
//				Debug.Log ("Sending viseme:" + _pendingPhonemeId + " Duration:" + _pendingPhonemeDuration);
				Agent.AnimationController.PlayViseme(_pendingPhonemeId, _pendingPhonemeDuration);
			}
		}
		if (PendingSyncQueue.Count > 0)
		{
			string sync = syncMessages[PendingSyncQueue.Dequeue()].Trim();

			Agent.AnimationController.EnqueueCommand(new RawXmlAnimationCommand("<" + sync + "/>", false));
		}
	}
	
	public string ReplaceFirst(string text, string search, string replace)
	{
		int pos = text.IndexOf(search);
		if (pos < 0)
			return text;
		else
			return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
	}
#endif
*/
}