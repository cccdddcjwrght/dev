using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;
using log4net;
using UnityEngine;
using UnityEngine.SceneManagement;
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
		/// <summary>
		/// 客人
		/// </summary>
		public const string TAG_BORN_CUSTOMER = "born_3";

		#endregion

		#region WorkerTags

		public readonly static IReadOnlyList<string> SCENE_WORK_TAG = new string[] { TAG_BORN_COOK, TAG_BORN_WAITER };

		public const string C_WORKER_TABLE_GO_TAG = "machine";
		public const string C_WORKER_TABLE_RECRUIT_TAG = "recruit";

		#endregion

		/// <summary>
		/// max标志
		/// </summary>
		public const string MAX = "MAX";

		private static ILog log = LogManager.GetLogger("StaticConst");

		public static float DISH_OFFSET_Y
		{
			get
			{
				int sceneID = DataCenter.Instance.GetUserData().scene;
				if (ConfigSystem.Instance.TryGet(sceneID, out LevelRowData config))
				{
					return config.DishOffsetY;
				}

				log.Error("not found level id=" + sceneID);
				return 0;
			}
		}

		private static float _FOODTIP_OFFSET_Y = -1001f;
		public static float FOODTIP_OFFSET_Y
		{
			get
			{
				if (_FOODTIP_OFFSET_Y < 1000)
					_FOODTIP_OFFSET_Y = GlobalDesginConfig.GetFloat("food_tip_offsety");

				return _FOODTIP_OFFSET_Y;
			}
		}
		
		private static float _FOODTIP_OFFSET_X = -1001f;
		public static float FOODTIP_OFFSET_X
		{
			get
			{
				if (_FOODTIP_OFFSET_X < 1000)
					_FOODTIP_OFFSET_X = GlobalDesginConfig.GetFloat("food_tip_offsetx");

				return _FOODTIP_OFFSET_X;
			}
		}

		#region 广告

#if AD_ON || USE_AD
		public const bool C_AD_OPEN = true; 
#else
		public const bool C_AD_OPEN = false;
#endif

		#endregion

		// 装备资源路径
		#region Equip

		public const string WEAPON_PATH = "Assets/BuildAsset/Prefabs/Equipments/";

		/// <summary>
		/// 装备升级品质材料数量
		/// </summary>
		public const int EQUIP_UP_QUALITY_MAT_COUNT = 3;

		/// <summary>
		/// 升级材料ID
		/// </summary>
		public const int EQUIP_UPLV_MAT = 2;

		/// <summary>
		/// 升品材料
		/// </summary>
		public const int EQUIP_UPQUALITY_MAT = 100;

		#endregion
	}

	/// <summary>
	/// 静态变量定义
	/// </summary>
	public static partial class StaticDefine
	{
		public static int G_Offline_Time = 0;

		public static bool G_WAIT_VIDEO = false;
		public static bool G_VIDEO_COMPLETE = false;
		/// <summary>
		/// 等待欢迎界面关闭
		/// </summary>
		public static bool G_WAIT_WELCOME = false;

		/// <summary>
		/// 装备等级上限
		/// </summary>
		public static int EQUIP_MAX_LEVEL;

		/// <summary>
		/// 暂停主界面刷新
		/// </summary>
		public static bool PAUSE_MAIN_REFRESH = false;

		public static List<string> CUSTOMER_TAG_BORN = new List<string>();

		public static bool G_IN_VIEW_GET_WORKER = false;
		public static Vector3 G_GET_WORKER_POS	= default;
		public static int G_GET_WORKER_TYPE		= 0;

		public static bool G_IS_LOADING = true;

	}
}
