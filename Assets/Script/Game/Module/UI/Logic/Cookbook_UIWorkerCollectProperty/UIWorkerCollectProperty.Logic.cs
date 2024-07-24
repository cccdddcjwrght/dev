
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Cookbook;
	using System.Collections.Generic;
	using GameConfigs;

	public partial class UIWorkerCollectProperty
	{
		private List<int[]> _buffs;

		partial void InitLogic(UIContext context)
		{
			_buffs = context.GetParam()?.Value.To<object[]>().Val<List<int[]>>(0);
			m_view.m_list.itemRenderer = OnSetBuffItem;
			m_view.m_list.RemoveChildrenToPool();
			m_view.m_list.numItems = _buffs.Count;
		}

		void OnSetBuffItem(int index , GObject gObject)
		{
			var v = gObject as UI_WorkerAddProperty;
			var d = _buffs[index];

			v.m_type.selectedIndex = 1;
			if (ConfigSystem.Instance.TryGet(d[0], out BuffRowData buff))
			{
				v.SetIcon(buff.Icon);
				UIListener.SetTextWithName(v, "full", buff.Describe.Local(null, d[1]), false);
			}

		}

		partial void UnInitLogic(UIContext context)
		{
			m_view.m_list.RemoveChildrenToPool();
			_buffs = null;
		}
	}
}
