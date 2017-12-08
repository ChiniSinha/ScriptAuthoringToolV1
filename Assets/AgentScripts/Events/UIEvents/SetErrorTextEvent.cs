public class SetErrorTextEvent : Event
{
    public SetErrorTextEvent(string errorText)
    {
        ErrorText = errorText;
    }

    public string ErrorText { get; private set; }
}