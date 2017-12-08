using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class InitializeProject : MonoBehaviour {

    public GameObject mainPanel;
    public GameObject newPanel;
    public GameObject helpPanel;
    public GameObject settingsPanel;
    public GameObject openProjectPanel;

	void Start () {
        mainPanel.SetActive(true);
        newPanel.SetActive(false);
        helpPanel.SetActive(false);
        settingsPanel.SetActive(false);
        openProjectPanel.SetActive(false);
        CreateAndSaveDocumentLocation();
    }

    void CreateAndSaveDocumentLocation()
    {
        try
        {
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), UsedValues.projectDir)))
            {
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), UsedValues.projectDir));
            }
            if(!File.Exists(Path.Combine(UsedValues.DefaultFilePath, UsedValues.ConfigFileName)))
            {
                
                MyConfig config = new MyConfig();
                config.projectData.projectPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), UsedValues.projectDir);
                MyConfig.Save(config);
                Debug.Log("Config value: " + config.projectData.projectPath);
                MyGlobals.SetConfig(config);
                Debug.Log("Value from file: " + JsonUtility.ToJson(MyGlobals.Config.projectData));
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Exception:" + ex.StackTrace);
        }
        
    }
}