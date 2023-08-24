using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGame;

/// <summary>
/// 数据量发送实现
/// </summary>
public static partial class Protocol
{
	static partial void DoSend(int msgID, byte[] buffer, int offset, int length, int svrType)
	{
		if (msgID > 0)
		{
			var svr = GetAdapter(svrType);
			if (svr != null)
			{
				if (!svr.IsConnected && _args.GetBool("connect"))
				{
					Connect(svrType, (s) =>
					{
						if (s) svr.Send(msgID, buffer, offset, length);
						ArrayPool<byte>.Push(buffer);
					});
					return;
				}
				else 
				{
					svr.Send(msgID, buffer, offset, length);
				}
			}
		}
		ArrayPool<byte>.Push(buffer);
	}

}