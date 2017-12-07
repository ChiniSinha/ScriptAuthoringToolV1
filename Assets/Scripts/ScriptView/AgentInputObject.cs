using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AgentInputObject : MonoBehaviour
{

    public InputField agentUtterance;

    // Use this for initialization
    void Start()
    {

    }

    public void SetUp(SpeechAction action)
    {
        agentUtterance.text = action.Speech;
    }
}
