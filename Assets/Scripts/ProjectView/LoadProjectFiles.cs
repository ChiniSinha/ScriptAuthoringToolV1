using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LoadProjectFiles : MonoBehaviour {

    public Transform contentPanel;
    public SimpleObjectPool textObjectPool;

    // Use this for initialization
    void Start () {
        var allfiles = Directory.GetFiles(Globals.PROJECTPATH,
            "*.*", SearchOption.AllDirectories).Where(name => !name.EndsWith(".cfg")); ;
        foreach (var file in allfiles)
        {
            FileInfo info = new FileInfo(file);
            Debug.Log("File: " + file);
            GameObject newText = textObjectPool.GetObject();
            newText.transform.SetParent(contentPanel);

            Text label = newText.GetComponent<Text>();
            label.text = file;
            //Debug.Log(info.Directory.Name);
            //Debug.Log(file.Split('\\').Length);
            // Do something with the Folder or just add them to a list via nameoflist.add();
        }
    }
	
	
}
