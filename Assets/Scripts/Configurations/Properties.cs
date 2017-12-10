using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Properties
{
    HashSet<Property> properties;

    public Properties()
    {
        properties = new HashSet<Property>();
    }

    public static void Save(Properties properties, bool prettyPrint = true) 
    {
        string json = JsonUtility.ToJson(properties, prettyPrint);
        File.WriteAllText(json, Path.Combine(MyGlobals.PROJECTPATH, UsedValues.propertyFile));
    }

    public static Properties Load()
    {
        string json = File.ReadAllText(Path.Combine(MyGlobals.PROJECTPATH, UsedValues.propertyFile));
        return JsonUtility.FromJson<Properties>(json);
    } 

    public static Property GetProperty(string property)
    {
        Property prop = new Property();
        Properties properties = Properties.Load();
        IEnumerable<Property> query = properties.properties.Where(p => p.property == property);

        foreach(Property p in query)
        {
            prop = p;
        }

        return prop;
    }

    public static void SetProperty(Property property)
    {
        Properties prop = Properties.Load();
        prop.properties.Add(property);
        Properties.Save(prop);
    }
}

[Serializable]
public class Property
{
    public string property;
    public string value;
}