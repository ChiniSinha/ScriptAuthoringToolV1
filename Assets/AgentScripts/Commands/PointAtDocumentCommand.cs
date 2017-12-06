#region

using System;

#endregion

public class PointAtDocumentCommand : BaseCommand
{
    protected string _docUrl;
    protected string _shape;
    protected float _x;
    protected float _y;

    public PointAtDocumentCommand(float x, float y, string shape, string docUrl = "")
    {
        _x = x;
        _y = y;
        _docUrl = docUrl;
        _shape = shape;
    }

    public override void Execute()
    {
        if (string.IsNullOrEmpty(_docUrl) || _docUrl.Equals("none", StringComparison.CurrentCultureIgnoreCase))
        {
            Globals.EventBus.Dispatch(new AgentDisplayDocumentEvent("", "L"));
        }
        else
        {
            Globals.EventBus.Dispatch(new AgentDisplayDocumentEvent(_docUrl, "L"));
            Globals.EventBus.Dispatch(new AgentChangePointEvent(_x, _y, "L", _shape));
        }
    }
}