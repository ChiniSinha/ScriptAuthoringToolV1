public class OpenHtmlRequestEvent : Event
{
    public OpenHtmlRequestEvent(string htmlContent)
    {
        HtmlContent = htmlContent;
    }

    public string HtmlContent { get; private set; }
}