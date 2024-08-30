

using System;
using System.Collections.Generic;

namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
    using SGame.UI.Common;
    using System.Collections;
    using System;
    using System.Collections.Generic;
    using Unity.Entities;
    using System.Linq;

    public partial class UIFightGift
	{
		private Action _call;
		private List<double[]> _rewards;
		private bool _get;
		private bool _flag;
		private int _type;
		private ItemList _itemList;
		List<Entity> _effects = new List<Entity>();

		partial void InitLogic(UIContext context)
		{

			m_view.z = -300;
			_flag = false;
			m_view.m_body.m_click.onClick.Add(OnClickBtn);
			var args = context.GetParam()?.Value.To<object[]>();
			_itemList = args.Val<ItemList>(0);
			_rewards = _itemList == null ? args.Val<List<double[]>>(0) : _itemList.vals;
			21.ToAudioID().PlayAudio();
			if (_rewards?.Count > 0)
			{
				SetBaseInfo(args);
				SetRewards();
				m_view.m_body.m_type.selectedIndex = _type;
				if (_get == true) PropertyManager.Instance.Insert2Cache(_rewards);
				return;
			}
			else
			{
				var call = args.Val<Action<UI_FightGiftUI>>(4);
				if (call != null)
				{
					SetBaseInfo(args);
					call?.Invoke(m_view);
					return;
				}
			}
			SGame.UIUtils.CloseUI(context.entity);
		}

		void SetBaseInfo(object[] args)
		{
			_call = args.Val<Action>(1);
			var title = args.Val<string>(2, "@ui_fight_gift_title");
			_get = args.Val<bool>(3, true);
			_type = args.Val<int>(4);
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

        public void OnClickBtn(EventContext data)
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
	partial class Utils
	{
		/// <summary>
		/// 显示奖励界面
		/// </summary>
		/// <param name="rewards">奖励列表</param>
		/// <param name="closeCall">关闭回调</param>
		/// <param name="title">标题</param>
		/// <param name="updatedata">是否修改数据</param>
		static public void ShowFightRewards(List<int[]> rewards, Action closeCall = null, string title = null, bool updatedata = true, int type = 0)
		{
			SGame.UIUtils.OpenUI("fightgift", ItemList.Current.Clear().Append(rewards).vals, closeCall, title, updatedata, type);
		}
	}
}




