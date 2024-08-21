
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	using SGame.UI.Common;
	using SGame.UI.Pet;

	public partial class UIExplore
	{
		void InitInfo()
		{
			onOpen += OnOpen_Info;
		}

		void OnOpen_Info(UIContext context)
		{
			SetBaseInfo();

		}

		void SetBaseInfo()
		{
			SetExploreLv();
			SetAttr();
			SetExploreToolInfo();
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

		void SetExploreToolInfo()
		{
			var toolnum = PropertyManager.Instance.GetItem(3).num;
			m_view.m_find.SetText(Utils.ConvertNumberStrLimit3(toolnum), false);
			m_view.m_tool.SetTextByKey("ui_common_lv1", exploreData.toolLevel);
		}
	}
}
