namespace RAG.Loading
{
    internal class ScriptingInterfaceReadyLoadEvent : LoadEvent
    {
        public ScriptingInterfaceReadyLoadEvent() : base(typeof(ScriptingModule))
        {
        }

        protected override Event Event
        {
            get { return new ScriptingInterfaceLoadedEvent(); }
        }
    }
}