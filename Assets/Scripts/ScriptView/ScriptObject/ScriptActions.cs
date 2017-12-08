using System;
using System.Collections.Generic;

[Serializable]
public abstract class Action
{
    public abstract BaseCommand GetCommand();
}

[Serializable]
public class CameraAction : Action
{
    public string Name;
    public float Position;
    public string Easing;
    public string Target;

    public override BaseCommand GetCommand()
    {
        return new UpdateCameraCommand(Name, Position, Easing, Target);
    }
}

[Serializable]
public class ClearGUI : Action
{
    public override BaseCommand GetCommand()
    {
        return new ClearGuiCommand();
    }
}

[Serializable]
public class ClearPageAction : Action
{
    public override BaseCommand GetCommand()
    {
        return new ShowImageCommand("NONE", "");
    }
}

[Serializable]
public class ClearPointerAction : Action
{
    public override BaseCommand GetCommand()
    {
        return new ClearPointCommand();
    }
}

[Serializable]
public class DelayAction : Action
{
    public int Duration;
    public override BaseCommand GetCommand()
    {
        return new DelayCommand(Duration);
    }
}

[Serializable]
public class FaceAction : Action
{
    public string Expression;
    public int Strength;
    public override BaseCommand GetCommand()
    {
        //TODO: Find out why strength was removed?
        return new SetExpressionCommand(Expression);
    }
}

[Serializable]
public class GazeAction : Action
{
    public string Target;
    public override BaseCommand GetCommand()
    {
        return new GazeCommand(Target);
    }
}

[Serializable]
public class GestureAction : Action
{
    public string Hand;
    public string Cmd;
    public override BaseCommand GetCommand()
    {
        return new GestureCommand(Hand, Cmd);
    }
}

[Serializable]
public class HeadNodAction : Action
{
    public override BaseCommand GetCommand()
    {
        return new AgentHeadNodCommand();
    }
}

[Serializable]
public class InteractionAction : Action
{
    public bool Status;
    public override BaseCommand GetCommand()
    {
        return new SetInteractivityCommand(Status);
    }
}

[Serializable]
public class WhiteboardAction : Action
{
    public string Url = "";
    public bool BoldTopRow = false;
    public List<List<string>> Contents;
    public override BaseCommand GetCommand()
    {
        return new DisplayWhiteboardCommand(Contents, Url, BoldTopRow);
    }
}

public class PageAction : Action
{
    public string Url = "";
    public string Zoom = "";
    public override BaseCommand GetCommand()
    {
        return new ShowImageCommand(Url, Zoom);
    }
}

[Serializable]
public class PlayAudioAction : Action
{
    public string Url;
    public string Source;
    public override BaseCommand GetCommand()
    {
        return new PlayAudioCommand(Url, Source);
    }
}

[Serializable]
public class PointerAction : Action
{
    public string Url = "";
    public string Shape = "";
    public float X = 0;
    public float Y = 0;
    public override BaseCommand GetCommand()
    {
        return new PointAtDocumentCommand(X, Y, Shape, Url);
    }
}

[Serializable]
public class PostureAction : Action
{
    public string Direction;
    public override BaseCommand GetCommand()
    {
        return new SetPostureCommand(Direction);
    }
}

[Serializable]
public class SpeechAction : Action
{
    public string Speech;
    public override BaseCommand GetCommand()
    {
        return new SpeakCommand(Speech);
    }
}

[Serializable]
public class TitleAction : Action
{
    public string DisplayText;
    public override BaseCommand GetCommand()
    {
        return new UpdateTitleCommand(DisplayText);
    }
}