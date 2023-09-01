using System.Collections;
using SGame;

// 等待网络消息 并带有超时判断
public class WaitMessage<T> : IEnumerator where T : class, Google.Protobuf.IMessage , new()
{
	public bool IsTimeOut { get { return GlobalTime.passTime > m_waitTime; } }
	public bool IsRecived { get; private set; }

	private EventHandleContainer m_handles = new EventHandleContainer();
	private double m_waitTime = 0;
	private bool m_isDone = false;

	public T Value;

	public WaitMessage(int waitMessageId, NetClient client = null, double waitTimeOut = 10.0f)
	{
		if (client == null)
			client = NetworkManager.Instance.GetClient();
		m_isDone = false;
		m_waitTime = waitTimeOut + GlobalTime.passTime;
		m_handles += client.RegMessage(waitMessageId, OnMessage);
		m_handles += client.RegDisconnectEvent(OnDisconnect);
	}

	void OnMessage(GamePackage message)
	{
		IsRecived = true;
		var d = message.data;
		Value = Protocol.Deserialize<T>(d.data, d.start_pos, d.len);
		Finish();
	}

	void OnDisconnect(int err, string errMessage)
	{
		m_waitTime = 0;
		Finish();
	}

	public void Dispose()
	{
		Finish();
		Value = null;
	}

	void Finish()
	{
		m_isDone = true;
		if (m_handles != null)
		{
			m_handles.Close();
			m_handles = null;
		}
	}

	public object Current { get { return null; } }

	public bool MoveNext()
	{
		if (m_isDone == true)
			return false;

		if (IsRecived || IsTimeOut)
		{
			// 多等一帧, 让事件派发完成
			Finish();
		}

		return true;
	}

	public void Reset()
	{

	}
}

public abstract class IMessageHandler 
{
	public abstract IEnumerator Wait();
	public abstract bool TryResult<R>(out R result) where R : class, Google.Protobuf.IMessage;

	public abstract bool IsTimeOut();

	static public IMessageHandler Send<T>(int msgid, T msg , out IMessageHandler handler , NetClient client = null, double waitTimeOut = 10.0f )  where T : class, Google.Protobuf.IMessage, new()
	{
		handler = new MessageHandler<T>(msgid, msg, client, waitTimeOut);
		return handler;
	}

}

public class MessageHandler<T> : IMessageHandler where T : class, Google.Protobuf.IMessage, new()
{
	private WaitMessage<T> m_wait;

	public MessageHandler(int msgid, T msg, NetClient client, double waitTimeOut = 10.0f)
	{
		if (client != null)
			client.SendMessage(msgid, msg);
		m_wait = new WaitMessage<T>(msgid, client, waitTimeOut);
	}

	public override IEnumerator Wait()
	{
		yield return m_wait;
	}

	public override bool IsTimeOut()
	{
		return m_wait!=null ? m_wait.IsTimeOut : false;
	}

	public override bool TryResult<R>(out R result) 
	{
		result = null;
		if (m_wait != null )
		{
			if (m_wait.IsRecived)
			{
				result = m_wait.Value as R;
				m_wait.Dispose();
				m_wait = null;
				return true;
			}
		}
		return false;
	}


	

}