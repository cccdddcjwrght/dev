
using System.Collections;
using Fibers;
using GameConfigs;
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Hud;
	
	public partial class UIFriendTip
	{
		private static float SHOW_TIME = -1.0f;
		private static float HIDE_TIME = -1.0f;
		private Fiber m_fiber = new Fiber(FiberBucket.Manual);
		
		partial void InitEvent(UIContext context){
			if (SHOW_TIME < 0)
				SHOW_TIME = GlobalDesginConfig.GetFloat("firend_showtip_time", 10.0f);
			if (HIDE_TIME < 0)
				HIDE_TIME = GlobalDesginConfig.GetFloat("firend_hidetip_time", 10.0f);
			
			m_view.visible = false;
			m_fiber.Start(RunLogic());
			context.onUpdate += OnUpdate;
		}

		IEnumerator RunLogic()
		{
			while (true)
			{
				// 隐藏泡泡
				m_view.visible = false;
				yield return FiberHelper.Wait(HIDE_TIME);

				// 显示泡泡
				m_view.visible = true;
				m_view.m_style.selectedIndex = RandomSystem.Instance.NextInt(0, m_view.m_style.pageCount);
				yield return FiberHelper.Wait(SHOW_TIME);
			}

			yield return null;
		}

		void OnUpdate(UIContext context)
		{
			m_fiber.Step();
		}
		
		partial void UnInitEvent(UIContext context){

		}
	}
}
