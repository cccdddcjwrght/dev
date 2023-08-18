using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cmd
{
	partial class ErrorCode
	{
		static partial void Convert(int id, ref string tips, ref bool isError)
		{
			var e	= (Cs.ErrorCode)id;
			isError = e != Cs.ErrorCode.ErrorSuccess;
			tips = e.ToString();
		}

		static partial void Tips(string key)
		{

		}

	}
}