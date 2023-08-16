using SGame;
using log4net;

public class NetworkManager : Singleton<NetworkManager>
{
	NetClient   m_Client;
	NetClient m_Battle;
	static ILog log = LogManager.GetLogger("System.Network");

	public NetworkManager()
	{
		m_Client = new NetClient();
		m_Battle = new NetClient();
	}

	public NetClient GetClient()
	{
		return m_Client;
	}

	public NetClient GetBattleclient()
	{
		return m_Battle;
	}

	public void Initalize()
	{
		m_Client.Initalize();
	}

	public void Update(float deltaTime)
	{
		m_Client.Update(deltaTime);
		m_Battle.Update(deltaTime);
	}

	public void Shutdown()
	{
		m_Client?.Close();
		m_Battle?.Close();
		m_Client = null;
		m_Battle = null;
	}
}