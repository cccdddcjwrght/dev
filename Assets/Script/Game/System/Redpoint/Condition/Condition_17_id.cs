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
		private GObject _icon;
		private double _num;
		private string _icStr;
		private bool _red;


		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			var num = ChestItemUtil.GetChestCount();
			var ic = ChestItemUtil.GetIcon();
			var s = num > 0;

			if (target is UI_ActBtn g)
				_icon = g.GetChild("icon").asLoader.component.GetChild("body");
			if (_icon != null)
			{
				if (s)
				{
					_icon.visible = true;
					if (num != _num)
						_icon.SetText(num.ToString(), false);
					if (ic != _icStr && ic != null)
						_icon.SetIcon(ic);
					if (s != _red)
						UIListener.SetControllerSelect(_icon, "hide", s ? 0 : 1 , false);
				}
				else
				{
					_icon.visible = false;
					_icon.icon = null;
					_icon = null;
				}
				_num = num;
				_icStr = ic;
				_red = s;
			}

			return num > 0;
		}
	}

}