using System;
using System.Collections;
using log4net;

// 封装异步请求与返回
public class MessageRequest<T> : IEnumerator where T : class,Google.Protobuf.IMessage,new()
{
	private IEnumerator m_process;
	private Action<T> m_onSuccess;
	private Action<T> m_onFail;
	private T m_req;
	private float m_timeOut;
	private Cs.GameMsgID m_messageId;
	private NetClient m_client;
	private static ILog log = LogManager.GetLogger("NetRequest");

	public MessageRequest(Cs.GameMsgID messageId, T req)
	{
		m_messageId = messageId;
		m_process = null;
		m_timeOut = 10.0f;
		m_onSuccess = null;
		m_onFail = null;
		m_req = req;
		m_client = NetworkManager.Instance.GetClient();
	}

	public static MessageRequest<T> Build(Cs.GameMsgID messageId, T req)
	{
		var ret = new MessageRequest<T>(messageId, req);
		return ret;
	}

	public void Run(bool isSend = false)
	{
		m_process = InterRun(isSend);
		FiberCtrl.Pool.Run(this);
	}

	public MessageRequest<T> SetClient(NetClient client)
	{
		m_client = client;
		return this;
	}

	public MessageRequest<T> SetTimeout(float timeout)
	{
		m_timeOut = timeout;
		return this;
	}

	public MessageRequest<T> SetRequest(T v)
	{
		m_req = v;
		return this;
	}

	public MessageRequest<T> SetOnSuccess(Action<T> onSuccess)
	{
		m_onSuccess = onSuccess;
		return this;
	}

	public MessageRequest<T> SetOnFail(Action<T> onFail)
	{
		m_onFail = onFail;
		return this;
	}

	public MessageRequest<T> SetClinet(NetClient client)
	{
		m_client = client;
		return this;
	}

	public object Current
	{
		get
		{
			return m_process.Current;
		}
	}

	public bool MoveNext()
	{
		return m_process.MoveNext();
	}

	public void Reset()
	{
		m_process.Reset();
	}

	IEnumerator InterRun(bool isSend)
	{
		if (m_client.IsConnected == false)
			yield break;

		// 发送数据
		if (isSend)
		{
			if(m_req!=null)
				m_client.SendMessage((int)m_messageId, m_req);
			else
				m_client.Send((int)m_messageId, null , 0,0);
		}

		// 等待数据回来
		var wait = new WaitMessage<T>((int)m_messageId, m_client, (double)m_timeOut);
		yield return wait;

		// 结束时销毁数据
		yield return new Fibers.Fiber.OnTerminate(() =>
		{
			wait.Dispose();
		});


		if (wait.IsTimeOut)
		{
			log.InfoFormat("Recive {0} Timeout", typeof(T).Name);

			if (m_onFail != null)
			{
				m_onFail(wait.Value);
			}
			wait.Dispose();
			yield break;
		}

		if (wait.IsRecived)
		{
			log.InfoFormat("Recive {0} Success! = {1}", typeof(T).Name, m_messageId.ToString());

			if (m_onSuccess != null)
			{
				m_onSuccess(wait.Value);
			}
		}
	}
}