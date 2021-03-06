# This file was automatically generated by SWIG (http://www.swig.org).
# Version 3.0.8
#
# Do not make changes to this file unless you know what you are doing--modify
# the SWIG interface file instead.





from sys import version_info
if version_info >= (2, 6, 0):
    def swig_import_helper():
        from os.path import dirname
        import imp
        fp = None
        try:
            fp, pathname, description = imp.find_module('_cerevoice_eng', [dirname(__file__)])
        except ImportError:
            import _cerevoice_eng
            return _cerevoice_eng
        if fp is not None:
            try:
                _mod = imp.load_module('_cerevoice_eng', fp, pathname, description)
            finally:
                fp.close()
            return _mod
    _cerevoice_eng = swig_import_helper()
    del swig_import_helper
else:
    import _cerevoice_eng
del version_info
try:
    _swig_property = property
except NameError:
    pass  # Python < 2.2 doesn't have 'property'.


def _swig_setattr_nondynamic(self, class_type, name, value, static=1):
    if (name == "thisown"):
        return self.this.own(value)
    if (name == "this"):
        if type(value).__name__ == 'SwigPyObject':
            self.__dict__[name] = value
            return
    method = class_type.__swig_setmethods__.get(name, None)
    if method:
        return method(self, value)
    if (not static):
        if _newclass:
            object.__setattr__(self, name, value)
        else:
            self.__dict__[name] = value
    else:
        raise AttributeError("You cannot add attributes to %s" % self)


def _swig_setattr(self, class_type, name, value):
    return _swig_setattr_nondynamic(self, class_type, name, value, 0)


def _swig_getattr_nondynamic(self, class_type, name, static=1):
    if (name == "thisown"):
        return self.this.own()
    method = class_type.__swig_getmethods__.get(name, None)
    if method:
        return method(self)
    if (not static):
        return object.__getattr__(self, name)
    else:
        raise AttributeError(name)

def _swig_getattr(self, class_type, name):
    return _swig_getattr_nondynamic(self, class_type, name, 0)


def _swig_repr(self):
    try:
        strthis = "proxy of " + self.this.__repr__()
    except Exception:
        strthis = ""
    return "<%s.%s; %s >" % (self.__class__.__module__, self.__class__.__name__, strthis,)

try:
    _object = object
    _newclass = 1
except AttributeError:
    class _object:
        pass
    _newclass = 0



def engine_set_callback(eng, chan, pyclass):
    """engine_set_callback(CPRCEN_engine * eng, CPRCEN_channel_handle chan, PyObject * pyclass) -> int"""
    return _cerevoice_eng.engine_set_callback(eng, chan, pyclass)

def engine_set_text_callback(eng, chan, pyinstance):
    """engine_set_text_callback(CPRCEN_engine * eng, CPRCEN_channel_handle chan, PyObject * pyinstance) -> int"""
    return _cerevoice_eng.engine_set_text_callback(eng, chan, pyinstance)

def data_to_abuf(l):
    """data_to_abuf(long l) -> CPRC_abuf *"""
    return _cerevoice_eng.data_to_abuf(l)

def abuf_to_string(abuf):
    """abuf_to_string(CPRC_abuf * abuf) -> PyObject *"""
    return _cerevoice_eng.abuf_to_string(abuf)
class CPRCEN_wav(_object):
    """Proxy of C++ CPRCEN_wav class."""

    __swig_setmethods__ = {}
    __setattr__ = lambda self, name, value: _swig_setattr(self, CPRCEN_wav, name, value)
    __swig_getmethods__ = {}
    __getattr__ = lambda self, name: _swig_getattr(self, CPRCEN_wav, name)
    __repr__ = _swig_repr
    __swig_setmethods__["wavdata"] = _cerevoice_eng.CPRCEN_wav_wavdata_set
    __swig_getmethods__["wavdata"] = _cerevoice_eng.CPRCEN_wav_wavdata_get
    if _newclass:
        wavdata = _swig_property(_cerevoice_eng.CPRCEN_wav_wavdata_get, _cerevoice_eng.CPRCEN_wav_wavdata_set)
    __swig_setmethods__["size"] = _cerevoice_eng.CPRCEN_wav_size_set
    __swig_getmethods__["size"] = _cerevoice_eng.CPRCEN_wav_size_get
    if _newclass:
        size = _swig_property(_cerevoice_eng.CPRCEN_wav_size_get, _cerevoice_eng.CPRCEN_wav_size_set)
    __swig_setmethods__["sample_rate"] = _cerevoice_eng.CPRCEN_wav_sample_rate_set
    __swig_getmethods__["sample_rate"] = _cerevoice_eng.CPRCEN_wav_sample_rate_get
    if _newclass:
        sample_rate = _swig_property(_cerevoice_eng.CPRCEN_wav_sample_rate_get, _cerevoice_eng.CPRCEN_wav_sample_rate_set)

    def __init__(self):
        """__init__(CPRCEN_wav self) -> CPRCEN_wav"""
        this = _cerevoice_eng.new_CPRCEN_wav()
        try:
            self.this.append(this)
        except Exception:
            self.this = this
    __swig_destroy__ = _cerevoice_eng.delete_CPRCEN_wav
    __del__ = lambda self: None
CPRCEN_wav_swigregister = _cerevoice_eng.CPRCEN_wav_swigregister
CPRCEN_wav_swigregister(CPRCEN_wav)


def CPRCEN_engine_load(licensef, voicef):
    """CPRCEN_engine_load(char const * licensef, char const * voicef) -> CPRCEN_engine *"""
    return _cerevoice_eng.CPRCEN_engine_load(licensef, voicef)

def CPRCEN_engine_load_config(licensef, voicef, voice_configf):
    """CPRCEN_engine_load_config(char const * licensef, char const * voicef, char const * voice_configf) -> CPRCEN_engine *"""
    return _cerevoice_eng.CPRCEN_engine_load_config(licensef, voicef, voice_configf)

def CPRCEN_engine_delete(eng):
    """CPRCEN_engine_delete(CPRCEN_engine * eng)"""
    return _cerevoice_eng.CPRCEN_engine_delete(eng)

def CPRCEN_engine_speak(eng, text):
    """CPRCEN_engine_speak(CPRCEN_engine * eng, char const * text) -> CPRCEN_wav *"""
    return _cerevoice_eng.CPRCEN_engine_speak(eng, text)

def CPRCEN_engine_speak_to_file(eng, text, fname):
    """CPRCEN_engine_speak_to_file(CPRCEN_engine * eng, char const * text, char const * fname) -> int"""
    return _cerevoice_eng.CPRCEN_engine_speak_to_file(eng, text, fname)

def CPRCEN_engine_clear(eng):
    """CPRCEN_engine_clear(CPRCEN_engine * eng) -> int"""
    return _cerevoice_eng.CPRCEN_engine_clear(eng)

_cerevoice_eng.CPRC_VOICE_LOAD_swigconstant(_cerevoice_eng)
CPRC_VOICE_LOAD = _cerevoice_eng.CPRC_VOICE_LOAD

_cerevoice_eng.CPRC_VOICE_LOAD_EMB_swigconstant(_cerevoice_eng)
CPRC_VOICE_LOAD_EMB = _cerevoice_eng.CPRC_VOICE_LOAD_EMB

_cerevoice_eng.CPRC_VOICE_LOAD_EMB_AUDIO_swigconstant(_cerevoice_eng)
CPRC_VOICE_LOAD_EMB_AUDIO = _cerevoice_eng.CPRC_VOICE_LOAD_EMB_AUDIO

_cerevoice_eng.CPRC_VOICE_LOAD_MEMMAP_swigconstant(_cerevoice_eng)
CPRC_VOICE_LOAD_MEMMAP = _cerevoice_eng.CPRC_VOICE_LOAD_MEMMAP

_cerevoice_eng.CPRC_VOICE_LOAD_TP_swigconstant(_cerevoice_eng)
CPRC_VOICE_LOAD_TP = _cerevoice_eng.CPRC_VOICE_LOAD_TP

_cerevoice_eng.CPRCEN_RAW_swigconstant(_cerevoice_eng)
CPRCEN_RAW = _cerevoice_eng.CPRCEN_RAW

_cerevoice_eng.CPRCEN_RIFF_swigconstant(_cerevoice_eng)
CPRCEN_RIFF = _cerevoice_eng.CPRCEN_RIFF

_cerevoice_eng.CPRCEN_AIFF_swigconstant(_cerevoice_eng)
CPRCEN_AIFF = _cerevoice_eng.CPRCEN_AIFF

_cerevoice_eng.CPRCEN_SYNTH_NONE_swigconstant(_cerevoice_eng)
CPRCEN_SYNTH_NONE = _cerevoice_eng.CPRCEN_SYNTH_NONE

_cerevoice_eng.CPRCEN_SYNTH_USEL_swigconstant(_cerevoice_eng)
CPRCEN_SYNTH_USEL = _cerevoice_eng.CPRCEN_SYNTH_USEL

_cerevoice_eng.CPRCEN_SYNTH_HTS_swigconstant(_cerevoice_eng)
CPRCEN_SYNTH_HTS = _cerevoice_eng.CPRCEN_SYNTH_HTS

_cerevoice_eng.CPRCEN_SYNTH_HTSPROS_swigconstant(_cerevoice_eng)
CPRCEN_SYNTH_HTSPROS = _cerevoice_eng.CPRCEN_SYNTH_HTSPROS

_cerevoice_eng.CPRCEN_SYNTH_PARAM_swigconstant(_cerevoice_eng)
CPRCEN_SYNTH_PARAM = _cerevoice_eng.CPRCEN_SYNTH_PARAM

_cerevoice_eng.CPRCEN_SYNTH_HYBRID_swigconstant(_cerevoice_eng)
CPRCEN_SYNTH_HYBRID = _cerevoice_eng.CPRCEN_SYNTH_HYBRID

_cerevoice_eng.CPRC_ABUF_TRANS_PHONE_swigconstant(_cerevoice_eng)
CPRC_ABUF_TRANS_PHONE = _cerevoice_eng.CPRC_ABUF_TRANS_PHONE

_cerevoice_eng.CPRC_ABUF_TRANS_WORD_swigconstant(_cerevoice_eng)
CPRC_ABUF_TRANS_WORD = _cerevoice_eng.CPRC_ABUF_TRANS_WORD

_cerevoice_eng.CPRC_ABUF_TRANS_MARK_swigconstant(_cerevoice_eng)
CPRC_ABUF_TRANS_MARK = _cerevoice_eng.CPRC_ABUF_TRANS_MARK

_cerevoice_eng.CPRC_ABUF_TRANS_ERROR_swigconstant(_cerevoice_eng)
CPRC_ABUF_TRANS_ERROR = _cerevoice_eng.CPRC_ABUF_TRANS_ERROR

_cerevoice_eng.CPRC_ABUF_TRANS_TYPES_swigconstant(_cerevoice_eng)
CPRC_ABUF_TRANS_TYPES = _cerevoice_eng.CPRC_ABUF_TRANS_TYPES
class CPRC_abuf_trans(_object):
    """Proxy of C++ CPRC_abuf_trans class."""

    __swig_setmethods__ = {}
    __setattr__ = lambda self, name, value: _swig_setattr(self, CPRC_abuf_trans, name, value)
    __swig_getmethods__ = {}
    __getattr__ = lambda self, name: _swig_getattr(self, CPRC_abuf_trans, name)
    __repr__ = _swig_repr
    __swig_setmethods__["name"] = _cerevoice_eng.CPRC_abuf_trans_name_set
    __swig_getmethods__["name"] = _cerevoice_eng.CPRC_abuf_trans_name_get
    if _newclass:
        name = _swig_property(_cerevoice_eng.CPRC_abuf_trans_name_get, _cerevoice_eng.CPRC_abuf_trans_name_set)
    __swig_setmethods__["type"] = _cerevoice_eng.CPRC_abuf_trans_type_set
    __swig_getmethods__["type"] = _cerevoice_eng.CPRC_abuf_trans_type_get
    if _newclass:
        type = _swig_property(_cerevoice_eng.CPRC_abuf_trans_type_get, _cerevoice_eng.CPRC_abuf_trans_type_set)
    __swig_setmethods__["start"] = _cerevoice_eng.CPRC_abuf_trans_start_set
    __swig_getmethods__["start"] = _cerevoice_eng.CPRC_abuf_trans_start_get
    if _newclass:
        start = _swig_property(_cerevoice_eng.CPRC_abuf_trans_start_get, _cerevoice_eng.CPRC_abuf_trans_start_set)
    __swig_setmethods__["end"] = _cerevoice_eng.CPRC_abuf_trans_end_set
    __swig_getmethods__["end"] = _cerevoice_eng.CPRC_abuf_trans_end_get
    if _newclass:
        end = _swig_property(_cerevoice_eng.CPRC_abuf_trans_end_get, _cerevoice_eng.CPRC_abuf_trans_end_set)
    __swig_setmethods__["dsp"] = _cerevoice_eng.CPRC_abuf_trans_dsp_set
    __swig_getmethods__["dsp"] = _cerevoice_eng.CPRC_abuf_trans_dsp_get
    if _newclass:
        dsp = _swig_property(_cerevoice_eng.CPRC_abuf_trans_dsp_get, _cerevoice_eng.CPRC_abuf_trans_dsp_set)
    __swig_setmethods__["phone"] = _cerevoice_eng.CPRC_abuf_trans_phone_set
    __swig_getmethods__["phone"] = _cerevoice_eng.CPRC_abuf_trans_phone_get
    if _newclass:
        phone = _swig_property(_cerevoice_eng.CPRC_abuf_trans_phone_get, _cerevoice_eng.CPRC_abuf_trans_phone_set)

    def __init__(self):
        """__init__(CPRC_abuf_trans self) -> CPRC_abuf_trans"""
        this = _cerevoice_eng.new_CPRC_abuf_trans()
        try:
            self.this.append(this)
        except Exception:
            self.this = this
    __swig_destroy__ = _cerevoice_eng.delete_CPRC_abuf_trans
    __del__ = lambda self: None
CPRC_abuf_trans_swigregister = _cerevoice_eng.CPRC_abuf_trans_swigregister
CPRC_abuf_trans_swigregister(CPRC_abuf_trans)

class CPTP_fixedbuf(_object):
    """Proxy of C++ CPTP_fixedbuf class."""

    __swig_setmethods__ = {}
    __setattr__ = lambda self, name, value: _swig_setattr(self, CPTP_fixedbuf, name, value)
    __swig_getmethods__ = {}
    __getattr__ = lambda self, name: _swig_getattr(self, CPTP_fixedbuf, name)
    __repr__ = _swig_repr
    __swig_setmethods__["_size"] = _cerevoice_eng.CPTP_fixedbuf__size_set
    __swig_getmethods__["_size"] = _cerevoice_eng.CPTP_fixedbuf__size_get
    if _newclass:
        _size = _swig_property(_cerevoice_eng.CPTP_fixedbuf__size_get, _cerevoice_eng.CPTP_fixedbuf__size_set)
    __swig_setmethods__["_buffer"] = _cerevoice_eng.CPTP_fixedbuf__buffer_set
    __swig_getmethods__["_buffer"] = _cerevoice_eng.CPTP_fixedbuf__buffer_get
    if _newclass:
        _buffer = _swig_property(_cerevoice_eng.CPTP_fixedbuf__buffer_get, _cerevoice_eng.CPTP_fixedbuf__buffer_set)

    def __init__(self):
        """__init__(CPTP_fixedbuf self) -> CPTP_fixedbuf"""
        this = _cerevoice_eng.new_CPTP_fixedbuf()
        try:
            self.this.append(this)
        except Exception:
            self.this = this
    __swig_destroy__ = _cerevoice_eng.delete_CPTP_fixedbuf
    __del__ = lambda self: None
CPTP_fixedbuf_swigregister = _cerevoice_eng.CPTP_fixedbuf_swigregister
CPTP_fixedbuf_swigregister(CPTP_fixedbuf)


def CPRCEN_engine_new():
    """CPRCEN_engine_new() -> CPRCEN_engine *"""
    return _cerevoice_eng.CPRCEN_engine_new()

def CPRCEN_engine_load_voice(eng, licensef, configf, voicef, load_type):
    """CPRCEN_engine_load_voice(CPRCEN_engine * eng, char const * licensef, char const * configf, char const * voicef, enum CPRC_VOICE_LOAD_TYPE load_type) -> int"""
    return _cerevoice_eng.CPRCEN_engine_load_voice(eng, licensef, configf, voicef, load_type)

def CPRCEN_engine_load_voice_licensestr(eng, license_text, signature, configf, voicef, load_type):
    """CPRCEN_engine_load_voice_licensestr(CPRCEN_engine * eng, char const * license_text, char const * signature, char const * configf, char const * voicef, enum CPRC_VOICE_LOAD_TYPE load_type) -> int"""
    return _cerevoice_eng.CPRCEN_engine_load_voice_licensestr(eng, license_text, signature, configf, voicef, load_type)

def CPRCEN_engine_unload_voice(eng, voice_index):
    """CPRCEN_engine_unload_voice(CPRCEN_engine * eng, int voice_index) -> int"""
    return _cerevoice_eng.CPRCEN_engine_unload_voice(eng, voice_index)

def CPRCEN_engine_load_user_lexicon(eng, voice_index, fname):
    """CPRCEN_engine_load_user_lexicon(CPRCEN_engine * eng, int voice_index, char const * fname) -> int"""
    return _cerevoice_eng.CPRCEN_engine_load_user_lexicon(eng, voice_index, fname)

def CPRCEN_engine_load_user_abbreviations(eng, voice_index, fname):
    """CPRCEN_engine_load_user_abbreviations(CPRCEN_engine * eng, int voice_index, char const * fname) -> int"""
    return _cerevoice_eng.CPRCEN_engine_load_user_abbreviations(eng, voice_index, fname)

def CPRCEN_engine_load_channel_lexicon(eng, chan, fname, lname):
    """CPRCEN_engine_load_channel_lexicon(CPRCEN_engine * eng, CPRCEN_channel_handle chan, char const * fname, char const * lname) -> int"""
    return _cerevoice_eng.CPRCEN_engine_load_channel_lexicon(eng, chan, fname, lname)

def CPRCEN_engine_load_channel_pls(eng, chan, fname, lname):
    """CPRCEN_engine_load_channel_pls(CPRCEN_engine * eng, CPRCEN_channel_handle chan, char const * fname, char const * lname) -> int"""
    return _cerevoice_eng.CPRCEN_engine_load_channel_pls(eng, chan, fname, lname)

def CPRCEN_engine_load_channel_abbreviation(eng, chan, fname, aname):
    """CPRCEN_engine_load_channel_abbreviation(CPRCEN_engine * eng, CPRCEN_channel_handle chan, char const * fname, char const * aname) -> int"""
    return _cerevoice_eng.CPRCEN_engine_load_channel_abbreviation(eng, chan, fname, aname)

def CPRCEN_engine_load_channel_pbreak(eng, chan, fname):
    """CPRCEN_engine_load_channel_pbreak(CPRCEN_engine * eng, CPRCEN_channel_handle chan, char const * fname) -> int"""
    return _cerevoice_eng.CPRCEN_engine_load_channel_pbreak(eng, chan, fname)

def CPRCEN_engine_get_voice_count(eng):
    """CPRCEN_engine_get_voice_count(CPRCEN_engine * eng) -> int"""
    return _cerevoice_eng.CPRCEN_engine_get_voice_count(eng)

def CPRCEN_engine_get_voice_info(eng, voice_index, key):
    """CPRCEN_engine_get_voice_info(CPRCEN_engine * eng, int voice_index, char const * key) -> char const *"""
    return _cerevoice_eng.CPRCEN_engine_get_voice_info(eng, voice_index, key)

def CPRCEN_engine_get_voice_file_info(fname, key):
    """CPRCEN_engine_get_voice_file_info(char const * fname, char const * key) -> char const *"""
    return _cerevoice_eng.CPRCEN_engine_get_voice_file_info(fname, key)

def CPRCEN_engine_open_channel(eng, iso_language_code, iso_region_code, voice_name, srate):
    """CPRCEN_engine_open_channel(CPRCEN_engine * eng, char const * iso_language_code, char const * iso_region_code, char const * voice_name, char const * srate) -> CPRCEN_channel_handle"""
    return _cerevoice_eng.CPRCEN_engine_open_channel(eng, iso_language_code, iso_region_code, voice_name, srate)

def CPRCEN_engine_open_default_channel(eng):
    """CPRCEN_engine_open_default_channel(CPRCEN_engine * eng) -> CPRCEN_channel_handle"""
    return _cerevoice_eng.CPRCEN_engine_open_default_channel(eng)

def CPRCEN_engine_channel_reset(eng, chan):
    """CPRCEN_engine_channel_reset(CPRCEN_engine * eng, CPRCEN_channel_handle chan) -> int"""
    return _cerevoice_eng.CPRCEN_engine_channel_reset(eng, chan)

def CPRCEN_engine_channel_close(eng, chan):
    """CPRCEN_engine_channel_close(CPRCEN_engine * eng, CPRCEN_channel_handle chan) -> int"""
    return _cerevoice_eng.CPRCEN_engine_channel_close(eng, chan)

def CPRCEN_engine_set_callback(eng, chan, userdata, callback):
    """CPRCEN_engine_set_callback(CPRCEN_engine * eng, CPRCEN_channel_handle chan, void * userdata, cprcen_channel_callback callback) -> int"""
    return _cerevoice_eng.CPRCEN_engine_set_callback(eng, chan, userdata, callback)

def CPRCEN_engine_clear_callback(eng, chan):
    """CPRCEN_engine_clear_callback(CPRCEN_engine * eng, CPRCEN_channel_handle chan) -> int"""
    return _cerevoice_eng.CPRCEN_engine_clear_callback(eng, chan)

def CPRCEN_engine_get_channel_userdata(eng, chan):
    """CPRCEN_engine_get_channel_userdata(CPRCEN_engine * eng, CPRCEN_channel_handle chan) -> void *"""
    return _cerevoice_eng.CPRCEN_engine_get_channel_userdata(eng, chan)

def CPRCEN_engine_channel_speak(eng, chan, text, textlen, flush):
    """CPRCEN_engine_channel_speak(CPRCEN_engine * eng, CPRCEN_channel_handle chan, char const * text, int textlen, int flush) -> CPRC_abuf *"""
    return _cerevoice_eng.CPRCEN_engine_channel_speak(eng, chan, text, textlen, flush)

def CPRCEN_channel_get_voice_info(eng, chan, key):
    """CPRCEN_channel_get_voice_info(CPRCEN_engine * eng, CPRCEN_channel_handle chan, char const * key) -> char const *"""
    return _cerevoice_eng.CPRCEN_channel_get_voice_info(eng, chan, key)

def CPRCEN_engine_channel_to_file(eng, chan, fname, format):
    """CPRCEN_engine_channel_to_file(CPRCEN_engine * eng, CPRCEN_channel_handle chan, char * fname, enum CPRCEN_AUDIO_FORMAT format) -> int"""
    return _cerevoice_eng.CPRCEN_engine_channel_to_file(eng, chan, fname, format)

def CPRCEN_engine_channel_append_to_file(eng, chan, fname, format):
    """CPRCEN_engine_channel_append_to_file(CPRCEN_engine * eng, CPRCEN_channel_handle chan, char * fname, enum CPRCEN_AUDIO_FORMAT format) -> int"""
    return _cerevoice_eng.CPRCEN_engine_channel_append_to_file(eng, chan, fname, format)

def CPRCEN_engine_channel_force_append_to_file(eng, chan, fname, format):
    """CPRCEN_engine_channel_force_append_to_file(CPRCEN_engine * eng, CPRCEN_channel_handle chan, char * fname, enum CPRCEN_AUDIO_FORMAT format) -> int"""
    return _cerevoice_eng.CPRCEN_engine_channel_force_append_to_file(eng, chan, fname, format)

def CPRCEN_engine_channel_no_file(eng, chan):
    """CPRCEN_engine_channel_no_file(CPRCEN_engine * eng, CPRCEN_channel_handle chan) -> int"""
    return _cerevoice_eng.CPRCEN_engine_channel_no_file(eng, chan)

def CPRCEN_channel_synth_type_hts(eng, chan):
    """CPRCEN_channel_synth_type_hts(CPRCEN_engine * eng, CPRCEN_channel_handle chan) -> int"""
    return _cerevoice_eng.CPRCEN_channel_synth_type_hts(eng, chan)

def CPRCEN_channel_synth_type_usel(eng, chan):
    """CPRCEN_channel_synth_type_usel(CPRCEN_engine * eng, CPRCEN_channel_handle chan) -> int"""
    return _cerevoice_eng.CPRCEN_channel_synth_type_usel(eng, chan)

def CPRCEN_channel_set_phone_min_max(eng, chan, min, max):
    """CPRCEN_channel_set_phone_min_max(CPRCEN_engine * eng, CPRCEN_channel_handle chan, int min, int max) -> int"""
    return _cerevoice_eng.CPRCEN_channel_set_phone_min_max(eng, chan, min, max)

def CPRC_abuf_get_trans(ab, i):
    """CPRC_abuf_get_trans(CPRC_abuf * ab, int i) -> CPRC_abuf_trans const *"""
    return _cerevoice_eng.CPRC_abuf_get_trans(ab, i)

def CPRC_abuf_trans_sz(ab):
    """CPRC_abuf_trans_sz(CPRC_abuf * ab) -> int"""
    return _cerevoice_eng.CPRC_abuf_trans_sz(ab)

def CPRC_abuf_trans_name(t):
    """CPRC_abuf_trans_name(CPRC_abuf_trans const * t) -> char const *"""
    return _cerevoice_eng.CPRC_abuf_trans_name(t)

def CPRC_abuf_trans_type(t):
    """CPRC_abuf_trans_type(CPRC_abuf_trans const * t) -> enum CPRC_ABUF_TRANS"""
    return _cerevoice_eng.CPRC_abuf_trans_type(t)

def CPRC_abuf_trans_start(t):
    """CPRC_abuf_trans_start(CPRC_abuf_trans const * t) -> float"""
    return _cerevoice_eng.CPRC_abuf_trans_start(t)

def CPRC_abuf_trans_end(t):
    """CPRC_abuf_trans_end(CPRC_abuf_trans const * t) -> float"""
    return _cerevoice_eng.CPRC_abuf_trans_end(t)

def CPRC_abuf_wav_sz(ab):
    """CPRC_abuf_wav_sz(CPRC_abuf * ab) -> int"""
    return _cerevoice_eng.CPRC_abuf_wav_sz(ab)

def CPRC_abuf_wav(ab, i):
    """CPRC_abuf_wav(CPRC_abuf * ab, int i) -> short"""
    return _cerevoice_eng.CPRC_abuf_wav(ab, i)

def CPRC_abuf_wav_data(ab):
    """CPRC_abuf_wav_data(CPRC_abuf * ab) -> short *"""
    return _cerevoice_eng.CPRC_abuf_wav_data(ab)

def CPRC_abuf_wav_mk(ab):
    """CPRC_abuf_wav_mk(CPRC_abuf * ab) -> int"""
    return _cerevoice_eng.CPRC_abuf_wav_mk(ab)

def CPRC_abuf_wav_done(ab):
    """CPRC_abuf_wav_done(CPRC_abuf * ab) -> int"""
    return _cerevoice_eng.CPRC_abuf_wav_done(ab)

def CPRC_abuf_wav_srate(ab):
    """CPRC_abuf_wav_srate(CPRC_abuf * ab) -> int"""
    return _cerevoice_eng.CPRC_abuf_wav_srate(ab)

def CPRC_riff_save(wav, fname):
    """CPRC_riff_save(CPRC_abuf * wav, char const * fname) -> int"""
    return _cerevoice_eng.CPRC_riff_save(wav, fname)

def CPRC_riff_append(wav, fname):
    """CPRC_riff_append(CPRC_abuf * wav, char const * fname) -> int"""
    return _cerevoice_eng.CPRC_riff_append(wav, fname)

def CPRC_riff_save_trans(wav, fname):
    """CPRC_riff_save_trans(CPRC_abuf * wav, char const * fname) -> int"""
    return _cerevoice_eng.CPRC_riff_save_trans(wav, fname)

def CPRC_riff_buffer(wav):
    """CPRC_riff_buffer(CPRC_abuf * wav) -> CPTP_fixedbuf *"""
    return _cerevoice_eng.CPRC_riff_buffer(wav)

def CPTP_fixedbuf_delete(fb):
    """CPTP_fixedbuf_delete(CPTP_fixedbuf * fb)"""
    return _cerevoice_eng.CPTP_fixedbuf_delete(fb)

def CPRC_abuf_copy(ab):
    """CPRC_abuf_copy(CPRC_abuf * ab) -> CPRC_abuf *"""
    return _cerevoice_eng.CPRC_abuf_copy(ab)

def CPRC_abuf_delete(ab):
    """CPRC_abuf_delete(CPRC_abuf * ab)"""
    return _cerevoice_eng.CPRC_abuf_delete(ab)

_cerevoice_eng.CPRCEN_CHAN_FULL_swigconstant(_cerevoice_eng)
CPRCEN_CHAN_FULL = _cerevoice_eng.CPRCEN_CHAN_FULL

_cerevoice_eng.CPRCEN_CHAN_TK_swigconstant(_cerevoice_eng)
CPRCEN_CHAN_TK = _cerevoice_eng.CPRCEN_CHAN_TK

_cerevoice_eng.CPRCEN_CHAN_BACK_swigconstant(_cerevoice_eng)
CPRCEN_CHAN_BACK = _cerevoice_eng.CPRCEN_CHAN_BACK

_cerevoice_eng.CPRCEN_CHAN_NORM_swigconstant(_cerevoice_eng)
CPRCEN_CHAN_NORM = _cerevoice_eng.CPRCEN_CHAN_NORM

_cerevoice_eng.CPRCEN_CHAN_SPURT_swigconstant(_cerevoice_eng)
CPRCEN_CHAN_SPURT = _cerevoice_eng.CPRCEN_CHAN_SPURT

_cerevoice_eng.CPRCEN_CHAN_ABOOK_swigconstant(_cerevoice_eng)
CPRCEN_CHAN_ABOOK = _cerevoice_eng.CPRCEN_CHAN_ABOOK

_cerevoice_eng.CPRC_ABUF_TRANS_STATUS_NODSP_swigconstant(_cerevoice_eng)
CPRC_ABUF_TRANS_STATUS_NODSP = _cerevoice_eng.CPRC_ABUF_TRANS_STATUS_NODSP

_cerevoice_eng.CPRC_ABUF_TRANS_STATUS_DSPTODO_swigconstant(_cerevoice_eng)
CPRC_ABUF_TRANS_STATUS_DSPTODO = _cerevoice_eng.CPRC_ABUF_TRANS_STATUS_DSPTODO

_cerevoice_eng.CPRC_ABUF_TRANS_STATUS_DSPDONE_swigconstant(_cerevoice_eng)
CPRC_ABUF_TRANS_STATUS_DSPDONE = _cerevoice_eng.CPRC_ABUF_TRANS_STATUS_DSPDONE
class CPRC_abuf_dsp(_object):
    """Proxy of C++ CPRC_abuf_dsp class."""

    __swig_setmethods__ = {}
    __setattr__ = lambda self, name, value: _swig_setattr(self, CPRC_abuf_dsp, name, value)
    __swig_getmethods__ = {}
    __getattr__ = lambda self, name: _swig_getattr(self, CPRC_abuf_dsp, name)
    __repr__ = _swig_repr
    __swig_setmethods__["status"] = _cerevoice_eng.CPRC_abuf_dsp_status_set
    __swig_getmethods__["status"] = _cerevoice_eng.CPRC_abuf_dsp_status_get
    if _newclass:
        status = _swig_property(_cerevoice_eng.CPRC_abuf_dsp_status_get, _cerevoice_eng.CPRC_abuf_dsp_status_set)
    __swig_setmethods__["mid"] = _cerevoice_eng.CPRC_abuf_dsp_mid_set
    __swig_getmethods__["mid"] = _cerevoice_eng.CPRC_abuf_dsp_mid_get
    if _newclass:
        mid = _swig_property(_cerevoice_eng.CPRC_abuf_dsp_mid_get, _cerevoice_eng.CPRC_abuf_dsp_mid_set)
    __swig_setmethods__["dur"] = _cerevoice_eng.CPRC_abuf_dsp_dur_set
    __swig_getmethods__["dur"] = _cerevoice_eng.CPRC_abuf_dsp_dur_get
    if _newclass:
        dur = _swig_property(_cerevoice_eng.CPRC_abuf_dsp_dur_get, _cerevoice_eng.CPRC_abuf_dsp_dur_set)
    __swig_setmethods__["f0h1"] = _cerevoice_eng.CPRC_abuf_dsp_f0h1_set
    __swig_getmethods__["f0h1"] = _cerevoice_eng.CPRC_abuf_dsp_f0h1_get
    if _newclass:
        f0h1 = _swig_property(_cerevoice_eng.CPRC_abuf_dsp_f0h1_get, _cerevoice_eng.CPRC_abuf_dsp_f0h1_set)
    __swig_setmethods__["f0h2"] = _cerevoice_eng.CPRC_abuf_dsp_f0h2_set
    __swig_getmethods__["f0h2"] = _cerevoice_eng.CPRC_abuf_dsp_f0h2_get
    if _newclass:
        f0h2 = _swig_property(_cerevoice_eng.CPRC_abuf_dsp_f0h2_get, _cerevoice_eng.CPRC_abuf_dsp_f0h2_set)
    __swig_setmethods__["lognrg"] = _cerevoice_eng.CPRC_abuf_dsp_lognrg_set
    __swig_getmethods__["lognrg"] = _cerevoice_eng.CPRC_abuf_dsp_lognrg_get
    if _newclass:
        lognrg = _swig_property(_cerevoice_eng.CPRC_abuf_dsp_lognrg_get, _cerevoice_eng.CPRC_abuf_dsp_lognrg_set)
    __swig_setmethods__["usel_start"] = _cerevoice_eng.CPRC_abuf_dsp_usel_start_set
    __swig_getmethods__["usel_start"] = _cerevoice_eng.CPRC_abuf_dsp_usel_start_get
    if _newclass:
        usel_start = _swig_property(_cerevoice_eng.CPRC_abuf_dsp_usel_start_get, _cerevoice_eng.CPRC_abuf_dsp_usel_start_set)
    __swig_setmethods__["usel_mid"] = _cerevoice_eng.CPRC_abuf_dsp_usel_mid_set
    __swig_getmethods__["usel_mid"] = _cerevoice_eng.CPRC_abuf_dsp_usel_mid_get
    if _newclass:
        usel_mid = _swig_property(_cerevoice_eng.CPRC_abuf_dsp_usel_mid_get, _cerevoice_eng.CPRC_abuf_dsp_usel_mid_set)
    __swig_setmethods__["usel_end"] = _cerevoice_eng.CPRC_abuf_dsp_usel_end_set
    __swig_getmethods__["usel_end"] = _cerevoice_eng.CPRC_abuf_dsp_usel_end_get
    if _newclass:
        usel_end = _swig_property(_cerevoice_eng.CPRC_abuf_dsp_usel_end_get, _cerevoice_eng.CPRC_abuf_dsp_usel_end_set)
    __swig_setmethods__["usel_dur"] = _cerevoice_eng.CPRC_abuf_dsp_usel_dur_set
    __swig_getmethods__["usel_dur"] = _cerevoice_eng.CPRC_abuf_dsp_usel_dur_get
    if _newclass:
        usel_dur = _swig_property(_cerevoice_eng.CPRC_abuf_dsp_usel_dur_get, _cerevoice_eng.CPRC_abuf_dsp_usel_dur_set)
    __swig_setmethods__["usel_f0h1"] = _cerevoice_eng.CPRC_abuf_dsp_usel_f0h1_set
    __swig_getmethods__["usel_f0h1"] = _cerevoice_eng.CPRC_abuf_dsp_usel_f0h1_get
    if _newclass:
        usel_f0h1 = _swig_property(_cerevoice_eng.CPRC_abuf_dsp_usel_f0h1_get, _cerevoice_eng.CPRC_abuf_dsp_usel_f0h1_set)
    __swig_setmethods__["usel_f0h2"] = _cerevoice_eng.CPRC_abuf_dsp_usel_f0h2_set
    __swig_getmethods__["usel_f0h2"] = _cerevoice_eng.CPRC_abuf_dsp_usel_f0h2_get
    if _newclass:
        usel_f0h2 = _swig_property(_cerevoice_eng.CPRC_abuf_dsp_usel_f0h2_get, _cerevoice_eng.CPRC_abuf_dsp_usel_f0h2_set)
    __swig_setmethods__["usel_lognrg"] = _cerevoice_eng.CPRC_abuf_dsp_usel_lognrg_set
    __swig_getmethods__["usel_lognrg"] = _cerevoice_eng.CPRC_abuf_dsp_usel_lognrg_get
    if _newclass:
        usel_lognrg = _swig_property(_cerevoice_eng.CPRC_abuf_dsp_usel_lognrg_get, _cerevoice_eng.CPRC_abuf_dsp_usel_lognrg_set)
    __swig_setmethods__["dsp_dur"] = _cerevoice_eng.CPRC_abuf_dsp_dsp_dur_set
    __swig_getmethods__["dsp_dur"] = _cerevoice_eng.CPRC_abuf_dsp_dsp_dur_get
    if _newclass:
        dsp_dur = _swig_property(_cerevoice_eng.CPRC_abuf_dsp_dsp_dur_get, _cerevoice_eng.CPRC_abuf_dsp_dsp_dur_set)
    __swig_setmethods__["dsp_f0h1"] = _cerevoice_eng.CPRC_abuf_dsp_dsp_f0h1_set
    __swig_getmethods__["dsp_f0h1"] = _cerevoice_eng.CPRC_abuf_dsp_dsp_f0h1_get
    if _newclass:
        dsp_f0h1 = _swig_property(_cerevoice_eng.CPRC_abuf_dsp_dsp_f0h1_get, _cerevoice_eng.CPRC_abuf_dsp_dsp_f0h1_set)
    __swig_setmethods__["dsp_f0h2"] = _cerevoice_eng.CPRC_abuf_dsp_dsp_f0h2_set
    __swig_getmethods__["dsp_f0h2"] = _cerevoice_eng.CPRC_abuf_dsp_dsp_f0h2_get
    if _newclass:
        dsp_f0h2 = _swig_property(_cerevoice_eng.CPRC_abuf_dsp_dsp_f0h2_get, _cerevoice_eng.CPRC_abuf_dsp_dsp_f0h2_set)
    __swig_setmethods__["dsp_lognrg"] = _cerevoice_eng.CPRC_abuf_dsp_dsp_lognrg_set
    __swig_getmethods__["dsp_lognrg"] = _cerevoice_eng.CPRC_abuf_dsp_dsp_lognrg_get
    if _newclass:
        dsp_lognrg = _swig_property(_cerevoice_eng.CPRC_abuf_dsp_dsp_lognrg_get, _cerevoice_eng.CPRC_abuf_dsp_dsp_lognrg_set)

    def __init__(self):
        """__init__(CPRC_abuf_dsp self) -> CPRC_abuf_dsp"""
        this = _cerevoice_eng.new_CPRC_abuf_dsp()
        try:
            self.this.append(this)
        except Exception:
            self.this = this
    __swig_destroy__ = _cerevoice_eng.delete_CPRC_abuf_dsp
    __del__ = lambda self: None
CPRC_abuf_dsp_swigregister = _cerevoice_eng.CPRC_abuf_dsp_swigregister
CPRC_abuf_dsp_swigregister(CPRC_abuf_dsp)

class CPRC_abuf(_object):
    """Proxy of C++ CPRC_abuf class."""

    __swig_setmethods__ = {}
    __setattr__ = lambda self, name, value: _swig_setattr(self, CPRC_abuf, name, value)
    __swig_getmethods__ = {}
    __getattr__ = lambda self, name: _swig_getattr(self, CPRC_abuf, name)
    __repr__ = _swig_repr
    __swig_setmethods__["wav_sz"] = _cerevoice_eng.CPRC_abuf_wav_sz_set
    __swig_getmethods__["wav_sz"] = _cerevoice_eng.CPRC_abuf_wav_sz_get
    if _newclass:
        wav_sz = _swig_property(_cerevoice_eng.CPRC_abuf_wav_sz_get, _cerevoice_eng.CPRC_abuf_wav_sz_set)
    __swig_setmethods__["wav_done"] = _cerevoice_eng.CPRC_abuf_wav_done_set
    __swig_getmethods__["wav_done"] = _cerevoice_eng.CPRC_abuf_wav_done_get
    if _newclass:
        wav_done = _swig_property(_cerevoice_eng.CPRC_abuf_wav_done_get, _cerevoice_eng.CPRC_abuf_wav_done_set)
    __swig_setmethods__["wav_mem"] = _cerevoice_eng.CPRC_abuf_wav_mem_set
    __swig_getmethods__["wav_mem"] = _cerevoice_eng.CPRC_abuf_wav_mem_get
    if _newclass:
        wav_mem = _swig_property(_cerevoice_eng.CPRC_abuf_wav_mem_get, _cerevoice_eng.CPRC_abuf_wav_mem_set)
    __swig_setmethods__["wav_mk"] = _cerevoice_eng.CPRC_abuf_wav_mk_set
    __swig_getmethods__["wav_mk"] = _cerevoice_eng.CPRC_abuf_wav_mk_get
    if _newclass:
        wav_mk = _swig_property(_cerevoice_eng.CPRC_abuf_wav_mk_get, _cerevoice_eng.CPRC_abuf_wav_mk_set)
    __swig_setmethods__["srate"] = _cerevoice_eng.CPRC_abuf_srate_set
    __swig_getmethods__["srate"] = _cerevoice_eng.CPRC_abuf_srate_get
    if _newclass:
        srate = _swig_property(_cerevoice_eng.CPRC_abuf_srate_get, _cerevoice_eng.CPRC_abuf_srate_set)
    __swig_setmethods__["wav"] = _cerevoice_eng.CPRC_abuf_wav_set
    __swig_getmethods__["wav"] = _cerevoice_eng.CPRC_abuf_wav_get
    if _newclass:
        wav = _swig_property(_cerevoice_eng.CPRC_abuf_wav_get, _cerevoice_eng.CPRC_abuf_wav_set)
    __swig_setmethods__["trans_char_sz"] = _cerevoice_eng.CPRC_abuf_trans_char_sz_set
    __swig_getmethods__["trans_char_sz"] = _cerevoice_eng.CPRC_abuf_trans_char_sz_get
    if _newclass:
        trans_char_sz = _swig_property(_cerevoice_eng.CPRC_abuf_trans_char_sz_get, _cerevoice_eng.CPRC_abuf_trans_char_sz_set)
    __swig_setmethods__["trans_char_mem"] = _cerevoice_eng.CPRC_abuf_trans_char_mem_set
    __swig_getmethods__["trans_char_mem"] = _cerevoice_eng.CPRC_abuf_trans_char_mem_get
    if _newclass:
        trans_char_mem = _swig_property(_cerevoice_eng.CPRC_abuf_trans_char_mem_get, _cerevoice_eng.CPRC_abuf_trans_char_mem_set)
    __swig_setmethods__["trans_char"] = _cerevoice_eng.CPRC_abuf_trans_char_set
    __swig_getmethods__["trans_char"] = _cerevoice_eng.CPRC_abuf_trans_char_get
    if _newclass:
        trans_char = _swig_property(_cerevoice_eng.CPRC_abuf_trans_char_get, _cerevoice_eng.CPRC_abuf_trans_char_set)
    __swig_setmethods__["trans_sz"] = _cerevoice_eng.CPRC_abuf_trans_sz_set
    __swig_getmethods__["trans_sz"] = _cerevoice_eng.CPRC_abuf_trans_sz_get
    if _newclass:
        trans_sz = _swig_property(_cerevoice_eng.CPRC_abuf_trans_sz_get, _cerevoice_eng.CPRC_abuf_trans_sz_set)
    __swig_setmethods__["trans_sz_type"] = _cerevoice_eng.CPRC_abuf_trans_sz_type_set
    __swig_getmethods__["trans_sz_type"] = _cerevoice_eng.CPRC_abuf_trans_sz_type_get
    if _newclass:
        trans_sz_type = _swig_property(_cerevoice_eng.CPRC_abuf_trans_sz_type_get, _cerevoice_eng.CPRC_abuf_trans_sz_type_set)
    __swig_setmethods__["trans_mem"] = _cerevoice_eng.CPRC_abuf_trans_mem_set
    __swig_getmethods__["trans_mem"] = _cerevoice_eng.CPRC_abuf_trans_mem_get
    if _newclass:
        trans_mem = _swig_property(_cerevoice_eng.CPRC_abuf_trans_mem_get, _cerevoice_eng.CPRC_abuf_trans_mem_set)
    __swig_setmethods__["trans"] = _cerevoice_eng.CPRC_abuf_trans_set
    __swig_getmethods__["trans"] = _cerevoice_eng.CPRC_abuf_trans_get
    if _newclass:
        trans = _swig_property(_cerevoice_eng.CPRC_abuf_trans_get, _cerevoice_eng.CPRC_abuf_trans_set)
    __swig_setmethods__["dsps_mem"] = _cerevoice_eng.CPRC_abuf_dsps_mem_set
    __swig_getmethods__["dsps_mem"] = _cerevoice_eng.CPRC_abuf_dsps_mem_get
    if _newclass:
        dsps_mem = _swig_property(_cerevoice_eng.CPRC_abuf_dsps_mem_get, _cerevoice_eng.CPRC_abuf_dsps_mem_set)
    __swig_setmethods__["dsps"] = _cerevoice_eng.CPRC_abuf_dsps_set
    __swig_getmethods__["dsps"] = _cerevoice_eng.CPRC_abuf_dsps_get
    if _newclass:
        dsps = _swig_property(_cerevoice_eng.CPRC_abuf_dsps_get, _cerevoice_eng.CPRC_abuf_dsps_set)

    def __init__(self):
        """__init__(CPRC_abuf self) -> CPRC_abuf"""
        this = _cerevoice_eng.new_CPRC_abuf()
        try:
            self.this.append(this)
        except Exception:
            self.this = this
    __swig_destroy__ = _cerevoice_eng.delete_CPRC_abuf
    __del__ = lambda self: None
CPRC_abuf_swigregister = _cerevoice_eng.CPRC_abuf_swigregister
CPRC_abuf_swigregister(CPRC_abuf)


def CPRCEN_engine_open_channel_with_type(eng, type, iso_language_code, iso_region_code, voice_code, srate):
    """CPRCEN_engine_open_channel_with_type(CPRCEN_engine * eng, enum CPRCEN_CHANNEL_TYPE type, char const * iso_language_code, char const * iso_region_code, char const * voice_code, char const * srate) -> CPRCEN_channel_handle"""
    return _cerevoice_eng.CPRCEN_engine_open_channel_with_type(eng, type, iso_language_code, iso_region_code, voice_code, srate)

def CPRCEN_channel_get_cerevoice(eng, hc):
    """CPRCEN_channel_get_cerevoice(CPRCEN_engine * eng, CPRCEN_channel_handle hc) -> CPRC_voice *"""
    return _cerevoice_eng.CPRCEN_channel_get_cerevoice(eng, hc)

def CPRCEN_engine_chan_text_process(eng, chan, text, textlen, flush):
    """CPRCEN_engine_chan_text_process(CPRCEN_engine * eng, CPRCEN_channel_handle chan, char const * text, int textlen, int flush) -> char const *"""
    return _cerevoice_eng.CPRCEN_engine_chan_text_process(eng, chan, text, textlen, flush)

def CPRCEN_engine_channel_speak_spurt(eng, chan, text, textlen):
    """CPRCEN_engine_channel_speak_spurt(CPRCEN_engine * eng, CPRCEN_channel_handle chan, char const * text, int textlen) -> CPRC_abuf *"""
    return _cerevoice_eng.CPRCEN_engine_channel_speak_spurt(eng, chan, text, textlen)

def CPRCEN_engine_chan_get_last_spurt(eng, chan):
    """CPRCEN_engine_chan_get_last_spurt(CPRCEN_engine * eng, CPRCEN_channel_handle chan) -> char const *"""
    return _cerevoice_eng.CPRCEN_engine_chan_get_last_spurt(eng, chan)

def CPRCEN_engine_chan_get_last_tpspurt(eng, chan):
    """CPRCEN_engine_chan_get_last_tpspurt(CPRCEN_engine * eng, CPRCEN_channel_handle chan) -> char const *"""
    return _cerevoice_eng.CPRCEN_engine_chan_get_last_tpspurt(eng, chan)

def CPRCEN_engine_chan_get_last_spurt_struct(eng, chan):
    """CPRCEN_engine_chan_get_last_spurt_struct(CPRCEN_engine * eng, CPRCEN_channel_handle chan) -> CPRC_spurt *"""
    return _cerevoice_eng.CPRCEN_engine_chan_get_last_spurt_struct(eng, chan)

def CPRCEN_engine_chan_get_last_units(eng, chan):
    """CPRCEN_engine_chan_get_last_units(CPRCEN_engine * eng, CPRCEN_channel_handle chan) -> char const *"""
    return _cerevoice_eng.CPRCEN_engine_chan_get_last_units(eng, chan)

def CPRCEN_engine_set_text_callback(eng, chan, userdata, text_callback):
    """CPRCEN_engine_set_text_callback(CPRCEN_engine * eng, CPRCEN_channel_handle chan, void * userdata, cprcen_channel_text_callback text_callback) -> int"""
    return _cerevoice_eng.CPRCEN_engine_set_text_callback(eng, chan, userdata, text_callback)

def CPRCEN_engine_clear_application_callback(eng, chan):
    """CPRCEN_engine_clear_application_callback(CPRCEN_engine * eng, CPRCEN_channel_handle chan) -> int"""
    return _cerevoice_eng.CPRCEN_engine_clear_application_callback(eng, chan)

def CPRCEN_engine_set_application_callback(eng, chan, userdata, text_callback):
    """CPRCEN_engine_set_application_callback(CPRCEN_engine * eng, CPRCEN_channel_handle chan, void * userdata, cprcen_channel_text_callback text_callback) -> int"""
    return _cerevoice_eng.CPRCEN_engine_set_application_callback(eng, chan, userdata, text_callback)

def CPRCEN_engine_get_channel_application_userdata(eng, chan):
    """CPRCEN_engine_get_channel_application_userdata(CPRCEN_engine * eng, CPRCEN_channel_handle chan) -> void *"""
    return _cerevoice_eng.CPRCEN_engine_get_channel_application_userdata(eng, chan)

def CPRCEN_engine_clear_text_callback(eng, chan):
    """CPRCEN_engine_clear_text_callback(CPRCEN_engine * eng, CPRCEN_channel_handle chan) -> int"""
    return _cerevoice_eng.CPRCEN_engine_clear_text_callback(eng, chan)

def CPRCEN_channel_get(eng, chan):
    """CPRCEN_channel_get(CPRCEN_engine * eng, CPRCEN_channel_handle chan) -> CPRCEN_channel *"""
    return _cerevoice_eng.CPRCEN_channel_get(eng, chan)

def CPRCEN_channel_fx(c, spurtxml):
    """CPRCEN_channel_fx(CPRCEN_channel * c, char const * spurtxml)"""
    return _cerevoice_eng.CPRCEN_channel_fx(c, spurtxml)

def CPRCEN_channel_audio_from_file(eng, chan, filename):
    """CPRCEN_channel_audio_from_file(CPRCEN_engine * eng, CPRCEN_channel_handle chan, char const * filename) -> int"""
    return _cerevoice_eng.CPRCEN_channel_audio_from_file(eng, chan, filename)

def CPRC_override_hts_durations(htsspurt, spt):
    """CPRC_override_hts_durations(CPRC_htsspurt * htsspurt, CPRC_spurt * spt) -> int"""
    return _cerevoice_eng.CPRC_override_hts_durations(htsspurt, spt)

def CPRC_override_hts_f0(htsspurt, spt):
    """CPRC_override_hts_f0(CPRC_htsspurt * htsspurt, CPRC_spurt * spt) -> int"""
    return _cerevoice_eng.CPRC_override_hts_f0(htsspurt, spt)

def CPRCEN_channel_synth_get_type(eng, chan):
    """CPRCEN_channel_synth_get_type(CPRCEN_engine * eng, CPRCEN_channel_handle chan) -> enum CPRCEN_SYNTH_TYPE"""
    return _cerevoice_eng.CPRCEN_channel_synth_get_type(eng, chan)

def CPRCEN_voice_synthtype(v):
    """CPRCEN_voice_synthtype(CPRCEN_voice * v) -> enum CPRCEN_SYNTH_TYPE"""
    return _cerevoice_eng.CPRCEN_voice_synthtype(v)

def CPRCEN_engine_channel_logger(eng, chan):
    """CPRCEN_engine_channel_logger(CPRCEN_engine * eng, CPRCEN_channel_handle chan) -> CPTP_logger *"""
    return _cerevoice_eng.CPRCEN_engine_channel_logger(eng, chan)

def CPRCEN_engine_channel_set_normaliser_tracing(eng, chan, tracing):
    """CPRCEN_engine_channel_set_normaliser_tracing(CPRCEN_engine * eng, CPRCEN_channel_handle chan, int tracing)"""
    return _cerevoice_eng.CPRCEN_engine_channel_set_normaliser_tracing(eng, chan, tracing)
# This file is compatible with both classic and new-style classes.


