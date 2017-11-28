using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeProject : MonoBehaviour {

    public GameObject openPanel;
    public GameObject helpPanel;

	void Start () {
        openPanel.SetActive(false);
        helpPanel.SetActive(false);

        // Useful for Settings - gives the path of the Document folder.
        //string DocumentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        //Debug.Log(DocumentsPath);

    }

}
