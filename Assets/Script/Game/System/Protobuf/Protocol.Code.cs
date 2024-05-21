#if PROTO
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Google.Protobuf;
using IPMessage = Google.Protobuf.IMessage;


public partial class Protocol
{

	static readonly private MemoryStream _writeBuff = new MemoryStream();
	static readonly private CodedOutputStream _out = new CodedOutputStream(_writeBuff);

	static partial void DoDeserialize(ref object msg, byte[] buffer, int offset, int len)
	{
		if (msg is IPMessage p)
			p.MergeFrom(buffer, offset, len);
	}

	static partial void DoSerialize(object msg, ref Stream buffer, ref int len)
	{
		len = 0;
		if (msg is IPMessage p)
		{
			buffer = _writeBuff;
			_writeBuff.Seek(0, SeekOrigin.Begin);
			p.WriteTo(_out);
			_out.Flush();
			len = (int)_writeBuff.Position;
			_writeBuff.Seek(0, SeekOrigin.Begin);
		}
	}

} 
#endif
