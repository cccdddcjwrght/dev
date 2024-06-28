
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.EnterScene;
	using GameConfigs;
	using System.Collections.Generic;

	public partial class UILevelCompleted
	{

		List<int[]> list;
		partial void InitLogic(UIContext context)
		{
			var roomID = DataCenter.Instance.roomData.roomID;
			if (!ConfigSystem.Instance.TryGet<RoomRowData>(roomID, out var cfg))
			{
				SGame.UIUtils.CloseUI(context.entity);
				return;
			}
			m_view.m_build.SetIcon(string.IsNullOrEmpty(cfg.EndImage) ? "ui_end_bg_" + roomID : cfg.EndImage);

			list = Utils.GetArrayList(cfg.GetReward1Array, cfg.GetReward2Array, cfg.GetReward3Array);
			PropertyManager.Instance.Insert2Cache(list);
			SGame.UIUtils.AddListItems(m_view.m_body.m_list, list, OnSetReward);

			EffectSystem.Instance.AddEffect(9, m_view.m_body, m_view);
			this.Delay(AddEffect, 300);

		}

		void AddEffect() => EffectSystem.Instance.AddEffect(10, m_view.m_body.m___effect1, m_view);

		void OnSetReward(int index, object data, GObject gObject)
		{
			var item = gObject as UI_RItem;
			var d = data as int[];
			item.SetIcon(Utils.GetItemIcon(d[0], d[1]));
			item.SetText("x" + Utils.ConvertNumberStr(d[2]), false);
		}

		partial void OnLevelCompletedBody_ClickClick(EventContext data)
		{
			//TransitionModule.Instance.PlayFlight(m_view.m_body.m_list, list);
			PropertyManager.Instance.CombineCache2Items();
			DelayExcuter.Instance.DelayOpen(null, "mainui", true, () =>
			{
				SGame.UIUtils.OpenUI("enterscenetemp");
				//SGame.UIUtils.OpenUI("enterscene", DataCenter.Instance.roomData.roomID + 1);
			});
			m_view.m_body.m_click.touchable = false;
			SGame.UIUtils.CloseUIByID(__id);
		}

		partial void UnInitLogic(UIContext context)
		{
			EffectSystem.Instance.ReleaseEffect(m_view);
		}

	}
}
