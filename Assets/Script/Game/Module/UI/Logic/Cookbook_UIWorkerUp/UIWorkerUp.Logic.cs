
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Cookbook;

	public partial class UIWorkerUp
	{
		private WorkerDataItem _data;


		partial void InitLogic(UIContext context)
		{
			_data = context.GetParam()?.Value.To<object[]>().Val<WorkerDataItem>(0);
			if (_data == null) { DoCloseUIClick(null); return; }
			m_view.SetWorkerInfo(_data, usedefaultval: _data.level <= 0);
			SetInfo();
		}

		void SetInfo()
		{
			EffectSystem.Instance.ReleaseEffect(m_view);
			switch (m_view.m_state.selectedIndex)
			{
				case 2:
					EffectSystem.Instance.AddEffect(45, m_view);
					break;
			}
			m_view.m_click.SetTextByKey(_data.IsSelected() ? "ui_worker_usebtn_1" : "ui_worker_usebtn_2");
		}

		partial void OnRewardClick(EventContext data)
		{
			if (DataCenter.WorkerDataUtils.GetReward(_data))
			{
				SetInfo();
				SwitchStatePage(1);
			}
		}

		partial void OnClickClick(EventContext data)
		{
			if (_data.IsSelected()) return;
			if (DataCenter.WorkerDataUtils.Select(_data))
			{
				DoCloseUIClick(null);
				//SwitchSelectedPage(1);
			}
		}

		partial void UnInitLogic(UIContext context)
		{
			EffectSystem.Instance.ReleaseEffect(m_view);
			_data = null;
		}
	}
}
