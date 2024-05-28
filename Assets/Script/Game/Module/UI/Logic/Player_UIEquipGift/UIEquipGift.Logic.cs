
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
	using Unity.Entities;

	public partial class UIEquipGift
	{
		private GObject _icon;
		private GList _list;

		private List<EquipmentRowData> _eqs;
		private Queue<ItemData.Value> _args;
		private bool _openAll;

		private ItemData.Value _current;
		private int _chestID;
		private double _count;
		private Entity _effect;


		private Dictionary<int, List<EquipmentRowData>> _tables = new Dictionary<int, List<EquipmentRowData>>();


		partial void InitLogic(UIContext context)
		{
			var a = context.GetParam()?.Value as object[];
			if (a != null)
			{
				_args = a.Val<Queue<ItemData.Value>>(0);
				_openAll = a.Val<bool>(1, false);
			}
			else
			{
				_openAll = true;
				_args = new Queue<ItemData.Value>(ConfigSystem.Instance
					.Finds<ItemRowData>(c => c.Type == ((int)EnumItemType.Chest))
					.Select(i => PropertyManager.Instance.GetItem(i.ItemId).SetNum(i.TypeId)).Where(i => i.id > 0));
			}

			if (_args?.Count == 0)
			{
				CloseUI(true);
				return;
			}

			_list = m_view.m_body.m_items;
			_icon = m_view.m_body.GetChild("icon");
			_list.itemRenderer = OnSetEquipInfo;
			context.onShown += OnShow;
			m_view.m_body.visible = false;
			_mask = true;
		}

		partial void UnInitLogic(UIContext context)
		{
			_list?.RemoveChildrenToPool();
			_list = null;
			_icon = default;
			_tables?.Clear();
			_tables = null;
			EventManager.Instance.Trigger(((int)GameEvent.GAME_MAIN_REFRESH));
		}

		private void OnShow(UIContext context)
		{
			this.Delay(OpenChest, 100);
		}

		private void OpenChest()
		{
			_count = 0;
			if (_current.id == 0)
			{
				_eqs?.Clear();
				if (_args?.Count == 0)
				{ CloseUI(); return; }
				else if (_openAll) _eqs = GetAllChestEquips(_args, out _current, out var cfg);
				else _current = _args.Dequeue();
			}
			_count = _count == 0 ? PropertyManager.Instance.GetGroup(PropertyGroup.ITEM).GetNum(_current.id) : _count;
			if (_count == 0 || !SetInfo((int)_current.num, _count, _eqs))
			{
				_current = default;
				OpenChest();
			}
			else if (_count > 0)
				PropertyManager.Instance.Update(1, _current.id, _count, true);
			else _current = default;
			ImmSave();
		}

		private bool SetInfo(int id, double count = 0, List<EquipmentRowData> equipments = null)
		{
			m_view.m_body.visible = true;
			_count = 0;
			_eqs = equipments?.Count > 0 ? equipments : GetRandomEqs(id, out var chest, ref count, _eqs);
			if (_eqs?.Count > 0)
			{
				_count = count;
				ReleaseEffect();
				m_view.m_type.SetSelectedPage(id.ToString());
				DataCenter.EquipUtil.AddEquips(true, _eqs.ToArray());
				_list.layout = _eqs.Count < 4 ? ListLayoutType.FlowHorizontal : ListLayoutType.SingleRow;
				_list.autoResizeItem = _eqs.Count < 4;
				_list.width = _eqs.Count < 4 ? 150 * _eqs.Count : 576;

				_list.numItems = _eqs.Count;
				EventManager.Instance.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RecordDataEnum.EQUIP_BOX, _eqs.Count);

				_mask = true;
				m_view.m_open.Play(() => _mask = false);
				this.Delay(() =>
				{
					if (m_view != null)
						_effect = EffectSystem.Instance.AddEffect(6, m_view.m_body);

				}, 1000);
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

		private List<EquipmentRowData> GetRandomEqs(int id, out ChestRowData chest, ref double count, List<EquipmentRowData> rets = null)
		{
			chest = default;
			if (id != 0 && ConfigSystem.Instance.TryGet<ChestRowData>(id, out var cfg))
			{
				chest = cfg;
				var weight = cfg.GetQualityWeightArray();
				var rand = new SGame.Randoms.Random();
				var acts = (cfg.GetActivityArray() ?? Array.Empty<int>()).ToList();
				rets = rets ?? new List<EquipmentRowData>();

				for (int i = 0; i < count; i++)
				{
					var ws = rand.NextWeights(weight, cfg.Num, false).GroupBy(v => v);
					foreach (var item in ws)
					{
						var k = item.Key + 1;
						if (!_tables.TryGetValue(k, out var ls))
							_tables[k] = ls = ConfigSystem.Instance.Finds<GameConfigs.EquipmentRowData>(e => e.Quality == k);
						var eqs = acts.Count == 0 ? ls : ls.FindAll(e => acts.Contains(e.Activity));
						rand.NextItem(eqs, item.Count(), ref rets);
					}
				}

				return rets;

			}
			return default;
		}

		private List<GameConfigs.EquipmentRowData> GetAllChestEquips(Queue<ItemData.Value> values, out ItemData.Value best, out ChestRowData bestCfg)
		{
			bestCfg = default;
			best = default;
			if (values?.Count > 0)
			{
				var q = 0;
				var eqs = _eqs ?? new List<EquipmentRowData>();
				eqs.Clear();
				while (values.Count > 0)
				{
					var v = values.Dequeue();
					var num = PropertyManager.Instance.GetItem(v.id).num;
					if (num > 0)
					{
						PropertyManager.Instance.Update(1, v.id, num, true);
						GetRandomEqs((int)v.num, out var c, ref num, eqs);
						if (c.ChestQuality > q) { best = v; bestCfg = c; q = c.ChestQuality; }
						_count = -1;
					}
				}
				return eqs;
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
			ReleaseEffect();
			if (imm)
				SGame.UIUtils.CloseUIByID(__id);
			else
				m_view.m_t1.Play(() => SGame.UIUtils.CloseUIByID(__id));
		}

		void ReleaseEffect()
		{
			if (_effect != default)
				EffectSystem.Instance.ReleaseEffect(_effect);
			_effect = default;
		}

		void ImmSave()
		{
			DataCenter.Instance.SavePlayerData();
		}

	}
}
