
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	using SGame.UI.Common;
	using SGame.UI.Pet;
	using System.Collections;
	using Unity.Entities.UniversalDelegates;

	public partial class UIExplore
	{
		private Coroutine _cd;
		private bool _clickeqflag;

		void InitInfo()
		{
			eventHandle += EventManager.Instance.Reg(((int)GameEvent.EXPLORE_TOOL_UP_LV), OnExploreToolUp);
			eventHandle += EventManager.Instance.Reg(((int)GameEvent.EXPLORE_TOOL_UP_LV_START), RefreshCD);
			eventHandle += EventManager.Instance.Reg<FightEquip>(((int)GameEvent.EXPLORE_CHNAGE_EQUIP), OnFightEquipChange);
			eventHandle += EventManager.Instance.Reg(((int)GameEvent.EXPLORE_UP_LEVEL), SetExploreLv);
			eventHandle += EventManager.Instance.Reg<int, int, int, int>(((int)GameEvent.ITEM_CHANGE_BURYINGPOINT), OnItemChange);
			onOpen += OnOpen_Info;
			onClose += OnClose_Info;
			m_view.onClick.Add(OnViewClick);
		}

		void OnOpen_Info(UIContext context)
		{
			exploreData.CheckCanUpLv();
			SetBaseInfo();
			RefreshCD();
		}

		void OnClose_Info(UIContext context)
		{
			m_view.onClick.Clear();
		}

		void SetBaseInfo()
		{
			SetExploreLv();
			SetAttr();
			SetExploreToolInfo();
			SetEquipInfo();
			ResetBtnState();
		}

		void RefreshCD()
		{
			var cd = m_view.m_tool.m_cd;
			IEnumerator Run()
			{
				var r = 0f;
				while (cd != null && (r = exploreData.ToolUpRemaining()) > 0)
				{
					cd.fillAmount = r / exploreData.exploreToolLevel.Time;
					m_view.m_tool.m_time.text = Utils.FormatTime((int)r);
					yield return null;
				}
			}
			_cd?.Stop();
			if (exploreData.uplvtime > 0)
			{
				cd.visible = true;
				cd.fillAmount = 1;
				_cd = Run().Start();
			}
			else
			{
				cd.visible = false;
				m_view.m_tool.m_time.text = default;
			}
		}

		void SetExploreLv()
		{
			if (exploreData.IsExploreMaxLv())
			{
				m_view.m_progress.max = 1;
				m_view.m_progress.value = 1;
			}
			else
			{
				m_view.m_progress.max = exploreData.exploreLevel.Exp;
				m_view.m_progress.value = Mathf.Min(exploreData.exp, exploreData.exploreLevel.Exp);
			}
			m_view.m_progress.SetTextByKey("ui_common_lv1", exploreData.level);
			m_view.m_progress.m_limit.SetTextByKey("ui_common_max_lv", DataCenter.ExploreUtil.GetCurrentMaxExploreLv());

			if (exploreData.addExp > 0)
			{
				m_view.m_exptips.text = "+" + exploreData.addExp;
				exploreData.addExp = 0;
				m_view.m_expanim.Play();
			}
			m_view.m_warn.visible = exploreData.lvPause;
		}

		void SetAttr()
		{
			m_view.m_power.SetText(Utils.ConvertNumberStrLimit3(exploreData.explorer.GetPower()));
			m_view.m_hp.m_val.SetText(Utils.ConvertNumberStrLimit3(exploreData.explorer.GetAttr(((int)EnumAttribute.Hp))));
			m_view.m_atk.m_val.SetText(Utils.ConvertNumberStrLimit3(exploreData.explorer.GetAttr(((int)EnumAttribute.Attack))));
		}

		void SetEquipInfo()
		{
			var eqs = exploreData.explorer.equips;
			for (int i = 1; i < eqs.Length; i++)
				SetEquipInfo(eqs[i], false);
		}

		void SetExploreToolInfo(bool refreshlv = true)
		{
			var toolnum = PropertyManager.Instance.GetItem(ConstDefine.EXPLORE_ITEM).num;
			m_view.m_find.grayed = fightState || autoState || toolnum <= 0 || isCombat;
			m_view.m_find.SetText(Utils.ConvertNumberStrLimit3(toolnum), false);
			if (refreshlv) m_view.m_tool.SetTextByKey("ui_common_lv1", exploreData.toolLevel);
		}

		void SetEquipInfo(FightEquip equip, bool refreshattr = false)
		{
			if (equip != null)
			{
				var eq = m_view.GetChild("eq" + equip.type);
				if (eq != null)
				{
					eq.SetFightEquipInfo(equip);
					eq.data = equip;
					if (refreshattr)
					{
						SetAttr();
					}
				}
			}
		}

		void ResetBtnState()
		{
			SetExploreToolInfo(false);
			RefreshAutoState();
			m_view.m_tool.grayed = false;
		}

		void OnExploreToolUp()
		{
			SetExploreToolInfo();
			RefreshCD();
		}

		void OnItemChange(int a, int b, int c, int d)
		{
			if (b == ConstDefine.EXPLORE_ITEM)
			{
				SetExploreToolInfo(false);
			}
		}

		void OnFightEquipChange(FightEquip equip)
		{
			SetEquipInfo(equip, true);
			_model?.RefreshModel();
		}

		void OnEqClick(EventContext data)
		{

			var g = data.sender as GObject;
			if (g != null)
			{
				var d = g.data as FightEquip;
				if (d != null)
				{
					_clickeqflag = true;
					m_view.m_eqinfostate.selectedIndex = 0;
					var info = m_view.m_eqinfo;
					if (g == m_view.m_eq11) info.m_type.selectedIndex = 1;
					else info.m_type.selectedIndex = 0;
					info.m_body.SetFightEquipInfo(d, attrsize: 1);
					info.m_body.m_attrs.ResizeToFit();
					info.xy = g.xy + new Vector2(50, -10);
					m_view.m_eqinfostate.selectedIndex = 1;
				}
			}
		}

		void OnViewClick(EventContext data)
		{

			if (!_clickeqflag)
			{
				if (m_view != null)
					m_view.m_eqinfostate.selectedIndex = 0;
			}
			_clickeqflag = false;
		}

		partial void OnWarnClick(EventContext data)
		{
			exploreData.exploreLevel.UnlockTips.Local().Tips();
		}

		partial void OnEq11Click(EventContext data) => OnEqClick(data);
		partial void OnEq12Click(EventContext data) => OnEqClick(data);
		partial void OnEq13Click(EventContext data) => OnEqClick(data);

	}
}
