using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame.UI.Shop
{
	partial class UI_Probability
	{
		public UI_Probability SetRates(params int[] rates)
		{
			m_list.RemoveChildrenToPool();
			SGame.UIUtils.AddListItems<int>(m_list, rates, (index, v, g) =>
			{
				UIListener.SetControllerSelect(g, "color", index);
				g.visible = ((int)v) > 0;
				g.SetText(string.Format("{0:p2}", (int)v * 0.0001f), false);
			});
			return this;
		}
	}
}
