using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Cmd;
#if PROTO
using IPMessage = Google.Protobuf.IMessage; 
#else
using IPMessage = IProto;
#endif

public partial class Protocol
{
	static public string Format(this object val, StringBuilder sb = null, string pix = null, string name = null)
	{
		if (val != null)
		{
			var s = sb == null;
			var nex = pix + "\t";
			sb = sb ?? new StringBuilder();
			name = name ?? val.GetType().Name;
			if (val is IPMessage p)
			{
				sb.AppendFormat(pix + "Msg {0}-{1}:【\n", val.GetType().Name, name);
				PropertyInfo[] props = val.GetType().GetProperties();
				foreach (PropertyInfo info in props)
				{
					//if (info.PropertyType.FullName.Contains("Google.")) continue;
					object value = info.GetGetMethod()?.Invoke(val, null);
					if (value != null)
						Format(value, sb, nex, info.Name);
				}
				sb.AppendLine(pix + "】");
			}
			else if (!(val is string) && val is IEnumerable er)
			{
				sb.AppendFormat(pix + "List {0}【\n", name);
				var e = er.GetEnumerator();
				var idx = 0;
				while (e.MoveNext())
					Format(e.Current, sb, nex, (idx++).ToString());
				sb.AppendLine(pix + "】");
			}
			else
			{
				sb.AppendLine(pix + (name ?? "Field") + ":" + val.ToString());
				ErrorCode.CheckError(val);
			}
			return s ? sb.ToString() : null;
		}
		return null;
	}

	static partial void PrintMsg(object msg, string append)
	{
		if (msg != null)
			UnityEngine.Debug.Log(append + "\n" + msg.Format());
	}

}