#region

using UnityEngine;

#endregion

public class UIElement : MonoBehaviour
{
    public UiAnimator PrimaryAnimator;

    public UIElement()
    {
        Interactable = true;
    }

    public bool Interactable { get; protected set; }


    public virtual void SetInteractable(bool interactable)
    {
    }
}