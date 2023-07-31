using UnityEngine;
using System;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public struct NetPackage
{
    public const int MAX_PACKAGESIZE = 65536; // 最大网络数据包大小64K
    public byte[] data;                       // 网络数据
    public int start_pos;                     // 起始位置
    public int len;                           // 数据长度
}

// 网络适配器接口, 纯粹负责网络数据收发 不处理 加密, 解密, 事件分发 使用Ping包保持网络连接 这类逻辑处理
// 目的是为了方便将网络处理接口抽象出来, WEBGL, TCP, UDP 等的网络功能可以使用 统一接口处理
public interface INetworkAdapter// : BehaviourSingleton<NetworkManager>
{
    // 事件接口
    Action<int, string> OnConnect { get; set; }         // 连接事件
    Action<int, string> OnDisconnect { get; set; }                   // 断开连接事件
    //Func<byte[], int, int> IsOnePackage { get; set; }   // 通过数据判断是否是一个包, 返回 当前数据长度, 返回包体数据长度 0表示不成一个包. 大于0 表示整个包的长度

    // 主线程中的数据包
    // 获取数据包列表
    // param maxCount 每次最大处理网络包个数 0 表示不限制, 至少会处理一个数据包
    // param maxData  每次最大数量网络包大小 0 表示不限制, 至少会处理一个数据包
    // return 返回处理的网络数据包列表
    IEnumerator<NetPackage> RecvMesssages(int maxCount, int maxData);     

    // 数据, startPos, len, package size
    // 初始化
    void Initialize(Func<byte[], int, int, int> onPackage);

    void Reset();

    bool Connected { get; }

    bool IsConnecting { get; }

    void Connect(string serverIP, int port, int times);

    // 每帧更新
    void UpdateFrame(float detalTime);

    // 关闭连接
    void CloseConnection(int errCode);

    // 释放资源
    void Dispose();

    // 发送数据
    void SendMessage(byte[] message, int start_pos, int len);


}