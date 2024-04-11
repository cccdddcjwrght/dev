
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.TomorrowGift;
	using SGame.UI.Shop;
	
	public partial class UINewbieGift
	{
		UI_Probability probabilityUI
		{
			get { return m_view.m_probablity as UI_Probability; }
		}
		
		partial void InitLogic(UIContext context){
			probabilityUI.m_bg.onClick.Add(OnRateClose);
			probabilityUI.m_bg.GetChild("close").onClick.Add(OnRateClose);
		}
		partial void UnInitLogic(UIContext context){

		}
		
		void OnRateClose()
		{
			probabilityUI.m_show.selectedIndex = 0;
		}
	}
}
