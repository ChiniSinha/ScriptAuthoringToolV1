#region

using System;
using System.Collections.Generic;
using System.Xml;

#endregion

public class ScriptLanguageDeserializer : IMessageDeserializer
{
    private readonly string _repeatButtonString = "Could you repeat that?";

    public BaseCommand DeserializeActionXml(XmlNode node)
    {
        string name = node.Name.ToLower();

        switch (name)
        {
            case "camera":
                return DeserializeCameraNode(node);

            case "page":
                return DeserializePageNode(node);

            case "speech":
                return DeserializeSpeechNode(node);

            case "sensor":
                return DeserializeSensorNode(node);

            case "buttons":
                return DeserializeButtonsNode(node);

            case "textinput":
                return DeserializeTextInputNode(node);

            case "checkbox":
            case "checkboxfull":
                return DeserializeCheckboxNode(node);
        }

        throw new NotImplementedException(node.OuterXml);
    }

    private BaseCommand DeserializeCameraNode(XmlNode node)
    {
        string cameraName = node.AttributeCaseInsensitive("name");
        if (string.IsNullOrEmpty(cameraName))
        {
            cameraName = Consts.ActiveCameraLabel;
        }

        string positionString = node.AttributeCaseInsensitive("position");
        if (string.IsNullOrEmpty(positionString))
        {
            // Legacy
            positionString = node.AttributeCaseInsensitive("zoom");
        }
        float position = positionString.ParseFloat(-1);

        string easing = node.AttributeCaseInsensitive("easing");

        string targetName = node.AttributeCaseInsensitive("target");

        return new UpdateCameraCommand(cameraName, position, easing, targetName);
    }

    private BaseCommand DeserializePageNode(XmlNode node)
    {
        if (node.Name.Equals("clearpage", StringComparison.CurrentCultureIgnoreCase))
        {
            return new ShowImageCommand("NONE", "");
        }
        string url = node.AttributeCaseInsensitive("url");
        string zoom = node.AttributeCaseInsensitive("zoom");

        return new ShowImageCommand(url, zoom);
    }

    private BaseCommand DeserializeSpeechNode(XmlNode node)
    {
        XmlDocument tempDoc = new XmlDocument();
        tempDoc.LoadXml(node.OuterXml.Replace("&lt;", "<").Replace("&gt;", ">"));
        return new SpeakCommand(tempDoc);
    }

    private BaseCommand DeserializeSensorNode(XmlNode node)
    {
        throw new NotImplementedException();
    }

    private BaseCommand DeserializeButtonsNode(XmlNode node)
    {
        XmlNodeList buttonList = node.SelectNodes("button");
        List<string> options = new List<string>();
        if (buttonList != null)
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                options.Add(buttonList[i].InnerText);
            }
        }
        string repeat = node.AttributeCaseInsensitive("hasRepeat");
        if (string.IsNullOrEmpty(repeat))
        {
            options.Add(_repeatButtonString);
        }
        return new ShowMenuCommand(options.ToArray());
    }

    private BaseCommand DeserializeTextInputNode(XmlNode node)
    {
        string[] menuButtons = node.AttributeCaseInsensitive("buttons")
            .Split(new[] {"|"}, StringSplitOptions.RemoveEmptyEntries);
        if (menuButtons.Length == 0)
        {
            menuButtons = new[] {"I'm Done"};
        }

        string prompt = node.AttributeCaseInsensitive("prompt");

        return new ShowTextInputCommand(prompt, menuButtons);
    }

    private BaseCommand DeserializeCheckboxNode(XmlNode node)
    {
        string[] menuButtons = node.AttributeCaseInsensitive("buttons")
            .Split(new[] {"|"}, StringSplitOptions.RemoveEmptyEntries);
        if (menuButtons.Length == 0)
        {
            menuButtons = new[] {"I'm Done"};
        }

        string prompt = node.AttributeCaseInsensitive("prompt");

        int limit = node.AttributeCaseInsensitive("limit").ParseInt();

        if (node.Name.Equals("checkboxfull", StringComparison.CurrentCultureIgnoreCase) ||
            node.AttributeCaseInsensitive("full").Equals("true", StringComparison.CurrentCultureIgnoreCase))
        {
            NestedString nested = NestedString.ParseChecklistString(node.AttributeCaseInsensitive("items"));
            return new ShowTieredChecklistCommand(prompt, nested, menuButtons, limit);
        }
        string[] items = node.AttributeCaseInsensitive("items")
            .Split(new[] {"|"}, StringSplitOptions.RemoveEmptyEntries);
        if (items.Length == 0)
        {
            items = new[] {"I'm Done"};
        }

        return new ShowChecklistCommand(prompt, items, menuButtons, limit);
    }
}