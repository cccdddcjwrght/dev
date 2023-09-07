
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Travel;
	
	public partial class UITravelLeave
	{
		partial void InitLogic(UIContext context)
		{
			var userProperty = PropertyManager.Instance.GetGroup(ItemType.USER);
			
			// 获得出行金币
			var travelGold = userProperty.GetNum((int)UserType.TRAVEL_GOLD);
			var travePower = userProperty.GetNum((int)UserType.TRAVEL_DICE_POWER);
			
			// 显示出行金币
			m_view.m_gold.value = travelGold * travePower;
			m_view.m_gold.max	= travelGold * travePower; //.text = string.Format("{0}X{1}", travelGold, travePower);
		}
		
		partial void UnInitLogic(UIContext context){

		}
	}
}
