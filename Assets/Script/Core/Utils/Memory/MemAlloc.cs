//------------------------------------------------------------------------------
//  FILE:  MemAlloc
//  DESCRIPTION:  内存池管理器, 通过内存池分配内存， 支持多线程 
//  资源使用方式:
//  COMPANY:  Longtu
//  CREATED:  17:15 2018/7/6
//-------------------------------------------------------------------------------
//using System.Threading;
using System;
namespace MemUtils
{
	public struct MemItem
	{
		public IntPtr pAddr; 	  // 内存地址	
	    public int   nCapacity;   // 内存块大小
	    public int   nSize;       // 内存标记大小
        public void Clear()
        {
            pAddr = IntPtr.Zero;
            nCapacity = 0;
            nSize = 0;
        }
	}

	// 内存分配其, 内部由各种内存池管理
	public class MemAlloc {
        public struct InitParam {
            public int count; // 内存块数量
            public int size;  // 内存块大小
        }

	    private MemPool[] m_pools;

        // 析构函数, 为了保险
        ~MemAlloc() {
            Disponse();
         }

        // 初始化内存分配对象, 实现方式使用多个block 大小不一的内存池来完成（注意, 初始化的block size 必须是从小到大排列, 不然初始化会失败!)
        // @param InitParam， 初始化参数列表
        // @return 错误代码
        public ErrCode Initialize(params InitParam[] initList) {
            // 重复初始化
            if (m_pools != null) {
                return ErrCode.TWICE_INIT;
            }

            // 检查参数数量
            if (initList == null || initList.Length <= 0) {
                return ErrCode.PARAM_FAIL;
            }

            // 检查参数, itemSize必须是从小到大排列(并且不能重复
            int itemSize = 0;
            for (int i = 0; i < initList.Length; i++) {
                if (initList[i].size <= itemSize || initList[i].count <= 0) {
                    // 传参错误！
                    return ErrCode.PARAM_FAIL;
                }
                itemSize = initList[i].size;
            }

            // 内存池初始化
            m_pools = new MemPool[initList.Length];
            if (m_pools == null) {
                return ErrCode.OUT_OF_MEMORY;
            }

            // 创建内存池
            for (int i = 0; i < initList.Length; i++) {
                var pool = new MemPool();
                if (pool == null) {
                    Disponse();
                    return ErrCode.OUT_OF_MEMORY;
                }
                var err = pool.Initialize(initList[i].count, initList[i].size);
                if (err != ErrCode.SUCCESS) {
                    Disponse();
                    return err;
                }

                m_pools[i] = pool;
            }
            //Sort();
            return ErrCode.SUCCESS;
	    }

        // 找到一个合适的内存分配器
        MemPool GetPool(int nSize)
        {
            MemPool pool = null;
            for (int i = 0; i < m_pools.Length; i++) { 
                pool = m_pools[i];

                // 由于itemSize是不会变化的, 因此不需要加锁
                if (pool != null && pool.ItemSize >= nSize) {
                    return pool;
                }
            }

            return null;
        }

        // 给内存地址找到对应的内存池
        MemPool FindPool(IntPtr addr)
        {
            if (m_pools == null)
                return null;

            MemPool pool;
            for (int i = 0; i < m_pools.Length; i++) {
                pool = m_pools[i];
                if (pool != null && pool.GetAddrIndex(addr) >= 0) {
                    return pool;
                }
            }

            return null;
        }

        // 获得itemSize
        public int GetMemLen(IntPtr addr)
        {
            var p = FindPool(addr);
            if (p == null)
                return 0;

            return p.ItemSize;
        }

        // 分配一个item
        public unsafe ErrCode Alloc(int nSize, out IntPtr pAddr) {
            pAddr = IntPtr.Zero;
            if (nSize <= 0) {
                return ErrCode.PARAM_FAIL;
            }

            // 获得使用的pool
            MemPool pool = GetPool(nSize);
            if (pool == null)
            {
                return ErrCode.OUT_OF_MEMORY;
            }

            ErrCode err = ErrCode.SUCCESS;
            lock (pool)  {
                err = pool.Alloc(out pAddr);
            }
            return err;
	    }

	    // 释放一个item
	    public unsafe ErrCode Free(IntPtr pAddr) {
            MemPool pool = FindPool(pAddr);
            if (pool == null)
                return ErrCode.FAIL_ADDRESS;

            ErrCode err = ErrCode.FAIL_ADDRESS;
            lock(pool) {
                err = pool.Free(pAddr);
            }
            return err;
	    }

        // 销毁 
        public void Disponse()
        {
            if (m_pools != null)
            {
                for (int i = 0; i < m_pools.Length; i++)
                {
                    var pool = m_pools[i];
                    if (pool != null)
                    {
                        lock (pool)
                        {
                            pool.Dispone();
                            m_pools[i] = null;
                        }
                    }
                }
                m_pools = null;
            }
        }

        // 获得内存池剩余数量(主要用于检测内存池是否有未回收的情况)
        public int GetFreeCount(int idx)
        {
            if (m_pools == null)
                return -1;

            if (idx < 0 || idx >= m_pools.Length)
                return -1;

            return m_pools[idx].GetFree();
        }

        // 通过block状态获得数量
        public int GetFreeCountFromBlock(int idx)
        {
            if (m_pools == null)
                return -1;

            if (idx < 0 || idx >= m_pools.Length)
                return -1;

            return m_pools[idx].GetFreeFromBlock();
        }
    }
}