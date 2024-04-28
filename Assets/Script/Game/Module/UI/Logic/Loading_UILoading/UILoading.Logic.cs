
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Loading;
	using System.Collections;
	using Fibers;
	using System;

	public partial class UILoading
	{
		int maxstate = 6;
		bool startFlag = false;
		int cprogress = 0;
		int progress;
		Action<bool> timer;

		// 协程对象
		private Fiber m_fiber;

		// 显示时间
		private float m_waitTime;

		private GProgressBar m_progressBar;

		private GTextField m_text;

		partial void InitLogic(UIContext context)
		{
			UI.UIUtils.SetLogo(m_view);

			m_progressBar = context.content.GetChild("n3").asProgress;
			m_text = context.content.GetChild("n4").asTextField;

			m_progressBar.min = 0;
			m_progressBar.max = 100;
			m_text.text = "ui_loading_tips".Local();
			SceneSystemV2.Instance.AddListener(OnStateChange, false, true);


		}

		void OnStateChange(int state)
		{
			progress = (int)Mathf.Clamp(((float)state / maxstate) * 100, 0, 100);
			if (!startFlag)
			{
				startFlag = true;
				timer = Utils.Timer(9999, () =>
				{
					if (!StaticDefine.G_WAIT_VIDEO)
					{
						var step = progress - cprogress > 20 ? 10 : 5;
						cprogress = Math.Clamp(cprogress + step, 0, progress);
					}
					else cprogress = 100;
					m_progressBar.value = cprogress;
				}, m_view);
			}
		}

		partial void UnInitLogic(UIContext context)
		{
			timer?.Invoke(false);
			SceneSystemV2.Instance.AddListener(OnStateChange, true, true);

		}
	}
}
