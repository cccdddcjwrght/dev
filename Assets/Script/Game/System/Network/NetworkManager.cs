using SGame;
using log4net;

public class NetworkManager : Singleton<NetworkManager>
{
	// 链接类型
	public enum CONNECT // 网络链接类型
	{
		CLIENT = 0,
	}
	
	NetClient  m_Client;
	static ILog log = LogManager.GetLogger("System.Network");

	public NetworkManager()
	{
		m_Client = new NetClient();
	}

	public NetClient GetClient(CONNECT conect = CONNECT.CLIENT)
	{
		return m_Client;
	}
	
	/// <summary>
	/// 初始化代码
	/// </summary>
	public void Initalize()
	{
		// 使用TCP 协议
		m_Client.Initalize(new NetworkAdapterTcp());
	}

	public void Update(float deltaTime)
	{
		m_Client.Update(deltaTime);
	}

	public void Shutdown()
	{
		m_Client?.Close();
		m_Client = null;
	}
}