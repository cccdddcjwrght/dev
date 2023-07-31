using UnityEngine;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System;

// ipv4 到ipv6 地址的转换
public class NetIPV6 
{
	public enum ADDRESSFAM
	{
		IPv4,
		IPv6
	}
	
#if UNITY_IPHONE && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern string getIPv6(string host);
#endif

	static string GetIPv6(string host)
	{
#if UNITY_IPHONE && !UNITY_EDITOR
		    return getIPv6 (host);
#else
		return host + "&&ipv4";
#endif
	}

	// Get IP type and synthesize IPv6, if needed, for iOS
	static void GetIPType(string serverIp, out String newServerIp, out AddressFamily IPType)
	{
		IPType = AddressFamily.InterNetwork;
		newServerIp = serverIp;
		try
		{
			string IPv6 = GetIPv6(serverIp);
			if (!string.IsNullOrEmpty(IPv6))
			{
				string[] tmp = System.Text.RegularExpressions.Regex.Split(IPv6, "&&");
				if (tmp != null && tmp.Length >= 2)
				{
					string type = tmp[1];
					if (type == "ipv6")
					{
						newServerIp = tmp[0];
						IPType = AddressFamily.InterNetworkV6;
					}
				}
			}
		}
		catch (Exception e)
		{
			Debug.LogErrorFormat("GetIPv6 error: {0}", e.Message);
		}
	}

	// Get IP address by AddressFamily and domain
	private static string GetIPAddress(string hostName, ADDRESSFAM AF)
	{
		if (AF == ADDRESSFAM.IPv6 && !System.Net.Sockets.Socket.OSSupportsIPv6)
			return null;
		if (string.IsNullOrEmpty(hostName))
			return null;
		System.Net.IPHostEntry host;
		string connectIP = "";
		try
		{
			host = System.Net.Dns.GetHostEntry(hostName);
			foreach (System.Net.IPAddress ip in host.AddressList)
			{
				if (AF == ADDRESSFAM.IPv4)
				{
					if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
						connectIP = ip.ToString();
				}
				else if (AF == ADDRESSFAM.IPv6)
				{
					if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
						connectIP = ip.ToString();
				}

			}
		}
		catch (Exception e)
		{
			Debug.LogErrorFormat("GetIPAddress error: {0}", e.Message);
		}
		return connectIP;
	}

	// Check IP or not
	static bool IsIPAddress(string data)
	{
		Match match = Regex.Match(data, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
		return match.Success;
	}

	static public string GetConnectAddress(string server)
    {
		string connectionHost = server;
		string convertedHost = "";
		AddressFamily convertedFamily = AddressFamily.InterNetwork;
		if (IsIPAddress(server))
		{
			GetIPType(server, out convertedHost, out convertedFamily);
			if (!string.IsNullOrEmpty(convertedHost))
				connectionHost = convertedHost;
		}
		else
		{
			convertedHost = GetIPAddress(server, ADDRESSFAM.IPv6);
			if (string.IsNullOrEmpty(convertedHost))
				convertedHost = GetIPAddress(server, ADDRESSFAM.IPv4);
			else
				convertedFamily = AddressFamily.InterNetworkV6;
			if (string.IsNullOrEmpty(convertedHost))
			{
				Debug.LogErrorFormat("Can't get IP address");
				return null;
			}
			else
				connectionHost = convertedHost;
		}
		Debug.LogFormat("Connecting to {0}, protocol {1}", connectionHost, convertedFamily);
		return connectionHost;
	}

}
