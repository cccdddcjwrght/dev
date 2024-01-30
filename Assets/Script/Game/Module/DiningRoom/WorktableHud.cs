using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGame.Dining;
using SGame.UI;
using Unity.Entities;
using Unity.Mathematics;

namespace SGame
{

	internal class WorktableHud : Singleton<WorktableHud>
	{
		public void Init()
		{
			EventManager.Instance.Reg<Region>(((int)GameEvent.WORK_TABLE_CLICK), OnRegionClick);
		}

		private void OnRegionClick(Region region)
		{
			UIUtils.OpenUI("worktable", new WorktableInfo()
			{
				id = region.cfgID,
				mid = (region.next ?? region.begin).cfgID,
				target = region.begin.transform.position
			}) ;
		}

	}
}
