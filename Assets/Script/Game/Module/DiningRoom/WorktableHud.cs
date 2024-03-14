using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FairyGUI;
using GameConfigs;
using SGame.Dining;
using SGame.UI;
using Unity.Entities;
using Unity.Mathematics;

namespace SGame
{
	internal class WorktableHud : Singleton<WorktableHud>
	{
		private int _cid;
		private bool _hit = false;
		private Entity _hud;

		public void Init()
		{
			EventManager.Instance.Reg<Dining.Region, int>(((int)GameEvent.WORK_TABLE_CLICK), OnRegionClick);
			ControlAxis.OnAnyKeyInput += ListenClick;

		}

		void ListenClick()
		{
			if (DataCenter.Instance.guideData.isGuide) return;
			if (!_hit) Close();
			_hit = false;
		}

		private void OnRegionClick(Dining.Region region, int type)
		{
			_hit = true;
			var place = type == 1 ? region.next ?? region.begin : region.begin;
			if (_cid != place.cfgID)
			{
				Close();
				_cid = place.cfgID;
				ConfigSystem.Instance.TryGet<RoomMachineRowData>(_cid, out var cfg);
				_hud = UIUtils.ShowHUD("worktable", place.transform, new float3(cfg.HudOffset(0), cfg.HudOffsetLength > 0 ? cfg.HudOffset(1) : 1, cfg.HudOffset(2)));
				_hud.SetParam(new WorktableInfo()
				{
					id = region.cfgID,
					mid = place.cfgID,
					target = place.transform.position,
					type = type
				});
			}
		}


		void Close()
		{
			if (_hud.IsExists())
			{
				UIUtils.CloseUI(_hud);
				UIModule.Instance.GetEntityManager().AddComponent<DespawningEntity>(_hud);
			}
			_cid = 0;
			_hud = default;
		}
	}
}
