using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FairyGUI;
using GameConfigs;
using SGame;
using SGame.UI.Cookbook;

partial class UIListenerExt
{
	static public void OnSetStarInfo(int index, object data, GObject gObject)
	{
		UIListener.SetControllerSelect(gObject, "type", (int)data);
	}

	static public GObject SetWorkerInfo(this GObject gObject, WorkerDataItem workerInfo, int starcount = -1, bool usedefaultval = false)
	{
		if (gObject != null)
		{
			if (workerInfo != null)
			{
				var com = gObject.asCom;
				var need = workerInfo.GetCost(out _, out var item);
				var icon = string.Empty;
				UIListener.SetControllerSelect(gObject, "state", !workerInfo.IsEnable() ? 0 : workerInfo.reward > 0 ? 2 : 1, false);
				UIListener.SetControllerSelect(gObject, "selected", workerInfo.IsSelected() ? 1 : 0, false);
				UIListener.SetControllerSelect(gObject, "maxlv", workerInfo.IsMaxLv() ? 1 : 0, false);
				UIListener.SetControllerSelect(gObject, "__redpoint", workerInfo.Check() ? 1 : 0, false);

				if (workerInfo.roleCfg.IsValid())
				{
					icon = workerInfo.roleCfg.Icon;
					gObject.SetTextByKey(workerInfo.roleCfg.Name);
					UIListener.SetTextWithName(gObject, "desc", workerInfo.roleCfg.Des.Local(), false);
				}
				if (com != null)
				{
					var role = (com.GetChild("role") ?? com.GetChild("customer")) as UI_Customer;
					if (role != null) role.SetIcon(icon, "Cookbook");
					else gObject.SetIcon(icon, "Cookbook");

					var p = com.GetChild("progress")?.asProgress;
					if (p != null)
					{
						if (need == 0) { p.max = 1; p.titleType = ProgressTitleType.Value; }
						else { p.max = need; p.titleType = ProgressTitleType.ValueAndMax; }
						p.value = PropertyManager.Instance.GetItem(item).num;

					}

					var stars = com.GetChild("stars")?.asList;
					if (stars != null)
					{
						var ss = DataCenter.MachineUtil.CalcuStarList(((int)EnumQualityType.Max) * 5, starcount >= 0 ? starcount : workerInfo.level);
						stars.RemoveChildrenToPool();
						UIUtils.AddListItems(stars, ss, OnSetStarInfo);
					}

					var pro = com.GetChild("property")?.asCom;
					if (pro != null)
					{
						var val = workerInfo.GetBuffVal(usedefaultval);
						var real = workerInfo.GetBuffVal();
						var last = workerInfo.lastVal;

						pro.SetText($"+{last}%", false);
						UIListener.SetTextWithName(pro, "next", $"+{real}%", false);
						if (ConfigSystem.Instance.TryGet<BuffRowData>(workerInfo.cfg.Buff, out var buff))
						{
							UIListener.SetTextWithName(pro, "full", buff.Describe.Local(null,  val), false);
							pro.SetIcon(buff.Icon);
						}
					}

				}
			}
		}
		return gObject;
	}

}

