using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FairyGUI;
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
			EventManager.Instance.Reg<Region, int>(((int)GameEvent.WORK_TABLE_CLICK), OnRegionClick);
			ControlAxis.OnAnyKeyInput += ListenClick;

		}

		void ListenClick()
		{
			if (!_hit) Close();
			_hit = false;
		}

		private void OnRegionClick(Region region, int type)
		{
			_hit = true;
			var place = type == 1 ? region.next ?? region.begin : region.begin;
			if (_cid != place.cfgID)
			{
				Close();
				_cid = place.cfgID;
				_hud = UIUtils.ShowHUD("worktable", place.transform, new float3(0,3,0));
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
