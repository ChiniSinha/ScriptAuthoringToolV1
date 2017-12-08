#region

using UnityEngine;
using UnityEngine.UI;

#endregion

public class ErrorText : MonoBehaviour
{
    protected IErrorTextMediator _mediator;
    public Text Text;

    private void Awake()
    {
        _mediator = new ErrorTextMediator(this);
        _mediator.OnRegister();
    }

    private void OnDestroy()
    {
        _mediator.OnRemove();
    }
}