
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Hud;
	
	public partial class UIHud
	{
		partial void InitLogic(UIContext context)
		{
			context.content.touchable = true;
		}
		
		partial void UnInitLogic(UIContext context){

		}
	}
}
