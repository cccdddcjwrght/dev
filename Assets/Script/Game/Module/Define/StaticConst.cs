using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGame
{
	/// <summary>
	/// 常量定义
	/// </summary>
	public static partial class ConstDefine
	{
		#region 餐桌
		/// <summary>
		/// 餐桌
		/// </summary>
		public const string TAG_PLACE = "place_1";
		/// <summary>
		/// 餐桌座位
		/// </summary>
		public const string TAG_SEAT = "seat";
		/// <summary>
		/// 餐桌点单上菜
		/// </summary>
		public const string TAG_SERVE = "serve_1";
		#endregion

		#region 上菜

		/// <summary>
		/// 上菜桌
		/// </summary>
		public const string TAG_TAKE_PLACE = "place";
		/// <summary>
		/// 上菜桌上菜位
		/// </summary>
		public const string TAG_TAKE_SERVE = "serve";
		/// <summary>
		/// 上菜桌取菜
		/// </summary>
		public const string TAG_TAKE = "take";

		#endregion

		#region 工作台

		/// <summary>
		/// 工作台做菜位置
		/// </summary>
		public const string TAG_MACHINE_WORK = "cook";

		#endregion

	}

	/// <summary>
	/// 静态变量定义
	/// </summary>
	public static partial class StaticDefine
	{

	}
}
