using log4net;
using FairyGUI;
using System.Collections.Generic;
using libx;
using Unity.Entities;
using UnityEngine;
using System;

namespace SGame.UI
{
    // 资源包加载
    public class UIPackageRequest : GAssetRequest
    { 
        static ILog log = LogManager.GetLogger("UISystem");

        public UIPackage            uiPackage;
        libx.BundleRequest          descBundle;
        libx.BundleRequest          resBundle;
        
        // 加载函数
        public delegate UIPackageRequest FUNC_LOAD(string name);
        
        
        // 依赖的uipackage
        private List<GAssetRequest> m_dependencies = new List<GAssetRequest>();
        private FUNC_LOAD           m_load;

        public UIPackageRequest(FUNC_LOAD load, bool callCompleted = true) : base(callCompleted)
        {
            m_load = load;
        }

        protected override void Complete()
        {
            if (loadState == LoadState.Loaded)
            {
                uiPackage.LoadAllAssets();
            }
            base.Complete();
        }
        
        // 加载进度
        public override float progress
        {
            get
            {
                if (isDone) return 1.0f;

                float count_value = 0.0f;
                foreach (var req in m_dependencies)
                {
                    count_value += req.progress;
                }

                float single_value = 0.0f;
                if (descBundle != null)
                    single_value += descBundle.progress;

                if (resBundle != null)
                {
                    single_value += resBundle.progress;
                    single_value /= 2.0f;
                }

                float ret = (count_value + single_value) / (m_dependencies.Count + 1);
                return ret;
            }
        }

        // 更新状态
        public override bool Update()
        {
            if (!base.Update()) return false;
                
            switch (loadState)
            {
                case LoadState.LoadAssetBundle:
                        if (descBundle.isDone && resBundle.isDone)
                        {
                            //loadState = LoadState.Loaded;
                            
                            if (string.IsNullOrEmpty(descBundle.error) == false)
                            {
                                log.Error("desc bundle load fail=" + descBundle.error);
                                error = descBundle.error;
                                loadState = LoadState.Loaded;
                                return false;
                            }

                            if (string.IsNullOrEmpty(resBundle.error) == false)
                            {
                                log.Error("res bundle load fail=" + resBundle.error);
                                error = resBundle.error;
                                loadState = LoadState.Loaded;
                                return false;
                            }

                            uiPackage = UIPackage.AddPackage(descBundle.assetBundle, resBundle.assetBundle);
                                
                            var dependencies = uiPackage.dependencies;
                            if (dependencies.Length == 0)
                            {
                                loadState = LoadState.Loaded;
                                return false;
                            }

                            foreach (var depend in dependencies)
                            {
                                var pkgName = depend["name"];
                                m_dependencies.Add(m_load(pkgName));
                            }
#if UI_DEBUG
                            GameDebug.Log("UI Request Finish=" + name);
#endif
                            loadState = LoadState.LoadAssetBundleDepend;
                            return true;
                        }
                    break;
                
                case LoadState.LoadAssetBundleDepend:
                    // 加载依赖
                    foreach (var pkg in m_dependencies)
                    {
                        if (pkg.isDone == false)
                            return true;
                    }

                    loadState = LoadState.Loaded;
                    break;
                
                default:
                    loadState = LoadState.Loaded;
                    error = "Value Is Not Right"; // 值不对
                     break;
            }

            return true;
        }

        internal override void Load()
        {
            string packageName = name;
#if UNITY_EDITOR
            // 本地模式, 无需打ab包
            if (libx.Assets.runtimeMode == false)
            {
                string pkgPath = UISystem.GetPackageDescPath(packageName);
                this.uiPackage = UIPackage.AddPackage(pkgPath);
                
                // 加载依赖
                var depenencies = uiPackage.dependencies;
                foreach (var depend in depenencies)
                {
                    var pkgName = depend["name"];
                    m_dependencies.Add(m_load(pkgName));
                }
                loadState = LoadState.Loaded;
                return;
            }
#endif
            // bundle 模式, 异步加载
            string descBundlePath = null;
            string resBundlePath = null;
            UISystem.GetPackageDescAndResName(packageName, out descBundlePath, out resBundlePath);
            this.descBundle = libx.Assets.LoadBundleAsync(descBundlePath);
            this.resBundle = libx.Assets.LoadBundleAsync(resBundlePath);
            loadState = LoadState.LoadAssetBundle;
        }

        internal override void Unload()
        {
            UIPackage.RemovePackage(name);
            if (descBundle != null)
            {
                descBundle.Release();
                descBundle = null;
            }

            if (resBundle != null)
            {
                resBundle.Release();
                resBundle = null;
            }

            uiPackage = null;
        }
    }
    
}