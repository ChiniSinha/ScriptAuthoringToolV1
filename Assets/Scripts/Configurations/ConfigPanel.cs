using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConfigPanel : MonoBehaviour
{

    public Dropdown agentDropdown;
    public Dropdown ttsDropdown;
    
    void Start()
    {
        Config config = Config.Load();
        if (config.Agent.Character == "TanyaFixed")
        {
            agentDropdown.value = 1;
        }
        if (config.Tts.Url != null)
        {
            ttsDropdown.value = 2;
        } else
        {
            ttsDropdown.value = 1;
        }

    }


}
