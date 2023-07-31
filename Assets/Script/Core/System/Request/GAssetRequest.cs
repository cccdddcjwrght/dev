using libx;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SGame
{
    
    public enum LoadState
    {
        Init,
        LoadAssetBundle,
        LoadAssetBundleDepend,
        LoadAsset,
        Loaded,
        Unload
    }

    public class GAssetRequest : Reference, IEnumerator
    {
        private LoadState               _loadState = LoadState.Init;
        private List<Object>            _requires;
        public Type                     assetType;

        public Action<GAssetRequest>    completed;
        public string                   name;
        private bool                    _callCompletedEvent;

        public GAssetRequest(bool callCompleted = true)
        {
            asset       = null;
            loadState   = LoadState.Init;
            _callCompletedEvent = callCompleted;
        }

        public LoadState loadState
        {
            get { return _loadState; }
            protected set
            {
                _loadState = value;
                if (value == LoadState.Loaded)
                {
                    Complete();
                }
            }
        }

        protected virtual void Complete()
        {
            if (_callCompletedEvent)
                CallCompleted();
        }

        public virtual bool isDone
        {
            get { return loadState == LoadState.Loaded || loadState == LoadState.Unload; }
        }

        public virtual float progress
        {
            get { return 1; }
        }

        public virtual string error { get; protected set; }

        public string text { get; protected set; }

        public byte[] bytes { get; protected set; }

        public object asset { get; internal set; }

        private bool checkRequires
        {
            get { return _requires != null; }
        }

        private void UpdateRequires()
        {
            for (var i = 0; i < _requires.Count; i++)
            {
                var item = _requires[i];
                if (item != null)
                    continue;
                Release();
                _requires.RemoveAt(i);
                i--;
            }

            if (_requires.Count == 0)
                _requires = null;
        }

        internal virtual void Load() 
        {
            //if (!Assets.runtimeMode && Assets.loadDelegate != null)
            //    asset = Assets.loadDelegate(name, assetType);
            //if (asset == null) error = "error! file not exist:" + name;
            loadState = LoadState.Loaded;
        }

        internal  virtual void Unload()
        {
            //if (asset == null)
            //    return;

            //if (!Assets.runtimeMode)
             //   if (!(asset is GameObject))
             //       Resources.UnloadAsset(asset);

            asset = null;
            loadState = LoadState.Unload;
        }

        public void CallCompleted()
        {
            if (completed == null)
                return;
            try
            {
                completed.Invoke(this);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            completed = null;
        }
        

        public virtual bool Update()
        {
            if (checkRequires)
                UpdateRequires();
            if (!isDone)
                return true;

            if (_callCompletedEvent)
            {
                CallCompleted();
            }
            return false;
        }

        internal virtual void LoadImmediate()
        {
        }

        #region IEnumerator implementation

        public bool MoveNext()
        {
            return !isDone;
        }

        public void Reset()
        {
        }

        public object Current
        {
            get { return null; }
        }

        #endregion
    }
}