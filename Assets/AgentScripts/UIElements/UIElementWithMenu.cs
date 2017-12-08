#region

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#endregion

public abstract class UIElementWithMenu : UIElement
{
    public bool AllowScaling;

    public bool AutoClose;
    public LayoutElement Contents;
    public bool HideUnclickedButtons;
    public bool IsDefault = true;

    public int MaximumVerticalHeight = 720;
    public Transform MenuArea;

    public MenuButton MenuButtonPrefab;
    public List<MenuButton> MenuButtons = new List<MenuButton>();

    public void SetupMenu(string[] menuOptions)
    {
        ClearMenu();

        for (int i = 0; i < menuOptions.Length; i++)
        {
            MenuButton newButton = Instantiate(MenuButtonPrefab);
            newButton.transform.SetParent(MenuArea);
            newButton.transform.localScale = Vector3.one;
            newButton.interactable = Interactable;
            MenuButtons.Add(newButton);
            newButton.ButtonText.text = menuOptions[i];
            newButton.onClick.AddListener(delegate { OnButtonPressed(newButton); });
        }

        if (AllowScaling && Contents != null)
        {
            Contents.preferredHeight =
                Mathf.Min(MenuButtonPrefab.GetComponent<LayoutElement>().preferredHeight*menuOptions.Length,
                    MaximumVerticalHeight);
        }
    }

    public void OnButtonPressed(MenuButton btn)
    {
        int pressedButtonIdx = MenuButtons.IndexOf(btn);
        DoButtonPressed(pressedButtonIdx);
    }

    protected abstract void DoButtonPressed(int pressedIndex);

    public override void SetInteractable(bool interactable)
    {
        Interactable = interactable;
        for (int i = 0; i < MenuButtons.Count; i++)
        {
            MenuButtons[i].interactable = Interactable;
        }
    }

    public void DisableButtons()
    {
        for (int i = 0; i < MenuButtons.Count; i++)
        {
            MenuButtons[i].interactable = false;
        }
    }

    public void EnableButtons()
    {
        for (int i = 0; i < MenuButtons.Count; i++)
        {
            MenuButtons[i].interactable = true;
        }
    }

    public void ClearMenu()
    {
        for (int i = 0; i < MenuButtons.Count; i++)
        {
            Destroy(MenuButtons[i].gameObject);
        }
        MenuButtons.Clear();
    }
}