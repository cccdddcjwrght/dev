using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using libx;
using log4net;
using UnityEngine;
using System;

namespace SGame
{
    /// <summary>
    /// 预加载资源
    /// </summary>
    public class GScenePreload : GAssetRequest
    {
        private static ILog log = LogManager.GetLogger("scene");

        private List<AssetRequest> m_assetGroups;
        private float              m_progress;

        static Type GetType(string path)
        {
            if (path.EndsWith(".prefab"))
                return typeof(GameObject);

            if (path.EndsWith(".png"))
                return typeof(Texture);

            if (path.EndsWith(".bytes"))
                return typeof(TextAsset);
            
            return typeof(GameObject);
        }
        
        // 初始化
        protected void Init()
        {
            if (!ConfigSystem.Instance.TryGet(name, out SceneRowData config))
            {
                log.Error("scene config not found=" + name);
                return;
            }

            if (config.PreloadLength <= 0)
            {
                loadState = LoadState.Loaded;
                return;
            }

            m_assetGroups = new List<AssetRequest>();
            for (int i = 0; i < config.PreloadLength; i++)
            {
                var assetPath = config.Preload(i);
                var asset = Assets.LoadAsset(assetPath, GetType(assetPath));
                m_assetGroups.Add(asset);
            }
            loadState = LoadState.LoadAsset;
        }

        public override float progress
        {
            get
            {
                return m_progress;
            }
        }

        void UpdateProgress()
        {
            // 更新进度
            string err = null;
            float p = 0;
            int finishCount = 0;
            foreach (var asset in m_assetGroups)
            {
                if (asset.isDone)
                    finishCount += 1; 
                
                p += asset.progress;
                if (!string.IsNullOrEmpty(asset.error))
                {
                    error = asset.error;
                    loadState = LoadState.Loaded;
                    return;
                }
            }

            m_progress = p / m_assetGroups.Count;
            if (finishCount == m_assetGroups.Count)
            {
                m_progress = 1.0f;
                loadState = LoadState.Loaded;
            }
        }

        void UpdateState()
        {
            switch (loadState)
            {
                case LoadState.Init:
                    Init();
                    break;
                case LoadState.LoadAsset:
                    UpdateProgress();
                    break;
            }
        }
        
        public override bool Update()
        {
            UpdateState();
            return base.Update();
        }

        public override void Unload()
        {
            if (m_assetGroups != null)
            {
                foreach (var asset in m_assetGroups)
                    asset.Release();
            }
            m_assetGroups = null;
            base.Unload();
        }
    }
}