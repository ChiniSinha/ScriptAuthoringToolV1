#ifdef __APPLE__
#include "TargetConditionals.h"
#if TARGET_OS_IPHONE
#include "cerevoice_eng.h"
#else
#include <cerevoice_eng.h>
#endif
#else
#include <cerevoice_eng.h>
#endif

#include "cerevoice_plugin.h"
#include <string.h>
#include <string>
#include <stdio.h>
#include "phoneme_viseme_mapping.h"
#include "unity_callbacks.h"


CPRCEN_engine* cereprocEngine;
std::string outputFilename = "output.wav";
char *audioRenderFileName;
float audioChunkOffset;
float lastAudioEventEnd;
CPRCEN_channel_handle channel;

void load_tts(const char* resourceDirectory, const char* cacheDirectory, const char* voiceName)
{
    std::string nameString = std::string(voiceName);
	std::string licensePath = resourceDirectory + nameString + ".license";
	std::string voicePath = resourceDirectory + nameString + ".voice";
    
    std::string audioFile = cacheDirectory + outputFilename;
    audioRenderFileName = new char[audioFile.length() + 1];
	strcpy(audioRenderFileName, audioFile.c_str());

	cereprocEngine = CPRCEN_engine_new();
	if (cereprocEngine == NULL)
	{
		report_tts_error(("Error creating engine. LicensePath: " + licensePath + " VoicePath: " + voicePath).c_str());
		return;
	}

	int stat = CPRCEN_engine_load_voice(cereprocEngine, licensePath.c_str(), NULL, voicePath.c_str(), CPRC_VOICE_LOAD_EMB);

	if (cereprocEngine == NULL)
	{
		report_tts_error(("Error starting engine. LicensePath: " + licensePath + " VoicePath: " + voicePath).c_str());
		return;
	}

	channel = CPRCEN_engine_open_default_channel(cereprocEngine);
	if (channel == 0)
	{
		report_tts_error("Error getting channel");
		return;
	}
}

void speak_ssml_block(const char* text)
{
	audioChunkOffset = 0;
	lastAudioEventEnd = 0;

	int ret = CPRCEN_engine_set_callback(cereprocEngine, channel, NULL, (cprcen_channel_callback)receive_callback_data);
	int fileSuccess = CPRCEN_engine_channel_to_file(cereprocEngine, channel, audioRenderFileName, CPRCEN_RIFF);
	if (fileSuccess == 0)
	{
		report_tts_error("Error opening audio render file");
		return;
	}

	CPRC_abuf* buff = CPRCEN_engine_channel_speak(cereprocEngine, channel, text, strlen(text), 1);
	if (buff == NULL)
	{
		report_tts_error("Error rendering audio");
	}
}

void cleanup_tts()
{
	CPRCEN_engine_channel_close(cereprocEngine, channel);
	CPRCEN_engine_delete(cereprocEngine);
}

void receive_callback_data(CPRC_abuf* abuf, void* userdata)
{
	const CPRC_abuf_trans* trans;
	std::string name;
	float start, end;
	std::string dataType;

	/* Process the transcription buffer items and print information. */
	for (int i = 0; i < CPRC_abuf_trans_sz(abuf); i++)
	{
		trans = CPRC_abuf_get_trans(abuf, i);
		start = CPRC_abuf_trans_start(trans); /* Start time in seconds */
		end = CPRC_abuf_trans_end(trans); /* End time in seconds */
		name = CPRC_abuf_trans_name(trans); /* Label, type dependent */

		if (start == 0)
		{
			audioChunkOffset += lastAudioEventEnd;
		}
		lastAudioEventEnd = end;
		start += audioChunkOffset;
		end += audioChunkOffset;

		if (CPRC_abuf_trans_type(trans) == CPRC_ABUF_TRANS_PHONE)
		{
			dataType = "viseme";
			PhonemeToViseme(name);
		}
		else if (CPRC_abuf_trans_type(trans) == CPRC_ABUF_TRANS_WORD)
		{
			dataType = "word";
		}
		else if (CPRC_abuf_trans_type(trans) == CPRC_ABUF_TRANS_MARK)
		{
			if (name == "cprc_final") // Marker that gets sent at the end of an utterance
			{
				report_audio_generation_complete(audioRenderFileName);
			}
			dataType = "marker";
		}
		else
		{
			report_tts_error(name.c_str());
			continue;
		}

		std::string description = dataType + "|" + name + "|" + std::to_string(start) + "|" + std::to_string(end);
		report_tts_event(description.c_str());
	}
}
