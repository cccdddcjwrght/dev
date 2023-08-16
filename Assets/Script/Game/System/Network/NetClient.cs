//*************************************************************************
//	创建日期:	2022-7-25
//	文件名称:	NetConnect.cs
//  创 建 人:   Silekey
//	版权所有:	等效原理
//	说    明:	客户端网络代码, 封装了包信息
//*************************************************************************

using System.Collections.Generic;
using System;
using log4net;
using UnityEngine;
using System.Collections;

//libx.Assets.LoadAssetAsync

// 客户端代码, 处理客户端包逻辑
public class ConnectHandle : IEnumerator
{
	// 是否存在错误
	string m_error = null;

	// 错误代码
	int m_errorCode = -1;
	bool m_isResult = false;
	bool m_isConnected = false;

	public int errorCode { get { return m_errorCode; } }
	public string error { get { return m_error; } }

	public void SetState(bool _isConnected, int _errorCode, string _error)
	{
		if (m_isResult == true)
			return;

		m_errorCode = _errorCode;
		m_error = _error;
		m_isConnected = _isConnected;
		m_isResult = true;
	}

	// IEnumerator 接口

	public object Current { get { return null; } }

	public bool MoveNext()
	{
		return m_isResult == false;
	}

	public void Reset() { }
}


public class NetClient
{
	// 网络事件
	public enum NET_EVENT : int
	{
		CONNECT_SUCC,   // 连接成功
		CONNECT_FAIL,   // 连接失败
		DISCONNECT,     // 断开连接
	}

	const int                             MESSAGE_SIZE = 64 * 1024;

	private INetworkAdapter               m_connect;

	static ILog                           log          = LogManager.GetLogger("System.NetClient");

	// 网络事件派发
	private SGame.EventDispatcherEx       eventPackage = new SGame.EventDispatcherEx();

	// 内部事件派发
	private SGame.EventDispatcherEx       interEvent    = new SGame.EventDispatcherEx();

	public Action<GamePackage>            OnNetPackage { get; set; }
	public Action<NetClient, GamePackage> OnSendFail { get; set; }
	
	// 网络序号
	private uint           m_clientSeq = 0;

	private ConnectHandle  m_cacheHandle;

	byte[] m_sendBuffer = new byte[MESSAGE_SIZE];

	public string Error { get; private set; }

	public bool Initalize(INetworkAdapter network)
	{
		m_connect = network;
		m_connect.OnConnect = OnInterConnect;
		m_connect.OnDisconnect = OnInterDisconnect;
		m_connect.Initialize(checkPackage);
		m_clientSeq = 1;
		return true;
	}

	public ConnectHandle Connect(string serverIp, int port, int timeOut)
	{
		string serverHost = NetIPV6.GetConnectAddress(serverIp);
		m_cacheHandle = new ConnectHandle();
		m_connect.Connect(serverIp, port, timeOut);
		return m_cacheHandle;
	}

	public SGame.EventHanle RegMessage(int _msgId, Callback<GamePackage> eventCaller)
	{
		var ret = eventPackage.Reg<GamePackage>(_msgId, eventCaller);
		return ret;
	}

	public SGame.EventHanle RegConnectSuccEvent(Callback eventCaller)
	{
		return interEvent.Reg((int)NET_EVENT.CONNECT_SUCC, eventCaller);
	}

	public SGame.EventHanle RegConnectFailEvent(Callback<int, string> eventCaller)
	{
		return interEvent.Reg((int)NET_EVENT.CONNECT_FAIL, eventCaller);
	}

	public SGame.EventHanle RegDisconnectEvent(Callback<int, string> eventCaller)
	{
		return interEvent.Reg((int)NET_EVENT.DISCONNECT, eventCaller);
	}

	void OnInterConnect(int errCode, string err)
	{
		m_clientSeq = 1;
		if (m_cacheHandle != null)
			m_cacheHandle.SetState(IsConnected, errCode, err);

		if (errCode == 0)
			interEvent.Trigger((int)NET_EVENT.CONNECT_SUCC);
		else
		{
			Error = err;
			interEvent.Trigger((int)NET_EVENT.CONNECT_FAIL, errCode, err);
		}
	}

	void OnInterDisconnect(int errCode, string err)
	{
		Error = err;

		if (m_cacheHandle != null)
			m_cacheHandle.SetState(false, errCode, err);

		interEvent.Trigger((int)NET_EVENT.DISCONNECT, errCode, err);
	}

	// 更新网络数据
	public void Update(float deltaTime)
	{
		// 更新状态
		m_connect.UpdateFrame(deltaTime);

		// 接受数据
		for (var iter = m_connect.RecvMesssages(5, 0); iter.MoveNext();)
		{
			// 包体处理
			var pkg = iter.Current;
			ProcessPackage(iter.Current);
		}
	}
	
	// 发送网络数据
	public unsafe void Send(int msgId, byte[] _data, int _start_pos, int _len)
	{
		if (!m_connect.Connected)
		{
			log.Warn("Networkd Is Disconnected!");
			OnSendFail?.Invoke(this, new GamePackage()
			{
				msgId = msgId,
				data = new NetPackage()
				{
					data = _data,
					start_pos = _start_pos,
					len = _len
				}
			}); ;
			return;
		}

		// 修改包头
		fixed (byte* dataPtr = &m_sendBuffer[0])
		{
			PackageHead* head = (PackageHead*)dataPtr;
			head->msgId = msgId;
			head->pkgLen = PackageHead.SIZE + _len;
			head->pkgSeq = m_clientSeq++;
			FillVer(ref *head);
		}

		// 发送数据包
		if (_len > 0)
		{
			Buffer.BlockCopy(_data, _start_pos, m_sendBuffer, PackageHead.SIZE, _len);
		}

		m_connect.SendMessage(m_sendBuffer, 0, PackageHead.SIZE + _len);
	}

	public void Close()
	{
		m_connect.CloseConnection(0);
	}

	static void FillVer(ref PackageHead head)
	{
		//head.ver0 = (byte)NetVer.VER0;
		//head.ver1 = (byte)NetVer.VER1;
		//head.ver2 = (byte)NetVer.VER2;
		head.ver = 1;
	}

	// 处理单个数据包
	unsafe void ProcessPackage(NetPackage pkg)
	{
		GamePackage gamePackage = new GamePackage();

		// 填充包数据
		fixed (byte* dataPtr = &pkg.data[0])
		{
			PackageHead* headPtr = (PackageHead*)dataPtr;
			gamePackage.msgId = headPtr->msgId;
			gamePackage.seq = headPtr->pkgSeq;

			gamePackage.data.data = pkg.data;
			gamePackage.data.len = pkg.len - PackageHead.SIZE;
			gamePackage.data.start_pos = pkg.start_pos + PackageHead.SIZE;
		}

		//eventPackage
		if (eventPackage.IsContains(gamePackage.msgId))
		{
			eventPackage.Trigger(gamePackage.msgId, gamePackage);
		}
		else
		{
			eventPackage.Trigger(0, gamePackage);
		}
	}

	// 网络数据拆包判断
	// 返回包体大小
	unsafe int checkPackage(byte[] _data, int _startPos, int _len)
	{
		if (_len < PackageHead.SIZE)
			return 0;

		fixed (byte* dataPtr = &_data[_startPos])
		{
			PackageHead* headPtr = (PackageHead*)dataPtr;
			int pkgLen = headPtr->pkgLen;
			if (pkgLen < PackageHead.SIZE)
			{
				m_connect.CloseConnection((int)NetError.NET_ERROR_ILLEGAL_PACKAGE);
				return 0;
			}


			if (_len >= pkgLen)
			{
				return pkgLen;
			}
		}


		return 0;
	}

	// 判断连接状态
	public bool IsConnected { get { return m_connect.Connected; } }

	// 是否正在连接
	public bool IsConnecting { get { return m_connect.IsConnecting; } }
}