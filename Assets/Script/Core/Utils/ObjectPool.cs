using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.Entities;

// 自定义的类对象池
namespace SGame
{
    
    public struct PoolID
    {
        // 数组索引
        public int Index ;      
        
        // 对象版本号, 防止已经索引的对象 数据串了
        public int Version;

        public override string ToString()
        {
            return "Index:" + Index.ToString() + " Version:" + Version.ToString();
        }

        public static PoolID NULL => new PoolID {Index = -1, Version = 0};

        // 符号判断
        public static bool operator==(PoolID lhs, PoolID rhs)
        {
            return lhs.Index == rhs.Index && lhs.Version == rhs.Version;
        }

        public static bool operator!=(PoolID lhs, PoolID rhs)
        {
            return !(lhs == rhs);
        }
        
        public bool Equals(PoolID entity)
        {
            return entity.Index == Index && entity.Version == Version;
        }
        
        public override bool Equals(object compare)
        {
            return compare is PoolID compareEntity && Equals(compareEntity);
        }

        public override int GetHashCode()
        {
            return Index;
        }
    }

    
    // 简单的对象池, 使用int 来索引对象
    public class ObjectPool<T> : IEnumerable<T> where T : class,new()
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
        
            // 当前数据是否已经在使用中
            public bool isUsed;
        }
        
        // 所有数据
        private List<T>           m_datas = new List<T>();

        // 描述数据
        private List<ExtendData>  m_exDatas = new List<ExtendData>();
        
        // 空数据链表
        private Stack<int>        m_free  = new Stack<int>();

        private AllocDelegate       m_Alloc   = null;
        private SpawnDelegate       m_Spawn   = null;
        private DeSpawnDelegate     m_DeSpawn = null;
        private DisposeDelegate     m_Dispose = null;
        
        public int usedCount => m_datas.Count - m_free.Count;
        
        // 实现Enumerator接口
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < m_datas.Count; i++)
            {
                if (m_exDatas[i].isUsed == true)
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

        private T NewObject()
        {
            T ret = null;
            if (m_Alloc == null)
                ret = new T();
            else
                ret = m_Alloc();
            return ret;
        }

        public ObjectPool(AllocDelegate alloc = null, SpawnDelegate spawn = null, DeSpawnDelegate deSpawn = null, DisposeDelegate dispose = null)
        {
            m_Alloc     = alloc;
            m_Spawn     = spawn;
            m_DeSpawn   = deSpawn;
            m_Dispose   = dispose;
        }

        // 使用数据, 并返回版本号, 将状态改为使用!
        private int UseData(int index)
        {
            ExtendData v = m_exDatas[index];
            GameDebug.Assert(v.isUsed == false, "state is not match!");
            
            m_Spawn?.Invoke(m_datas[index]);
            v.isUsed = true;
            m_exDatas[index] = v;
            return v.Version;
        }
        
        public PoolID Alloc()
        {
            int index = 0;
            PoolID id = PoolID.NULL; 
            if (m_free.TryPop(out id.Index))
            {
                id.Version = UseData(id.Index);
                return id;
            }
            
            // 新的
            m_datas.Add(NewObject());
            m_exDatas.Add( new ExtendData(){Version =  0, isUsed = false});
            index = m_datas.Count - 1;
            int version = UseData(index);
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
            if (exData.Version != id.Version || exData.isUsed == false)
            {
                return false;
            }
            
            return true;
        }

        // 通过对象找ID, 不建议使用， 新年差!
        PoolID GetID(T value)
        {
            for (int i = 0; i < m_datas.Count; i++)
            {
                if (m_datas[i] == value)
                {
                    // 该对象已经被释放了, 不能返回对象ID
                    if (m_exDatas[i].isUsed == false)
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

        // 释放对象
        public bool Free(PoolID id)
        {
            if (isExits(id) == false)
            {
                return false;
            }

            if (m_free.Contains(id.Index))
            {
                GameDebug.Assert(false, "Data state not match!");
                return false;
            }

            // 原有ID失效, 并加入
            var exData = m_exDatas[id.Index];
            exData.Version++;                   // 有效对象版本号加1
            exData.isUsed = false;
            m_exDatas[id.Index] = exData;
            
            m_DeSpawn?.Invoke(m_datas[id.Index]);
            m_free.Push(id.Index);
            return true;
        }

        public void Dispose()
        {
            foreach (var item in m_datas)
                m_Dispose(item);
            
            m_datas.Clear();
            m_exDatas.Clear();
            m_free.Clear();
        }
    }
}