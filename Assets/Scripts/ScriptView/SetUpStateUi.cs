using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetUpStateUi : MonoBehaviour
{

    private void Update()
    {
        GetComponent<Text>().text = Globals.speechString;
    }

}
