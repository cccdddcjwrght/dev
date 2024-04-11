
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
		
		public class CheckItem
		{
			public int					 funcID;		// 功能ID
			public FunctionConfigRowData config;		// 配置信息
			public Func<bool>			 funcCanShow;	// 额外判定是否可显示
			public Func<int>		     funcTime;		// 倒计时
		}
		private Dictionary<int, CheckItem> m_data = new Dictionary<int, CheckItem>();

		/// <summary>
		/// 注册信息
		/// </summary>
		/// <param name="index">UI索引</param>
		/// <param name="funcID">功能ID</param>
		/// <param name="canShow">额外判定是否开启</param>
		/// <param name="funcTime">倒计时</param>
		public void Register(int index, int funcID, Func<bool> canShow = null, Func<int> funcTime = null)
		{
			if (m_data.ContainsKey(index))
			{
				log.Error("repeate mainui key=" + index);
				return;
			}

			if (!ConfigSystem.Instance.TryGet(funcID, out FunctionConfigRowData config))
			{
				log.Error("function id not found=" + funcID);
				return;
			}
			
			m_data.Add(index, new CheckItem()
			{
				funcID = funcID,
				config = config,
				funcCanShow = canShow,
				funcTime =  funcTime
			});
		}

		/// <summary>
		/// 获得数据
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public CheckItem GetData(int index)
		{
			if (m_data.TryGetValue(index, out CheckItem item))
				return item;
			
			return null;
		}

		/// <summary>
		/// 判断UI索引是否可显示
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public bool IsVisible(int index)
		{
			var item = GetData(index);
			if (item == null)
				return false;

			if (!item.funcID.IsOpend(false))
				return false;

			if (item.funcCanShow != null)
				return item.funcCanShow();

			return true;
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
		private CheckingManager m_rightIcons = new CheckingManager();

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
