using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RAG.Loading
{
    internal class EnvironmentModule : LoadableModule
    {
        private string _sceneName;
        private Scene _loadedScene;

        public EnvironmentModule() : base(typeof(ResourceLoaderModule))
        {
        }

        protected override void DoLoad()
		{
            _sceneName = Globals.Config.Agent.Scenery;
            Globals.Get<ResourceLoader>().LoadScene(_sceneName, true,OnEnvironmentReady);
        }

        private void OnEnvironmentReady()
        {
            _loadedScene = SceneManager.GetSceneByName(_sceneName);
            SceneManager.SetActiveScene(_loadedScene);

            Status = LoadingStatus.LOADED;

            GameObject[] roots = _loadedScene.GetRootGameObjects();

            for (int i = 0; i < roots.Length; i++)
            {
                Environment[] environments = roots[i].GetComponentsInChildren<Environment>();
                for (int j = 0; j < environments.Length; j++)
                {
                    Environment environment = environments[j];
                    if (environment.IsDefault)
                    {
                        environment.Activate();
                    }
                    else
                    {
                        environment.Deactivate();
                    }
                }
            }
        }
    }
}