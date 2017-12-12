#ifdef _WIN32
#include <cstring>
#include "../src/unity_callbacks.h"
#include "../src/cerevoice_plugin.h"

unityCallback event_callback;
unityCallback audio_generated_callback;
unityCallback error_callback;

char* caller_name;

void report_tts_error(const char* error)
{
	error_callback(caller_name, error);
}

void report_audio_generation_complete(const char* filename)
{
	audio_generated_callback(caller_name, filename);
}

void report_tts_event(const char* eventDescription)
{
	event_callback(caller_name, eventDescription);
}

void set_callback_listener(const char* objectName)
{
	caller_name = new char[strlen(objectName)];
	strcpy(caller_name, objectName);
}

void set_unity_callbacks(unityCallback eventCallback, unityCallback audioGenCallback, unityCallback errorCallback)
{
	event_callback = eventCallback;
	audio_generated_callback = audioGenCallback;
	error_callback = errorCallback;
}

extern "C"
{
	__declspec(dllexport) void _cdecl CleanupTTS()
	{
		cleanup_tts();
	}

	__declspec(dllexport) void _cdecl SpeakSSMLBlock(const char* text)
	{
		speak_ssml_block(text);
	}

	__declspec(dllexport) void _cdecl LoadTTS(const char* resourceDirectory, const char* cacheDirectory, const char* voiceName)
	{
		load_tts(resourceDirectory, cacheDirectory, voiceName);
	}

	__declspec(dllexport) void _cdecl SetListenerObject(const char* listener)
	{
		caller_name = new char[strlen(listener)];
		strcpy(caller_name, listener);
	}

	__declspec(dllexport) void _cdecl SetCallbacks(unityCallback eventCallback, unityCallback audioGenCallback, unityCallback errorCallback)
	{
		event_callback = eventCallback;
		audio_generated_callback = audioGenCallback;
		error_callback = errorCallback;
	}
}

#endif
