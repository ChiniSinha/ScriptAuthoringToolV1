#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace RAG.Loading
{
    public class ModuleLoader : MonoBehaviour
    {
        private bool _configReady;

        [SerializeField] private readonly List<LoadableModule> _modules = new List<LoadableModule>();

#if UNITY_EDITOR
        public string[] ModuleStatuses
        {
            get
            {
                string[] ret = new string[_modules.Count];
                for (int i = 0; i < ret.Length; i++)
                {
                    ret[i] = _modules[i].GetType() + ": " + _modules[i].Status;
                }
                return ret;
            }
        }
#endif

        private void Awake()
        {
            Globals.EventBus.Register<ConfigurationLoadedEvent>(OnConfigLoaded);
        }

        private void OnConfigLoaded(ConfigurationLoadedEvent e)
        {
            Globals.SetConfig(e.Configuration);
            _modules.Add(new AgentModule());
            _modules.Add(new EnvironmentModule());
            _modules.Add(new GuiModule());
            _modules.Add(new ScriptingModule());
            _modules.Add(new ResourceLoaderModule());

            if (Globals.Config.SpeechRecognition.EnableSpeechRecognition)
            {
                _modules.Add(new SpeechRecognitionModule());
            }

            _modules.Add(new GraphicsReadyLoadEvent());
            _modules.Add(new ScriptingInterfaceReadyLoadEvent());

            _configReady = true;
        }

        private void Update()
        {
            if (!_configReady)
            {
                return;
            }

            LoadableModule loadableModule;
            for (int i = 0; i < _modules.Count; i++)
            {
                loadableModule = _modules[i];
                if (loadableModule.Status == LoadableModule.LoadingStatus.LOADED)
                {
                    for (int j = 0; j < _modules.Count; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }
                        _modules[j].ReportModuleLoaded(loadableModule);
                    }
                    _modules.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < _modules.Count; i++)
            {
                loadableModule = _modules[i];
                if (loadableModule.Status == LoadableModule.LoadingStatus.NOT_STARTED && loadableModule.CanStartLoading)
                {
                    loadableModule.StartLoading();
                }
            }

            if (_modules.Count == 0 && !Application.isShowingSplashScreen)
            {
                Globals.EventBus.Dispatch(new LoadCompleteEvent());
                Destroy(this);
            }
        }
    }
}