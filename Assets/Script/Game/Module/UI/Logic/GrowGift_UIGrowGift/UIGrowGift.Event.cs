
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.GrowGift;
	
	public partial class UIGrowGift
	{
		private int m_goodsID = 0;
		private GrowGiftData m_datas;
		partial void InitEvent(UIContext context)
		{
			var entityManager = context.gameWorld.GetEntityManager();
			var uiParam = entityManager.GetComponentObject<UIParam>(context.entity);

			var myparam = uiParam.Value as object[];
			int index = (int)myparam[0];


			m_goodsID = GrowGiftModule.Instance.GetActiveGoodID(index);
			m_datas = GrowGiftModule.Instance.GetGiftData(m_goodsID);

			m_view.m_listRewards.itemRenderer = OnRenderRewards;
			m_view.m_listRewards.numItems = m_datas.m_rewards.Count;
			log.Error("open ui param =" + index + " goodsID=" + m_goodsID);
		}

		void OnRenderRewards(int index, GObject gObject)
		{
			
		}
		
		partial void UnInitEvent(UIContext context){

		}
	}
}
