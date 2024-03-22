
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Buff;
	
	public partial class UILevelTech
	{
		partial void InitLogic(UIContext context){


		}

		partial void UnInitLogic(UIContext context){

		}

		partial void OnUICloseClick(ref bool state)
		{
			if (DataCenter.Instance.guideData.isGuide)
			{
				state = false;
			}
		}
	}
}
