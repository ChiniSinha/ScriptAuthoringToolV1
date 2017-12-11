namespace RAG.Loading
{
    internal class GraphicsReadyLoadEvent : LoadEvent
    {
        public GraphicsReadyLoadEvent() : base(typeof(GuiModule), typeof(EnvironmentModule), typeof(AgentModule))
        {
        }

        protected override Event Event
        {
            get { return new GraphicalAssetsLoadedEvent(); }
        }
    }
}