#region

using UnityEngine;
using UnityEngine.UI;

#endregion

public class KeyboardKey : Button
{
    protected bool _caps;
    [SerializeField]
    [HideInInspector]
    private string _character;
    [SerializeField] protected Text _text;

    public bool Caps
    {
        get { return _caps; }
        set
        {
            _caps = value;
            UpdateCharacterInternal();
        }
    }

    public string Character
    {
        get { return _character; }
        set
        {
            _character = value;
            UpdateCharacterInternal();
        }
    }

    private void UpdateCharacterInternal()
    {
        if (!_text || string.IsNullOrEmpty(Character))
        {
            return;
        }
        if (_caps)
        {
            _text.text = Character.ToUpper();
        }
        else
        {
            _text.text = Character.ToLower();
        }
    }

    private void Awake()
    {
        if (!_text)
        {
            _text = GetComponentInChildren<Text>();
        }

        Caps = false;
    }
}