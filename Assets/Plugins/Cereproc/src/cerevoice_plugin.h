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

void receive_callback_data(CPRC_abuf* abuf, void* userdata);

void cleanup_tts();
void speak_ssml_block(const char* text);
void load_tts(const char* resourceDirectory, const char* cacheDirectory, const char* voiceName);
