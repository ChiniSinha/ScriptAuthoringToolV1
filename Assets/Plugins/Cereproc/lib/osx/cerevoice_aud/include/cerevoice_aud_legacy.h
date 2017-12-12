#ifndef __CEREVOICE_AUD_LEGACY_H__
#define __CEREVOICE_AUD_LEGACY_H__

/* $Id:
 *=======================================================================
 *
 *                       Copyright (c) 2005-2016 CereProc Ltd.
 *                       All Rights Reserved.
 *
 *=======================================================================
 */

/* Matthew Aylett: 051205 */


/* legacy API to cerevoice: Include can be added to old apps that used
this interface if required. (Please list apps that need this when they
are encountered here): 

- CereCapture requires the beep and wavmono functionality (the Python
  wrapper has been changed to use include this file).


MA060411
 */

#ifndef __CEREVOICE_AUD_H__
#include <cerevoice_aud.h>
#endif

#ifdef __cplusplus
extern "C" {
#endif

typedef struct wavmono wavmono;
typedef void PortAudioStream;
typedef wavmono RCD_audio_string;
typedef struct PABLIO_Stream PABLIO_Stream;

typedef struct lock lock;

struct lock {
  int lock;
};

struct wavmono
{
    int          frameIndex;  /* Index into sample array. */
    int          maxFrameIndex;
    short        *data;
    int          num_samples;
    int          sample_rate;
};

wavmono * get_buffer(int max_sz, int sample_rate);
wavmono * pythonstr(wavedata * data, int len, int sample_rate);
void delete_buffer(wavmono *data);
PortAudioStream * startrecording(wavmono * data);
PortAudioStream * startplaying(wavmono * data);

int killstream(PortAudioStream *stream);
PortAudioStream * continueplaying(PortAudioStream *stream);
wavmono * beep(float seconds, int hertz, float amplitude, int sample_rate);
wavmono * riff_load(char* fname);
void riff_save(wavmono * wav, const char * fname);

PABLIO_Stream * open_audiostream(int sample_rate);
void play_audiostream(PABLIO_Stream * audiostream, short * data, int numsamples,  lock * l);
void close_audiostream(PABLIO_Stream * audiostream);
lock * lock_new();
void lock_delete(lock * l);

#ifdef __cplusplus
}
#endif

#endif /* __CEREVOICE_AUD_LEGACY_H__*/
