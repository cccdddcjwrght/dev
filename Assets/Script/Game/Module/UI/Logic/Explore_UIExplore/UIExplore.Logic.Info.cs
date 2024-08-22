
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	using SGame.UI.Common;
	using SGame.UI.Pet;
	using System.Collections;

	public partial class UIExplore
	{
		private Coroutine _cd;

		void InitInfo()
		{
			eventHandle += EventManager.Instance.Reg(((int)GameEvent.EXPLORE_TOOL_UP_LV), OnExploreToolUp);
			eventHandle += EventManager.Instance.Reg(((int)GameEvent.EXPLORE_TOOL_UP_LV_START), RefreshCD);
			eventHandle += EventManager.Instance.Reg<int, int, int, int>(((int)GameEvent.ITEM_CHANGE_BURYINGPOINT), OnItemChange);
			onOpen += OnOpen_Info;
		}

		void OnOpen_Info(UIContext context)
		{
			SetBaseInfo();
			RefreshCD();
		}

		void SetBaseInfo()
		{
			SetExploreLv();
			SetAttr();
			SetExploreToolInfo();
			SetEquipInfo();
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
				cd.visible = false;
		}

		void SetExploreLv()
		{
			m_view.m_progress.value = exploreData.exp;
			m_view.m_progress.max = exploreData.exploreLevel.Exp;
			m_view.m_progress.SetTextByKey("ui_common_lv1", exploreData.level);
		}

		void SetAttr()
		{
			m_view.m_power.SetText(Utils.ConvertNumberStrLimit3(exploreData.explorer.GetPower()));
			m_view.m_hp.m_val.SetText(Utils.ConvertNumberStrLimit3(exploreData.explorer.GetPower()));
			m_view.m_atk.m_val.SetText(Utils.ConvertNumberStrLimit3(exploreData.explorer.GetPower()));
		}

		void SetEquipInfo()
		{
			var eqs = exploreData.explorer.equips;
			for (int i = 1; i < eqs.Length; i++)
			{
				var idx = i + 10;
				var eq = m_view.GetChild("eq" + idx);
				if (eq != null)
				{
					eq.SetEquipInfo(eqs[i], true, idx);
				}
			}
		}

		void SetExploreToolInfo(bool refreshlv = true)
		{
			var toolnum = PropertyManager.Instance.GetItem(ConstDefine.EXPLORE_ITEM).num;
			m_view.m_find.SetText(Utils.ConvertNumberStrLimit3(toolnum), false);
			m_view.m_find.grayed = toolnum <= 0;
			if (refreshlv)
				m_view.m_tool.SetTextByKey("ui_common_lv1", exploreData.toolLevel);
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

	}
}
