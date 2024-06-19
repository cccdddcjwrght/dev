
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Guide;
	
	public partial class UIGuidePiercing
	{
		GuideFingerHandler m_Handler;
		partial void InitLogic(UIContext context){
			context.onUpdate += OnUpdate;
			m_Handler = context.gameWorld.GetEntityManager().GetComponentObject<UIParam>(context.entity).Value as GuideFingerHandler;
		}

		private void OnUpdate(UIContext context)
		{
			var size = m_Handler.GetTargetSize();
			var pos = m_Handler.GetTargetPos();

			m_view.m_blank.xy = pos;
			m_view.m_blank.size = size;
		}

		partial void UnInitLogic(UIContext context){
			context.onUpdate -= OnUpdate;
		}
	}
}
