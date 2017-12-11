using System;
using UnityEngine;

namespace RAG.Loading
{
    internal class ScriptingModule : LoadableModule
    {
        public ScriptingModule() : base(typeof(GuiModule))
        {
        }

        protected override void DoLoad()
        {
            GameObject adapterObject = Globals.SystemObject;
            ExternalConnection externalConnection = adapterObject.AddComponent<ScriptRunnerConnection>();
                 
            

            if (externalConnection != null)
            {
                Globals.Register(externalConnection);
            }

            if (Globals.Config.System.Mode == Config.CommandMode.NONE)
            {
                Globals.Register<ICommandProtocol>(new LocalCommandProtocol());
            }

            
            Status = LoadingStatus.LOADED;
        }
    }
}