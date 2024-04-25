
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Flight;
	
	public partial class UIFlight
	{
		private ItemGroup m_itemProperty;
		partial void InitLogic(UIContext context){
			m_itemProperty = PropertyManager.Instance.GetGroup(PropertyGroup.ITEM);
		}
		partial void UnInitLogic(UIContext context){

		}


	}
}
