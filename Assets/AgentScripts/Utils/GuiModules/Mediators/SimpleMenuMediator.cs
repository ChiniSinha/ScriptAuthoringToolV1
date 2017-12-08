using UnityEngine;

public class SimpleMenuMediator : IMenuMediator
{
    protected MenuPanel _panel;

    public SimpleMenuMediator(MenuPanel panel)
    {
        _panel = panel;
    }

    public void OnRegister()
    {
        Globals.EventBus.Register<ShowMenuEvent>(OnShowMenu);
        Globals.EventBus.Register<HideMenuEvent>(OnHideMenu);
        Globals.EventBus.Register<ClearUiEvent>(OnClearUi);
        Globals.EventBus.Register<InteractableChangeEvent>(OnInteractableChange);
        Globals.EventBus.Register<MenuSelectedEvent>(OnMenuSelectedEvent);
        Globals.EventBus.Register<PossibleVoiceRecognitionEvent>(OnVoiceRecognition);
        Globals.EventBus.Register<CancelledVoiceRecognitionEvent>(OnVoiceRecognitionCancelled);
        _panel.PrimaryAnimator.Hide();
    }

    public void OnRemove()
    {
        Globals.EventBus.Unregister<ShowMenuEvent>(OnShowMenu);
        Globals.EventBus.Unregister<HideMenuEvent>(OnHideMenu);
        Globals.EventBus.Unregister<ClearUiEvent>(OnClearUi);
        Globals.EventBus.Unregister<InteractableChangeEvent>(OnInteractableChange);
        Globals.EventBus.Unregister<MenuSelectedEvent>(OnMenuSelectedEvent);
        Globals.EventBus.Unregister<PossibleVoiceRecognitionEvent>(OnVoiceRecognition);
        Globals.EventBus.Unregister<CancelledVoiceRecognitionEvent>(OnVoiceRecognitionCancelled);
    }

    private void OnVoiceRecognitionCancelled(CancelledVoiceRecognitionEvent e)
    {
        _panel.EnableButtons();
    }

    private void OnVoiceRecognition(PossibleVoiceRecognitionEvent e)
    {
        _panel.DisableButtons();
        _panel.MenuButtons[e.OptionIdx].interactable = true;
    }

    private void OnMenuSelectedEvent(MenuSelectedEvent e)
    {
        _panel.DisableButtons();

        if (_panel.AutoClose)
        {
            Hide();
        }

        if (_panel.HideUnclickedButtons)
        {
            for (int i = 0; i < _panel.MenuButtons.Count; i++)
            {
                _panel.MenuButtons[i].Displaying = i == e.SelectedOptionIdx;
            }
        }
    }

    private void OnInteractableChange(InteractableChangeEvent e)
    {
        _panel.SetInteractable(e.Interactable);
    }

    private void OnClearUi(ClearUiEvent e)
    {
        Hide();
    }

    private void OnShowMenu(ShowMenuEvent e)
    {
        if (!(e.ElementName == _panel.name || (string.IsNullOrEmpty(e.ElementName) && _panel.IsDefault)))
        {
            return;
        }

        if (_panel.PrimaryAnimator.Showing)
        {
            _panel.PrimaryAnimator.Hide(delegate { DoShow(e); });
        }
        else
        {
            DoShow(e);
        }
    }

    private void DoShow(ShowMenuEvent e)
    {
        string logString = "";
        for(int i = 0; i < e.Options.Length; i++)
        {
            if (logString != "")
                logString += ",";
            logString += "[" +  e.Options[i] + "]";
        }
        Debug.Log("Displaying menu options: " + logString);
        _panel.CreateMenuButtons(e.Options);
        _panel.PrimaryAnimator.Show();
        _panel.SetInteractable(e.Interactable);
    }

    private void OnHideMenu(HideMenuEvent e)
    {
        if (e.ElementName == _panel.name || string.IsNullOrEmpty(e.ElementName))
        {
            Hide();
        }
    }

    private void Hide()
    {
        if (_panel.PrimaryAnimator.Showing)
        {
            _panel.PrimaryAnimator.Hide();
        }
    }
}