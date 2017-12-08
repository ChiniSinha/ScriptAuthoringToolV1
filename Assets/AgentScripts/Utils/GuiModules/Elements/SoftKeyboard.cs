#region

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class SoftKeyboard : UIElementWithMenu
{
    [SerializeField] protected Button _capsLockButton;

    protected bool _capsLocked;

    [SerializeField] protected List<KeyboardKey> _keys;

    protected IGuiElementMediator _mediator;

    [SerializeField] protected Button _shiftButton;

    protected bool _shifted;

    protected string _userInput;

    public Color CapsSelectedColor;

    public InputField InputField;
    public Text Prompt;

    public string UserInput
    {
        get { return _userInput; }
        set
        {
            _userInput = value;
            InputField.text = _userInput;
        }
    }

    public bool Caps
    {
        get { return (_capsLocked && !_shifted) || (!_capsLocked && _shifted); }
    }

    protected virtual void Awake()
    {
        _mediator = new SimpleKeyboardMediator(this);
        _mediator.OnRegister();
    }

    protected void OnDestroy()
    {
        _mediator.OnRemove();
    }

    protected override void DoButtonPressed(int pressedIndex)
    {
        DisableButtons();

        Globals.EventBus.Dispatch(new KeyboardInputEvent(UserInput, pressedIndex));
        SetInteractable(false);
    }

    public void OnKeyPressed(KeyboardKey key)
    {
        if (Caps)
        {
            UserInput += key.Character.ToUpper();
        }
        else
        {
            UserInput += key.Character.ToLower();
        }

        _shifted = false;
        UpdateCaps();
    }

    private void UpdateCaps()
    {
        if (_shifted)
        {
            _shiftButton.image.color = CapsSelectedColor;
        }
        else
        {
            _shiftButton.image.color = Color.white;
        }

        if (_capsLocked)
        {
            _capsLockButton.image.color = CapsSelectedColor;
        }
        else
        {
            _capsLockButton.image.color = Color.white;
        }

        for (int i = 0; i < _keys.Count; i++)
        {
            _keys[i].Caps = Caps;
        }
    }

    public void OnShiftPressed()
    {
        _shifted = true;
        UpdateCaps();
    }

    public void OnCapsLockPressed()
    {
        _capsLocked = !_capsLocked;
        UpdateCaps();
    }

    public void OnBackspacePressed()
    {
        UserInput = UserInput.Remove(UserInput.Length - 1);
    }

    [ContextMenu("AutoPopulate Keys")]
    protected void AutoPopulateKeys()
    {
        _keys = new List<KeyboardKey>(GetComponentsInChildren<KeyboardKey>());
    }
}