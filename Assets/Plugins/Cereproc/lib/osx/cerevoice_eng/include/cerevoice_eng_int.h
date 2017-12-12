/* $Id:
 *=======================================================================
 *
 *                       Copyright (c) 2011-2016 CereProc Ltd.
 *                       All Rights Reserved.
 *
 *=======================================================================
 */

/* Chris Pidcock 110314 */
/* Internal Engine functionality, used only for CereProc applications */
/* that need access to underlying CereVoice structures, different */
/* channels types etc. */
#if !(defined __CEREVOICE_ENG_INT_H__)
#define __CEREVOICE_ENG_INT_H__

#include "cerevoice_eng.h"
#ifdef __cplusplus
extern "C" {
#endif

#if !(defined __CPRCEN_ENGINE_INT_H__)
/** type of channel

    full: full synthesis
    tk: tokenise text in only (for client/server)
    back: assume pretokenisation (for server/client) 
    
    Note difference between CPTP_TPC_TYPE
*/
enum CPRCEN_CHANNEL_TYPE { CPRCEN_CHAN_FULL = 0,
			   CPRCEN_CHAN_TK = 1,
			   CPRCEN_CHAN_BACK = 2,
			   CPRCEN_CHAN_NORM = 3,
			   CPRCEN_CHAN_SPURT = 4,
			   CPRCEN_CHAN_ABOOK = 5};

typedef struct CPRCEN_voice CPRCEN_voice;
#endif

typedef void (* cprcen_channel_text_callback) (const char *, int len, void * userdata);

#ifndef CEREVOICE_HEADER
/** Whether abuffer has been processed by DSP */
enum CPRC_ABUF_TRANS_STATUS { CPRC_ABUF_TRANS_STATUS_NODSP = 0,
			      CPRC_ABUF_TRANS_STATUS_DSPTODO = 1,
			      CPRC_ABUF_TRANS_STATUS_DSPDONE = 2};

typedef struct CPRC_voice CPRC_voice;
typedef struct CPRC_spurt CPRC_spurt;
typedef struct CPRC_htsspurt CPRC_htsspurt;

/** hold information on dsp modification requested for the audio */
struct CPRC_abuf_dsp {
    enum CPRC_ABUF_TRANS_STATUS status;
    /** actual values */
    float mid;
    float dur;
    float f0h1;
    float f0h2;
    float lognrg;
    
    /** initial values */
    float usel_start;
    float usel_mid;
    float usel_end;
    float usel_dur;
    float usel_f0h1;
    float usel_f0h2;
    float usel_lognrg;

    /** values requested for dsp */
    float dsp_dur;
    float dsp_f0h1;
    float dsp_f0h2;
    float dsp_lognrg;
};

/** Audio buffer

    dynamic buffer containing short * to point to 16bit mono waveform
    data and structures to hold a transcription of synthesised speech
    for lip synching. Markers are included to help record current
    location in buffer.
*/
struct CPRC_abuf {
    /** number of samples present */
    int wav_sz;
    /** safe to play up to here */
    int wav_done; 
    /** memory allocated to wav */
    int wav_mem; 
    /** location marker */
    int wav_mk;
    /** sample rate */
    int srate;
    /** wave form data */
    short * wav;
    /** transcription char data */
    int trans_char_sz;
    int trans_char_mem;
    char * trans_char;
    /** number of items in transcription */
    int trans_sz;
    /** number of items by type */
    int trans_sz_type[CPRC_ABUF_TRANS_TYPES];
    /** memory allocated */
    int trans_mem;
    /** transcription items */
    CPRC_abuf_trans * trans;
    /** memory allocated for dsps (sz = trans_sz_type[CPRC_ABUF_TRANS_PHONE]*/
    int dsps_mem;
    /** dsp items */
    CPRC_abuf_dsp * dsps;
};

#endif

typedef struct CPRCEN_channel CPRCEN_channel;

/** return a new channel handle of type full, tokeniser or back end)

    returns 0 if unable to create channel 
    if it can't match voicename, language or srate 
    finds the best default */
 CPRCEN_channel_handle CPRCEN_engine_open_channel_with_type(CPRCEN_engine * eng,
						 enum CPRCEN_CHANNEL_TYPE type,
						 const char * iso_language_code,
						 const char * iso_region_code,
						 const char * voice_code, 
						 const char * srate);

/** return a CPRC voice to be used by cerevoice API */
CPRC_voice * CPRCEN_channel_get_cerevoice(CPRCEN_engine * eng, CPRCEN_channel_handle hc);

/** only do text processing steps on the input text */
const char * CPRCEN_engine_chan_text_process(CPRCEN_engine * eng, CPRCEN_channel_handle chan, 
				const char * text, int textlen, int flush);

/** create audio from a well formed spurtxml
  * Be very careful using this function. There must be the appropriate attributes for the spurt
  *
  */
CPRC_abuf * CPRCEN_engine_channel_speak_spurt(CPRCEN_engine * eng, CPRCEN_channel_handle chan, 
					      const char * text, int textlen);
/** returns the last generated spurt in xml format */
const char * CPRCEN_engine_chan_get_last_spurt(CPRCEN_engine * eng, CPRCEN_channel_handle chan);
const char * CPRCEN_engine_chan_get_last_tpspurt(CPRCEN_engine * eng, CPRCEN_channel_handle chan);
/** returns the last generated spurt in CPRC_spurt format */
CPRC_spurt * CPRCEN_engine_chan_get_last_spurt_struct(CPRCEN_engine * eng, CPRCEN_channel_handle chan);
/** return the list of units used in the last spurt, only useful for debugging */
const char * CPRCEN_engine_chan_get_last_units(CPRCEN_engine * eng, CPRCEN_channel_handle chan);

/** set callback for feeding processed text back incrementally

    setting callback prevents text being appended to the text buffer during 
    normalisation.

    valid only for tk, norm, spurt, abook, channels
*/
int CPRCEN_engine_set_text_callback(CPRCEN_engine * eng, CPRCEN_channel_handle chan, 
				    void * userdata, cprcen_channel_text_callback text_callback);

/** clear callback for application change information (i.e. voice switching)

    setting callback allows system to carry out actions before default behavior
    for example loading a new voice if one is requested.

    valid only for full channels
*/
int CPRCEN_engine_clear_application_callback(CPRCEN_engine * eng, CPRCEN_channel_handle chan);

/** set callback for application change information (i.e. voice switching)

    setting callback allows system to carry out actions before default behavior
    for example loading a new voice if one is requested.

    valid only for full channels
*/
int CPRCEN_engine_set_application_callback(CPRCEN_engine * eng, CPRCEN_channel_handle chan, 
				    void * userdata, cprcen_channel_text_callback text_callback);

/** Get the pointer to the application user data on a channel 

Returns NULL if the user data cannot be retrieved.
*/
void * CPRCEN_engine_get_channel_application_userdata(CPRCEN_engine * eng, CPRCEN_channel_handle chan);

/** clear callback for feeding processed text back incrementally

    clearinging callback causess text to be appended to the text buffer during 
    normalisation.

    valid only for tk, norm, spurt, abook, channels
*/
int CPRCEN_engine_clear_text_callback(CPRCEN_engine * eng, CPRCEN_channel_handle chan);

/** Convert a channel number into the channel structure required for deeper processing
*/
CPRCEN_channel * CPRCEN_channel_get(CPRCEN_engine * eng, CPRCEN_channel_handle chan);

/** Process spurt xml using spurt to generate a feature vector 
   
    Used in hybrid systems to generate parametric feature set to in turn 
    generate unit selection feature set.
*/
void CPRCEN_channel_fx(CPRCEN_channel * c, const char * spurtxml);

/** Insert audio file into the output */
int CPRCEN_channel_audio_from_file(CPRCEN_engine * eng, CPRCEN_channel_handle chan, const char * filename);

/** Use manual HTS durations */
int CPRC_override_hts_durations(CPRC_htsspurt * htsspurt, CPRC_spurt * spt);

/** Use manual HTS f0 */
int CPRC_override_hts_f0(CPRC_htsspurt * htsspurt, CPRC_spurt * spt);


/** Get the current synthesis type for the channel */
enum CPRCEN_SYNTH_TYPE CPRCEN_channel_synth_get_type(CPRCEN_engine * eng, CPRCEN_channel_handle chan);

/** Get the synthesis type on the voice */
enum CPRCEN_SYNTH_TYPE CPRCEN_voice_synthtype(CPRCEN_voice * v);



#if (!defined __CPTP_EXTCONTAINERS__)
#define __CPTP_EXTCONTAINERS__
/** type def for general float array structure
 */
typedef struct CPTP_floatarray CPTP_floatarray;
/** type def for general double array structure
 */
typedef struct CPTP_doublearray CPTP_doublearray;
#ifndef CPRC_NO_COMPLEX
/** type def for general complex double array structure
 */
typedef struct CPTP_complexdoublearray CPTP_complexdoublearray;
#endif /* CPRC_NO_COMPLEX */
/** type def for double matrix structure
 */
typedef struct CPTP_doublematrix CPTP_doublematrix;
typedef struct CPTP_doublematrix CPTP_doublevector;
typedef struct CPTP_doublematrix CPTP_doublevector_arraywrapper;
/** type def for general integer array structure
 */
typedef struct CPTP_intarray CPTP_intarray;
/** type def for string array structure
 */
typedef struct CPTP_strarray CPTP_strarray;

typedef struct CPTP_paramdbmgr CPTP_paramdbmgr;
typedef struct CPTP_paramdbset CPTP_paramdbset;

/** type def for cereproc log
 */
typedef struct CPTP_logger CPTP_logger;
typedef struct CPTP_phoneset_phone      CPTP_phoneset_phone;
typedef struct CPTP_phoneset_modifier   CPTP_phoneset_modifier;
typedef struct CPTP_phoneset_question   CPTP_phoneset_question;
typedef struct CPTP_phoneset_stress     CPTP_phoneset_stress;
#endif





CPTP_logger * CPRCEN_engine_channel_logger(CPRCEN_engine * eng, CPRCEN_channel_handle chan);
void CPRCEN_engine_channel_set_normaliser_tracing(CPRCEN_engine * eng, CPRCEN_channel_handle chan, int tracing);


#ifdef __cplusplus
}
#endif

#endif /* __CEREVOICE_ENG_INT_H__ */
