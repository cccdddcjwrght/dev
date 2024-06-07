
using log4net;
using UnityEngine;
namespace SGame.UI
{
	using SGame;
	using System;
	using System.Collections.Generic;
	using Unity.Entities;
	using GameConfigs;
	using System.Linq;

	/// <summary>
	/// 主界面检测管理类, 管理是否开启, 与倒计时处理
	/// </summary>
	public class CheckingManager// : Singleton<CheckingManager>
	{
		private static ILog log = LogManager.GetLogger("game.mainui");
		public const int LEFT_BAR_ID = 101;
		public const int RIRIGHT_BAR_ID = 102;

		private static bool IS_FIRST_ENTER = true; // 是否是第一次进入界面


		public class CheckItem
		{
			public int funcID;      // 功能ID
			public FunctionConfigRowData config;        // 配置信息
			public Func<bool> funcCanShow;  // 额外判定是否可显示
			public Func<int> funcTime;      // 倒计时
			public int visibaleCount = 0; // 显示次数统计 0 未显示, 1 首次显示, 2多次显示
			public object param;         // 额外参数
			public string Name;
			public Action complete;     //完成回调		
			public int order => config.Order;           // 排序

			public Func<string> getIcon;
			public Func<string> getTooltip;

			public string uiname
			{
				get
				{
					if (string.IsNullOrEmpty(Name))
						return config.Uniqid;

					return Name;
				}
			}

			public string icon
			{
				get
				{
					if (getIcon == null) return config.Icon;
					return getIcon();
				}
			}

			/// <summary>
			/// 判断UI索引是否可显示
			/// </summary>
			/// <param name="index"></param>
			/// <returns></returns>
			public bool IsVisible()
			{
				if (funcCanShow != null)
				{
					var ret = funcCanShow();
					return ret;
				}

				return funcID.IsOpend(false);
			}

			public void Goto()
			{
				config.Uniqid.Goto();
			}

			/// <summary>
			/// 判断是否首次显示
			/// </summary>
			/// <param name="index"></param>
			/// <returns></returns>
			public bool IsFirstVisible => visibaleCount == 1;


			/// <summary>
			/// 开启UI
			/// </summary>
			/// <param name="index"></param>
			public void OpenUI()
			{
				if (!string.IsNullOrEmpty(config.Ui))
				{
					if (param != null)
						SGame.UIUtils.OpenUI(config.Ui, param);
					else
						SGame.UIUtils.OpenUI(config.Ui);
				}
			}

			/// <summary>
			/// 检测登录是否弹框
			/// </summary>
			/// <returns></returns>
			bool CheckLogin()
			{
				if (config.LoginShow <= 0)
					return false;

				if (config.LoginShow == 2) // 每次登录都弹
				{
					return IS_FIRST_ENTER == true;
				}

				if (config.LoginShow == 1) // 每日只弹一次
				{
					if (IS_FIRST_ENTER)
					{
						string key = "FunctionMenu.dayopen_" + funcID;      // 每日弹窗
						return Utils.IsFirstLoginInDay(key);
					}
				}
				return false;
			}

			/// <summary>
			/// 检测是否首次打开
			/// </summary>
			/// <returns></returns>
			bool CheckFirstOpen()
			{
				if (config.FirstOpen <= 0)
					return false;

				// 判断历史上是否开启过
				string key = "FunctionMenu.firstOpen_" + funcID; // 
				if (DataCenter.GetIntValue(key, 0) != 0)
				{
					return false;
				}

				DataCenter.SetIntValue(key, 1);
				return true;
			}

			/// <summary>
			/// UI自动开启功能 
			/// </summary>
			public void AutoOpenUI()
			{
				bool loginOpen = CheckLogin();              // 登录弹窗
				bool checkFirstOpen = CheckFirstOpen();         // 首次开启弹窗
				if (loginOpen || checkFirstOpen)
				{
					if (!string.IsNullOrEmpty(config.Ui))
					{
						if (param != null)
							DelayExcuter.Instance.DelayOpen(config.Ui, "mainui", false, null, param);
						else
							DelayExcuter.Instance.DelayOpen(config.Ui, "mainui");
					}
				}
			}

			public CheckItem SetIcon(Func<string> geticon)
			{
				this.getIcon = geticon;
				return this;
			}

			public CheckItem SetTips(Func<string> getTooltip)
			{
				this.getTooltip = getTooltip;
				return this;
			}

		}
		private Dictionary<int, List<CheckItem>> m_data = new Dictionary<int, List<CheckItem>>();

		public void UpdateState()
		{
			IS_FIRST_ENTER = false;
		}

		/// <summary>
		/// 获取某个栏位的UI
		/// </summary>
		/// <param name="parentID"></param>
		/// <returns></returns>
		public List<CheckItem> GetDatas(int parentID)
		{
			var items = GetOrCreateItem(parentID);
			var ret = new List<CheckItem>();

			// 相同的功能ID, 只存在一个, 比如成长礼包是相同的功能ID
			Dictionary<int, CheckItem> showUI = new Dictionary<int, CheckItem>();

			// 遍历功能, 判断是否达到开启条件
			foreach (var item in items)
			{
				if (item.IsVisible())
				{
					//if (GuideModule.Instance.IsGuideFinsih())
					//{
						if (item.visibaleCount == 0)
						{
							// 首次显示
							showUI.TryAdd(item.funcID, item);
							item.visibaleCount = 1;
						}
						else
						{
							item.visibaleCount = 2;
						}
					//}
					ret.Add(item);
				}
			}

			// 对开启的功能排序
			ret.Sort((a, b) => a.order - b.order);

			/// 首次弹窗
			foreach (var item in showUI.Values)
			{
				item.AutoOpenUI();
			}
			return ret;
		}

		List<CheckItem> GetOrCreateItem(int parentID)
		{
			if (m_data.TryGetValue(parentID, out List<CheckItem> item))
			{
				return item;
			}

			item = new List<CheckItem>();
			m_data.Add(parentID, item);
			return item;
		}

		/// <summary>
		/// 注册信息
		/// </summary>
		/// <param name="index">UI索引</param>
		/// <param name="funcID">功能ID</param>
		/// <param name="canShow">额外判定是否开启</param>
		/// <param name="funcTime">倒计时</param>
		public CheckItem Register(int funcID, Func<bool> canShow = null, Func<int> funcTime = null, object param = null, string uiname = null, Action complete = null)
		{
			if (!ConfigSystem.Instance.TryGet(funcID, out FunctionConfigRowData config))
			{
				log.Error("function id not found=" + funcID);
				return default;
			}

			if (config.Parent == 0)
			{
				log.Error("parent is zero function id=" + funcID);
				return default;
			}

			var item = GetOrCreateItem(config.Parent);
			var data = new CheckItem()
			{
				funcID = funcID,
				config = config,
				funcCanShow = canShow,
				funcTime = funcTime,
				param = param,
				Name = uiname,
				complete = complete,
			};
			item.Add(data);
			return data;
		}

		/// <summary>
		/// 注册活动功能
		/// </summary>
		/// <param name="funcID"></param>
		public void RegisterActFunc(int funcID, FunctionConfigRowData config = default)
		{
			if (!config.IsValid() && !ConfigSystem.Instance.TryGet(funcID, out config))
			{
				log.Error("function id not found=" + funcID);
				return;
			}
			funcID = config.Id;
			if (config.Activity == 0)
			{
				log.Warn("function id not activity=" + funcID);
				return;
			}
			if (config.OpenType == -1)
			{
				log.Warn("function is close=" + funcID);
				return;
			}

			var act = config.Activity;
			var pix = string.IsNullOrEmpty(config.Uniqid) ? "act" : config.Uniqid;
			if (act > 0)
			{
				Register(funcID,
					() => funcID.IsOpend(false) && ActiveTimeSystem.Instance.IsActive(act, GameServerTime.Instance.serverTime),
					() => ActiveTimeSystem.Instance.GetLeftTime(act, GameServerTime.Instance.serverTime),
					act, pix + "_" + act
				);
			}
			else
			{
				var cs = ConfigSystem.Instance.Finds<ActivityTimeRowData>(c => c.Value == -act);
				if (cs?.Count > 0)
				{
					cs.ForEach((c) =>
					{
						Register(funcID,
							() => funcID.IsOpend(showtips: false) && ActiveTimeSystem.Instance.IsActive(c.Id, GameServerTime.Instance.serverTime),
							() => ActiveTimeSystem.Instance.GetLeftTime(c.Id, GameServerTime.Instance.serverTime),
							c.Id, pix + "_" + c.Id
						);
					});
				}
			}

		}

		public void RegisterAllActFunc()
		{
			var all = ConfigSystem.Instance.Finds<FunctionConfigRowData>(c => c.Activity != 0);
			all.ForEach(c => RegisterActFunc(0, c));
		}

	}

	public partial class UIMain
	{
		private UserData m_userData;
		private UIContext m_context;
		private ItemGroup m_itemProperty;


		/// <summary>
		/// 右边栏ICON
		/// </summary>
		private static CheckingManager m_funcManager = null;//new CheckingManager();

		/// <summary>
		/// 初始化右边栏数据
		/// </summary>
		void InitRightItem(int index, int funcID, Func<int> timeCounter)
		{

		}

		partial void InitLogic(UIContext context)
		{
			m_context = context;
			context.onUpdate += onUpdate;

			if (!SGame.UIUtils.IsWaterDotSceen())
			{
				float offset = SGame.UIUtils.GetSafeUIOffset();
				if (offset > 0)
				{
					m_view.m_top.y = offset + 5;
				}		
			}

			m_itemProperty = PropertyManager.Instance.GetGroup(PropertyGroup.ITEM);
			m_userData = DataCenter.Instance.GetUserData();
			m_view.m_Gold.SetText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)ItemID.GOLD)), false);
			m_view.m_Diamond.SetText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)ItemID.DIAMOND)), false);

		}

		EntityManager EntityManager
		{
			get
			{
				return m_context.gameWorld.GetEntityManager();
			}
		}



		private void onUpdate(UIContext context)
		{
			UserSetting setting = DataCenter.Instance.GetUserSetting();
			OnRefreshAdState();
		}

		partial void UnInitLogic(UIContext context)
		{
			context.onUpdate -= onUpdate;
		}

	}
}
