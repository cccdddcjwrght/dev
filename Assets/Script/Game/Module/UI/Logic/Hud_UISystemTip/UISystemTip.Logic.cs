
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Hud;
	
	public partial class UISystemTip
	{
		partial void InitLogic(UIContext context)
		{			
			context.window.AddEventListener("UpdateTip", OnUpdateTip);
			context.content.xy = Vector2.zero;
			if (context.gameWorld.GetEntityManager().HasComponent<UIParam>(context.entity))
			{
				UIParam param = context.gameWorld.GetEntityManager().GetComponentObject<UIParam>(context.entity);
				ShowText(param.Value as string);			
			}
			else
			{
				m_view.visible = false;
			}
		}

		void ShowText(string name)
		{
			if (string.IsNullOrEmpty(name)) return;
			m_view.visible = true;
			m_view.m_title.text = name;
			m_view.m_myfloat.Stop(false, false);
			m_view.m_myfloat.Play();
		}

		void OnUpdateTip(EventContext fcontex)
		{
			ShowText(fcontex.data as string);
		}
		
		partial void UnInitLogic(UIContext context)
		{
		}
	}
}
