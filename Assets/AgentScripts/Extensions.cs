using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Extensions
{
    public static string SafeGetAttribute(this XmlNode node, string attributeName)
    {
        if (node.Attributes != null && node.Attributes[attributeName] != null)
        {
            return node.Attributes[attributeName].Value;
        }
        return "";
    }

    public static string AttributeCaseInsensitive(this XmlNode node, string attribute)
    {
        if (node.Attributes == null)
        {
            return "";
        }

        foreach (XmlAttribute att in node.Attributes)
        {
            if (string.Equals(att.Name, attribute, StringComparison.CurrentCultureIgnoreCase))
            {
                return att.Value;
            }
        }
        return "";
    }


    public static Transform FindChildRecursive(this Transform t, string name)
    {
        Transform child = t.Find(name);
        if (child)
        {
            return child;
        }

        for (int i = 0; i < t.childCount; i++)
        {
            Transform grandchild = t.GetChild(i).FindChildRecursive(name);
            if (grandchild)
            {
                return grandchild;
            }
        }

        return null;
    }

    public static int ParseInt(this string s, int @default = 0)
    {
        int val;
        if (!int.TryParse(s, out val))
        {
            val = @default;
        }
        return val;
    }

    public static float ParseFloat(this string s, float @default = 0)
    {
        float val;
        if (!float.TryParse(s, out val))
        {
            val = @default;
        }
        return val;
    }

    public static bool ParseBool(this string s, bool @default = false)
    {
        bool val;
        if (!bool.TryParse(s, out val))
        {
            val = @default;
        }
        return val;
    }
}