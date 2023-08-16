using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

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

		static public implicit operator PID(ProtocolID id)
		{
			return new PID() { id = (int)id };
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

		static public implicit operator PID<T>(ProtocolID id)
		{
			return new PID<T>() { id = (int)id };
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

	static readonly private ProtoBuf.Meta.TypeModel _builder = GetProtobufBuilder();
	static readonly private byte[] _empty = System.Array.Empty<byte>();
	static readonly private MemoryStream _stream = new MemoryStream(new byte[1024 * 10]);
	static private object[] _args;

	static public PID ToPID(this int id)
	{
		return id;
	}

	static public PID ToPID(this ProtocolID id)
	{
		return id;
	}


	/// <summary>
	/// 协议推送
	/// </summary>
	/// <typeparam name="T">协议类型</typeparam>
	/// <param name="item">协议对象</param>
	/// <param name="msgID">协议id</param>
	/// <param name="svrType">服务器id，默认0</param>
	static public PID<T> Send<T>(this T item, ProtocolID msgID, int svrType = 0) where T : class, ProtoBuf.IExtensible
	{
		if (msgID > 0 && item != null)
		{
			var buffer = item.Serialize();
			if (buffer != null)
				DoSend((int)msgID, buffer, 0, buffer.Length, svrType);
		}
		return msgID;
	}

	static public PID<T> Send<T>(this T item, int msgID, int svrType = 0) where T : class, ProtoBuf.IExtensible
	{
		if (msgID > 0 && item != null)
		{
			var buffer = item.Serialize();
			if (buffer != null)
				Send(msgID, buffer, svrType);
		}
		return msgID;
	}

	/// <summary>
	/// 出来Protobuf数据，其他数据可能有AOT问题
	/// </summary>
	/// <param name="msgID"></param>
	/// <param name="item"></param>
	/// <param name="svrType"></param>
	/// <returns></returns>
	static public int SendCS(int msgID, object item, int svrType = 0)
	{
		if (msgID > 0)
		{
			var buffer = item != null ? item.SerializeCS() : _empty;
			Send(msgID, buffer, svrType);
		}
		return msgID;
	}

	static public PID Send(int msgID, byte[] buffer, int svrType = 0)
	{
		if (msgID > 0)
			DoSend(msgID, buffer, 0, buffer != null ? buffer.Length : 0, svrType);
		_args = null;
		return msgID;
	}

	/// <summary>
	/// 协议注册监听
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="protoID">协议ID，int型或ProtocolID枚举 都可以</param>
	/// <param name="action"></param>
	static public PID Register<T>(this PID protoID, Action<int, T> action, bool once = false, bool unregister = false)
	{
		if (protoID > 0)
			DoRegister(protoID, action, once, unregister);
		return protoID;
	}

	static public PID Register<T>(this PID<T> protoID, Action<int, T> action, bool once = false, bool unregister = false)
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
	static public byte[] Serialize<T>(this T val) where T : class, ProtoBuf.IExtensible
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
	/// 序列号非协议类型数据
	/// !!!不建议使用
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="val"></param>
	/// <returns></returns>
	static public byte[] SerializeCS<T>(this T val)
	{
		var len = SerializeToBuff(val, _empty, -1);
		if (len > 0)
		{
			var buffer = ArrayPool<byte>.Pop(len);
			CopyStreamToBuffer(buffer, 0, len);
			return buffer;
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
	static public T Deserialize<T>(byte[] buffer, int offset, int length) where T : class, ProtoBuf.IExtensible
	{
		return Deserialize(typeof(T), buffer, offset, length) as T;
	}

	static public object Deserialize(Type type, byte[] buffer, int offset, int length)
	{
		if (buffer != null && buffer.Length >= offset + length)
		{
			object ret = null;
			using (var m = new MemoryStream(buffer, offset, length, false))
			{
				ret = _builder.Deserialize(m, null, type);
			}
			return ret;
		}
		return default;
	}

	static public int SerializeToBuff<T>(this T val, byte[] buffer, int offset) where T : class, ProtoBuf.IExtensible
	{
		if (val != null && buffer != null && buffer.Length > offset)
			return SerializeToBuff((object)val, buffer, offset);
		return 0;
	}


	static private int SerializeToBuff(object val, byte[] buffer, int offset)
	{
		_stream.Seek(0, SeekOrigin.Begin);
		_builder.Serialize(_stream, val);
		var len = (int)_stream.Position;
		_stream.Seek(0, SeekOrigin.Begin);
		if (buffer != null && buffer.Length > 0)
			return CopyStreamToBuffer(buffer, Math.Max(0, offset), len);
		return len;
	}

	static private int CopyStreamToBuffer(byte[] buffer, int offset, int length)
	{
		length = length > 0 ? length : (int)_stream.Length;
		return _stream.Read(buffer, offset, length);
	}

	static private ProtoBuf.Meta.TypeModel GetProtobufBuilder()
	{
		ProtoBuf.Meta.TypeModel builder = default;
		DoGetProtobufBuilder(ref builder);
		if (builder == null)
			throw new Exception("Protobuf Builder Is Null!!!");
		return builder;
	}

	static partial void DoSend(int msgID, byte[] buffer, int offset, int length, int svrType);

	static partial void DoRecv(int msgID, byte[] buffer, int offset, int count, int seqID);

	static partial void DoRegister<T>(int protoID, Action<int, T> action, bool once = false, bool unregister = false);

	static partial void DoGetProtobufBuilder(ref ProtoBuf.Meta.TypeModel builder);
}
