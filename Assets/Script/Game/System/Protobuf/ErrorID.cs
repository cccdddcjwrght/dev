using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cmd
{
	public struct ErrorID
	{
		public int id;

		static public implicit operator ErrorID(int id)
		{
			return new ErrorID() { id = id };
		}


		static public implicit operator int(ErrorID id)
		{
			return id.id;
		}
	}

	public static partial class ErrorCode
	{

		static public ErrorID ToEID(this int id)
		{
			return id;
		}

		static public bool IsSuccess(this ErrorID id, bool tips = true)
		{
			return !IsError(id, tips);
		}

		static public bool IsError(this ErrorID id, bool tips = true)
		{
			if (id > 0)
			{
				var t = $"proto error id:{id.id}";
				var s = false;
				Convert(id, ref t, ref s);
				if (s)
				{
					Tips(t);
					GameDebug.LogError(t);
				}
				return s;
			}
			return false;
		}

		static public bool CheckError(object val , bool tips = true)
		{
			var ret = false;
			IsErrorProperty(val, ref ret, tips);
			return ret;
		}

		static partial void Tips(string key);

		static partial void Convert(int id ,ref string tips , ref bool isError);

		static partial void IsErrorProperty(object val , ref bool ret, bool showtips = false);

	}

}
