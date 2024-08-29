using UnityEngine;
using System.Collections;
using libx;
namespace SGame
{
    public class GameObjectPool : IEnumerator
    {
        private SObjectPool<GameObject> m_pool;

        private AssetRequest m_request;
        private SObjectPool<GameObject>.DeSpawnDelegate m_onDespawn;

        public GameObjectPool(AssetRequest req, 
            SObjectPool<GameObject>.SpawnDelegate spawn = null,
            SObjectPool<GameObject>.DeSpawnDelegate despawn = null)
        {
            m_pool = new SObjectPool<GameObject>(OnAlloc,
            spawn, 
            despawn, 
            OnDispose);
            m_request = req;
            m_request.Retain(); 
        }

        public bool MoveNext() => m_request.MoveNext();
        public object Current => null;
        public void Reset() { }
        public bool isDone => m_request.isDone;

        GameObject OnAlloc()
        {
            GameObject newInstance = GameObject.Instantiate(m_request.asset as GameObject);
            return newInstance;
        }

        void OnDispose(GameObject obj)
        {
            GameObject.Destroy(obj);
        }

        public PoolID Alloc() => m_pool.Alloc();

        public GameObject Get(PoolID id)
        {
            if (m_pool.TryGet(id, out GameObject obj))
                return obj;

            return null;
        }

        public bool Free(PoolID id) => m_pool.Free(id);

        public bool Dispose(PoolID id) => m_pool.Dispose(id);

        public void Dispose()
        {
            m_pool.DisposeAll();
            m_request.Release();
            m_request = null;
            m_pool = null;
        }
    }
}