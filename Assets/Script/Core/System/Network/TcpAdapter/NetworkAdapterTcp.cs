using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameNet;

// TCP 网络适配器实现
public class NetworkAdapterTcp : INetworkAdapter//, INetController
{
    const float CONNECT_TIME_INTERVAL   = 1.0f;             // 网络连接间隔时间, 太快会照成线程释放问题
    const int NETWORK_BUFFER            = 1024 * 200;       // 200K的收发缓存
    const int DEF_RECV_BUFFER_SIZE      = 64 * 1024;		// default initial buffer size of recvStream

    // 事件接口
    public Action<int, string> OnConnect { get; set; }         // 连接事件
    public Action<int, string> OnDisconnect { get; set; }                   // 断开连接事件
    //public Func<byte[], int, int> IsOnePackage { get; set; }   // 通过数据判断是否是一个包, 返回 当前数据长度, 返回包体数据长度 0表示不成一个包. 大于0 表示整个包的长度

    private NetConnect clientConnect;
    private MessageProcess _messageBuffer;           // 消息缓存
    private Func<byte[], int, int, int> _funOnePackageCheck;
    private string _serverIP;
    private int _port;
    private float _connectTime = 0;

    public IEnumerator<NetPackage> RecvMesssages(int maxCount, int maxData)            // 主线程中的数据包
    {
        if (clientConnect == null || clientConnect.IsConnect() == false)
            yield break;

        var iter = _messageBuffer.RecvMesssages(maxCount, maxData);
        while (iter.MoveNext()) {
            yield return iter.Current;
        }
    }

    // 初始化
    public void Initialize(Func<byte[], int, int, int> onPackage)
    {
        clientConnect = new NetConnect();
        _messageBuffer = new MessageProcess();
        _funOnePackageCheck = onPackage;
        clientConnect.Initinalize(NETWORK_BUFFER, NETWORK_BUFFER, DEF_RECV_BUFFER_SIZE);
        _messageBuffer.Initialize(DEF_RECV_BUFFER_SIZE, onPackage, clientConnect.Recive);
        clientConnect.onStateChange += OnStateChange;
    }

    public void Reset()
    {
        Debug.Log("Network Reset");
    }

    public bool Connected { get { return clientConnect.GetConnState() == CONN_STATE.CONNECTED; } }

    public bool IsConnecting
    {
        get
        {
			if (clientConnect == null) return false;
            var state = clientConnect.GetConnState();// == CONN_STATE.CONNING; 
            if (state == CONN_STATE.CONNING)
                return true;

            if (state == CONN_STATE.CONNECTFAIL)
            {
                // 防止短时间多次连接
                return _connectTime > 0;
            }

            return false;
        }
    }

    public void Connect(string serverIP, int port, int times)
    {
        if (IsConnecting)
            return;

        Debug.Log("Network Connect");
        clientConnect.isIPV4 = !serverIP.Contains(":");
        clientConnect.Connect(serverIP, port);
    }

    // 每帧更新
    public void UpdateFrame(float detalTime)
    {
        if(_connectTime > 0)
        {
            _connectTime -= detalTime;
        }
        
        if (clientConnect != null)
            clientConnect.UpdateConnState();
    }

    // 关闭连接
    public void CloseConnection(int errCode)
    {
        Debug.Log("Network Close Connect" + errCode);
        if (clientConnect != null)
            clientConnect.Close(false);

        if (OnDisconnect != null)
            OnDisconnect(errCode, null);
    }

    // 释放资源
    public void Dispose()
    {
        Debug.Log("Network Close Disponse");
        if (clientConnect != null)
            clientConnect.Close(false);
    }

    // 发送数据
    public void SendMessage(byte[] message, int start_pos, int len)
    {
        clientConnect.Send(message, start_pos, len);
    }

	public void OnStateChange(NetConnect conn, CONN_STATE nextState, CONN_STATE beforeState) // 状态改变通知
	{
		Debug.Log("Network State Change=" + nextState.ToString());
		if (nextState == CONN_STATE.CLOSE || nextState == CONN_STATE.PASSIVE_CLOSE || nextState == CONN_STATE.CONNECTED)
		{
			_messageBuffer.Clear();
		}


		if (nextState == CONN_STATE.CONNECTED)
		{
			if (OnConnect != null)
			{
				OnConnect(0, null);
			}
		}
        else if (nextState == CONN_STATE.PASSIVE_CLOSE)
        {
            if (OnDisconnect != null)
            {
                OnDisconnect(0, null);
            }
        }
        else if (nextState == CONN_STATE.CONNECTFAIL)
        {
            _connectTime = CONNECT_TIME_INTERVAL;
			if (OnConnect != null)
			{
				OnConnect((int)conn.GetConnErr(), string.Format("connect fail socket err={0}", conn.GetSocketErr()));
			}
		}
    }

    public int CheckPackage(byte[] buff, int start_pos, int len)                           // 检测数据是否是一个包
    {
        return _funOnePackageCheck(buff, start_pos, len);
    }
}
