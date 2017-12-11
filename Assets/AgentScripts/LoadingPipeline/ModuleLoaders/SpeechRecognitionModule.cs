using System;

namespace RAG.Loading
{
    internal class SpeechRecognitionModule : LoadableModule
    {
        protected override void DoLoad()
        {
            Globals.SystemObject.AddComponent<WindowsSpeechRecognizer>();
            Status = LoadingStatus.LOADED;
        }
    }
}