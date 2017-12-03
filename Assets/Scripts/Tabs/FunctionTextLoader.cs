using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FunctionTextLoader : MonoBehaviour {

    public InputField textArea;
    string functFilePath;

	void Start () {
        ProjectConfig config = ProjectConfig.Load();
        try
        {
            foreach (ProjectConfig.Files funcFile in config.files)
            {
                if (funcFile.fileName == UsedValues.functionFile)
                {
                    functFilePath = funcFile.filePath;
                    string functText = File.ReadAllText(functFilePath);
                    textArea.text = functText;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.StackTrace);
        }
        
	}

    public void saveFunctionsFile()
    {
        string finalFile = textArea.text;
        try
        {
            File.WriteAllText(functFilePath, finalFile);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.StackTrace);
        }
    }

}
