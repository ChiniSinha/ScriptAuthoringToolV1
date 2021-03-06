﻿using System;
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
            foreach (Property p in properties.properties)
            {
                if (p.property == property)
                {
                    prop = p;
                    break;
                }
            }
            return prop.value;
        }
        return null;   
    }

    public static void DeleteProperty(Property property)
    {
        
        Properties properties = Properties.Load();
        List<Property> duplicateItem = new List<Property>();
        if (properties.properties.Count > 0)
        {
            foreach (Property p in properties.properties)
            {
                if (p.property == property.property)
                {
                    duplicateItem.Add(p);
                }

            }
            foreach (Property pr in duplicateItem)
            {
                properties.properties.Remove(pr);
            }
        }
        
        Save(properties);
    }

    public static void SetProperty(Property property)
    {
        Properties prop = Properties.Load();
        List<Property> duplicateItem = new List<Property>();
        if (prop == null)
        {
            prop = new Properties();
        }
        if (prop.properties.Count > 0)
        {
            foreach(Property p in prop.properties)
            {
                if (p.property == property.property)
                {
                    duplicateItem.Add(p);
                }
                
            }
            foreach(Property pr in duplicateItem)
            {
                prop.properties.Remove(pr);
            }
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