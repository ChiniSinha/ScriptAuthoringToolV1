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
        properties = new List<Property>();
    }

    public static void Save(Properties properties, bool prettyPrint = true)
    {
        string json = JsonUtility.ToJson(properties, prettyPrint);
        File.WriteAllText(Path.Combine(MyGlobals.PROJECTPATH, UsedValues.propertyFile), json);
    }

    public static Properties Load()
    {
        string json = File.ReadAllText(Path.Combine(MyGlobals.PROJECTPATH, UsedValues.propertyFile));
        return JsonUtility.FromJson<Properties>(json);
    }

    public static string GetProperty(string property)
    {
        Property prop = new Property();
        Properties properties = Properties.Load();
        if(properties != null)
        {
            IEnumerable<Property> query = properties.properties.Where(p => p.property == property);
            foreach (Property p in query)
            {
                prop = p;
            }
            return prop.value;
        }
        return null;
       
        
    }

    public static void DeleteProperty(Property property)
    {
        Property prop = new Property();
        Properties properties = Properties.Load();
        var itemToRemove = properties.properties.Single(r => r.property == property.property);
        properties.properties.Remove(itemToRemove);
        Save(properties);
    }

    public static void SetProperty(Property property)
    {
        Properties prop = Properties.Load();
        if (prop == null)
        {
            prop = new Properties();
        }
        var itemToRemove = prop.properties.Single(r => r.property == property.property);
        if(itemToRemove != null)
        {
            prop.properties.Remove(itemToRemove);
        }
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