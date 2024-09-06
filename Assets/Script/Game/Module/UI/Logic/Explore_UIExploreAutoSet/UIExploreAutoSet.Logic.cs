
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	using GameConfigs;
	using System.Linq;

	public partial class UIExploreAutoSet
	{
		ExploreAutoCfg autoCfg;
		ExploreData data;
		static string[] qstrs = new string[] {
			((int)EnumQuality.White).ToString(),
			((int)EnumQuality.Green).ToString(),
			((int)EnumQuality.Blue).ToString(),
			((int)EnumQuality.Purple).ToString(),
			((int)EnumQuality.Orange).ToString(),
			((int)EnumQuality.Red).ToString(),
			((int)EnumQuality.Max).ToString(),
		};

		partial void InitLogic(UIContext context)
		{
			InitView();
			DoOpen(context);
		}

		partial void DoOpen(UIContext context)
		{
			data = DataCenter.Instance.exploreData;
			autoCfg = data.autoCfg;
			SetInfo();
		}

		partial void DoHide(UIContext context)
		{
			autoCfg = default;
			data = default;
		}

		partial void OnClickClick(EventContext data)
		{
			if (PropertyManager.Instance.GetItem(ConstDefine.EXPLORE_ITEM).num >= autoCfg.cost)
			{
				EventManager.Instance.Trigger(((int)GameEvent.EXPLORE_AUTO_TOGGLE), true);
				DoCloseUIClick(null);
			}
			else
				"@ui_explore_tool_not_enough".Tips();
		}

		partial void UnInitLogic(UIContext context)
		{

		}

		void InitView()
		{
			InitCostList();
			InitQuality();
			InitToggle();
		}

		void SetInfo()
		{
			SetCostSelect();
			SetQuality();
			SetToggle();
		}

		#region Cost
		void InitCostList()
		{
			ExploreAutoCfg.GetCostList(out var item, out var conditions);
			if (item != null)
			{
				m_view.m_costselect.data = 1;
				m_view.m_costselect.onChanged.Set(OnCostChange);
				m_view.m_costselect.items = item;
				m_view.m_costselect.values = conditions;
			}
		}

		void SetCostSelect()
		{
			var selectBox = m_view.m_costselect;
			selectBox.selectedIndex = autoCfg.GetCurrentCostIndex();
		}

		void OnCostChange()
		{
			var selectBox = m_view.m_costselect;
			var select = selectBox.selectedIndex;
			if (int.TryParse(selectBox.value, out var lv) && data.level < lv)
			{
				"@ui_explore_auto_tips10".Tips();
				selectBox.selectedIndex = autoCfg.GetCurrentCostIndex();
			}
			else
				int.TryParse(selectBox.items[select], out autoCfg.cost);
		}
		#endregion

		#region Quality

		void InitQuality()
		{
			var qSelect = m_view.m_qualityselect;
			qSelect.data = 2;
			qSelect.items = qstrs;
			qSelect.values = qstrs;
			qSelect.onChanged.Set(OnQualityChange);
		}

		void SetQuality()
		{
			var qSelect = m_view.m_qualityselect;
			DataCenter.EquipUtil.ConvertQuality(autoCfg.quality, out var qt, out _);
			m_view.m_quality.selectedIndex = qt - 1;
			UIListener.SetTextWithName(qSelect, "title", "@ui_quality_name_" + autoCfg.quality, true);
		}

		void OnQualityChange()
		{

			var qSelect = m_view.m_qualityselect;
			int.TryParse(qSelect.value, out autoCfg.quality);
			SetQuality();
		}

		#endregion

		#region Other

		void InitToggle()
		{
			var attrs = DataCenter.ExploreUtil.c_fight_attrs_1.ToList();
			attrs.Add(EnumAttribute.None);
			m_view.m_atts.onClickItem.Set(OnAttrToggleClick);
			m_view.m_atts.RemoveChildrenToPool();
			SGame.UIUtils.AddListItems(m_view.m_atts, attrs, SetAttrToggle);

		}

		void SetToggle()
		{
			m_view.m_cdtype.selectedIndex = autoCfg.and ? 1 : 0;
			m_view.m_comparepower.selectedIndex = autoCfg.comparePower ? 1 : 0;
			m_view.m_atts.ClearSelection();
			var attrs = DataCenter.ExploreUtil.c_fight_attrs_1;
			for (int i = 0; i < attrs.Count; i++)
			{
				var a = ((int)attrs[i]);
				if (autoCfg.attrFilter.Contains(a))
					m_view.m_atts.AddSelection(i, false);
			}
		}

		void SetAttrToggle(int index, object data, GObject gObject)
		{
			var attr = ((int)((EnumAttribute)data));
			if (attr == 0)
			{
				gObject.alpha = 0;
				gObject.touchable = false;
			}
			else
			{
				gObject.alpha = 1;
				gObject.touchable = true;
				gObject.data = attr;
				gObject.name = attr.ToString();
				gObject.SetTextByKey("ui_fight_attr_" + data.ToString().ToLower());
			}
		}

		void OnAttrToggleClick(EventContext context)
		{
			var data = context.data as GButton;
			if (data != null)
			{
				var attr = (int)data.data;
				if (data.selected)
				{
					if (!autoCfg.attrFilter.Contains(attr))
						autoCfg.attrFilter.Add(attr);
				}
				else if (autoCfg.attrFilter.Contains(attr))
					autoCfg.attrFilter.Remove(attr);
			}
		}

		partial void OnCdtypeChanged(EventContext data)
		{
			autoCfg.and = m_view.m_cdtype.selectedIndex == 1;
		}

		partial void OnComparepowerChanged(EventContext data)
		{
			autoCfg.comparePower = m_view.m_comparepower.selectedIndex == 1;
		}


		#endregion

	}
}
