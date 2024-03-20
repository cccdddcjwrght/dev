
using System.Collections.Generic;
using SGame.UI.Common;

namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Setting;
	
	public partial class UIChangeHead
	{
		private AccountData _accountData;
		private SetData     _setData;
		private GList list;
		private List<SetData.HeadFrameData> headFramelist;


		partial void BeforeInit(UIContext context)
		{
			_accountData = DataCenter.Instance.accountData;
			_setData = DataCenter.Instance.setData;
		}

		
		partial void InitLogic(UIContext context)
		{
			m_view.m_State.selectedIndex = 0;
			list=m_view.m_list;
			headFramelist = _setData.headDataList;
			list.itemRenderer = OnSetHeadList;
			list.numItems = headFramelist.Count;
		}
		

		partial void OnStateChanged(EventContext data)
		{
			if (m_view.m_State.selectedIndex == 0)
			{
				headFramelist = _setData.headDataList;
				
				
			}
			else
			{
				headFramelist = _setData.freamDataList;
			}
			list.numItems = headFramelist.Count;
		}
		
         /// <summary>
         /// 头像头像框渲染
         /// </summary>
         /// <param name="index"></param>
         /// <param name="item"></param>
		private void OnSetHeadList(int index, GObject item)
         {
	        var headData= headFramelist[index];
	        var obj = item as UI_SimpleHeadIcon;
	        item.onClick.Set(()=>
	        {
		        
		        var showHead = m_view.m_icon as UI_HeadBtn;
		        if (m_view.m_State.selectedIndex == 0)
		        {
			        showHead.m_headImg.url=string.Format("ui://IconHead/{0}",headData.icon);
			        _accountData.head = headData.id;
		        }
		        else
		        {
			        showHead.m_frame.url=string.Format("ui://IconHead/{0}",headData.icon);
			        _accountData.frame = headData.id;
		        }
		        list.numItems = headFramelist.Count;
		        EventManager.Instance.Trigger(((int)GameEvent.SETTING_UPDATE_HEAD));
	        });
	        
			if (m_view.m_State.selectedIndex == 0)
			{
				obj.m_state.selectedIndex = 1;
				var head = obj.m_body as UI_HeadBtn;
				head.m_headImg.url=string.Format("ui://IconHead/{0}",headData.icon);
			}
			else
			{
				obj.m_state.selectedIndex = 0;
				var head = obj.m_body as UI_HeadBtn;
				head.m_frame.url=string.Format("ui://IconHead/{0}",headData.icon);
			}
			
			if (_accountData.GetHead() == headData.id || _accountData.GetFrame() == headData.id)
			{
				obj.m_check.selectedIndex = 1;
			}
			else
			{
				obj.m_check.selectedIndex = 0;
			}
         }

		partial void UnInitLogic(UIContext context){

		}
	}
}
