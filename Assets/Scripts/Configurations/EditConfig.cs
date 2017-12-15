using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EditConfig : MonoBehaviour
{

    public Dropdown agentDropdown;
    public Dropdown ttsDropdown;

    public void HandleSave()
    {
        Config config = Config.Load();
        // Agent dropdown needs implementation - Future scope

        if(ttsDropdown.value == 1)
        {
            config.Tts = new Config.TtsSection();
            config.Tts.Mode = Config.TtsMode.LOCAL_CEREVOICE;
            Config.Save(config);
        } else if (ttsDropdown.value == 2)
        {
            config.Tts = new Config.TtsSection();
            config.Tts.Mode = Config.TtsMode.WEB_CEREVOICE;
            config.Tts.Pass = "0";
            config.Tts.Url = "https://ragstudy.ccs.neu.edu/pccGabby/";
            config.Tts.User = "0";
            Config.Save(config, true);
        }

        Globals.SetConfig(config);
    }
}
