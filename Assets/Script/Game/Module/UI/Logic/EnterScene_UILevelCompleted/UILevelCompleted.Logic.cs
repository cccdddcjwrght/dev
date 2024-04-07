
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.EnterScene;
	using GameConfigs;

	public partial class UILevelCompleted
	{

		partial void InitLogic(UIContext context)
		{
			var roomID = DataCenter.Instance.roomData.roomID;
			if (!ConfigSystem.Instance.TryGet<RoomRowData>(roomID, out var cfg))
			{
				SGame.UIUtils.CloseUI(context.entity);
				return;
			}

			var list = Utils.GetArrayList(cfg.GetReward1Array, cfg.GetReward2Array, cfg.GetReward3Array);
			PropertyManager.Instance.Insert2Cache(list);
			SGame.UIUtils.AddListItems(m_view.m_list, list, OnSetReward);

		}

		void OnSetReward(int index, object data, GObject gObject)
		{
			var item = gObject as UI_RItem;
			var d = data as int[];
			item.SetIcon(Utils.GetItemIcon(d[0], d[1]));
			item.SetText("x" + Utils.ConvertNumberStr(d[2]), false);
		}

		partial void OnClickClick(EventContext data)
		{
			PropertyManager.Instance.CombineCache2Items();
			DelayExcuter.Instance.DelayOpen(null, "mainui", true, () =>
			{
				SGame.UIUtils.OpenUI("enterscene", DataCenter.Instance.roomData.roomID + 1);
			});
			SGame.UIUtils.CloseUIByID(__id);
		}

		partial void UnInitLogic(UIContext context)
		{

		}

	}
}
