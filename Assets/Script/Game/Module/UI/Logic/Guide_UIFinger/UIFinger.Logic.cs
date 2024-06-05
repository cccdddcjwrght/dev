
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Guide;

    public partial class UIFinger
    {
        GuideFingerHandler m_Handler;
        partial void InitLogic(UIContext context)
        {
            context.onUpdate += OnUpdate;
            m_Handler = context.gameWorld.GetEntityManager().GetComponentObject<UIParam>(context.entity).Value as GuideFingerHandler;
        }

        private void OnUpdate(UIContext context)
        {
            m_view.m_Finger.xy = m_Handler.GetTargetPos();
        }
        partial void UnInitLogic(UIContext context)
        {
            context.onUpdate -= OnUpdate;
        }
    }

}
