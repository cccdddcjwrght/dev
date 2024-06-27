
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FairyGUI;
using SGame.UI.Common;

namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Common;
	using System;
	using Unity.Entities;

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
		private bool _flag;
		private ItemList _itemList;
		List<Entity> _effects = new List<Entity>();

		partial void InitLogic(UIContext context)
		{

			m_view.z = -300;
			_flag = false;
			context.window.AddEventListener("OnMaskClick", OnClickClick);
			var args = context.GetParam()?.Value.To<object[]>();
			_itemList = args.Val<ItemList>(0);
			_rewards = _itemList == null ? args.Val<List<double[]>>(0) : _itemList.vals;

			if (_rewards?.Count > 0)
			{
				SetBaseInfo(args);
				SetRewards();
				if (_get == true) PropertyManager.Instance.Insert2Cache(_rewards);
				return;
			}
			else
			{
				var call = args.Val<Action<UI_CommonRewardBody>>(4);
				if (call != null)
				{
					SetBaseInfo(args);
					call?.Invoke(m_view.m_body);
					return;
				}
			}
			SGame.UIUtils.CloseUI(context.entity);
		}

		void SetBaseInfo(object[] args)
		{
			_call = args.Val<Action>(1);
			var title = args.Val<string>(2, "@ui_reward_title");
			_get = args.Val<bool>(3, true);
			m_view.SetText(title);
			this.Delay(() => _flag = true, 500);
		}

		partial void UnInitLogic(UIContext context)
		{
			if (_get || _itemList?.fly == true)
			{
				TransitionModule.Instance.PlayFlight(m_view.m_body.m_list, _rewards.Select(v => Array.ConvertAll<double, int>(v, a => (int)a)).ToList());
				Do().Start();
			}
			_effects.Foreach((e) => EffectSystem.Instance.ReleaseEffect(e));
		}

		IEnumerator Do()
		{
			UILockManager.Instance.Require("rewardlist");
			yield return new WaitForSeconds(0.1f);
			yield return new WaitUntil(() => !TransitionModule.isPlay);
			PropertyManager.Instance.CombineCache2Items();
			yield return new WaitForSeconds(0.1f);
			UILockManager.Instance.Release("rewardlist");

		}

		void OnClickClick()
		{
			//界面不能点击时，不能关闭
			if (!_flag || !m_view.touchable) return;
			SGame.UIUtils.CloseUIByID(__id);
			var c = _call;
			_call = null;
			c?.Invoke();
		}

		void SetRewards()
		{
			SGame.UIUtils.AddListItems(m_view.m_body.m_list, _rewards, (index, data, g) =>
			{
				g.SetCommonItem(null, data as double[]);
				var item = (UI_BigItem)g;
				_effects.Add(EffectSystem.Instance.AddEffect(36, item.m_body));
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
		public string tips;
		public bool fly;

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

		public ItemList SetTips(string tips)
		{

			this.tips = tips;
			return this;
		}

		static double To(int val) => val;
	}

	partial class Utils
	{
		/// <summary>
		/// 显示奖励界面
		/// </summary>
		/// <param name="rewards">奖励列表</param>
		/// <param name="closeCall">关闭回调</param>
		/// <param name="title">标题</param>
		/// <param name="updatedata">是否修改数据</param>
		static public void ShowRewards(List<int[]> rewards, Action closeCall = null, string title = null, bool updatedata = true)
		{
			UIUtils.OpenUI("rewardlist", ItemList.Current.Clear().Append(rewards).vals, closeCall, title, updatedata);
		}

		static public void ShowRewards(List<double[]> rewards, Action closeCall = null, string title = null, bool updatedata = true)
		{
			UIUtils.OpenUI("rewardlist", rewards, closeCall, title, updatedata);
		}

		static public ItemList ShowRewards(
			Action closeCall = null, string title = null, bool updatedata = true,
			Action<UI_CommonRewardBody> contentCall = null)
		{
			var list = new ItemList();
			UIUtils.OpenUI("rewardlist", list, closeCall, title, updatedata, contentCall);
			return list;
		}

		static public void ShowRewards(string title = null, Action close = null, bool updatedata = true, params double[][] rewards)
		{
			UIUtils.OpenUI("rewardlist", rewards.ToList(), close, title, updatedata);
		}
	}
}