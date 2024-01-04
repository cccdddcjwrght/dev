

using GameConfigs;
using Unity.Entities;

namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Main;
	using Unity.Mathematics;
	
	public partial class UIMain
	{
		private EventHandleContainer m_handles = new EventHandleContainer();
		
		partial void InitEvent(UIContext context)
		{
			var levelBtn = m_view.m_levelBtn;
			levelBtn.onClick.Add(OnlevelBtnClick);

			m_handles += EventManager.Instance.Reg<int,long,int>((int)GameEvent.PROPERTY_GOLD,			OnEventGoldChange);
		}
		

		partial void UnInitEvent(UIContext context){

		}

		// 金币添加事件
		void OnEventGoldChange(int value, long newValue, int playerId)
		{
			log.Info("On Gold Update add =" + value + " newvalue=" + newValue + " plyaerid=" + playerId);
			SetGoldText(newValue.ToString());
		}
		

		void UpdateGoldText()
		{
			SetGoldText(m_userProperty.GetNum((int)UserType.GOLD).ToString());
		}

		void OnlevelBtnClick(EventContext context)
		{
			Entity mainUI = UIRequest.Create(EntityManager, SGame.UIUtils.GetUI("popup"));
		}
		
		void UnInitEvent()
		{
			m_handles.Close();
		}
	}
}
