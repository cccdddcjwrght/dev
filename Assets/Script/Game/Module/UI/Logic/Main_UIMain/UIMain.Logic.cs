
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Main;
	using Fibers;
	using System;
	using System.Collections;
	using Unity.Entities;
	using Unity.Mathematics;
	using Unity.Transforms;
	
	public partial class UIMain
	{
		private UserData         m_userData;
		private UIContext        m_context;
		private ItemGroup        m_itemProperty;

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
