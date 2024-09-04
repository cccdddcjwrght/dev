using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

// 自定义的类对象池
namespace SGame
{
    

    // 简单的对象池, 使用int 来索引对象
    public class SObjectPool<T> : IEnumerable<T> where T : class//,new()
    {
        public delegate T    AllocDelegate();
        public delegate void DeSpawnDelegate(T obj);

        public delegate void SpawnDelegate(T obj);
        
        public delegate void DisposeDelegate(T obj);

        
        // 扩展数据
        struct ExtendData
        {
            // 当前数据的版本号
            public int  Version;

            public enum State : int
            {
                EMPTY   = 0,
                USED    = 1,
                FREE    = 2
            }

            // 状态
            public State state;
        }
        
        // 所有数据
        private List<T>           m_datas = new List<T>();

        // 描述数据
        private List<ExtendData>  m_exDatas = new List<ExtendData>();
        
        // 空数据链表
        // private Stack<int>        m_free  = new Stack<int>();

        private AllocDelegate       m_Alloc   = null;
        private SpawnDelegate       m_Spawn   = null;
        private DeSpawnDelegate     m_DeSpawn = null;
        private DisposeDelegate     m_Dispose = null;
        
        // 已经使用的数量
        private int                 m_usedCount = 0;
        
        // 在缓存中的数量
        private int                 m_freeCount = 0;

        // 使用数量
        public int usedCount        => m_usedCount;

        // 统计重量
        public int allCount => m_usedCount + m_freeCount;

        /// <summary>
        /// 剩余数量
        /// </summary>
        public int freeCount => m_freeCount;
        
        // 实现Enumerator接口
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < m_datas.Count; i++)
            {
                if (m_exDatas[i].state == ExtendData.State.USED)
                {
                    yield return m_datas[i];
                }
            }
       }
        
        private IEnumerator GetEnumerator1()
        {
            return this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator1();
        }

        private T NewObject() => m_Alloc();

        public SObjectPool(AllocDelegate alloc = null, SpawnDelegate spawn = null, DeSpawnDelegate deSpawn = null, DisposeDelegate dispose = null)
        {
            m_Alloc     = alloc;
            m_Spawn     = spawn;
            m_DeSpawn   = deSpawn;
            m_Dispose   = dispose;
            m_usedCount = 0;
            m_freeCount = 0;
        }

        // 使用数据, 并返回版本号, 将状态改为使用!
        private int UseData(int index)
        {
            ExtendData v = m_exDatas[index];
            GameDebug.Assert(v.state != ExtendData.State.USED, "state is not match!");

            if (v.state == ExtendData.State.FREE)
                m_freeCount--;
            
            v.state = ExtendData.State.USED;
            v.Version++;
            m_exDatas[index] = v;
            m_usedCount++;
            m_Spawn?.Invoke(m_datas[index]);
            return v.Version;
        }

        /// <summary>
        /// 查找空闲节点的索引
        /// </summary>
        /// <returns></returns>
        int GetFreeIndex()
        {
            if (m_freeCount == 0)
                return -1;

            return FindIndex(ExtendData.State.FREE);
        }

        int FindIndex(ExtendData.State state)
        {
            for (int i = 0; i < m_exDatas.Count; i++)
            {
                if (m_exDatas[i].state == state)
                    return i;
            }

            return -1;
        }

        public PoolID Alloc()
        {
            // 获取已有的
            int index = GetFreeIndex();
            int version = 0;
            if (index >= 0)
            {
                version = UseData(index);
                return new PoolID() { Index = index, Version = version };
            }
            
            
            // 新的
            index = FindIndex(ExtendData.State.EMPTY);
            if (index >= 0)
            {
                // 内部有空位
                m_datas[index] = NewObject();
                version = UseData(index);
            }
            else
            {
                // 添加新的
                m_datas.Add(NewObject());
                m_exDatas.Add(new ExtendData() { Version = 0, state = ExtendData.State.EMPTY });
                index = m_datas.Count - 1;
                version = UseData(index);
            }
            return new PoolID {Index = index, Version = version};
        }

        // 获取该值, 推荐使用!
        public bool TryGet(PoolID id, out T value)
        {
            if (isExits(id) == false)
            {
                value = null;
                return false;
            }

            value = m_datas[id.Index];
            return true;
        }

        // 该对象是否有效
        public bool isExits(PoolID id)
        {
            if (id.Index < 0 || id.Index >= m_datas.Count)
            {
                return false;
            }

            var exData = m_exDatas[id.Index];
            if (exData.Version != id.Version || exData.state != ExtendData.State.USED)
            {
                return false;
            }
            
            return true;
        }

        // 通过对象找ID, 不建议使用， 性能差
        PoolID GetID(T value)
        {
            for (int i = 0; i < m_datas.Count; i++)
            {
                if (m_datas[i] == value)
                {
                    // 该对象已经被释放了, 不能返回对象ID
                    if (m_exDatas[i].state != ExtendData.State.USED)
                        return PoolID.NULL;

                    return new PoolID() {Index = i, Version = m_exDatas[i].Version};
                }
            }

            return PoolID.NULL;
        }

        // 直接释放对象, 不建议使用, 性能差
        bool Free(T value)
        {
            var id = GetID(value);
            if (id == PoolID.NULL)
                return false;

            return Free(id);
        }

        // 回收对象
        public bool Free(PoolID id)
        {
            if (isExits(id) == false)
            {
                return false;
            }

            // 原有ID失效, 并加入
            var exData = m_exDatas[id.Index];
            exData.state = ExtendData.State.FREE;
            m_exDatas[id.Index] = exData;
            m_usedCount--;
            m_freeCount++;
            
            m_DeSpawn?.Invoke(m_datas[id.Index]);
            return true;
        }

        /// <summary>
        /// 关闭对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Dispose(PoolID id)
        {
            // 原有ID失效, 并加入
            var exData = m_exDatas[id.Index];
            if (exData.state == ExtendData.State.EMPTY)
                return false;

            switch (exData.state)
            {
                case ExtendData.State.FREE:
                    m_freeCount--;
                    break;
                
                case ExtendData.State.USED:
                    m_usedCount--;
                    break;
            }
            
            exData.state = ExtendData.State.EMPTY;
            m_exDatas[id.Index] = exData;
            m_Dispose?.Invoke(m_datas[id.Index]);
            return true;
        }

        /// <summary>
        /// 清空所有
        /// </summary>
        public void DisposeAll()
        {
            foreach (var item in m_datas)
                m_Dispose(item);
            
            m_datas.Clear();
            m_exDatas.Clear();
        }
        
        /// <summary>
        /// 获得所有可用的对象
        /// </summary>
        /// <returns></returns>
        public List<PoolID> GetFreeList()
        {
            List<PoolID> ret = new List<PoolID>();
            for (int i = 0; i < m_exDatas.Count; i++)
            {
                var item = m_exDatas[i];
                if (item.state == ExtendData.State.FREE)
                {
                    ret.Add(new PoolID()
                    {
                        Version = item.Version, 
                        Index = i
                    });
                }
            }

            if (m_freeCount != ret.Count)
            {
                Debug.LogError("not match free count");
            }

            return ret;
        }

    }
}