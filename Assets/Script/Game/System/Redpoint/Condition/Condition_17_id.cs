using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using FlatBuffers;
using SGame.UI;
using SGame.UI.Main;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// id：17
	/// 宝箱
	/// </summary>
	public class Condition_17_id : IConditonCalculator
	{
		const string def_icon = "ui_icon_box_03";

		private GObject _icon;
		private double _num;
		private string _icStr;
		private bool _red;
		private float _time;

		public Condition_17_id()
		{
			EventManager.Instance.Reg<int>(((int)GameEvent.AFTER_ENTER_ROOM), (a) => Clear());
			EventManager.Instance.Reg(((int)GameEvent.GAME_MAIN_REFRESH), () => Refresh());

		}


		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			if (target is UI_ActBtn g)
			{
				g.m_redpoint.visible = false;
				_icon = g.GetChild("icon").asLoader.component?.GetChild("body");
			}
			return Refresh();

		}

		bool Refresh()
		{
			var num = ChestItemUtil.GetChestCount();
			var ic = ChestItemUtil.GetIcon() ?? def_icon;
			var s = num > 0;
			if (_icon != null)
			{
				if (s)
				{
					_icon.visible = true;
					_icon.SetText(num.ToString(), false);
					_icon.SetIcon(ic);
					UIListener.SetControllerSelect(_icon, "hide", s ? 0 : 1, false);
					_icStr = ic;
					_red = s;
				}
				else
				{
					_icon.visible = false;
					_icon.icon = null;
					_icon = null;
					Clear();
				}

			}
			else Clear();
			return s;
		}

		void Clear()
		{
			_icStr = null;
			_red = false;
		}

	}

}