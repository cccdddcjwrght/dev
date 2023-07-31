using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 与循环内存不同的是, SimpleBuffer能保证内存的连续性
// 而且只是用于单线程
public class SimpleBuffer
{
    byte[] _buffer;
    int _readPtr;
    int _writePtr;

    public byte[] buffer { get { return _buffer;  } }

    // 读取位置
    public int  offsetRead { get { return _readPtr; } }

    public int offsetWrite { get { return _writePtr; } }

    // 可读数量
    public int readSize { get { return _writePtr - _readPtr; } }

    // 可写数量
    public int writeSize { get { return _buffer.Length - _writePtr; } }

    // 初始化
    public bool Initialize(int buff_size)
    {
        if (buff_size <= 0)
            return false;

        _buffer = new byte[buff_size];
        _readPtr = 0;
        _writePtr = 0;
        return true;
    }

    // 清空
    public void Clear()
    {
        _readPtr = 0;
        _writePtr = 0;
    }

    // 移动写指针
    public bool Write(int len)
    {
        if (len > writeSize || len <= 0)
            return false;

        _writePtr += len;
        return true;
    }

    // 移动读指针
    public bool Read(int len)
    {
        if (len > readSize)
            return false;

        _readPtr += len;
        return true;
    }

    // 整理内存
    public bool Arrage()
    {
        if (_readPtr == 0)// || readSize == 0)
            return false;

        if(readSize == 0)
        {
            Clear();
            return true;
        }

        Buffer.BlockCopy(_buffer, _readPtr, _buffer, 0, readSize);
        _writePtr -= _readPtr;
        _readPtr = 0;
        return true;
    }
}
