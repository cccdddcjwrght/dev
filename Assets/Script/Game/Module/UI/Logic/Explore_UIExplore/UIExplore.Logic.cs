
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	using SGame.UI.Common;
	using SGame.UI.Pet;
	using System;
	using System.Collections;

	public partial class UIExplore
	{
		private Action<UIContext> onOpen;
		private Action<UIContext> onHide;
		private Action<UIContext> onClose;

		private EventHandleContainer eventHandle = new EventHandleContainer();

		private ExploreData exploreData { get { return DataCenter.Instance.exploreData; } }

		partial void InitLogic(UIContext context)
		{
			InitBar();
			InitMap();
			InitInfo();
			InitPlay();
		}

		partial void DoOpen(UIContext context)
		{
			onOpen?.Invoke(context);
			SetHead();
		}

		partial void DoHide(UIContext context)
		{
			onHide?.Invoke(context);
			MapLoop(true);
		}

		partial void UnInitLogic(UIContext context)
		{
			onClose?.Invoke(context);
			eventHandle?.Close();
			eventHandle = null;
		}

		partial void OnToolClick(EventContext data)
		{
			SGame.UIUtils.OpenUI("exploretool");
		}

		partial void OnHelpClick(EventContext data)
		{
			SGame.UIUtils.OpenUI("fightinfo", exploreData.explorer);
		}



		void InitBar()
		{
			var bar = m_view.m_topbar as UI_CurrencyHead;
			if (bar != null)
			{
				bar.SetCurrency(1, "gold", iconCtr: "1");
				bar.SetCurrency(2, "diamond", iconCtr: "2");
			}

		}

		void SetHead()
		{
			var head = m_view.m_topbar.GetChildByPath("head") as UI_HeadBtn;
			if (head != null)
			{
				head.m_headImg.url = string.Format("ui://IconHead/{0}", DataCenter.Instance.setData.GetHeadFrameIcon(1, DataCenter.Instance.accountData.GetHead()));
				head.m_frame.url = string.Format("ui://IconHead/{0}", DataCenter.Instance.setData.GetHeadFrameIcon(2, DataCenter.Instance.accountData.GetFrame()));
			}
		}

		

	}
}
