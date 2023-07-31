//------------------------------------------------------------------------------
//  FILE:  MemPool.lua
//  DESCRIPTION:  通用内存池
//  资源使用方式:
//	快速分配与回收内存(分配与回收效率都是O(1))
//  不支持多线程
//  内部使用内存块标记技术, 支持重复释放内存报错
//                          支持错误内存地址校验
//  COMPANY:  Longtu
//  CREATED:  17:15 2018/7/6
//-------------------------------------------------------------------------------

using System.Runtime.InteropServices;
using System;

namespace MemUtils
{


// 内存池
public unsafe class MemPool {


	// 内存块的状态
	public enum BLOCK_SATE
	{
		FREE = 0, // 空的内存
		USED = 1, // 已使用的内存
		FAIL = 2, // 无效的内存
	}

	const int MINI_ITEM = 8;	// pool 标记(用于标记该对象是否已经被分配(主要用于校验内存释放与分配), 防止重复释放某段内存
	const int BLOCK_USED = 1;	// 使用标记
	const int BLOCK_FREE = 0;   // 位使用标记
    const int MAX_MEMROY = 1024 * 1024 * 500; // 最大500M缓冲(已经很大了!)
	protected byte* m_pool;       // 分配内存的初始地址
	protected byte* m_poolState;  // 每个内存块标记， 用于标记某个内存块的状态
    //protected ulong m_first;
    protected byte* m_last;
	//protected byte* m_lastItem;
	protected int m_free;		 // 剩余地址索引(以下标1开始, 0表示无效)(直接用指针的话, 64位占8个字节呢)
    protected int m_nMaxItem;    // 最大Item数量
    protected int m_nItemSize;   // 每个ItemSize 的大小
	protected int m_nAlloc;		 // 已分配内存

	~MemPool()
	{
		Dispone(false);
	}

    public int ItemSize { get { return m_nItemSize;  } }

    // 初始化内存分配对象
    // @param nMaxItem, 最大Item
    // @param nItemSize, 每个Item的值
	public unsafe ErrCode Initialize(int nMaxItem, int nItemSize) {
		if (nMaxItem < 0 || nItemSize < 0) {
			return ErrCode.PARAM_FAIL;
		}
		if (nItemSize < MINI_ITEM) {
			return ErrCode.ITEMSIZE8;
		}

        // 防止重复初始化
        if (m_pool != null)
            return ErrCode.TWICE_INIT;

        Int64 allocMem = nMaxItem;
        allocMem *= nItemSize;
        if (allocMem > MAX_MEMROY || allocMem < 0) {
            return ErrCode.OUT_OF_MEMORY;
        }

        // 开始分配内存了!
        m_nAlloc = 0;
        m_nMaxItem = nMaxItem;
        m_nItemSize = nItemSize;
        m_pool = (byte*)Marshal.AllocHGlobal ((int)allocMem);
		m_poolState = (byte*)Marshal.AllocHGlobal(nMaxItem);
		if (m_pool == null || m_poolState == null)
		{
			Dispone();
			return ErrCode.OUT_OF_MEMORY;
		}

        //m_first = (ulong)m_pool;
		m_last = m_pool + ((nMaxItem - 1) * nItemSize);
		InitChain ();// 构造内存链表
		return ErrCode.SUCCESS;
    }
    
    // 对能存通过链表统一起来
	private unsafe void InitChain(){
		// 清除内存块状态
		for (int i = 0; i < m_nMaxItem; i++)
		{
			m_poolState[i] = BLOCK_FREE;
		}

		// 对可用内存创建链表
		byte* pHead = m_pool;
		for (int i = 0; i < m_nMaxItem - 1; i++) {
			*(int*)pHead = i + 1;
			pHead += m_nItemSize;
		}
		*(int*)pHead = -1; // 最后一个没有了!
		m_free = 0;		   // 指向第一个
	}

	// 获得剩余内存
	public int GetFree() {
		return m_nMaxItem - m_nAlloc;
	}

    // 通过block的状态获得剩余数量
    public int GetFreeFromBlock()
    {
        int count = 0;
        for (int i = 0; i < m_nMaxItem; i++)
        {
            if (m_poolState[i] == 0)
            {
                count++;
            }
        }

        return count;
    }

	// 通过内存地址获得内存块下标
	// @param addr, 内存块地址
	// @return -1, 无效内存块. >0 内存块索引 取值范围[0, m_nMaxItem)
	public int GetAddrIndex(IntPtr addr)
	{
        byte* v = (byte*)addr;
		if (v < m_pool || v > m_last) {
			return -1;
		}

        int idx = (int)(v - m_pool);
		if (idx % m_nItemSize != 0) {
			return -1;
		}

		idx /= m_nItemSize;
		return idx;
	}

	// 通过idx获得地址
	byte* GetAddr(int idx)
	{
		if (m_pool == null)
			return null;
		if (idx < 0 || idx >= m_nMaxItem)
			return null;

		byte* addr = m_pool + idx * m_nItemSize;
		return addr;
	}

	// 获得内存块的状态
	public unsafe BLOCK_SATE GetBlockState(int idx)
	{
		if (m_poolState == null || idx < 0 || idx >= m_nMaxItem)
			return BLOCK_SATE.FAIL;

		if (m_poolState[idx] == BLOCK_FREE){
			return BLOCK_SATE.FREE;
		}

		return BLOCK_SATE.USED;
	}

    // 分配一个item
	public ErrCode Alloc(out IntPtr pRet) {
		pRet = IntPtr.Zero;
		if (m_pool == null)
			return ErrCode.UNINIT;
		
		if (m_free < 0) {
			return ErrCode.OUT_OF_MEMORY;
		}

		var state = GetBlockState (m_free);
		if (state != BLOCK_SATE.FREE) {
			return ErrCode.FAIL_TAG;
		}

		pRet = (IntPtr)GetAddr (m_free);
		m_poolState [m_free] = BLOCK_USED; 	// 写入状态
		m_free = *(int*)pRet;     		// 链表下移

		// 返回分配的内存
		m_nAlloc++;
		return ErrCode.SUCCESS;
    }

    // 释放一个item
	public ErrCode Free(IntPtr addr) {
		if (m_pool == null)
			return ErrCode.UNINIT;

        byte* pAddr = (byte*)addr;
        int idx = GetAddrIndex (addr);
		if (idx < 0) {
			return ErrCode.FAIL_ADDRESS;
		}

		if (GetBlockState (idx) != BLOCK_SATE.USED) {
			return ErrCode.TWICE_FREE; // 重复释放内存(根据内部标记判断!)
		}

		// 插入free链表
		*(int*)pAddr = m_free;
		m_free = idx;
		m_poolState [idx] = BLOCK_FREE; // 状态恢复为空闲
		m_nAlloc--;
		return ErrCode.SUCCESS;
    }

	// 释放内存
	public void Dispone(bool disposing = true) {
		if (m_pool != null)
			Marshal.FreeHGlobal ((IntPtr)m_pool);

		if (m_poolState != null)
			Marshal.FreeHGlobal ((IntPtr)m_poolState);
		
		m_pool = null;
		m_poolState = null;
		//m_lastItem = null;
		m_free = 0;		  // 剩余地址索引(以下标1开始, 0表示无效)(直接用指针的话, 64位占8个字节呢)
		m_nMaxItem = 0;   // 最大Item数量
		m_nItemSize = 0;  // 每个ItemSize 的大小
		m_nAlloc = 0;	  // 已分配内存
	}
}
}