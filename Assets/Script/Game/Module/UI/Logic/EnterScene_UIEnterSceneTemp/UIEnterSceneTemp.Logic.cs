
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.EnterScene;
	using GameConfigs;
    using System.Collections.Generic;

    public partial class UIEnterSceneTemp
	{
		private List<GameConfigs.RoomRowData> _sceneCfgs;
		int _curScene = 0;
		int _nextScene = 0;

		bool _canSwitch;
		bool _isLastScene;

		partial void InitLogic(UIContext context){

			m_view.m_list.itemRenderer = OnItemRenderer;

			InitData();
			RefreshLevelList();

			m_view.m_list.ScrollToView(_sceneCfgs.FindIndex(v => v.ID == _curScene) + 1);
		}

		public void InitData() 
		{
			_curScene = DataCenter.Instance.roomData.roomID;
			_nextScene = _curScene + 1;

			_isLastScene = !ConfigSystem.Instance.TryGet<RoomRowData>(_nextScene, out _);
			if (!_isLastScene) _canSwitch = DataCenter.MachineUtil.CheckAllWorktableIsMaxLv();
		}

		public void RefreshLevelList() 
		{
			var c = DataCenter.Instance.roomData.roomID;

			var ws = DataCenter.MachineUtil.GetWorktables((w) => !w.isTable);
			int maxLevel = 0;
			int curLevel = 0; 
			if (ws?.Count > 0) ws.ForEach(w => 
			{
				curLevel += w.level;
				maxLevel += w.maxlv;
			});
			m_view.m_bar.fillAmount = (float)curLevel / maxLevel;

			if (ConfigSystem.Instance.TryGet<RoomRowData>(c, out var cfg))
			{
				if (ConfigSystem.Instance.TryGet<RegionRowData>(cfg.RegionId, out var region))
				{
					_sceneCfgs = ConfigSystem.Instance.Finds<RoomRowData>((c) => c.RegionId == region.ID);
					_sceneCfgs.Reverse();

					m_view.m_tip.SetTextByKey("ui_enterscene_tips_1", cfg.LevelMax);
					m_view.m_list.numItems = _sceneCfgs.Count + 1;
				}
			}
		}

		public void OnItemRenderer(int index, GObject gObject)
		{
			var view = (UI_PassItem)gObject;
			view.m_dir.selectedIndex = index % 2;

			if (index == 0)
			{
				view.m_max.selectedIndex = 1;
				return;
			}
			view.m_max.selectedIndex = 0;
			var cfg = _sceneCfgs[index - 1];
			view.SetIcon(cfg.Icon);
			view.m_name.SetTextByKey(cfg.Name);

			view.m_leftBar.fillAmount = 0;
			view.m_rightBar.fillAmount = 0;
			if (_curScene == cfg.ID) 
			{
				if (_canSwitch) view.m_t4.Play();
				view.m_isMeet.selectedIndex = _canSwitch ? 1 : 0;
			}
			else if (_curScene > cfg.ID) 
			{ 
				view.m_isMeet.selectedIndex = 1;
				view.m_leftBar.fillAmount = 1;
				view.m_rightBar.fillAmount = 1;
			}

			if (!_isLastScene && cfg.ID == _nextScene) 
			{ 
				view.m_isMeet.selectedIndex = _canSwitch ? 2 : 3;
				if (_canSwitch) 
				{
					if (view.m_dir.selectedIndex == 0) view.m_t2.Play(1, 0.45f ,null);
					else view.m_t3.Play(1, 0.45f, null);
				}
			}
			else if (cfg.ID > _nextScene) view.m_isMeet.selectedIndex = 3;

			view.onClick.Set(() =>
			{
				if (view.m_isMeet.selectedIndex != 2) return;

				UILockManager.Instance.Require("enterScene");

				//提前设置当前场景id
				DataCenter.Instance.roomData.roomID = _nextScene;
				//记录关卡完成数量
				EventManager.Instance.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RecordDataEnum.LEVEL, 1);

				var lastObj = (UI_PassItem)m_view.m_list.GetChildAt(index + 1);
				m_view.m_list.ScrollToView(index + 1);
				if (lastObj.m_dir.selectedIndex == 0) lastObj.m_right.Play(LoadNextScene);
				else lastObj.m_left.Play(LoadNextScene);

				GTween.To(0, 1, lastObj.m_right.totalDuration - 0.2f).OnUpdate((t) =>
				{
					lastObj.m_rightBar.fillAmount = (float)t.value.d;
					lastObj.m_leftBar.fillAmount = (float)t.value.d;
				});
			});
		}

		void LoadNextScene() 
		{
			UILockManager.Instance.Release("enterScene");

			SGame.UIUtils.CloseUIByID(__id);
			Dining.DiningRoomSystem.Instance.LoadRoom(_nextScene);
		}

		partial void UnInitLogic(UIContext context){

		}
	}
}
