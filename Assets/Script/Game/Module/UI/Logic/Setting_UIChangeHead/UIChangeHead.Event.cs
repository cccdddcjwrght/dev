
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Setting;
	
	public partial class UIChangeHead
	{
		partial void InitEvent(UIContext context){

		}
		partial void UnInitEvent(UIContext context){

		}

		partial void OnHeadClick(EventContext data)
		{
			OnTabClick(0);
		}

		partial void OnFrameClick(EventContext data)
		{
			OnTabClick(1);
		}

		private void OnTabClick(int index)
		{
			
		}
	}
}
