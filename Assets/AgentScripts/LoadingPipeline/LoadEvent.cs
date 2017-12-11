using System;

namespace RAG.Loading
{
    internal abstract class LoadEvent : LoadableModule
    {
        public LoadEvent(params Type[] dependencies) : base(dependencies)
        {
        }

        protected override void DoLoad()
        {
            Status = LoadingStatus.LOADED;
            Event e = Event;
            Globals.EventBus.Dispatch(e, e.GetType());
        }

        protected abstract Event Event { get; }
    }
}