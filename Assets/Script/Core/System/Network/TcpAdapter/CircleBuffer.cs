using System;

// 循环buffer, 支持一个线程读, 一个线程写
public class CircleBuffer 
{
    byte[]  _buffer;    // 内部存储数据
    int     _readPtr;   // 读指针 指向的是当前可读位置
    int     _writePtr;  // 写指针 指向的是当前可写的位置
    int     _bufferSize;

#if UNITY_EDITOR
    // 用于单元测试
    public byte[] GetBuffer() { return _buffer; }
    public int GetReadPtr() { return _readPtr; }
    public int GetWritePtr() { return _writePtr; }
    public int GetBufferSize() { return _bufferSize; }
#endif

    // 初始哈
    // param bufferSize 缓存大小
    public bool Initialize(int bufferSize) {
        if (bufferSize <= 0) {
            return false;
        }

        _buffer = new byte[bufferSize + 1]; // 需要预留一个位置防止写指针追上读指针
        _readPtr = 0;   // 读指针
        _writePtr = 0;  // 写指针
        _bufferSize = bufferSize + 1;
        return true;
    }

    // 可读大小 (读指针可以追上写指针, 追上后就是可度数量为0)
    public int readSize { get {
            return (_writePtr + _bufferSize - _readPtr) % _bufferSize;
        } 
    }

    // 可写大小 (写指针要留一个位置, 不能追上读指针, 否则无法判定写满和空的状态)
    public int writeSize { get { 
            return (_readPtr + _bufferSize - 1 - _writePtr) % _bufferSize;
        } 
    }

    // 清空缓存
    public void Clear()
    {
        _readPtr = 0;
        _writePtr = 0;
    }

    // 写入缓存 (只会修改 _writePtr 与 _buffer 内容)
    // param buff, 数据
    // param start_pos, 开始位置
    // param len, 要写入的数据长度
    // return. 实际写入的长度
    public int Write(byte[] buff, int start_pos, int len)
    {
        int write_size = writeSize;
        len = len <= write_size ? len : write_size;
        if (len <= _bufferSize - _writePtr)
        {
            // 这是一段连续内存, 直接拷贝
            Buffer.BlockCopy(buff, start_pos, _buffer, _writePtr, len);
        }
        else
        {
            // 这是两段内存, 拷贝两次
            int cpyLen1 = _bufferSize - _writePtr;
            int cpyLen2 = len - cpyLen1;
            Buffer.BlockCopy(buff, start_pos, _buffer, _writePtr, cpyLen1);
            Buffer.BlockCopy(buff, start_pos + cpyLen1, _buffer, 0, cpyLen2);
        }

        _writePtr = (_writePtr + len) % _bufferSize;
        return len;
    }

    // 读取数据(只会修改 _readPtr 与 _buffer 内容)
    // param buffer, 存放数据位置
    // param start_pos, buffer 的开始位置
    // param len, 需要读取的数据长度
    public int Read(byte[] buffer, int start_pos, int len)
    {
        int read_size = readSize;
        len = len <= read_size ? len : read_size;

        if (len <= _bufferSize - _readPtr)
        {
            // 这是一段连续内存, 直接拷贝
            Buffer.BlockCopy(_buffer, _readPtr, buffer, start_pos, len);
        }
        else
        {
            // 这是两段内存, 拷贝两次
            int cpyLen1 = _bufferSize - _readPtr;
            int cpyLen2 = len - cpyLen1;

            Buffer.BlockCopy(_buffer, _readPtr, buffer, start_pos, cpyLen1);
            Buffer.BlockCopy(_buffer, 0, buffer, start_pos + cpyLen1, cpyLen2);
        }

        _readPtr = (_readPtr + len) % _bufferSize;
        return len;
    }
}