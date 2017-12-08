#region

using UnityEngine;
using UnityEngine.UI;

#endregion

public class DebugText : MonoBehaviour
{
    protected IDebugTextMediator _mediator;
    public Text Text;

    private void Awake()
    {
        _mediator = new DebugTextMediator(this);
        _mediator.OnRegister();
    }

    private void OnDestroy()
    {
        _mediator.OnRemove();
    }
}