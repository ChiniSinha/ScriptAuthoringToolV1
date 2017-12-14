using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyGlobals
{ 

    public static MyConfig Config { get; private set; }
    public static ProjectConfig ProjectConfig { get; private set; }

    public static void SetConfig(MyConfig config)
    {
        Config = config;
    }
  
    public static void SetProjectConfig(ProjectConfig projectConfig)
    {
         ProjectConfig = projectConfig;
    }
 
    public static string PROJECTNAME;
    public static string PROJECTPATH;

    public static string CURRENTSCRIPTNAME;
    public static string CURRENTSCRIPTPATH;

    public static bool isDisplay = false;

    public static void Reset(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
}