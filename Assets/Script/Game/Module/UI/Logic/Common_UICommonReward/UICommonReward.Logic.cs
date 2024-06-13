
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Common;
	using System;

	/// <summary>
	/// 参数0：奖励列表
	/// 参数1：点击回调
	/// 参数2：标题
	/// 参数3：界面打开就领取奖励
	/// </summary>
	public partial class UICommonReward
	{
		private Action _call;
		private List<double[]> _rewards;
		private bool _get;

		partial void InitLogic(UIContext context)
		{
			context.window.AddEventListener("OnMaskClick", OnClickClick);
			var args = context.GetParam()?.Value.To<object[]>();
			_rewards = args.Val<List<double[]>>(0) ?? args.Val<ItemList>(0)?.vals;

			if (_rewards?.Count > 0)
			{
				_call = args.Val<Action>(1);
				var title = args.Val<string>(2, "@ui_reward_title");
				var getreward = _get = args.Val<bool>(3, true);
				m_view.SetText(title);
				if (getreward == true)
					PropertyManager.Instance.Insert2Cache(_rewards);
				SetRewards();
				return;
			}
			SGame.UIUtils.CloseUI(context.entity);
		}

		partial void UnInitLogic(UIContext context)
		{
			if (_get)
			{
				TransitionModule.Instance.PlayFlight(m_view.m_list, _rewards
					.Select(v => Array.ConvertAll<double, int>(v, a => (int)a))
					.ToList());
				PropertyManager.Instance.CombineCache2Items();
			}
		}

		void OnClickClick()
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
				g.SetCommonItem(null, data as double[]);
			}, ignoreNull: true);
		}
	}
}

namespace SGame
{
	public class ItemList
	{
		static public readonly ItemList Current = new ItemList();

		public readonly List<double[]> vals = new List<double[]>();

		public ItemList Append(int id, double count, int type = 1, bool clear = false, bool ignorezero = false)
		{
			if (clear) vals.Clear();
			if (!ignorezero || count != 0)
				vals.Add(new double[] { type, id, count });
			return this;
		}

		public ItemList Append(params int[][] items)
		{
			if (items?.Length > 0)
			{
				for (int i = 0; i < items.Length; i++)
				{
					if (items[i] != null)
						vals.Add(Array.ConvertAll<int, double>(items[i], To));
				}
			}
			return this;
		}

		public ItemList Append(params double[][] items)
		{
			if (items?.Length > 0)
			{
				for (int i = 0; i < items.Length; i++)
				{
					if (items[i] != null)
						vals.Add(items[i]);
				}
			}
			return this;
		}

		public ItemList Append(List<int[]> items)
		{
			foreach (var item in items)
			{
				if (item != null)
					vals.Add(Array.ConvertAll<int, double>(item, To));
			}
			return this;
		}

		public ItemList Append(List<double[]> items)
		{
			foreach (var item in items)
			{
				if (item != null)
					vals.Add(item);
			}
			return this;
		}

		public ItemList Clear()
		{
			vals.Clear();
			return this;
		}

		public List<double[]> Copy(bool clear = false)
		{

			var ret = new List<double[]>(vals);
			if (clear) vals.Clear();
			return ret;

		}

		static double To(int val) => val;
	}

	partial class Utils
	{
		static public void ShowRewards(List<int[]> rewards, Action closeCall = null, string title = null, bool updatedata = true)
		{
			UIUtils.OpenUI("rewardlist", ItemList.Current.Clear().Append(rewards).vals, closeCall, title, updatedata);
		}

		static public ItemList ShowRewards(Action closeCall = null, string title = null, bool updatedata = true)
		{
			var list = new ItemList();
			UIUtils.OpenUI("rewardlist", list, closeCall, title, updatedata);
			return list;
		}

		static public void ShowRewards(string title = null, Action close = null, bool updatedata = true, params double[][] rewards)
		{
			UIUtils.OpenUI("rewardlist", rewards.ToList(), close, title, updatedata);
		}
	}
}