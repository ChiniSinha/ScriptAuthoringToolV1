using UnityEngine;

namespace RAG.Loading
{
    internal class ResourceLoaderModule : LoadableModule
    {
        protected override void DoLoad()
        {
            Globals.EventBus.Register<ResourceLoaderReadyEvent>(OnResourceReady);

            GameObject adapterObject = Globals.SystemObject;
            ResourceLoader resourceLoader;
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                resourceLoader = adapterObject.AddComponent<WebResourceLoader>();
            }
            else
            {
                resourceLoader = adapterObject.AddComponent<LocalResourceLoader>();
            }

            Globals.Register(resourceLoader);
            resourceLoader.Initialize(Globals.Config.Network.NetworkAddress);
        }

        private void OnResourceReady(ResourceLoaderReadyEvent e)
        {
            Globals.EventBus.Unregister<ResourceLoaderReadyEvent>(OnResourceReady);
            Status = LoadingStatus.LOADED;
        }
    }
}