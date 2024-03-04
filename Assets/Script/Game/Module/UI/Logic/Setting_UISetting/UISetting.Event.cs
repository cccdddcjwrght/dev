
using Unity.Entities;

namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Setting;
	
	public partial class UISetting
	{
		partial void InitEvent(UIContext context){
		         

		}
		partial void UnInitEvent(UIContext context){

		}

		partial void OnHeadClick(EventContext data)
		{
			Entity headUI = UIRequest.Create(EntityManager, SGame.UIUtils.GetUI("changehead"));
		}

		partial void OnNameClick(EventContext data)
		{
			Entity nameUI = UIRequest.Create(EntityManager, SGame.UIUtils.GetUI("changename"));
		}
		
	}
}
