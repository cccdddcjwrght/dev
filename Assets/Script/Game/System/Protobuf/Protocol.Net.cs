using System;
using System.Collections;

/// <summary>
/// 连接处理
/// </summary>
public static partial class Protocol
{

	static public NetClient GetAdapter(int type)
	{
		var net = NetworkManager.Instance.GetClient();
		return net;
	}


	static public void Connect(int svr, Action<bool> complete)
	{
		Connect(complete, svr).Start();
	}

	static private IEnumerator Connect(Action<bool> complete, int svr)
	{
		var net = GetAdapter(svr);
		if (net == null) yield break;
		net.Connect("192.168.2.159", 3010, 1000);
		yield return new UnityEngine.WaitUntil(() => net.IsConnected || !string.IsNullOrEmpty(net.Error));
		if (!string.IsNullOrEmpty(net.Error))
			GameDebug.LogError("Net Connect Error:" + net.Error);
		complete?.Invoke(IsConnected(svr));
	}

	static private bool IsConnected(int svr)
	{
		return GetAdapter(0)?.IsConnected == true;
	}
}

