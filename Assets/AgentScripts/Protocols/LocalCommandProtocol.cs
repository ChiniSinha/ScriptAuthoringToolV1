#region

using UnityEngine;

#endregion

public class LocalCommandProtocol : ICommandProtocol
{
    public void TryConnect()
    {
        Debug.Log("Connecting");
        Globals.EventBus.Dispatch(new NetworkStatusEvent(NetworkStatusEvent.AuthStatus.CONNECTION_ESTABLISHED));
    }

    public void Startup()
    {
        Debug.Log("Startup");
    }

    public void SendDummyInputResponse()
    {
        Debug.Log("Dummy Response");
    }

    public void Shutdown()
    {
        Application.Quit();
    }

    
    public void PreviewScript(){
        
    }
    

    public void SendSequenceComplete()
    {
        Debug.Log("Sequence Complete");
    }

    public void SendMenuSelection(int selectedOption)
    {
        Debug.Log("Menu Selection (Option " + selectedOption + ")");
    }

    public void SendTextInput(string input, int buttonPressed)
    {
        Debug.Log("Text Input (Input: " + input + " Button: " + buttonPressed + ")");
    }

    public void SendReportInput(string type, string message)
    {
        Debug.Log("Report Input (Type: " + type + "; Message: " + message + ")");
    }

    public void SendNumberInput(int number, int buttonPressed)
    {
        Debug.Log("Number Input (Input: " + number + " Button: " + buttonPressed + ")");
    }

    public void SendCheckboxInput(int[] selectedOptions, int buttonPressed)
    {
        Debug.Log("Checkbox Input (Selected: " + selectedOptions + " Button: " + buttonPressed + ")");
    }

    public void SendWidgetResponse(string message)
    {
        Debug.Log("Widget Message (Message: " + message + ")");
    }

    public void SendKeyboardInput(string userValue)
    {
        Debug.Log("Keyboard Input (Input: " + userValue + ")");
    }

    public void SendExternalProcessSuccess(bool success)
    {
        Debug.Log("Process Success (Successful: " + success + ")");
    }

    public void SendVideoPlayerInput(int buttonPressed)
    {
        Debug.Log("Video Player Input (Button: " + buttonPressed + ")");
    }

    public void SendTableDisplayInput(int buttonPressed)
    {
        Debug.Log("Table Display Input (Button: " + buttonPressed + ")");
    }

    public void SendSliderInput(float sliderValue)
    {
        Debug.Log("Slider Input (Input: " + sliderValue + ")");
    }

    public void SendBasicInputResponse(string type, string input)
    {
        Debug.Log("Basic Input (Input: " + input + " Type: " + type + ")");
    }
}