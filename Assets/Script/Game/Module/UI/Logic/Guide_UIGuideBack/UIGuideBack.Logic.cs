
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Guide;
	
	public partial class UIGuideBack
	{
		GuideFingerHandler m_Handler;
		partial void InitLogic(UIContext context){
			context.onUpdate += OnUpdate;
			m_Handler = context.gameWorld.GetEntityManager().GetComponentObject<UIParam>(context.entity).Value as GuideFingerHandler;
		}

		private void OnUpdate(UIContext context) 
		{
			m_view.m_blank.xy = m_Handler.GetTargetPos();
			m_view.m_blank.size = m_Handler.GetTargetSize();
		}
		partial void UnInitLogic(UIContext context){
			context.onUpdate -= OnUpdate;
		}
	}
}
