#region

using System;

#endregion

public class AssetBundlePrefixAttribute : Attribute
{
    public AssetBundlePrefixAttribute(string prefix)
    {
        Prefix = prefix;
    }

    public string Prefix { get; set; }
}