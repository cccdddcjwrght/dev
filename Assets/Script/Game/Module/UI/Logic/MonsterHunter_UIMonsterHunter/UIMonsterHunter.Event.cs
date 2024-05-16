
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.MonsterHunter;

	public partial class UIMonsterHunter
	{
		private LongPressGesture pressGesture;

		partial void InitEvent(UIContext context)
		{

			pressGesture = new LongPressGesture(m_view.m_panel.m_playbtn)
			{
				trigger = 1f,
				once = true,
			};
			pressGesture.onAction.Add(() =>
			{
				if (m_view != null)
					SwitchAutoPage(1);

			});

			EventManager.Instance.Reg<int>(((int)GameEvent.ACTIVITY_CLOSE), OnActivityClose);


		}

		partial void UnInitEvent(UIContext context)
		{
			EventManager.Instance.UnReg<int>(((int)GameEvent.ACTIVITY_CLOSE), OnActivityClose);

			pressGesture?.Dispose();
		}


		void OnActivityClose(int id)
		{
			if (id == _actID)
			{
				"@ui_activity_hunter_close".Tips();
				DoCloseUIClick(null);
			}
		}
	}
}
