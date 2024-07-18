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
			EventManager.Instance.Reg<Dining.Build, int>(((int)GameEvent.WORK_TABLE_CLICK), OnRegionClick);
			ControlAxis.OnAnyKeyInput += ListenClick;

		}

		void ListenClick()
		{
			if (!_hit) Close();
			_hit = false;
		}

		private void OnRegionClick(Dining.Build build, int type)
		{
			_hit = true;
			var region = build as Dining.Region;
			if (region != null)
			{
				var place = type == 1 ? region.next ?? region.begin : region.begin;
				if (place != null)
				{
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
			}
			else
			{
				if (_cid == build.cfgID) return;
				_cid = build.cfgID;
				_hud = UIUtils.ShowHUD("worktable", build.holder, new float3(0, 1, 0));
				_hud.SetParam(new WorktableInfo()
				{
					id = build.cfgID,
					target = build.holder.position,
					type = type
				});
			}
		}


		public void Close()
		{
			/*if (_hud != default && _hud.IsExists())
			{
				if (UIUtils.CloseUI(_hud))
					UIModule.Instance.GetEntityManager().AddComponent<DespawningEntity>(_hud);//不延迟
			}*/

			if (GuideManager.Instance.IsCoerce) return;
#if GAME_GUIDE
			UnityEngine.Debug.Log("Worktable close -------------");
#endif

			if (UIUtils.CheckUIIsOpen("worktable"))
				UIUtils.CloseUIByName("worktable");
			_cid = 0;
			_hud = default;
		}
	}
}
