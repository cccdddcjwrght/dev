
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

			m_itemProperty		= PropertyManager.Instance.GetGroup(PropertyGroup.ITEM);
			m_userData			= DataCenter.Instance.GetUserData();
			SetGoldText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)ItemID.GOLD)));
			SetDiamondText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)ItemID.DIAMOND)));
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
