#region

using System;
using System.Collections.Generic;
using System.Xml;

#endregion

public class DefaultRAG2Deserializer : IMessageDeserializer
{
    public BaseCommand DeserializeActionXml(XmlNode node)
    {
        switch (node.Name.ToLower())
        {

            case "opentab":
                return DeserializeOpenTabBlock(node);

            case "sync":
                return DeserializeSyncBlock(node);

            case "textdisplay":
                return DeserializeInputBlock(node);

            case "menu":
                return DeserializeMenuBlock(node);

            case "orientation":
                return DeserializeOrientationBlock(node);

            case "keyboard":
                return DeserializeKeyboardBlock(node);


            case "numberinput":
                return DeserializeNumberInputBlock(node);

            case "title":
                return DeserializeTitleBlock(node);

            case "slider":
                return DeserializeSliderBlock(node);

            case "interactable":
                return DeserializeInteractableBlock(node);

            case "input":
            case "textinput":
                return DeserializeTextInputBlock(node);

            case "checkbox":
                return DeserializeCheckboxBlock(node);

            case "speech":
                return DeserializeSpeechBlock(node);

            case "gesture":
                return DeserializeGestureBlock(node);

            case "delay":
                return DeserializeDelayBlock(node);

            case "animationdelay":
                return DeserializeAnimationDelayBlock(node);

            case "gaze":
                return DeserializeGazeBlock(node);

            case "posture":
                return DeserializePostureBlock(node);

            case "session":
                return DeserializeSessionNode(node);

            case "exit":
                return DeserializeExitNode(node);

            case "flush":
                return DeserializeFlushBlock(node);

            case "background":
            case "clearbackground":
                return DeserializeBackgroundBlock(node);

            case "expression":
                return DeserializeExpressionBlock(node);

            case "face":
                return DeserializeFaceBlock(node);

            case "page":
            case "clearpage":
                return DeserializePageBlock(node);

            case "docpoint":
                return DeserializePointerBlock(node);

            case "clearpointer":
                return DeserializeClearPointerBlock(node);

            case "headnod":
                return DeserializeHeadNodBlock(node);

            case "camera":
                return DeserializeCameraBlock(node);

            case "idle":
                return DeserializeIdleBlock(node);

            case "playaudio":
                return DeserializeAudioBlock(node);

            case "tabledisplay":
                return DeserializeTableDisplayBlock(node);

            case "eyebrows":
                return DeserializeEyebrowsBlock(node);

            case "pause":
                return DeserializePauseBlock(node);
        }

        return null;
    }


    private BaseCommand DeserializePauseBlock(XmlNode node)
    {
        string option = node.AttributeCaseInsensitive("status");

        return new PauseButtonCommand(option.Equals("visible", StringComparison.CurrentCultureIgnoreCase));
    }

    private BaseCommand DeserializeHeadNodBlock(XmlNode node)
    {
        return new AgentHeadNodCommand();
    }

    private BaseCommand DeserializeOpenTabBlock(XmlNode node)
    {
        string url = node.AttributeCaseInsensitive("url");
        return new OpenPageCommand(url);
    }

    private BaseCommand DeserializeSliderBlock(XmlNode node)
    {
        string title = node.AttributeCaseInsensitive("title");
        string minLabel = node.AttributeCaseInsensitive("minText");
        string maxLabel = node.AttributeCaseInsensitive("maxText");
        float maxVal = node.AttributeCaseInsensitive("maxValue").ParseFloat();
        float minVal = node.AttributeCaseInsensitive("minValue").ParseFloat();
        float resolution = node.AttributeCaseInsensitive("resolution").ParseFloat();

        return new ShowSliderCommand(title, new[] {"Submit"}, minLabel, maxLabel, minVal, maxVal, resolution);
    }

    private BaseCommand DeserializeTableDisplayBlock(XmlNode node)
    {
        string buttonList = node.AttributeCaseInsensitive("buttons");
        List<List<string>> tableContents = new List<List<string>>();
        XmlNodeList rows = node.SelectNodes("row");
        for (int i = 0; i < rows.Count; i++)
        {
            List<string> rowContent = new List<string>();
            XmlNodeList columns = rows[i].SelectNodes("cell");
            for (int j = 0; j < columns.Count; j++)
            {
                rowContent.Add(columns[j].InnerText);
            }
            tableContents.Add(rowContent);
        }

        return new DisplayTableWithMenuCommand(tableContents, new List<string>(buttonList.Split('|')));
    }

    private BaseCommand DeserializeCameraBlock(XmlNode node)
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
        float position;
        if (!float.TryParse(positionString, out position))
        {
            position = -1f;
        }

        string easing = node.AttributeCaseInsensitive("easing");

        string targetName = node.AttributeCaseInsensitive("target");

        return new UpdateCameraCommand(cameraName, position, easing, targetName);
    }

    private BaseCommand DeserializeSpeechBlock(XmlNode node)
    {
        return new SpeakCommand(node);
    }

    private BaseCommand DeserializeSyncBlock(XmlNode node)
    {
        return new SpeakCommand(node);
    }

    private BaseCommand DeserializeCheckboxBlock(XmlNode node)
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

    private BaseCommand DeserializeTextInputBlock(XmlNode node)
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

    private BaseCommand DeserializeInteractableBlock(XmlNode node)
    {
        return
            new SetInteractivityCommand(node.AttributeCaseInsensitive("status")
                .Equals("true", StringComparison.CurrentCultureIgnoreCase));
    }

    private BaseCommand DeserializeTitleBlock(XmlNode node)
    {
        return new UpdateTitleCommand(node.AttributeCaseInsensitive("value"));
    }

    private BaseCommand DeserializeNumberInputBlock(XmlNode node)
    {
        string[] menuButtons = node.AttributeCaseInsensitive("buttons")
            .Split(new[] {"|"}, StringSplitOptions.RemoveEmptyEntries);
        if (menuButtons.Length == 0)
        {
            menuButtons = new[] {"I'm Done"};
        }

        string prompt = node.AttributeCaseInsensitive("prompt");

        return new ShowKeypadCommand(prompt, menuButtons);
    }

    private BaseCommand DeserializeKeyboardBlock(XmlNode node)
    {
        string[] menuButtons = node.AttributeCaseInsensitive("buttons")
            .Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        if (menuButtons.Length == 0)
        {
            menuButtons = new[] { "I'm Done" };
        }

        string prompt = node.AttributeCaseInsensitive("prompt");

        return new ShowKeyboardCommand(prompt, menuButtons);
    }

    private BaseCommand DeserializeOrientationBlock(XmlNode node)
    {
        if (node.AttributeCaseInsensitive("dir").Equals("horizontal", StringComparison.CurrentCultureIgnoreCase))
        {
            return new LoadGuiCommand(Globals.Config.Gui.HorizontalLayout);
        }
        return new LoadGuiCommand(Globals.Config.Gui.VerticalLayout);
    }

    protected virtual BaseCommand DeserializeMenuBlock(XmlNode node)
    {
        XmlNodeList children = node.SelectNodes("item");
        string[] items = new string[children.Count];
        for (int i = 0; i < children.Count; i++)
        {
            items[i] = children[i].InnerText;
        }
        return new ShowMenuCommand(items);
    }

    private BaseCommand DeserializeClearPointerBlock(XmlNode clearPointer)
    {
        return new ClearPointCommand();
    }

    private BaseCommand DeserializePointerBlock(XmlNode pointerBlock)
    {
        float x = pointerBlock.AttributeCaseInsensitive("x").ParseFloat();
        float y = pointerBlock.AttributeCaseInsensitive("y").ParseFloat();

        return new PointAtDocumentCommand(x, y, pointerBlock.AttributeCaseInsensitive("shape"),
            pointerBlock.AttributeCaseInsensitive("url"));
    }

    private BaseCommand DeserializeIdleBlock(XmlNode idleBlock)
    {
        return new IdleCommand(true);
    }

    private BaseCommand DeserializeExpressionBlock(XmlNode expressionNode)
    {
        string expression = expressionNode.AttributeCaseInsensitive("type");
        // Convert from legacy command
        if (expression == "HAPPY")
        {
            expression = "SMILE";
        }
        return new SetExpressionCommand(expression);
    }

    private BaseCommand DeserializeFaceBlock(XmlNode faceNode)
    {
        string expression = faceNode.AttributeCaseInsensitive("expr");
        string strength = faceNode.AttributeCaseInsensitive("strength");
        int s = strength.ParseInt(100);

        return new SetExpressionCommand(expression);
    }

    private BaseCommand DeserializeEyebrowsBlock(XmlNode expressionNode)
    {
        string expression = expressionNode.AttributeCaseInsensitive("dir");
        switch (expression)
        {
            case "UP":
                return new SetExpressionCommand("EYEBROWS_UP");

            case "DOWN":
                return new SetExpressionCommand("EYEBROWS_NEUTRAL");
        }
        throw new NotImplementedException();
    }

    private BaseCommand DeserializePageBlock(XmlNode pageNode)
    {
        if (pageNode.Name.Equals("clearpage", StringComparison.CurrentCultureIgnoreCase))
        {
            return new ShowImageCommand("NONE", "");
        }

        return new ShowImageCommand(pageNode.AttributeCaseInsensitive("url"), pageNode.AttributeCaseInsensitive("zoom"));
    }

    private BaseCommand DeserializeBackgroundBlock(XmlNode imageBlock)
    {
        throw new NotImplementedException();
    }

    protected BaseCommand DeserializePostureBlock(XmlNode posture)
    {
        string direction = posture.AttributeCaseInsensitive("dir");
        return new SetPostureCommand(direction);
    }

    protected BaseCommand DeserializeGazeBlock(XmlNode gaze)
    {
        string target = gaze.AttributeCaseInsensitive("target");
        if (string.IsNullOrEmpty(target))
        {
            target = gaze.AttributeCaseInsensitive("direction");
        }
        if (string.IsNullOrEmpty(target))
        {
            target = gaze.AttributeCaseInsensitive("dir");
        }
        return new GazeCommand(target);
    }

    protected BaseCommand DeserializeInputBlock(XmlNode input)
    {
        throw new NotImplementedException();
    }

    protected BaseCommand DeserializeFlushBlock(XmlNode flush)
    {
        return new ClearGuiCommand();
    }

    protected BaseCommand DeserializeExitNode(XmlNode exit)
    {
        string redirect = exit.AttributeCaseInsensitive("url");
        if (string.IsNullOrEmpty(redirect))
        {
            redirect = "timeout.html";
        }
        return new ExitApplicationCommand(redirect);
    }

    protected BaseCommand DeserializeSessionNode(XmlNode session)
    {
        if (!string.IsNullOrEmpty(session.AttributeCaseInsensitive("exit")))
        {
            return new ExitApplicationCommand();
        }
        return null;
    }

    private BaseCommand DeserializeDelayBlock(XmlNode delay)
    {
        string ms = delay.AttributeCaseInsensitive("duration");
        if (string.IsNullOrEmpty(ms))
        {
            ms = delay.AttributeCaseInsensitive("ms");
        }
        return new DelayCommand(int.Parse(ms));
    }

    private BaseCommand DeserializeAnimationDelayBlock(XmlNode delay)
    {
        string ms = delay.AttributeCaseInsensitive("duration");
        if (string.IsNullOrEmpty(ms))
        {
            ms = delay.AttributeCaseInsensitive("ms");
        }
        return new AnimationDelayCommand(int.Parse(ms));
    }

    protected BaseCommand DeserializeGestureBlock(XmlNode gesture)
    {
        string hand = gesture.AttributeCaseInsensitive("hand");
        string cmd = gesture.AttributeCaseInsensitive("cmd");
        if (string.IsNullOrEmpty(hand))
        {
            hand = gesture.AttributeCaseInsensitive("type");
        }

        return new GestureCommand(hand, cmd);
    }

    protected BaseCommand DeserializeAudioBlock(XmlNode audio)
    {
        return new PlayAudioCommand(audio.AttributeCaseInsensitive("url"), audio.AttributeCaseInsensitive("source"));
    }
}