using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 消息处理逻辑, 可以将数据转为 对一个一个数据包处理的迭代器
public class MessageProcess 
{
    // 检测一段内存是否有数据包的接口
    // param data, 要检测的网络数据包
    // param start_pos, 数据开始位置
    // param len, 数据长度
    // param 0 表示没有数据包, > 0表示数据包长度
    public delegate int CHECK_ONE_PACKAGE(byte[] data, int start_pos, int len);

    // 持续获取数据的接口
    // param data, 要
    // param start_pos, 数据开始位置
    // param len, 数据长度
    // param 0 表示没有数据包, > 0表示数据包长度
    public delegate int RECIVE_DATA(byte[] data, int start_pos, int len);


    Func<byte[], int, int, int> _funcCheckOnePackage; 
    RECIVE_DATA       _funcRecive;
    SimpleBuffer _Buffer;
    
    public bool Initialize(int bufferSize, Func<byte[], int, int, int> checkPackage, RECIVE_DATA reciveData)
    {
        _funcCheckOnePackage = checkPackage;
        _funcRecive = reciveData;
        _Buffer = new SimpleBuffer();
        _Buffer.Initialize(bufferSize);
        return true;
    }

    public void Clear()
    {
        _Buffer.Clear();
    }

    // 分发网络数据
    IEnumerator<NetPackage> DispatchMessage()//ref int maxPackage)
    {
        for (int pkgLen = _funcCheckOnePackage(_Buffer.buffer, _Buffer.offsetRead, _Buffer.readSize);
            pkgLen > 0;
            pkgLen =_funcCheckOnePackage(_Buffer.buffer, _Buffer.offsetRead, _Buffer.readSize))
        {
            // 网络处理
            NetPackage pkgData = new NetPackage
            {
                data = _Buffer.buffer,
                start_pos =  _Buffer.offsetRead,
                len = pkgLen
            };

            _Buffer.Read(pkgLen); // 设置buffer 读取位置
            yield return pkgData;
        }

        // 处理完后, 整理BUFF
        //_Buffer.Arrage();
        //return processPackage;
    }

    // 将网络缓存放入主线程中的缓存中
    int PullReviceData()
    {
        _Buffer.Arrage();
        int readLen = _Buffer.writeSize;

        readLen = _funcRecive(_Buffer.buffer, _Buffer.offsetWrite, readLen);
        if (readLen > 0) {
            _Buffer.Write(readLen);
        }
        return readLen;
    }


    // 将网络数据转换为一个一个包
    public IEnumerator<NetPackage> RecvMesssages(int maxProcess, int maxData)
    {
        int processPackageCount = maxProcess; // 需要处理的网络消息
        int processData         = maxData;
        while (true) //processPackageCount > 0)
        {
            // 没有数据了
            int reciveLen = PullReviceData();
            var iter = DispatchMessage();
            while (iter.MoveNext())
            {
                processPackageCount++;
                processData += iter.Current.len;

                yield return iter.Current;

                if (maxProcess != 0 && processPackageCount >= maxProcess)
                {
                    yield break;
                }
                if (maxData != 0 && processData >= maxData)
                {
                    yield break;
                }
            }
            
            if (reciveLen == 0)// && isProcess == false)
            {
                // 0 数据 此轮结束
                yield break;
            }
        }
    }
}
