
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.PiggyBank;

	public partial class UIPiggyBank
	{
		private EventHandleContainer m_handles = new EventHandleContainer();
		partial void InitEvent(UIContext context) {
			m_handles += EventManager.Instance.Reg<int>(((int)GameEvent.SHOP_GOODS_BUY_RESULT), OnPiggyBankBuyResult);
			//m_handles += EventManager.Instance.Reg((int)GameEvent.PIGGYBANK_UPDATE, RefreshAll);
		}

		void OnPiggyBankBuyResult(int shopId) 
		{
			if (shopId == DataCenter.PiggyBankUtils.PIGGYITEM_ID)
				DataCenter.PiggyBankUtils.GainPiggyBankResult();
		}


		partial void UnInitEvent(UIContext context){
			m_handles.Close();
			m_handles = null;
		}
	}
}
