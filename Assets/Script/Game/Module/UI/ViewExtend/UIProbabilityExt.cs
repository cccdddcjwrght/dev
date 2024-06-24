using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame.UI.Shop
{
	partial class UI_Probability
	{
		public UI_Probability SetRates(int[] rates)
		{
			List<int> rateList = new List<int>();
			List<int> colors = new List<int>();
			for (int i = 0; i < rates.Length; i++)
			{
				if (rates[i] > 0)
				{
					rateList.Add(rates[i]);
					colors.Add(i);
				}
			}

			m_list.RemoveChildrenToPool();
			SGame.UIUtils.AddListItems<int>(m_list, rateList.ToArray(), (index, v, g) =>
			{
				UIListener.SetControllerSelect(g, "color", colors[index]);
				g.visible = ((int)v) > 0;
				g.SetText(string.Format("{0:p2}", (int)v * 0.0001f), false);
			});
			return this;
		}
	}
}
