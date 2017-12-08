using System;
using System.Collections.Generic;

public class NestedString
{
    protected const string ColumnHeaderDelimiter = "COL_HEADER ";
    protected const string SubHeaderDelimiter = "SUB_HEADER ";
    public List<NestedString> Children = new List<NestedString>();
    public bool IsLeaf;

    public string Value;

    // TA: This is to parse an XML attribute that comes back with the fullscreen checkbox command
    // The data in that string is way too structured to be put into an attribute, so we should fix
    // that and make it proper XML
    public static NestedString ParseChecklistString(string checklistString)
    {
        checklistString = checklistString.Replace("\\", " \\ ");

        string[] columns = checklistString.Split(new[] {" || "}, StringSplitOptions.RemoveEmptyEntries);
        NestedString masterObj = new NestedString();

        for (int i = 0; i < columns.Length; i++)
        {
            NestedString columnString = new NestedString();
            masterObj.Children.Add(columnString);
            List<string> entries = new List<string>(columns[i].Split('|'));
            if (entries[0].Trim().StartsWith(ColumnHeaderDelimiter))
            {
                columnString.Value = entries[0].Trim().Replace(ColumnHeaderDelimiter, "");
                entries.RemoveAt(0);
            }
            NestedString subHeader = columnString;
            for (int j = 0; j < entries.Count; j++)
            {
                if (entries[j].StartsWith(SubHeaderDelimiter))
                {
                    subHeader = new NestedString();
                    subHeader.Value = entries[j].Trim().Replace(SubHeaderDelimiter, "");
                    columnString.Children.Add(subHeader);
                }
                else
                {
                    NestedString leaf = new NestedString();
                    leaf.Value = entries[j];
                    subHeader.Children.Add(leaf);
                    leaf.IsLeaf = true;
                }
            }
        }

        return masterObj;
    }
}