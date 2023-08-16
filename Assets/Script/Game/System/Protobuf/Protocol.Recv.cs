using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 数据流接收处理
/// </summary>
public static partial class Protocol
{
	static partial void DoRecv(int msgID, byte[] buffer, int offset, int count, int seqID)
	{
		Cmd.CmdSystem.Instance.Excute(
			msgID,
			buffer,
			offset,
			count,
			seqID
		);
	}

	static partial void DoRegister<T>(int protoID, Action<int, T> action, bool once, bool unregister)
	{
		if (!unregister)
			Cmd.CommonCmd.AddListen<T>(protoID, action, once);
		else
			Cmd.CommonCmd.RemoveListen<T>(protoID, action);
	}

}