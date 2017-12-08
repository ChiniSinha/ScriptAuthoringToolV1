#region

using UnityEngine.UI;

#endregion

public class TitleDisplay : UIElement
{
    private ITitleMediator _mediator;
    public Text Title;

    protected void Awake()
    {
        _mediator = new SimpleTitleMediator(this);
        _mediator.OnRegister();
    }

    protected void OnDestroy()
    {
        _mediator.OnRemove();
    }
}