
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Worktable;
	using GameConfigs;

	public partial class UIWorktablePanel
	{
		private LongPressGesture _press;
		private float ct;
		private float it;
		private float minit;
		private bool pressFlag;

		partial void InitEvent(UIContext context)
		{

			ct = GlobalDesginConfig.GetFloat("worktable_longpress_check_time");
			it = GlobalDesginConfig.GetFloat("worktable_longpress_interval");
			minit = GlobalDesginConfig.GetFloat("worktable_longpress_interval_min");

			ct = ct > 0 ? ct : 0.5f;
			it = it > 0 ? it : 0.2f;
			minit = minit > 0 ? minit : 0.05f;

			m_view.SetPivot(0.5f, 1f, true);
			if (info.id > 0 && info.type == 2)
			{
				_press = new LongPressGesture(m_view.m_click)
				{
					once = false,
					interval = it,
					trigger = ct
				};
				_press.onBegin.Add(OnBegin);
				_press.onEnd.Add(() => pressFlag = false);
			}

			GRoot.inst.onClick.Add(OnOtherUIClick);

			EventManager.Instance.Reg<double, double>(((int)GameEvent.PROPERTY_GOLD_CHANGE), OnGoldChange);

		}

		partial void UnInitEvent(UIContext context)
		{
			GRoot.inst.onClick.Remove(OnOtherUIClick);

			EventManager.Instance.UnReg<double, double>(((int)GameEvent.PROPERTY_GOLD_CHANGE), OnGoldChange);
		}

		void OnBegin()
		{
			pressFlag = true;

			System.Collections.IEnumerator Run()
			{
				var interval = it;
				while (pressFlag)
				{
					OnClickClick(null);
					yield return new WaitForSeconds(interval);
					if (interval >= minit)
						interval -= (it - minit) * 0.1f;
				}
			}

			Run().Start();
		}

		void OnGoldChange(double val, double change)
		{
			RefreshClick();
		}

		void OnOtherUIClick(EventContext data)
		{
			if (GRoot.inst.focus != null && !SGame.UIUtils.IsChild(m_view, GRoot.inst.focus))
			{
				if (DataCenter.Instance.guideData.isGuide) return;
				SGame.UIUtils.CloseUIByID(__id);
			}
		}
	}
}
