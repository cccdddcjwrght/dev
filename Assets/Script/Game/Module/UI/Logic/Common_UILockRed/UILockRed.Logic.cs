
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Common;
	using System.Linq;
	using SGame.UI.Worktable;
	using Unity.Entities;
	using GameConfigs;

	public partial class UILockRed
	{
		private int area;
		private GameObject target;
		private GameConfigs.RedConfigRowData redCfg;
		private UI_LockPanelUI panel;
		private GameConfigs.RoomAreaRowData cfg;

		private EventHandleContainer eHandler = new EventHandleContainer();

		partial void InitLogic(UIContext context)
		{
			var args = context.GetParam().Value.To<object[]>();
			target = args.Val<GameObject>(0);
			redCfg = args.Val<GameConfigs.RedConfigRowData>(1);
			int.TryParse(target.name.Split('_').Last(), out area);
			ConfigSystem.Instance.TryGet<RoomAreaRowData>(area, out cfg);

			eHandler += EventManager.Instance.Reg<double, double>(((int)GameEvent.PROPERTY_GOLD_CHANGE), OnGoldRefresh);
			eHandler += EventManager.Instance.Reg<int>(((int)GameEvent.WORK_AREA_UNLOCK), a => RefreshInfo());
			eHandler += EventManager.Instance.Reg((int)GameEvent.GUIDE_START, RefreshInfo);
			eHandler += EventManager.Instance.Reg((int)GameEvent.RELOAD_ALL_UI, UpdateText);

			var sys = World.DefaultGameObjectInjectionWorld.GetExistingSystem<SpawnUISystem>();
			sys.LoadPackage("Worktable").Wait(s => RefreshInfo());
		}

		private void RefreshInfo()
		{
			var state = DataCenter.Instance.roomData.current.GetAreaType(area);
			if (state == 0)
			{
				if (panel == null)
				{
					m_view.m_child.icon = "ui://Worktable/LockPanelUI";
					panel = m_view.m_child.component as UI_LockPanelUI;
				}
#if GAME_GUIDE
				if (Game.Instance.enableGuide) 
				{
					//等对应任务完成后显示
					m_view.m_child.visible = DataCenter.Instance.taskMainData.cfgId > GlobalDesginConfig.GetInt("guide_are_taskId");
				}
#endif
				SetUnlockBtn(cfg.GetCostArray());
				m_view.m_flag.selectedIndex = 1;
			}
			else
			{
				m_view.m_flag.selectedIndex = 0;
				panel?.Dispose();
				panel = null;
				m_view.m_child.url = null;
			}
		}

		//更新文本
		void UpdateText() 
		{
			panel?.Dispose();
			panel = null;
			m_view.m_child.url = null;
			RefreshInfo();
		}

		private void OnGoldRefresh(double v1, double v2)
		{
			if (panel != null && cfg.IsValid())
				SetUnlockBtn(cfg.GetCostArray());
		}

		private void SetUnlockBtn(float[] cost)
		{
			if (panel == null) return;
			var state = PropertyManager.Instance.CheckCountByArgs(cost);
			panel.m_unlock.SetTextByKey("ui_unlock_tips");
			panel.m_btnty.selectedIndex = state ? 0 : 1;
			panel.m_click.touchable = state;
			UIListener.SetText(panel.m_click, SGame.Utils.ConvertNumberStr(cost[2]));
			UIListener.SetControllerSelect(panel.m_click, "limit", 0);
			UIListener.SetControllerSelect(panel.m_click, "gray", state ? 0 : 1);

			if (!state) panel.m_click.GetChild("bg").icon = null;
			panel.m_click.onClick?.Clear();
			panel.m_click.onClick.Add(Unlock);
		}

		private void Unlock()
		{
			RequestExcuteSystem.UnlockArea(area);
			SGame.UIUtils.CloseUIByID(__id);
		}

		partial void UnInitLogic(UIContext context)
		{
			eHandler?.Close();
			eHandler = null;
		}
	}
}
