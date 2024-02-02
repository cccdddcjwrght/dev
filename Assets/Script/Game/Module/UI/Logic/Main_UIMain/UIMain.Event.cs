

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
			var adBtn    = m_view.m_AdBtn;
			var taskBtn  = m_view.m_taskRewardBtn;
			var leftList        = m_view.m_leftList.m_left;
			leftList.itemRenderer += RenderListItem;
			leftList.numItems = 3;
			levelBtn.onClick.Add(OnlevelBtnClick);
			adBtn.onClick.Add(OnadBtnClick);
			taskBtn.onClick.Add(OntaskBtnClick);
			
			m_handles += EventManager.Instance.Reg<int,double>((int)GameEvent.PROPERTY_GOLD,			OnEventGoldChange);
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
		void OnEventGoldChange(int Id,double newValue)
		{
			log.Info("On Gold Update add  newvalue=" + newValue + " id=" + Id);
			m_itemProperty.AddNum(Id, -newValue);
			UpdateItemText();
		}
		

		void UpdateItemText()
		{
			SetGoldText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)ItemID.GOLD)));
			SetDiamondText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)ItemID.DIAMOND)));
		}

		void OnlevelBtnClick(EventContext context)
		{
			Entity popupUI = UIRequest.Create(EntityManager, SGame.UIUtils.GetUI("popup"));
			EntityManager.AddComponentData(popupUI, new UIParam() { Value = 0 });
		}
		
		private void OntaskBtnClick(EventContext context)
		{
			UIRequest.Create(EntityManager, SGame.UIUtils.GetUI("leveltech"));
		}

		private void OnadBtnClick(EventContext context)
		{
			Entity popupUI = UIRequest.Create(EntityManager, SGame.UIUtils.GetUI("popup"));
			EntityManager.AddComponentData(popupUI, new UIParam() { Value =1 });
		}
		
		partial void UnInitEvent(UIContext context){
			m_handles.Close();
		}
		
	}
}
