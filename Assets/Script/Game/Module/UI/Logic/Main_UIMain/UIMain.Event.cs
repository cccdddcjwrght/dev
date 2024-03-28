using SGame.UI.Common;
using Unity.Entities;

namespace SGame.UI
{
	using FairyGUI;
	using SGame;

	public partial class UIMain
	{
		private EventHandleContainer m_handles = new EventHandleContainer();
		private GList leftList;
		private SetData m_setData;

		partial void InitEvent(UIContext context)
		{
			m_setData			= DataCenter.Instance.setData;
			leftList= m_view.m_leftList.m_left;
			leftList.opaque = false;
			var headBtn = m_view.m_head;
			leftList.itemRenderer += RenderListItem;
			leftList.numItems = 3;
			headBtn.onClick.Add(OnheadBtnClick);
			m_handles += EventManager.Instance.Reg((int)GameEvent.PROPERTY_GOLD, OnEventGoldChange);
			m_handles += EventManager.Instance.Reg((int)GameEvent.GAME_MAIN_REFRESH, OnEventRefreshItem);
			m_handles+=EventManager.Instance.Reg(((int)GameEvent.SETTING_UPDATE_HEAD), OnHeadSetting);
			OnHeadSetting();
			OnEventRefreshItem();
		}

		private void OnHeadSetting()
		{
			var head = m_view.m_head as UI_HeadBtn;
			head.m_headImg.url=string.Format("ui://IconHead/{0}",m_setData.GetHeadFrameIcon(1,DataCenter.Instance.accountData.GetHead()));
			head.m_frame.url=string.Format("ui://IconHead/{0}",m_setData.GetHeadFrameIcon(2,DataCenter.Instance.accountData.GetFrame()));
		}

		private void OnEventRefreshItem()
		{
			if (DataCenter.Instance.guideData.isGuide)
			{
				var levelBtn = m_view.m_levelBtn;
				levelBtn.visible = 15.IsOpend(false);
				var leveltechBtn = m_view.m_taskRewardBtn;
				leveltechBtn.visible = 13.IsOpend(false);
			}
		
			var adBtn = m_view.m_AdBtn;
			adBtn.visible = 16.IsOpend(false);
			
			leftList.GetChildAt(0).visible = 12.IsOpend(false);
			leftList.GetChildAt(1).visible = 17.IsOpend(false);
			leftList.GetChildAt(2).visible = 14.IsOpend(false);
		}

		private void RenderListItem(int index, GObject item)
		{
			item.onClick.Add(() =>
			{
				switch (index)
				{
					case 0:
						"player".Goto();
						break;
					case 1:
						"technology".Goto();
						break;
					case 2:
						"friend".Goto();
						break;
				}
			});
		}


		// 金币添加事件
		void OnEventGoldChange()
		{
			SetGoldText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)ItemID.GOLD)));
			SetDiamondText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)ItemID.DIAMOND)));
		}
		
		void OnheadBtnClick(EventContext context)
		{
			Entity popupUI = UIRequest.Create(EntityManager, SGame.UIUtils.GetUI("setting"));
		}

		partial void OnTaskRewardBtnClick(EventContext data)
		{
			"leveltech".Goto();
		}

		partial void OnLevelBtnClick(EventContext data)
		{
			if (DataCenter.MachineUtil.CheckAllWorktableIsMaxLv())
			{
				SGame.UIUtils.OpenUI("enterscene", DataCenter.Instance.roomData.current.id + 1);
			}
			else
			{
				"@ui_worktable_goto_next_fail".Tips();
			}
		}

		partial void UnInitEvent(UIContext context)
		{
			m_handles.Close();
		}

		partial void OnDiamondClick(EventContext data)
		{
			"shop".Goto();
		}

	}
}
