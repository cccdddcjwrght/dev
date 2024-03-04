﻿

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
			var leftList        = m_view.m_leftList.m_left;
			var headBtn  = m_view.m_head;
			leftList.itemRenderer += RenderListItem;
			leftList.numItems = 3;
			headBtn.onClick.Add(OnheadBtnClick);

			m_handles += EventManager.Instance.Reg((int)GameEvent.PROPERTY_GOLD,OnEventGoldChange);
		}

		private void RenderListItem(int index, GObject item)
		{
			item.onClick.Add(() =>
			{
				if (index == 1)
				{
					Debug.Log("点击了"+item);
					Entity techUI = UIRequest.Create(EntityManager, SGame.UIUtils.GetUI("technology"));
				}
			});
		}


		// 金币添加事件
		void OnEventGoldChange()
		{
			SetGoldText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)ItemID.GOLD)));
			SetDiamondText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)ItemID.DIAMOND)));
		}
		

		void UpdateItemText()
		{
			
		}

		void OnheadBtnClick(EventContext context)
		{
			Entity popupUI = UIRequest.Create(EntityManager, SGame.UIUtils.GetUI("setting"));
		}

		partial void UnInitEvent(UIContext context){
			m_handles.Close();
		}

		partial void OnDiamondClick(EventContext data)
		{
			"shop".Goto();
		}

	}
}
