using UnityEngine;

namespace RAG.Loading
{
    internal class AgentModule : LoadableModule
    {
        private const string AgentLoadTag = "agent";

        public AgentModule() : base(typeof(ResourceLoaderModule), typeof(EnvironmentModule))
        {
        }

        protected override void DoLoad()
        {
            string characterAsset = Globals.Config.Agent.Character;
            Globals.Get<ResourceLoader>()
                .LoadAsset<GameObject>(characterAsset, "agents/" + characterAsset.ToLower(), OnAgentLoaded);
        }

        private void OnAgentLoaded(GameObject loadedObject)
        {
            Object.Instantiate(loadedObject);
            Status = LoadingStatus.LOADED;
        }
    }
}