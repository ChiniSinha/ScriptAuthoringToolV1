using System;
using System.Collections.Generic;

[Serializable]
public abstract class Action
{
}

[Serializable]
public class CameraAction : Action
{
    public string Name;
    public float Position;
    public string Easing;
    public string Target;
}



[Serializable]
public class DebugAction : Action
{
    public string DisplayText;
}


[Serializable]
public class FaceAction : Action
{
    public string Expression;
    public int Strength;
}

[Serializable]
public class GazeAction : Action
{
    public string Target;
}

[Serializable]
public class GestureAction : Action
{
    public string Hand;
    public string Cmd;
}

[Serializable]
public class HeadNodAction : Action
{
}

[Serializable]
public class InteractionAction : Action
{
    public bool Status;
}

[Serializable]
public class WhiteboardAction : Action
{
    public string Url = "";
    public bool BoldTopRow = false;
    public List<List<string>> Contents;
}

public class PageAction : Action
{
    public string Url = "";
    public string Zoom = "";
}

[Serializable]
public class PlayAudioAction : Action
{
    public string Url;
    public string Source;
}

[Serializable]
public class PointerAction : Action
{
    public string Url = "";
    public string Shape = "";
    public float X = 0;
    public float Y = 0;
}

[Serializable]
public class PostureAction : Action
{
    public string Direction;
}
    
[Serializable]
public class SpeechAction : Action
{
    public string Speech;
}

[Serializable]
public class TitleAction : Action
{
    public string DisplayText;
}

[Serializable]
public class ShowReportAction : Action
{
}