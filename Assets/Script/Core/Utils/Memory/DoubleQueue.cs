//*************************************************************************
//	创建日期:	2016-8-22
//	文件名称:	DoubleQueue.cs
//  创 建 人:   Silekey
//	版权所有:	LT
//	说    明:	简单线程安全的双Queue, 主要用于多线程内部的读写操作(效率最高的情况是一个读, 一个写)
//*************************************************************************

//using UnityEngine;
using System.Collections.Generic;
using System.Threading;

// 双缓存BUFF, (使用BetterList代替,Queue, 让性能可控. yinwe
namespace MemUtils
{
    public class DoubleQueue<T> //where T : class,struct
    {
        BetterList<T> mBuffWrite;    // 写队列
        BetterList<T> mBuffRead;     // 读队列
        object mLockWrite; // 写锁 
        object mLockRead;  // 读锁

        volatile int mWriteLen;     // 已写数据数量
        volatile int mReadLen;      // 可读数据数量
        //volatile int mReadPos;      // 读取的地址

        // @param bufLen, 默认开启多少个
        public DoubleQueue(int bufLen = 0)
        {
            mBuffWrite = new BetterList<T>(); //new byte[bufLen];
            mBuffRead = new BetterList<T>();  //new byte[bufLen];
            mLockWrite = new object(); // 写锁 
            mLockRead = new object();  // 读锁
            mReadLen = 0;
            mWriteLen = 0;

            if (bufLen > 0) {
                mBuffWrite.buffer = new T[bufLen];
                mBuffRead.buffer = new T[bufLen];
            }
        }

        // 返回写入数据0 表示写入失败, 通常是缓冲满了
        public ErrCode Write(T data)
        {
            lock (mLockWrite)
            {
                //mBuffWrite.Enqueue(data);
                mBuffWrite.Add(data);
                mWriteLen++;
                //Interlocked.Increment(ref mWriteLen);
            }

            return ErrCode.SUCCESS;
        }

        // 读取数据
        public ErrCode Read(out T value)
        {
            value = default(T);
            lock (mLockRead) {
                if (mReadLen <= 0) {
                    return ErrCode.DATA_EMPTY;
                }

                int readPos = mBuffRead.size - mReadLen;
                value = mBuffRead.buffer[readPos];
                //Interlocked.Decrement(ref mReadLen);
                mReadLen--;
            }

            return ErrCode.SUCCESS;
        }

        // 读BUFF与写BUFF互换（当读取完当前BUFF后)
        public void Swich()
        {
            lock (mLockWrite)
            {
                lock (mLockRead)
                {
                    if (mWriteLen == 0)
                    {
                        mReadLen = 0;       // 清空读取数据
                        mBuffRead.Clear();
                        return;
                    }

                    // 交换队列
                    var tmp = mBuffWrite;
                    mBuffWrite = mBuffRead;
                    mBuffRead = tmp;

                    mBuffWrite.Clear();
                    mReadLen = mBuffRead.size;
                    mWriteLen = 0;
                }
            }
        }

        // 获取已写数量
        public int GetWriteLen()
        {
            return mWriteLen;
        }

        // 获取
        public int GetReadLen()
        {
            return mReadLen;
        }

        // 数据是否是空的
        public bool IsEmpty()
        {
            return mWriteLen <= 0 && mReadLen <= 0;
        }

        // 清空数据
        public void Clear()
        {
            lock (mLockWrite)
            {
                lock (mLockRead)
                {
                    mWriteLen = 0;      // 已写数据
                    mReadLen = 0;   // 可读数据
                    mBuffRead.Clear();
                    mBuffWrite.Clear();
                }
            }
        }

    }
}