/// <summary>
/// Expected callbacks for the Cereproc plugin.
/// 
/// To fully implement the interface for the Cereproc plugin, be sure to include
/// external calls to the following methods:
/// 
/// void LoadTTS(string resourcePath, string cachePath, string voice);
/// void SpeakSSMLBlock(string ssmlText);
/// void CleanupTTS();
/// 
/// </summary>
public interface ICereprocTTS
{
    /// <summary>
    /// Receive event data from the Cereproc plugin. Occurs while generating audio.
    /// </summary>
    /// <param name="dataString">A pipe-delineated array of event data.
    /// <para>First entry is the type of event (options: 'viseme', 'marker', 'word')</para>
    /// <para>Second entry is userdata (for viseme, the viseme id; for marker, the marker name; for word, the word about to be spoken)</para>
    /// <para>Third entry is the start time of this event during playback</para>
    /// <para>Fourth entry is the end time fo this event during playback</para>
    /// </param>
    void OnTtsEvent(string dataString);

    /// <summary>
    /// Receive error data from the Cereproc plugin. Typically occurs during
    /// initialization, but occasionally during audio generation
    /// </summary>
    /// <param name="error">A description of the error that occurred</param>
    void OnTtsError(string error);

    /// <summary>
    /// Callback from the plugin notifying that audio generation is complete
    /// and that the audio file can be safely played.
    /// </summary>
    /// <param name="filename">The absolute path to the generated audio file</param>
    void OnAudioGenerationComplete(string filename);
}