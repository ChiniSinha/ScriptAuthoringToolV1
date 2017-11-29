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
        if (!File.Exists(Application.persistentDataPath + "/defaultProjectValues.dat"))
        {
            string documentsPath = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            Debug.Log(documentsPath);

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/defaultProjectValues.dat");

            ProjectData projectData = new ProjectData();
            projectData.filePath = documentsPath;

            bf.Serialize(file, projectData);
            file.Close();
        }
    }
}

[Serializable]
class ProjectData
{
    public string filePath;
}
