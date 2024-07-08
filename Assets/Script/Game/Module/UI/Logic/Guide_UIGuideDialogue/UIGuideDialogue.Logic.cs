
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Guide;
	
	public partial class UIGuideDialogue
	{
		GuideFingerHandler m_Handler;
		partial void InitLogic(UIContext context){
			m_Handler = context.gameWorld.GetEntityManager().GetComponentObject<UIParam>(context.entity).Value as GuideFingerHandler;
			m_view.m_icon.SetIcon(m_Handler.config.Icon);
			m_view.m_dialogue.SetText(UIListener.Local(m_Handler.config.StringParam));

			float x1 = m_Handler.config.FloatParam(0);
			float y1 = m_Handler.config.FloatParam(1);
			float x2 = m_Handler.config.FloatParam(2);
			float y2 = m_Handler.config.FloatParam(3);

			float wr = GRoot.inst.width / 750f - 1;
			float hr = GRoot.inst.height / 1334f - 1;

			float ww = Mathf.Abs(GRoot.inst.width - 750f) * wr;
			float hh = Mathf.Abs(GRoot.inst.height - 1334f) * hr;

			if(x1 > 0 && y1 > 0) m_view.m_icon.xy = new Vector2(x1 * (1 + wr) + ww, y1 * (1 + hr) - hh);
			if(x2 > 0 && y2 > 0) m_view.m_dialogue.xy = new Vector2(x2 * (1 + wr) + ww, y2 * (1 + hr) - hh);

			//设置对话框宽度
			if(m_Handler.config.Width > 0) m_view.m_dialogue.width = m_Handler.config.Width;
			if (m_Handler.config.Height > 0) m_view.m_dialogue.height = m_Handler.config.Height;

			if (m_Handler.config.Force == 1)//弱指引 需要点击关闭
				m_view.m_mask.visible = true;
		}
		partial void UnInitLogic(UIContext context){

		}
	}
}
