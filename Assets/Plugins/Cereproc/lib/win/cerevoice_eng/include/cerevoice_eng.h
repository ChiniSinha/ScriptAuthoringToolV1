#if !(defined __CEREVOICE_ENG_H__)
#define __CEREVOICE_ENG_H__
/* $Id:
 *=======================================================================
 *
 *                       Copyright (c) 2010-2016 CereProc Ltd.
 *                       All Rights Reserved.
 *
 *=======================================================================
 */

/* Matthew Aylett: 100208 */
/* Chris Pidcock: 110314 */

/** \file cerevoice_eng.h

CereVoice Engine Text to Speech API
*/

/** \mainpage

The CereVoice Engine Text to Speech (TTS) API is an advanced TTS API
developed by CereProc Ltd.  

Two header files are included, cerevoice_eng_simp.h is a simple API,
allowing TTS audio data or wave files to be generated with two
function calls.

The API described in cerevoice_eng.h is an extended version of the
simple API, allowing the use of multiple voices and channels,
low-latency synthesis via a callback API, control of memory footprint,
user lexicons, and access to marker events and phoneme timings.
*/

#ifdef __cplusplus
extern "C" {
#endif

#ifndef CEREVOICE_IO_HEADER
/** Voice load configuration */
enum CPRC_VOICE_LOAD_TYPE { 
    /** All data loaded to memory (fastest, requires most RAM) */
    CPRC_VOICE_LOAD = 0,
    /** Audio and index data is read from disk (slowest performance, requires 
	the least RAM) */
    CPRC_VOICE_LOAD_EMB = 1,
    /** Audio data is read from disk (reasonably fast, relatively low RAM 
	footprint) */
    CPRC_VOICE_LOAD_EMB_AUDIO = 2,
    /** Load using memmap, if supported on the platform */
    CPRC_VOICE_LOAD_MEMMAP = 3,
    /** Not used in engine processing */
    CPRC_VOICE_LOAD_TP = 4
};
#endif

/** Audio output file type */
#if !(defined __CPRCEN_ENGINE_H__)
enum CPRCEN_AUDIO_FORMAT { 
    /** Raw linear PCM output */
    CPRCEN_RAW  = 0, 
    /** RIFF wave output */
    CPRCEN_RIFF = 1,
    /** AIFF output (commonly used on Mac OS X) */
    CPRCEN_AIFF = 2
};

/** Type of synthesis to use */
enum CPRCEN_SYNTH_TYPE {
    /** No synthesis */
    CPRCEN_SYNTH_NONE = -1,
    /** Unit selection synthesis */
    CPRCEN_SYNTH_USEL = 0,
    /** HTS synthesis */
    CPRCEN_SYNTH_HTS = 1,
    /** HTS Prosody synthesis */
    CPRCEN_SYNTH_HTSPROS = 2,
    /** CereProc Parameteric synthesis */
    CPRCEN_SYNTH_PARAM = 3,
    /** Hybrid parameteric and unit selection synthesis */
    CPRCEN_SYNTH_HYBRID = 4
};



#endif

#ifndef CEREVOICE_HEADER
/** Type of transcription entry

The transcription holds useful non-speech information about the audio output.
It can be used for a variety of purposes, such as lip syncing for animated 
characters (using the phoneme transcriptions), or word highlighting in an 
application (using the 'cptk' markers).  User-specified input markers are also
stored in the transcription structure.
*/
enum CPRC_ABUF_TRANS { 
    /** Phone transcription (phones can also be referred to as 'phonemes') */
    CPRC_ABUF_TRANS_PHONE = 0,
    /** Word transcription - these are speech-based word transcriptions, hence 
	'21' is two word events (twenty one). 
    */
    CPRC_ABUF_TRANS_WORD = 1,
    /** Marker transcription, can be user generated or contain CereProc
	tokenisation markers.  CereProc token markers have the form 
	'cptk_n1_n2_n3_n4', where n1 is the character offset, n2 is the
	byte offset, n3 is the number of characters in the word, and n4 
	is the number of bytes in the word. 
    */
    CPRC_ABUF_TRANS_MARK = 2,
    /** Error retrieving transcription */
    CPRC_ABUF_TRANS_ERROR = 3,
    /** Number of transcription types */
    CPRC_ABUF_TRANS_TYPES = 4
};

/** Audio data returned by a speak command */
typedef struct CPRC_abuf CPRC_abuf;

/** Structure to hold transcription data for the audio buffer */
typedef struct CPRC_abuf_trans CPRC_abuf_trans;

/** Audio DSP structure, not user-configurable */
typedef struct CPRC_abuf_dsp CPRC_abuf_dsp;

/** Structure to hold transcription data for the audio buffer

The transcription holds useful non-speech information about the audio output.
It can be used for a variety of purposes, such as lip syncing for animated 
characters (using the phoneme transcriptions), or word highlighting in an 
application (using the 'cptk' markers).  User-specified input markers are also
stored in the transcription structure.

All data is also accessible via helper functions.
*/
struct CPRC_abuf_trans {
/** Text content of the transcription. 
- Phone transcription - text contains the CereProc phoneme string. 
- Word transcription - contains the text of the word being spoken.
- Marker transcription 
  - contains the text of the marker name, as supplied by the user via 
    markup such as SSML (e.g. 'marker_test' for 
    '<mark name="marker_test"\/>'
  - can also contain a CereProc tokenisation marker. CereProc token markers 
    have the form 'cptk_n1_n2_n3_n4', where:
    - n1 - integer character offset of the token
    - n2 - integer byte offset of the token
    - n3 - integer characters length of the token
    - n4 - integer byte length of the token.
    */
    const char * name;
    /** Type of transcription */
    enum CPRC_ABUF_TRANS type;
    /** Start time, in seconds, of the transcription event */
    float start;
    /** End time, in seconds, of the transcription event */
    float end;
    /** Not used */ 
    CPRC_abuf_dsp * dsp;
    /** Extra field containing the full phone transcription, only valid for phone type */
    const char * phone;
};
#endif

/** Generic fixed buffer structure */ 
#if !(defined __CPTP_CERETP_H__)
typedef struct CPTP_fixedbuf CPTP_fixedbuf;
typedef unsigned char byte;
/** Generic fixed buffer structure */
struct CPTP_fixedbuf {
    /** Size of the buffer */
    int _size;
    /** Byte sequence */
    byte * _buffer;
};
#endif

/** CereVoice Engine structure

The engine is responsible for loading and maintaining voices.
*/
typedef struct CPRCEN_engine CPRCEN_engine;

/** Handle to a CereVoice Engine Channel

The channel is the main interface to synthesis.  A typical session
begins by creating an engine, loading a voice, then creating a
channel. Multiple channels can be used, as long as the channel handles
are correctly tracked.
*/
typedef int CPRCEN_channel_handle;

/** Channel callback function

Optionally, a user can set their own callback function. This allows
the user to process audio incrementally (phrase by phrase). The
user_data pointer is used to store user-configurable information.
 */
typedef void (* cprcen_channel_callback) (CPRC_abuf * abuf, 
					  void * user_data);


/** \name Engine Creation and I/O Functions */
/** @{ */

/** Create an empty engine

The engine is responsible for loading and unloading voices. This
function creates an empty engine, initially with no voices loaded.  The
CPRCEN_engine_load_voice() function must be used to load a voice into
the engine.

Alternatively create an engine using CPRCEN_engine_load() or
CPRCEN_engine_load_config() functions.
*/
CPRCEN_engine * CPRCEN_engine_new();

/** Create an engine and load a voice

Valid license and voice files must be supplied. Additional voices can
be loaded with CPRCEN_engine_load_voice().

Returns NULL if there has been an error.
*/
CPRCEN_engine * CPRCEN_engine_load(const char * licensef, const char * voicef);

/** Create an engine and load a voice, with configuration

Valid license and voice files must be supplied, along with a configuration 
file. Additional voices can be loaded with CPRCEN_engine_load_voice().

Returns NULL if there has been an error.
*/
CPRCEN_engine * CPRCEN_engine_load_config(const char * licensef, 
					  const char * voicef, 
					  const char * voice_configf);

/** Load a voice into the engine 

A voice file and valid license must be supplied.  To use the default 
configuration pass an empty string as the configf argument.  See the load type
documentation for information on the different load configurations.

Multiple voices can be loaded using this function.  The most recently loaded
voice becomes the default voice for CPRCEN_engine_open_default_channel().

Returns FALSE if there has been an error.
*/
int CPRCEN_engine_load_voice(CPRCEN_engine * eng, 
			     const char * licensef, 
			     const char * configf, 
			     const char * voicef, 
			     enum CPRC_VOICE_LOAD_TYPE load_type);

/** Load a voice into the engine with a license string

A voice file and valid license text and signature must be supplied.  To use the default 
configuration pass an empty string as the configf argument.  See the load type
documentation for information on the different load configurations.

The license text must contain the upper part of a license key (key-value pairs e.g. VID=AAA).
The signature is the final 256 character checksum, and must not contain a newline at the end
of the string. 

Multiple voices can be loaded using this function.  The most recently loaded
voice becomes the default voice for CPRCEN_engine_open_default_channel().

Returns FALSE if there has been an error.
*/
int CPRCEN_engine_load_voice_licensestr(CPRCEN_engine * eng, 
					const char * license_text, const char * signature, const char * configf, 
					const char * voicef, enum CPRC_VOICE_LOAD_TYPE load_type);
/** Unload a voice from the engine
 
Any open channels using the voice are closed automatically.

Note that it is not necessary to unload voices prior to calling
CPRCEN_engine_delete().  CPRCEN_engine_get_voice_count() and
CPRCEN_engine_get_voice_info() should be used to check the loaded
voices.

The index will be less than the number returned by
CPRCEN_engine_get_voice_count().

On success, returns the number of voices currently loaded.  Returns -1
if there has been an error. 
*/
int CPRCEN_engine_unload_voice(CPRCEN_engine * eng, int voice_index);

/** Delete and clean up the engine

Deleting the engine cleans up any voices that have been loaded, as well as any
open channels.
 */
void CPRCEN_engine_delete(CPRCEN_engine * eng);

/** Load user lexicon 

Simple entries are in the format:
hello h_\@0_l_ou1

The index will be less than the number returned by
CPRCEN_engine_get_voice_count().

Returns FALSE if there has been an error.
*/
int CPRCEN_engine_load_user_lexicon(CPRCEN_engine * eng,
				    int voice_index,
				    const char * fname);

/** Loads a user abbreviation file for the voice

Simple entries are in the format:
Dr	1	doctor
The first column is the abbreviation to match, the second one indicates whether
the following period -if any- should be removed, the third one the replacement.

The index will be less than the number returned by
CPRCEN_engine_get_voice_count().

Returns FALSE if there has been an error.
*/
int CPRCEN_engine_load_user_abbreviations(CPRCEN_engine * eng,
					  int voice_index,
					  const char * fname);

/** Load channel lexicon 

Simple entries are in the format:
hello h_\@0_l_ou1

The channel handle must correspond to an open channel. The user
lexicon will be available only on the given channel, as long
as it stays open.

The fname parameter must point to a valid file. 

The lname optional parameter allows the user to give a specific 
name to the lexicon for use with the

ASCII lexicons as well as compressed lexicon in cerelex formats 
are supported. PLS files are loaded with a seperate function.

Returns FALSE if there has been an error.
*/
int CPRCEN_engine_load_channel_lexicon(CPRCEN_engine * eng,
				       CPRCEN_channel_handle chan,
				       const char * fname,
				       const char * lname);


int CPRCEN_engine_load_channel_pls(CPRCEN_engine * eng, 
                                   CPRCEN_channel_handle chan, 
                                   const char * fname, 
                                   const char *lname);

int CPRCEN_engine_load_channel_abbreviation(CPRCEN_engine * eng,
					    CPRCEN_channel_handle chan,
					    const char * fname,
					    const char *aname);

int CPRCEN_engine_load_channel_pbreak(CPRCEN_engine * eng,
				      CPRCEN_channel_handle chan,
				      const char * fname);
/** @} */

/** \name Engine Information Functions */
/** @{ */

/** Return the number of loaded voices */
int CPRCEN_engine_get_voice_count(CPRCEN_engine * eng);

/** Get voice information 

Returns a char containing voice information.  The key is a string used to
look up the information about the voice. Useful keys:
- SAMPLE_RATE - sample rate, in hertz, of the voice (e.g. '22050')
- VOICE_NAME - name of the CereProc voice (e.g. 'Sarah')
- LANGUAGE_CODE_ISO - two-letter ISO language code (e.g. 'en')
- COUNTRY_CODE_ISO - two-letter ISO country code (e.g. 'GB')
- SEX - gender of the voice, ('male' or 'female')
- LANGUAGE_CODE_MICROSOFT - Language code used by MS SAPI (e.g. '809')
- COUNTRY - human-readable country description (e.g. 'Great Britain')
- REGION - human-readable region description (e.g. 'England')

The index will be less than the number returned by
CPRCEN_engine_get_voice_count().

Returns an empty string if the key is invalid or the voice does not
exist.  
*/
const char * CPRCEN_engine_get_voice_info(CPRCEN_engine * eng, 
					  int voice_index, 
					  const char * key);
/** Get voice file information

Returns a char containing voice information without loading a voice
into the engine.  The key parameters can be found in the documentation
for CPRCEN_engine_get_voice_info().

Returns an empty string if the key is invalid or the voice does not
exist.  
*/
const char * CPRCEN_engine_get_voice_file_info(const char * fname,
					       const char * key);

/* NOTE: XML information is not yet implemented */
/** Return XML information on loaded voices */
/* const char * CPRCEN_engine_get_voice_info_xml(CPRCEN_engine * eng); */

/** @} */

/** \name Channel Creation Functions */
/** @{ */

/** Create a new channel handle

The engine is searched for the best match of voice name, ISO language/region 
code, and sample rate (voice name is preferred). 

Returns FALSE if unable to create channel.
*/
CPRCEN_channel_handle CPRCEN_engine_open_channel(CPRCEN_engine * eng,
						 const char * iso_language_code,
						 const char * iso_region_code,
						 const char * voice_name, 
						 const char * srate);

/** Return a new channel handle for the default voice

The default channel is useful when a single voice is loaded.  When using 
multiple voices, the CPRCEN_engine_open_channel() function can select between
then when opening a channel.

Returns FALSE if unable to create channel.
*/
CPRCEN_channel_handle CPRCEN_engine_open_default_channel(CPRCEN_engine * eng);

/** Reset a channel

This function is safe to call inside the callback function.  Future processing 
is halted.  The channel is cleaned up by the engine for reuse. It does not
clear the callback - the CPRCEN_engine_clear_callback() should be used to clear
the callback data.

Returns FALSE if there is an error.
*/
int CPRCEN_engine_channel_reset(CPRCEN_engine * eng, 
				CPRCEN_channel_handle chan);

/** Release and delete a channel 

This function cannot be called within the callback function.

Returns FALSE if there is an error.
*/
int CPRCEN_engine_channel_close(CPRCEN_engine * eng, 
				CPRCEN_channel_handle chan);

/** @} */

/** \name Channel Callback Functions */
/** @{ */

/** Set a callback function for processing audio incrementally

The callback is fired phrase-by-phrase to allow incremental, low latency, 
processing of the speech output. The user_data pointer is used to store 
user-configurable information.

After a user callback has been set, speak calls do not return an audio buffer. 

Example callback:
\code
// A simple example callback function, appends audio to a file
void channel_callback(CPRC_abuf * abuf, void * userdata) {
   char * f = (char *) userdata;
   CPRC_riff_append(abuf, f);
}
// The callback would be initialised and set like this:
char * outfile = "out.wav";
CPRCEN_engine_set_callback(eng, chan, (void *)outfile, channel_callback);
\endcode

The tts_callback.c and tts_callback.py example programs contain
more extensive callback demonstration code.

Returns FALSE if unable to set the callback.
*/
int CPRCEN_engine_set_callback(CPRCEN_engine * eng, 
			       CPRCEN_channel_handle chan, 
			       void * userdata, 
			       cprcen_channel_callback callback);

/** Clear the callback data

After a user callback has been cleared, speak calls return an audio buffer. 

Returns FALSE if unable to clear the callback.
*/
int CPRCEN_engine_clear_callback(CPRCEN_engine * eng, 
				 CPRCEN_channel_handle chan);

/** Get the pointer to the user data on a channel 

Returns NULL if the user data cannot be retrieved.
*/
void * CPRCEN_engine_get_channel_userdata(CPRCEN_engine * eng, 
					  CPRCEN_channel_handle chan);
/** @} */

/** \name Channel Text to Speech Functions */
/** @{ */
/** Speak input text or XML

If no callback is set, a default callback is used and output is appended to 
the returned audio buffer.  To clear the output between requests, use the 
CPRCEN_engine_clear_callback() function.

If a callback has been set, audio will be processed by the callback function.

If flush is TRUE, regard text as complete and flush the output buffer.

Returns NULL if there is an error (e.g if the channel is not open).
*/
CPRC_abuf * CPRCEN_engine_channel_speak(CPRCEN_engine * eng, 
					CPRCEN_channel_handle chan, 
					const char * text, 
					int textlen, 
					int flush);

/** @} */

/** \name Channel Information Functions */

/** @{ */
/** Get information about the voice 

See the CPRCEN_engine_get_voice_info() section for information on the types of
information that are available, and example keys.

Returns an empty string if the key is invalid.
*/  
const char * CPRCEN_channel_get_voice_info(CPRCEN_engine * eng, 
					   CPRCEN_channel_handle chan, 
					   const char * key);

/* NOTE: XML information is not yet implemented */
/** Get XML describing channel attributes */
/* const char * CPRCEN_engine_get_channel_info_xml(CPRCEN_engine * eng, */
/*						CPRCEN_channel_handle chan); */



/** @} */

/** \name Channel Configuration Functions */

/** @{ */
#if !(defined __CPRCEN_ENGINE_H__)
/** Write audio generated on the channel to a file 

If the file exists, it is overwritten.  Subsequent
CPRCEN_engine_channel_speak() calls append to the file. If the
CPRCEN_engine_clear_callback() function is called, the output file
will be overwritten again on a speak.  To continually append to a
file, use the CPRCEN_engine_channel_append_to_file() function.

Returns FALSE if there is an error.
*/
int CPRCEN_engine_channel_to_file(CPRCEN_engine * eng, 
				  CPRCEN_channel_handle chan, 
				  char * fname, 
				  enum CPRCEN_AUDIO_FORMAT format);

/** Append audio generated on the channel to a file 

If the file does not exist, it will be created.  
If the file name changes then it will start clear the file
Audio continues to be appended to the file after calling CPRCEN_engine_clear_callback().

Returns FALSE if there is an error.
*/
int CPRCEN_engine_channel_append_to_file(CPRCEN_engine * eng, 
					 CPRCEN_channel_handle chan, 
					 char * fname, 
					 enum CPRCEN_AUDIO_FORMAT format);



/** Append audio generated on the channel to a file 

If the file does not exist, it will be created.  
Even if the file name is different it will append to the file.
Audio continues to be appended to the file after calling CPRCEN_engine_clear_callback().

Returns FALSE if there is an error.
*/
int CPRCEN_engine_channel_force_append_to_file(CPRCEN_engine * eng, 
                                         CPRCEN_channel_handle chan, 
                                         char * fname, 
                                         enum CPRCEN_AUDIO_FORMAT format);

/** Stop audio generated on the channel being appended to a file 

Returns FALSE if there is an error.
*/
int CPRCEN_engine_channel_no_file(CPRCEN_engine * eng, 
				  CPRCEN_channel_handle chan);
#endif

/** Set HTS mode, if available 

Returns FALSE if there is an error.
*/ 
int CPRCEN_channel_synth_type_hts(CPRCEN_engine * eng, 
				  CPRCEN_channel_handle chan);

/** Set unit selection mode

Returns FALSE if there is an error.
*/
int CPRCEN_channel_synth_type_usel(CPRCEN_engine * eng, 
				   CPRCEN_channel_handle chan);

/** Set the number of phones before audio is generated

Set the minimum and maximum number of phones to process before audio
output is generated. In normal operation, all the phones in a phrase
are processed before the channel callback is fired.  On slower CPUs a
long phrase may introduce unacceptable latency.  This mode can be used
to enable ultra-low latency speech output by setting a range of phones
within which the system must return speech.  For example, setting
min=10 and max=20 will ensure speech output is returned between phones
10 and 20 of the output.

When this mode is enabled, the channel callback will potentially fire
multiple times for each spurt.  The user must only process the data
between the 'wav_mk' and 'wav_done' audio buffer parameters (see
CPRC_abuf_wav_mk() and CPRC_abuf_wav_done()).

To reset to the default, call the function with min=0 and max=0.

Returns FALSE if there is an error.
*/
int CPRCEN_channel_set_phone_min_max(CPRCEN_engine * eng, 
				     CPRCEN_channel_handle chan,
				     int min, int max);

/** @} */

/** \name Audio Output Buffer Transcription Functions */
/** @{ */

/** Return a pointer to the transcription structure at index i 

Returns NULL if the index i is out of bounds.
*/
extern const CPRC_abuf_trans * CPRC_abuf_get_trans(CPRC_abuf * ab, 
						   int i);

/** Return the size of the transcription data */
extern int CPRC_abuf_trans_sz(CPRC_abuf * ab);

/** Return the name of the transcription element 

Returns an empty string if there has been an error.
*/
extern const char * CPRC_abuf_trans_name(const CPRC_abuf_trans * t);

/** Return the type of the transcription element

Returns CPRC_ABUF_TRANS_ERROR if there has been an error.
 */
extern enum CPRC_ABUF_TRANS CPRC_abuf_trans_type(const CPRC_abuf_trans * t);

/** Return start time (in seconds) of a transcription element 

Returns -1.0 if there has been an error.
*/
extern float CPRC_abuf_trans_start(const CPRC_abuf_trans * t);

/** Return end time (in seconds) of a transcription element 

Returns -1.0 if there has been an error.
*/
extern float CPRC_abuf_trans_end(const CPRC_abuf_trans * t);

/** \name Audio Output Buffer Access and Information Functions */
/** @{ */

/** Get length of the audio data in an audio buffer */
extern int CPRC_abuf_wav_sz(CPRC_abuf * ab);

/** Get a single sample from the audio data 

Returns 0 if the index i is out of bounds */
extern short CPRC_abuf_wav(CPRC_abuf * ab, 
			   int i);

/** Get a pointer to the raw audio data */
extern short * CPRC_abuf_wav_data(CPRC_abuf * ab);

/** Get the start point of safe data to process  */
extern int CPRC_abuf_wav_mk(CPRC_abuf * ab);

/** Get the end point of safe data to process */
extern int CPRC_abuf_wav_done(CPRC_abuf * ab);

/** Get the sample rate of the audio buffer 

The CPRCEN_engine_get_voice_info() and CPRCEN_channel_get_voice_info() 
functions should normally be used to access voice-related sample rate
information, as the audio buffer sample rate is changed to match the voice 
(if necessary) at synthesis time.
*/
extern int CPRC_abuf_wav_srate(CPRC_abuf * ab);

/** @} */

/** \name Audio Output Buffer Save Functions */
/** @{ */

/** Save the audio data as a RIFF wave file 

Returns FALSE if there has been an error.
*/
int CPRC_riff_save(CPRC_abuf * wav, const char * fname);

/** Append the audio data to a RIFF wave file 

Returns FALSE if there has been an error.
*/
int CPRC_riff_append(CPRC_abuf * wav, const char * fname);

/** Save the transcription section of the audio buffer 

Returns FALSE if there has been an error.
*/
int CPRC_riff_save_trans(CPRC_abuf * wav, const char * fname);

/** @} */

/** \name Advanced Audio Output Buffer Functions */

/** @{ */

/** Copy audio to a RIFF buffer

Copy the audio to a fixed in-memory buffer containing a RIFF header
and the audio data.  This can be useful for playing back in-memory
audio in some applications, avoiding the need to write audio to disk.
*/
CPTP_fixedbuf * CPRC_riff_buffer(CPRC_abuf * wav);

/** Delete a fixed buffer 

Delete a buffer as returned by CPRC_riff_buffer().
*/
void CPTP_fixedbuf_delete(CPTP_fixedbuf * fb);

/**  Make a copy of an audio buffer 

Make a copy of an audio buffer.  Useful if the user wishes to process
audio on a separate thread and allow the callback to continue.
*/
extern CPRC_abuf * CPRC_abuf_copy(CPRC_abuf * ab);

/** Delete an audio buffer

Clean up an audio buffer.  If a user creates a buffer with
CPRC_abuf_copy(), they should delete it with this function when
finished processing.
*/
extern void CPRC_abuf_delete(CPRC_abuf * ab);

/** @} */

#ifdef __cplusplus
}
#endif

#endif /* __CEREVOICE_ENG_H__ */
