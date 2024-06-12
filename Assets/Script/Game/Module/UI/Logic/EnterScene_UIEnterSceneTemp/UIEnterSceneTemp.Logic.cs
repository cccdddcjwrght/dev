
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
			if (ConfigSystem.Instance.TryGet<RoomRowData>(c, out var cfg))
			{
				if (ConfigSystem.Instance.TryGet<RegionRowData>(cfg.RegionId, out var region))
				{
					_sceneCfgs = ConfigSystem.Instance.Finds<RoomRowData>((c) => c.RegionId == region.ID);
					_sceneCfgs.Reverse();

					m_view.m_list.numItems = _sceneCfgs.Count;
				}
			}
		}

		public void OnItemRenderer(int index, GObject gObject) 
		{
			var cfg = _sceneCfgs[index]; 
			var view = (UI_PassItem)gObject;

			view.SetIcon(cfg.Icon);
			view.m_name.SetTextByKey(cfg.Name);
			view.m_icon.grayed = cfg.ID > DataCenter.Instance.roomData.roomID;

			if (index == 0) view.m_dir.selectedIndex = 2;
			else view.m_dir.selectedIndex = index % 2;

			if (!_isLastScene && cfg.ID == _nextScene) 
			{
				view.m_isMeet.selectedIndex = !_canSwitch ? 1 : 0;
			}
			view.m_btn.onClick.Add(() =>
			{
				SGame.UIUtils.CloseUIByID(__id);
				Dining.DiningRoomSystem.Instance.LoadRoom(_nextScene);
			});
		}

		partial void UnInitLogic(UIContext context){

		}
	}
}
