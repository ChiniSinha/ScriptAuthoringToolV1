using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class InitializeProject : MonoBehaviour {

    public GameObject openPanel;
    public GameObject helpPanel;
    public GameObject settingsPanel;

	void Start () {
        openPanel.SetActive(false);
        helpPanel.SetActive(false);
        settingsPanel.SetActive(false);
        CreateAndSaveDocumentLocation();
    }

    void CreateAndSaveDocumentLocation()
    {
        try
        {
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), UsedValues.projectDir)))
            {
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), UsedValues.projectDir));
                Config config = new Config();
                config.projectData.projectPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), UsedValues.projectDir);
                Config.Save(config);
                Debug.Log("Config value: " + config.projectData.projectPath);
                Globals.SetConfig(config);
                Debug.Log("Value from file: " + JsonUtility.ToJson(Globals.Config.projectData));
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Exception:" + ex.StackTrace);
        }
        
    }
}