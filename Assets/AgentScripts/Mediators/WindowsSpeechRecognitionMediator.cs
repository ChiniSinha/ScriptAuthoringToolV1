#region

#endregion

public class WindowsSpeechRecognitionMediator : IMediator
{
    private readonly WindowsSpeechRecognizer _recognizer;

    public WindowsSpeechRecognitionMediator(WindowsSpeechRecognizer recognizer)
    {
        _recognizer = recognizer;
    }

    public void Setup()
    {
        Globals.EventBus.Register<ShowMenuEvent>(OnShowMenu);
        Globals.EventBus.Register<MenuSelectedEvent>(OnMenuSelectEvent);
    }

    public void Cleanup()
    {
        Globals.EventBus.Unregister<ShowMenuEvent>(OnShowMenu);
        Globals.EventBus.Unregister<MenuSelectedEvent>(OnMenuSelectEvent);
    }

    public void OnPossibleRecognition(int option)
    {
        Globals.EventBus.Dispatch(new PossibleVoiceRecognitionEvent(option));
    }

    public void OnRecognitionCancelled()
    {
        Globals.EventBus.Dispatch(new CancelledVoiceRecognitionEvent());
        _recognizer.RestartListener();
    }

    public void OnRecognitionSuccessful(int wordIndex)
    {
        Globals.EventBus.Dispatch(new MenuSelectedEvent(wordIndex));
    }

    private void OnShowMenu(ShowMenuEvent e)
    {
        _recognizer.StartListen(e.Options);
    }

    private void OnMenuSelectEvent(MenuSelectedEvent e)
    {
        _recognizer.StopListener();
    }
}