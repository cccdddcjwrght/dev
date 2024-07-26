
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

			float x1 = m_Handler.config.FloatParam(0) / 750f;
			float y1 = m_Handler.config.FloatParam(1) / 1334f;
			float x2 = m_Handler.config.FloatParam(2) / 750f;
			float y2 = m_Handler.config.FloatParam(3) / 1334f;

			x1 = x1 * GRoot.inst.width;
			y1 = y1 * GRoot.inst.height;

			x2 = x2 * GRoot.inst.width;
			y2 = y1 + (m_Handler.config.FloatParam(3) - m_Handler.config.FloatParam(1));
			//y2 = y2 * GRoot.inst.height;
			//float wr = GRoot.inst.width / 750f ;
			//float hr = GRoot.inst.height / 1334f ;

			//float ww = (750 * wr) - 750;
			//float hh = (1334 * hr) - 1334;

			if (x1 > 0 && y1 > 0) m_view.m_icon.xy = new Vector2(x1, y1);
			if(x2 > 0 && y2 > 0) m_view.m_dialogue.xy = new Vector2(x2, y2);

			//设置对话框宽度
			if(m_Handler.config.Width > 0) m_view.m_dialogue.width = m_Handler.config.Width;
			if (m_Handler.config.Height > 0) m_view.m_dialogue.height = m_Handler.config.Height;

			m_view.m_mask.onClick.Clear();

			if (m_Handler.config.Force == 1)//弱指引 需要点击关闭
				m_view.m_mask.visible = true;
		}
		partial void UnInitLogic(UIContext context){

		}
	}
}
