#region

using System;
using System.Collections.Generic;

#endregion

namespace RAG.Loading
{
    [Serializable]
    internal abstract class LoadableModule
    {
        public enum LoadingStatus
        {
            NOT_STARTED,
            LOADING,
            LOADED
        }

        protected List<Type> _unsatisfiedDependencies;

        public LoadableModule(params Type[] dependencies)
        {
            _unsatisfiedDependencies = new List<Type>(dependencies);
        }

        public LoadingStatus Status { get; protected set; }

        public bool CanStartLoading
        {
            get { return _unsatisfiedDependencies.Count == 0; }
        }

        public void StartLoading()
        {
            Status = LoadingStatus.LOADING;
            DoLoad();
        }

        protected abstract void DoLoad();

        public void ReportModuleLoaded(LoadableModule module)
        {
            Type t = module.GetType();
            _unsatisfiedDependencies.Remove(t);
        }
    }
}