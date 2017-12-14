using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChoicesInput : MonoBehaviour
{

    public InputField choiceInput;

    public void SetUp(string choice)
    {
        choiceInput.text = choice;
    }
}
