
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Common;
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// 参数0：奖励列表
	/// 参数1：点击回调
	/// 参数2：标题
	/// 参数3：界面打开就领取奖励
	/// </summary>
	public partial class UIRewardShow
	{
		private Action _call;
		private List<int[]> _rewards;

		partial void InitLogic(UIContext context)
		{
			context.window.AddEventListener("OnMaskClick", () => OnClickClick(null));
			var args = context.GetParam()?.Value.To<object[]>();
			_rewards = args.Val<List<int[]>>(0);
			_call = args.Val<Action>(1);
			var title = args.Val<string>(2,"@ui_reward_show_title");
			var getreward = args.Val<bool>(3 , true);
			m_view.m_title.SetText(title);
			if (_rewards?.Count > 0)
			{
				if (getreward == true)
					PropertyManager.Instance.Insert2Cache(_rewards);
				SetRewards();
				return;
			}
			SGame.UIUtils.CloseUI(context.entity);
		}

		partial void UnInitLogic(UIContext context)
		{
			PropertyManager.Instance.CombineCache2Items();
		}

		partial void OnClickClick(EventContext data)
		{
			SGame.UIUtils.CloseUIByID(__id);
			var c = _call;
			_call = null;
			c?.Invoke();
		}

		void SetRewards()
		{
			SGame.UIUtils.AddListItems(m_view.m_list, _rewards, (index, data, g) =>
			{
				g.SetCommonItem(null, data as int[]);
			}, ignoreNull: true);
		}

	}
}
