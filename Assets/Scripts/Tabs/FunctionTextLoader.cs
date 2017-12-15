using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FunctionTextLoader : MonoBehaviour {

    public InputField textArea;
    string functFilePath;
    public SimpleObjectPool errorPool;
    public Transform contentPanel;

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
        File.WriteAllText(functFilePath, finalFile);
        ErrorViewPanel[] errorView = contentPanel.GetComponentsInChildren<ErrorViewPanel>();
        foreach (ErrorViewPanel ev in errorView)
        {
            Destroy(ev.gameObject);
        }
        
        try
        {
            if (finalFile != "")
            {
                
                string errors;
                bool hasErrors = Jint.JintEngine.HasErrors(finalFile, out errors);
                if (errors.Length > 0)
                {
                    GameObject errorElement = errorPool.GetObject();
                    errorElement.transform.SetParent(contentPanel);
                    errorElement.transform.Reset();
                    errorElement.transform.GetComponent<ErrorViewPanel>().errorText.text = "Errors in JavaScript Syntax: " + errors;
                }
            } 
            else if(finalFile == "")
            {
                GameObject errorElement = errorPool.GetObject();
                errorElement.transform.SetParent(contentPanel);
                errorElement.transform.Reset();
                errorElement.transform.GetComponent<ErrorViewPanel>().errorText.text = "Nothing added to functions file.";
            }
            
        }
        catch (Exception ex)
        {
            Debug.Log(ex.StackTrace);
        }
    }

}
