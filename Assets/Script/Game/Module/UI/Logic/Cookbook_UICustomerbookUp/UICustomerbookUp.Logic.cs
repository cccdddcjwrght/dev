
using Unity.Entities;
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Cookbook;
	
	public partial class UICustomerbookUp
	{
		private CustomerBookData m_data;
		partial void InitLogic(UIContext context)
		{
			var mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
			m_data = mgr.GetComponentObject<UIParam>(context.entity).Value as CustomerBookData;

			m_view.m_pros.itemRenderer = OnRenderFoodItems;
			m_view.m_pros.numItems = m_data.FoodLength;

			m_view.m_customer.m_body.icon = m_data.Icon;
			m_view.m_body.title = LanagueSystem.Instance.GetValue(m_data.Config.Name);

			m_view.m_take_reward.selectedIndex = m_data.isRewarded ? 1 : 0;
			m_view.m_click.onClick.Set(OnClickReward);
		}

		void OnClickReward()
		{
			CustomerBookModule.Instance.TakeReward(m_data);
			m_view.m_take_reward.selectedIndex = m_data.isRewarded ? 1 : 0;
		}

		void OnRenderFoodItems(int index, GObject gObject)
		{
			int itemID = m_data.FoodID(index);
			gObject.SetIcon(Utils.GetItemIcon(1, itemID));
		}
		
		partial void UnInitLogic(UIContext context){

		}
	}
}
