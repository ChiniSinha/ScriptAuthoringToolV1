using UnityEngine;

namespace RAG.Loading
{
    internal class GuiModule : LoadableModule
    {
        private const string GuiLoadTag = "gui";

        public GuiModule() : base(typeof(ResourceLoaderModule))
        {
        }

        protected override void DoLoad()
		{
            string layoutName = Globals.Config.Gui.VerticalLayout;
            Globals.Get<ResourceLoader>()
                .LoadAsset<GameObject>(layoutName, "guis/" + layoutName.ToLower(), OnGuiLoaded);
        }

        private void OnGuiLoaded(GameObject loadedobject)
        {
            Object.Instantiate(loadedobject);
            Status = LoadingStatus.LOADED;
        }
    }
}