using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FairyGUI;
using GameConfigs;
using UnityEngine;

namespace SGame.UI.Cookbook
{
	public class CollectWorkerTab : ICookbookSub
	{
		public UI_CookbookUI view { get; set; }
		public int type;

		private List<WorkerDataItem> _datas;
		private List<int[]> _buffs;
		private EventHandleContainer eventHandle = new EventHandleContainer();

		public void Enter()
		{
			var ty = type == 2 ? 2 : 1;
			_datas = DataCenter.WorkerDataUtils.GetDatas(d => d.cfg.RoleType == ty);
			eventHandle += EventManager.Instance.Reg<int>(((int)GameEvent.WORKER_UPDAETE), OnWorkerSelected);
			view.m_addpropertys.itemRenderer = OnSetBuffItem;
			view.m_addpropertys.RemoveChildrenToPool();
			view.m_addpropertys.onClick.Clear();
			view.m_addpropertys.onClick.Add(OnPropertyClick);
			SetList();
			RefreshPropertyList();
		}

		public void Update()
		{
		}

		public void OnSetListItem(int index, GObject gObject)
		{
		}

		private void SetList()
		{
			_datas.Sort((a, b) =>
			{
				if (a.IsSelected()) return -1;
				if (b.IsSelected()) return 1;
				var c = b.CanUpLv().CompareTo(a.CanUpLv());
				if(c == 0)
				{
					c = b.level.CompareTo(a.level);
					if(c == 0)
						return a.id.CompareTo(b.id);
				}
				return c;

			});
			view.m_listworker.RemoveChildrenToPool();
			SGame.UIUtils.AddListItems(view.m_listworker, _datas, OnSetItem);
		}

		private void RefreshPropertyList()
		{
			if (_buffs == null || view.m_addpropertys.numItems == 0)
				_buffs = DataCenter.WorkerDataUtils.GetBuffList(0, _datas);
			if (_buffs?.Count > 0)
			{
				view.m_addpropertys.scrollPane.touchEffect = _buffs.Count > 4;
				view.m_addpropertys.numItems = _buffs.Count;
			}
		}

		private void OnSetBuffItem(int index, GObject gObject)
		{
			var v = gObject as UI_WorkerAddProperty;
			var d = _buffs[index];
			v.name = d[0].ToString();
			v.SetText($"+{d[1]}%", false);
			v.m_color.selectedIndex = d[1] > 0 ? 2 : 0;
			if (ConfigSystem.Instance.TryGet(d[0], out BuffRowData buff))
				v.SetIcon(buff.Icon);
		}

		private void OnSetItem(int index, object data, GObject gObject)
		{

			var v = gObject as UI_WorkerItem;
			var d = data as WorkerDataItem;
			v.SetWorkerInfo(d);
			v.onClick.Clear();
			v.onClick.Add(() => OnItemClick(gObject, d));
		}

		private void OnItemClick(GObject gObject, WorkerDataItem data)
		{
			if (!data.CanUpLv())
			{
				SGame.UIUtils.OpenUI("workerup", data);
			}
			else
			{
				if (DataCenter.WorkerDataUtils.UpLv(data.id))
					AnimLogic(gObject, data).Start();
			}
		}

		private void OnPropertyClick()
		{
			SGame.UIUtils.OpenUI("workerpropertytips", _buffs);
		}

		IEnumerator AnimLogic(GObject gObject, WorkerDataItem data)
		{
			var e = SGame.UIUtils.OpenUI("workeruptips", data);
			yield return null;
			yield return new WaitUIClose(UIModule.Instance.GetEntityManager(), e);
			view.touchable = false;
			var target = view.m_addpropertys.GetChild(data.cfg.Buff.ToString());
			var index = view.m_addpropertys.GetChildIndex(target);
			var tpos = target.TransformPoint(target.size * 0.5f, view);
			_buffs[index][1] += data.GetBuffVal() - data.lastVal;
			view.m_effectholder.xy = gObject.TransformPoint(gObject.size * 0.5f, view);
			var eff = EffectSystem.Instance.SpawnUI(33, view.m_effectholder);
			yield return EffectSystem.Instance.WaitEffectLoaded(eff);
			var t = view.m_effectholder.TweenMove(tpos, 0.5f);
			yield return new WaitUntil(() => t.completed);
			EffectSystem.Instance.ReleaseEffect(eff);
			OnSetBuffItem(index, target);
			if (data.lastLv <= 0) SGame.UIUtils.OpenUI("workerup", data);
			view.touchable = true;

		}

		private void OnWorkerSelected(int id)
		{
			SetList();
		}

		public void Exit()
		{
			eventHandle?.Close();
			_buffs = null;
		}

		public void Clear()
		{
			eventHandle?.Close();
			eventHandle = null;
			view = null;
			_buffs = null;
		}
	}
}
