
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Pet;
	using System;
	using System.Collections.Generic;

	public partial class UIRandomSelect
	{
		private int lastIndex;
		private List<string[]> _list;
		private string[] _select;
		private Action _call;
		private Unity.Entities.Entity _effect;
		private bool _mask = true;

		private bool _imm = false;

		partial void InitLogic(UIContext context)
		{
			context.window.AddEventListener("OnMaskClick", ClickMask);
			var args = context.GetParam()?.Value.To<object[]>();
			_list = args.Val<List<string[]>>(0);
			_call = args.Val<Action>(1);
			if (_list?.Count == 0)
			{
				_mask = false;
				_call?.Invoke();
				ClickMask();
				return;
			}

			_select = _list[_list.Count - 1];
			m_view.SetText(_select[1], false);

			m_view.m_list.SetVirtualAndLoop();
			m_view.m_list.itemRenderer = OnItemSet;
			m_view.m_list.numItems = 50;
			_mask = true;
			Loop().Start();
		}

		void OnItemSet(int index, GObject g)
		{
			lastIndex = index;
			if (index == 49)
			{
				g.SetIcon(_select[0]);
				g.name = "select";
			}
			else
			{
				var val = _list[index % _list.Count];
				g.SetIcon(val[0]);
			}
		}

		System.Collections.IEnumerator Loop()
		{
			var step = 0f;
			var max = 20;
			var time = 3f;
			var flag = false;
			while (true)
			{

				if (_imm)
				{
					m_view.m_list.ScrollToView(m_view.m_list.numItems - 1);
					break;
				}

				yield return null;
				time -= Time.deltaTime;
				if (time > 0) step += 0.1f;
				else
				{
					if (!flag)
						flag = lastIndex == 30;
					else
					{
						step = Math.Max(5, step - 0.2f);
						if (lastIndex == 2) break;

					}
				}
				m_view.m_list.scrollPane.posX += Math.Clamp(step, 0, max);
				m_view.m_list.RefreshVirtualList();
			}

			var cs = m_view.m_list.GetChildren();
			foreach (var item in cs)
			{
				if (item.name == "select")
				{
					item.asCom.GetController("select").selectedIndex = 1;
					_effect = EffectSystem.Instance.AddEffect(11, item);
				}
				else
					item.asCom.GetController("select").selectedIndex = 2;
			}
			m_view.m_show.Play(() =>
			{

				_mask = false;
				_call?.Invoke();
			});
		}

		void ClickMask()
		{
			if (!_mask)
				SGame.UIUtils.CloseUIByID(__id);
			else
				_imm = true;
		}

		partial void UnInitLogic(UIContext context)
		{
			_list?.Clear();
			_list = null;
			_call = null;
			_select = null;
			if (_effect != default)
				EffectSystem.Instance.ReleaseEffect(_effect);
			_effect = default;
		}
	}
}
