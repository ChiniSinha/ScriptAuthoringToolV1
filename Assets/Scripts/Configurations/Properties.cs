using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Properties
{
    public List<Property> properties;

    public Properties()
    {
        //        properties = new List<Property>();
        Load();
    }

    public void Save(List<Property> properties) 
    {
        Debug.Log("Property Path: " + Path.Combine(MyGlobals.PROJECTPATH, UsedValues.propertyFile));
        string json = JsonUtility.ToJson(properties);
        File.WriteAllText(json, Path.Combine(MyGlobals.PROJECTPATH, UsedValues.propertyFile));
    }

    public void Load()
    {
        string json = File.ReadAllText(Path.Combine(MyGlobals.PROJECTPATH, UsedValues.propertyFile));
        Properties loadedProps = JsonUtility.FromJson<Properties>(json);
        this.properties = loadedProps.properties;
    } 

    public string GetProperty(string property)
    {
        Property prop = new Property();
        //Properties properties = Load();
        IEnumerable<Property> query = properties.Where(p => p.property == property);

        foreach(Property p in query)
        {
            prop = p;
        }

        return prop.value;
    }

    public void DeleteProperty(Property property)
    {
        //Property prop = new Property();
        //properties = Load();
        /*if(properties == null)
        {
            properties = new Properties();
            Save(properties);
        }*/
        IEnumerable<Property> query = properties.Where(p => p == property);

        foreach(Property p in query)
        {
            property = p;
        }

        properties.Remove(property);
        Save(properties);
    }

    public void SetProperty(Property property)
    {
        Property returnProp = new Property();
        /*
        Properties prop = Load();
        if (prop == null)
        {
            prop = new Properties();
            Save(properties);
        }*/
        IEnumerable<Property> query = properties.Where(p => p == property);
        if (query != null) {
            foreach (Property p in query)
            {
                returnProp = p;
            }
            DeleteProperty(returnProp);
        }
        properties.Add(property);
        Save(properties);
    }
}

[Serializable]
public class Property
{
    public string property;
    public string value;
}