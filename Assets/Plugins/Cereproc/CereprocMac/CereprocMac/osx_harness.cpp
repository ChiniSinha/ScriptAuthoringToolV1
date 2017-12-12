#ifdef __APPLE__
#include "TargetConditionals.h"
#if !TARGET_OS_IPHONE

#include "cerevoice_plugin.h"
#include "unity_callbacks.h"
#include <string.h>

unityCallback event_callback;
unityCallback audio_generated_callback;
unityCallback error_callback;
char *caller_name;

void report_tts_error(const char *error)
{
    error_callback(caller_name, error);
}

void report_audio_generation_complete(const char *filename)
{
    audio_generated_callback(caller_name, filename);
}

void report_tts_event(const char *eventDescription)
{
    event_callback(caller_name, eventDescription);
}

extern "C"
{
void CleanupTTS()
{
    cleanup_tts();
}
    
void SpeakSSMLBlock(const char *text)
{
    speak_ssml_block(text);
}
    
void LoadTTS(const char *resourceDirectory, const char *cacheDirectory, const char *voiceName)
{
    load_tts(resourceDirectory, cacheDirectory, voiceName);
}

void SetListenerObject(const char *listener)
{
    caller_name = new char[strlen(listener)];
    strcpy(caller_name, listener);
}
void SetCallbacks(unityCallback eventCallback, unityCallback audioGenCallback, unityCallback errorCallback)
{
    event_callback = eventCallback;
    audio_generated_callback = audioGenCallback;
    error_callback = errorCallback;
}
}

#endif
#endif
