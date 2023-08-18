using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using IPMessage = Google.Protobuf.IMessage;

/// <summary>
/// 协议解析序列化，推送接收
/// </summary>
public static partial class Protocol
{
	public struct PID
	{

		public int id;

		static public implicit operator PID(int id)
		{
			return new PID() { id = id };
		}

		static public implicit operator int(PID id)
		{
			return id.id;
		}
	}

	public struct PID<T>
	{
		public int id;

		static public implicit operator PID<T>(int id)
		{
			return new PID<T>() { id = id };
		}

		static public implicit operator int(PID<T> id)
		{
			return id.id;
		}

		static public implicit operator PID<T>(PID id)
		{
			return id.id;
		}

		static public implicit operator PID(PID<T> id)
		{
			return id.id;
		}

	}

	static readonly private byte[] _empty = System.Array.Empty<byte>();
	static private Stream _outstream;
	static private object[] _args;

	static public PID ToPID(this int id)
	{
		return id;
	}

	static public PID<T> Send<T>(this T item, int msgID, int svrType = 0) where T : class, IPMessage, new()
	{
		SendMsg(item, msgID, svrType);
		return msgID;
	}

	static public PID Send(int msgID, byte[] buffer, int svrType = 0)
	{
		if (msgID > 0)
			DoSend(msgID, buffer, 0, buffer != null ? buffer.Length : 0, svrType);
		_args = null;
		return msgID;
	}

	static public PID SendMsg(this IPMessage item, int msgID, int svrType = 0)
	{

		if (msgID > 0 && item != null)
		{
			PrintMsg(item);
			var buffer = item.Serialize();
			if (buffer != null)
				Send(msgID, buffer, svrType);
		}
		return msgID;

	}

	/// <summary>
	/// 协议注册监听
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="protoID">协议ID，int型或ProtocolID枚举 都可以</param>
	/// <param name="action"></param>
	static public PID Register<T>(this PID protoID, Action<int, T> action, bool once = false, bool unregister = false) where T : class, IPMessage, new()
	{
		if (protoID > 0)
			DoRegister(protoID, action, once, unregister);
		return protoID;
	}

	static public PID Register<T>(this PID<T> protoID, Action<int, T> action, bool once = false, bool unregister = false) where T : class, IPMessage, new()
	{
		if (protoID > 0)
			DoRegister(protoID, action, once, unregister);
		return protoID;
	}

	/// <summary>
	/// buffer接收
	/// </summary>
	/// <param name="msgID">id</param>
	/// <param name="buffer"></param>
	/// <param name="offset">偏移</param>
	/// <param name="count">长度 -1代表去除偏移后的全部长度</param>
	/// <param name="seqID">序列id</param>
	static public PID Recv(this PID msgID, byte[] buffer, int offset, int count, int seqID)
	{
		Recv(msgID.id, buffer, offset, count, seqID);
		_args = null;
		return msgID;
	}

	static public void Recv(int msgID, byte[] buffer, int offset, int count, int seqID)
	{
		if (msgID != 0 && buffer != null)
			DoRecv(msgID, buffer, offset, count >= 0 ? count : buffer.Length - offset, seqID);
		_args = null;
	}

	static public void SetArgs(params object[] args)
	{
		_args = args;
	}

	/// <summary>
	/// 序列化
	/// </summary>
	/// <typeparam name="T">协议类型</typeparam>
	/// <param name="val">协议对象</param>
	/// <returns></returns>
	static public byte[] Serialize<T>(this T val) where T : class, IPMessage
	{
		if (val != null)
		{
			var len = SerializeToBuff(val, _empty, -1);
			if (len > 0)
			{
				var buffer = ArrayPool<byte>.Pop(len);
				CopyStreamToBuffer(buffer, 0, len);
				return buffer;
			}
		}
		return null;
	}

	/// <summary>
	/// 反序列化
	/// </summary>
	/// <typeparam name="T">协议类型</typeparam>
	/// <param name="buffer">协议对象</param>
	/// <param name="offset">偏移</param>
	/// <param name="length">长度</param>
	/// <returns></returns>
	static public T Deserialize<T>(byte[] buffer, int offset, int length) where T : class, IPMessage, new()
	{
		object msg = new T();
		DoDeserialize(ref msg, buffer, offset, length);
		PrintMsg(msg);
		return msg as T;
	}

	static public int SerializeToBuff<T>(this T val, byte[] buffer, int offset) where T : class, IPMessage, new()
	{
		if (val != null && buffer != null && buffer.Length > offset)
			return SerializeToBuff((object)val, buffer, offset);
		return 0;
	}

	static private int SerializeToBuff(object val, byte[] buffer, int offset)
	{
		var len = 0;
		offset = Math.Max(0, offset);
		DoSerialize(val, ref _outstream, ref len);
		if (len > 0 && buffer != null && buffer.Length > offset + len)
			return CopyStreamToBuffer(buffer, offset, len);
		return len;
	}

	static private int CopyStreamToBuffer(byte[] buffer, int offset, int length)
	{
		length = length > 0 ? length : (int)_outstream.Length;
		return _outstream.Read(buffer, offset, length);
	}

	static partial void DoSerialize(object msg, ref Stream buffer, ref int len);

	static partial void DoDeserialize(ref object msg, byte[] buffer, int offset, int len);

	static partial void DoSend(int msgID, byte[] buffer, int offset, int length, int svrType);

	static partial void DoRecv(int msgID, byte[] buffer, int offset, int count, int seqID);

	static partial void DoRegister<T>(int protoID, Action<int, T> action, bool once = false, bool unregister = false) where T : class, IPMessage, new();

	[Conditional("DEBUG")]
	static partial void PrintMsg(object msg);
}
