
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	
	public partial class UIFightLose
	{
		partial void InitLogic(UIContext context){

		}

        partial void OnConfirmBtnClick(EventContext data)
        {
			SGame.UIUtils.CloseUIByID(__id);
        }

        partial void UnInitLogic(UIContext context){

		}
	}
}
