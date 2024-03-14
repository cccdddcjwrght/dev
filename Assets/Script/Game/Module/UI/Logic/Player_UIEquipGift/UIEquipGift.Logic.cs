
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	using GameConfigs;
	using System.Linq;
	using System.Collections.Generic;
	using System;

	public partial class UIEquipGift
	{
		private GObject _icon;
		private GList _list;

		private List<EquipmentRowData> _eqs;
		private Queue<ItemData.Value> _args;

		private ItemData.Value _current;
		private int _chestID;
		private int _count;

		partial void InitLogic(UIContext context)
		{
			_args = (context.GetParam().Value as object[]).Val<Queue<ItemData.Value>>(0);
			if (_args?.Count == 0)
			{
				CloseUI(true);
				return;
			}

			_list = m_view.m_body.m_items;
			_icon = m_view.m_body.GetChild("icon");
			_list.itemRenderer = OnSetEquipInfo;
			context.onShown += OnShow;
			_mask = true;
		}

		partial void UnInitLogic(UIContext context)
		{
			_list.RemoveChildrenToPool();
			_icon = default;
		}

		private void OnShow(UIContext context)
		{
			OpenChest();
		}

		private void OpenChest()
		{
			if (_current.id == 0)
			{
				if (_args?.Count == 0)
				{ CloseUI(); return; }
				else _current = _args.Dequeue();
			}
			var num = PropertyManager.Instance.GetGroup(PropertyGroup.ITEM).GetNum(_current.id);
			if (num == 0 || !SetInfo((int)_current.num))
			{
				_current = default;
				OpenChest();
			}
			else
				PropertyManager.Instance.Update(1, _current.id, 1, true);
		}

		private bool SetInfo(int id)
		{
			_eqs = GetRandomEqs(id, out var chest);
			if (_eqs != null)
			{
				DataCenter.EquipUtil.AddEquips(true, _eqs.ToArray());
				_list.numItems = (int)(_eqs?.Count);
				m_view.m_type.SetSelectedPage(id.ToString());


				_count--;
				_mask = true;
				m_view.m_open.Play(() => _mask = false);
				return true;
			}
			return false;
		}

		private void OnSetEquipInfo(int index, GObject gObject)
		{
			var cfg = _eqs[index];
			var eq = gObject as UI_EquipBox;
			eq.m_body.SetEquipInfo(cfg);
		}

		private List<EquipmentRowData> GetRandomEqs(int id, out ChestRowData chest)
		{
			chest = default;
			if (id != 0 && ConfigSystem.Instance.TryGet<ChestRowData>(id, out var cfg))
			{
				chest = cfg;
				var weight = cfg.GetQualityWeightArray();
				var rand = new SGame.Randoms.Random();
				var ws = rand.NextWeights(weight, cfg.Num, false).GroupBy(v => v);
				var rets = new List<EquipmentRowData>();
				var acts = (cfg.GetActivityArray() ?? Array.Empty<int>()).ToList();
				foreach (var item in ws)
				{
					var eqs = ConfigSystem.Instance.Finds<GameConfigs.EquipmentRowData>(e => e.Quality == item.Key + 1 && (acts.Count == 0 || acts.Contains(e.Activity)));
					rand.NextItem(eqs, item.Count(), ref rets);
				}

				return rets;

			}
			return default;
		}

		partial void OnGiftBody_ClickClick(EventContext data)
		{
			if (!_mask)
			{
				_list.RemoveChildrenToPool();
				OpenChest();
			}
		}

		void CloseUI(bool imm = false)
		{
			if (imm)
				SGame.UIUtils.CloseUIByID(__id);
			else
				m_view.m_t1.Play(() => SGame.UIUtils.CloseUIByID(__id));
		}

	}
}
