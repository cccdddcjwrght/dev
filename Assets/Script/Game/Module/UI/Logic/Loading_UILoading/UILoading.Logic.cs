
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Loading;
	using System.Collections;
	using Fibers;
	
	public partial class UILoading
	{
		// 协程对象
		private Fiber			m_fiber;

		// 显示时间
		private float			m_waitTime;

		private GProgressBar	m_progressBar;

		private GTextField		m_text;
        
		partial void InitLogic(UIContext context)
		{
			m_progressBar = context.content.GetChild("n3").asProgress;
			m_text = context.content.GetChild("n4").asTextField;
			context.onUpdate += onUpdate;
			m_fiber = new Fiber(RunLogic(context));

			var param = context.GetParam();
			if(param!=null )
			{
				// 获得参数
				m_waitTime = (float)param.Value;
			}
			m_waitTime = Mathf.Max(1,m_waitTime);

		}

		IEnumerator RunLogic(UIContext context)
		{
			// 播放动画
			float run = 0;
			m_progressBar.min = 0;
			m_progressBar.max = m_waitTime;
			while (run <= m_waitTime)
			{
				run += Time.deltaTime;
				float per = Mathf.Clamp01(run / m_waitTime);
				m_text.text = "ui_loading_tips".Local();
				//string.Format("LOADING ... {0:0.00}%", per * 100);
				m_progressBar.value = run;
				yield return null;
			}
			m_progressBar.value = m_waitTime;

			EventManager.Instance.Trigger((int)GameEvent.ENTER_GAME);
		}

		void onUpdate(UIContext context)
		{
			m_fiber.Step();
		}
		
		partial void UnInitLogic(UIContext context){

		}
	}
}
