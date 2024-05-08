﻿
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
		int maxstate = 5;
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
			m_progressBar.value = 0;
			m_text.text = "ui_loading_tips".Local();
			context.onUpdate += OnUpdate;
		}

		void OnUpdate(UIContext context)
		{
			m_progressBar.value = SceneSystemV2.Instance.progress * 100; // cprogress;
		}

		void OnStateChange(int state)
		{
			/*
			progress = (int)Mathf.Clamp(((float)state / maxstate) * 100, 0, 100);
			if (!startFlag)
			{
				startFlag = true;
				timer = Utils.Timer(9999, () =>
				{
					//var step = progress - cprogress > 20 ? 10 : 5;
					//cprogress = Math.Clamp(cprogress + step, 0, progress);
					m_progressBar.value = SceneSystemV2.Instance.progress * 100;// cprogress;
				}, m_view);
			}
			*/
		}

		partial void UnInitLogic(UIContext context)
		{
			context.onUpdate -= OnUpdate;

			//timer?.Invoke(false);
			//SceneSystemV2.Instance.AddListener(OnStateChange, true, true);

		}
	}
}
