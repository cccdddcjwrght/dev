using System.Collections.Generic;
using System.Collections;


namespace SGame
{
    // 资源加载逻辑, 抽象xasset 中资源加载流程
    public class ResourceLoader<T> : IEnumerable<T> where T : GAssetRequest
    {
        public delegate T Factory();
        
        // 所有资源
        private Dictionary<string, T> m_assets = new Dictionary<string, T>();
        
        // 加载中的资源
        private List<T> m_loading = new List<T>();
        
        // 要卸载的资源
        private List<T> m_unusedAssets = new List<T>();

        private Factory m_factory;

        public IEnumerator<T> GetEnumerator()
        {
            return m_assets.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_assets.Values.GetEnumerator();
        }
        
        public ResourceLoader(Factory factory)
        {
            m_factory = factory;
        }

        // 跟新状态
        public void Update()
        {
            // 跟新状态
            for (var i = 0; i < m_loading.Count; ++i)
            {
                var request = m_loading[i];
                if (request.Update())
                    continue;
                m_loading.RemoveAt(i);
                --i;
            }

            //  触发卸载
            foreach (var item in m_assets)
            {
                if (item.Value.isDone && item.Value.IsUnused())
                {
                    m_unusedAssets.Add(item.Value);
                }
            }

            // 销毁对象
            if (m_unusedAssets.Count > 0)
            {
                for (var i = 0; i < m_unusedAssets.Count; ++i)
                {
                    var request = m_unusedAssets[i];
                    //Log(string.Format("UnloadAsset:{0}", request.name));
                    m_assets.Remove(request.name);
                    request.Unload();
                }
                m_unusedAssets.Clear();
            }
        }

        // 加载资源
        public T Load(string name)
        {
            if (m_assets.TryGetValue(name, out T ret))
            {
                ret.Retain();
                return ret;
            }

            ret         = m_factory();
            ret.name    = name;
            m_assets.Add(name, ret);
            m_loading.Add(ret);
            
            ret.Retain();
            ret.Load();
            
            return ret;
        }

        // 尝试获取UI
        public bool TryGetValue(string name, out T req)
        {
            return m_assets.TryGetValue(name, out req);
        }

        // 添加UI
        public bool TryAdd(string name, T req)
        {
            return m_assets.TryAdd(name, req);
        }

        public void Dispose()
        {
            foreach (var v in m_assets)
            {
                v.Value.Unload();
            }
            m_assets.Clear();
            m_loading.Clear();
            m_unusedAssets.Clear();
        }
    }
}