public class AgentDisplayDocumentEvent : Event
{
    public AgentDisplayDocumentEvent(string docUrl, string hand)
    {
        DocUrl = docUrl;
        Hand = hand;
    }

    public string DocUrl { get; private set; }
    public string Hand { get; private set; }
}