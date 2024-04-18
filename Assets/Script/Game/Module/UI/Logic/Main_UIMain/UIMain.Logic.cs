
using log4net;
using UnityEngine;
namespace SGame.UI{
	using SGame;
	using System;
	using System.Collections.Generic;
	using Unity.Entities;
	using GameConfigs;

	/// <summary>
	/// 主界面检测管理类, 管理是否开启, 与倒计时处理
	/// </summary>
	public class CheckingManager// : Singleton<CheckingManager>
	{
		private static ILog log = LogManager.GetLogger("game.mainui");
		public const int LEFT_BAR_ID	= 101;
		public const int RIRIGHT_BAR_ID = 102;
		
		public class CheckItem
		{
			public int					 funcID;		// 功能ID
			public FunctionConfigRowData config;		// 配置信息
			public Func<bool>			 funcCanShow;	// 额外判定是否可显示
			public Func<int>		     funcTime;		// 倒计时
			public int                   visibaleCount = 0; // 显示次数统计 0 未显示, 1 首次显示, 2多次显示
			public object				 param;         // 额外参数
			public int order => config.Order;			// 排序
			
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
			/// 首次打开触发功能开启UI
			/// </summary>
			public void FuncOpenUI()
			{
				/*
				if (config.FirstOpen > 0)
					OpenUI();
					*/
			}
		}
		private Dictionary<int, List<CheckItem>> m_data = new Dictionary<int, List<CheckItem>>();

		/// <summary>
		/// 获取某个栏位的UI
		/// </summary>
		/// <param name="parentID"></param>
		/// <returns></returns>
		public List<CheckItem> GetDatas(int parentID)
		{
			var items = GetOrCreateItem(parentID);
			var ret = new List<CheckItem>();
			Dictionary<int, CheckItem> showUI = new Dictionary<int, CheckItem>();
			foreach (var item in items)
			{
				if (item.IsVisible())
				{
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
					ret.Add(item);
				}
			}

			ret.Sort((a, b) => a.order - b.order);

			/// 首次弹窗
			foreach (var item in showUI.Values)
			{
				item.FuncOpenUI();
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
		public void Register(int funcID, Func<bool> canShow = null, Func<int> funcTime = null, object param = null)
		{
			if (!ConfigSystem.Instance.TryGet(funcID, out FunctionConfigRowData config))
			{
				log.Error("function id not found=" + funcID);
				return;
			}

			if (config.Parent == 0)
			{
				log.Error("parent is zero function id=" + funcID);
				return;
			}

			var item = GetOrCreateItem(config.Parent);
			item.Add(new CheckItem()
			{
				funcID		= funcID,
				config		= config,
				funcCanShow = canShow,
				funcTime	= funcTime,
				param		= param,
			});
		}
	}

	public partial class UIMain
	{
		private UserData         m_userData;
		private UIContext        m_context;
		private ItemGroup        m_itemProperty;


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
		
		partial void InitLogic(UIContext context){
			m_context			= context;
			context.onUpdate	+= onUpdate;
			float offset = SGame.UIUtils.GetSafeUIOffset();
			if (offset > 0)
			{
				m_view.m_top.y = offset+5 ; 
			}
			
			m_itemProperty		= PropertyManager.Instance.GetGroup(PropertyGroup.ITEM);
			m_userData			= DataCenter.Instance.GetUserData();
			SetGoldText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)ItemID.GOLD)));
			SetDiamondText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)ItemID.DIAMOND)));
			//科技数据初始化
			DataCenter.Instance.abilityData.InitAbilityList(); 
		}

		EntityManager EntityManager
		{
			get
			{
				return m_context.gameWorld.GetEntityManager();
			}
		}
		
		
		
		private  void onUpdate(UIContext context)
		{
			UserSetting setting = DataCenter.Instance.GetUserSetting();
			
		}
		
		partial void UnInitLogic(UIContext context)
		{
			context.onUpdate -= onUpdate;
		}
		
	}
}
