﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;
using static Fibers.Fiber;

namespace SGame
{
	/// <summary>
	/// 常量定义
	/// </summary>
	public static partial class ConstDefine
	{
		#region 百分比缩放

		public const double C_PER_SCALE = 0.01;

		#endregion

		#region 目标枚举

		/// <summary>
		/// 所有人
		/// </summary>
		public readonly static EnumTarget AllRole = EnumTarget.Player | EnumTarget.Cook | EnumTarget.Waiter | EnumTarget.Customer;


		#endregion

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

		#region 出生地标签
		/// <summary>
		/// 厨师
		/// </summary>
		public const string TAG_BORN_COOK = "born_0";
		/// <summary>
		/// 服务生
		/// </summary>
		public const string TAG_BORN_WAITER = "born_1";
		#endregion

		#region WorkerTags

		public readonly static IReadOnlyList<string> SCENE_WORK_TAG = new string[] { TAG_BORN_COOK, TAG_BORN_WAITER };

		public const string C_WORKER_TABLE_GO_TAG = "machine";

		#endregion

		/// <summary>
		/// max标志
		/// </summary>
		public const string MAX = "MAX";


		/// <summary>
		/// 餐盘位置
		/// </summary>
		private static float _DISH_OFFSET_Y = -1;
		public static float DISH_OFFSET_Y
		{
			get
			{
				if (_DISH_OFFSET_Y < 0)
					_DISH_OFFSET_Y = GlobalDesginConfig.GetFloat("dish_offsety");

				return _DISH_OFFSET_Y;
			}
		}

		/// <summary>
		/// 系统提示时间
		/// </summary>
		private static float _SYSTEM_TIP_TIME = -1;
		public static float SYSTEM_TIP_TIME
		{
			get
			{
				if (_SYSTEM_TIP_TIME < 0)
					_SYSTEM_TIP_TIME = GlobalDesginConfig.GetFloat("system_tip_time");
				return _SYSTEM_TIP_TIME;
			}
		}

		/// <summary>
		/// 系统提示速度 像素/秒
		/// </summary>
		public static float SYSTEM_TIP_SPEED = 10;

		/// <summary>
		/// 游戏提示速度 米/秒
		/// </summary>
		public static float GAME_TIP_SPEED = 10;
	}

	/// <summary>
	/// 静态变量定义
	/// </summary>
	public static partial class StaticDefine
	{
	}
}
