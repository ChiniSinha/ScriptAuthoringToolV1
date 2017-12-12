#pragma once

void report_tts_error(const char *error);
void report_audio_generation_complete(const char *filename);
void report_tts_event(const char *eventDescription);

typedef void(*unityCallback)(const char *, const char *);
