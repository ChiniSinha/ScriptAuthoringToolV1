using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProjectPanel : MonoBehaviour
{
 
    public GameObject openPanel;
    public GameObject mainPanel;
    public GameObject helpPanel;
    public GameObject settingsPanel;

    public InputField projectName;
    public InputField defaultPath;

    public void OpenProjectPane()
    {
        openPanel.SetActive(true);
        mainPanel.SetActive(false);
        helpPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void helpOnClick()
    {
        helpPanel.SetActive(true);
        mainPanel.SetActive(false);
        openPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void settingsOnClick()
    {
        helpPanel.SetActive(false);
        mainPanel.SetActive(false);
        openPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void LoadEditor(int index)
    {
        Debug.Log(projectName.text);
        if (projectName.text == "")
        {
            projectName.placeholder.color = Color.red;
        }
        else
        {
            //TODO: Create the files and then load the scene
            SceneManager.LoadScene(index);
        }
    }

    public void cancelAction()
    {
        openPanel.SetActive(false);
        mainPanel.SetActive(true);
        helpPanel.SetActive(false);
        settingsPanel.SetActive(false);
        projectName.placeholder.color = Color.gray;
        settingsPanel.GetComponentInChildren<Text>().text = "";
    }

    public void getDefaultPath()
    {
        if (File.Exists(Application.persistentDataPath + "/defaultProjectValues.dat"))
        {
            BinaryFormatter bf1 = new BinaryFormatter();
            FileStream file1 = File.OpenRead(Application.persistentDataPath + "/defaultProjectValues.dat");
            ProjectData pd = (ProjectData)bf1.Deserialize(file1);
            file1.Close();

            Debug.Log("saved path = " + pd.filePath);
            defaultPath.text = pd.filePath;
        }
    }

    public void updateDefaultPath()
    {
        if (File.Exists(Application.persistentDataPath + "/defaultProjectValues.dat"))
        {
            string documentsPath = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            Debug.Log(documentsPath);

            BinaryFormatter bf = new BinaryFormatter();
            ProjectData projectData = new ProjectData();
            projectData.filePath = settingsPanel.GetComponentInChildren<InputField>().text;
            Debug.Log("Updated Path: " + projectData.filePath);


            FileStream file = File.Create(Application.persistentDataPath + "/defaultProjectValues.dat");
            bf.Serialize(file, projectData);
            file.Close();

            settingsPanel.GetComponentInChildren<Text>().text = "Update Successful!";
        }

    }

    public void exit()
    {
        Application.Quit();
    }

    void CreateProjectMetaDataAndFiles()
    {

    }
}