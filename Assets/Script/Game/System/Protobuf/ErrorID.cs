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
			/* todo
			var s = id > 1;
			if (s && tips)
			{
				var code = (cs.ErrorCode)id.id;
				if (code != cs.ErrorCode.Error_None)
				{
					var k = code.ToString();
					Tips(k);
					GameDebug.LogError(k);
				}
				else
					GameDebug.LogError($"svr errorID:{id.id}");

			}
			return s;
			*/
			return false;
		}

		static partial void Tips(string key);
	}

}
