
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
		private int headID;
		private int freamID;

		partial void BeforeInit(UIContext context)
		{
			_accountData = DataCenter.Instance.accountData;
			_setData = DataCenter.Instance.setData;
			headID = _accountData.head;
			freamID = _accountData.frame;
		}

		
		partial void InitLogic(UIContext context)
		{
			m_view.m_State.selectedIndex = 0;
			list=m_view.m_list;
			list.RemoveChildrenToPool();
			SGame.UIUtils.AddListItems(list, _setData.headDataList, OnSetHeadList);
		
		}
		

		partial void OnStateChanged(EventContext data)
		{
			if (m_view.m_State.selectedIndex == 0)
			{
				list.RemoveChildrenToPool();
				SGame.UIUtils.AddListItems(list, _setData.headDataList, OnSetHeadList);
				
			}
			else
			{
				list.RemoveChildrenToPool();
				SGame.UIUtils.AddListItems(list, _setData.headDataList, OnSetHeadList);
			
			}
			
		}
		
         /// <summary>
         /// 头像头像框渲染
         /// </summary>
         /// <param name="index"></param>
         /// <param name="item"></param>
		private void OnSetHeadList(int index,object data, GObject item)
         {
	        var headData= data as SetData.HeadFrameData;
	         var obj = item as UI_SimpleHeadIcon;
			if (m_view.m_State.selectedIndex == 0)
			{
				var head = obj.m_body as UI_HeadBtn;
				head.m_state.selectedIndex = 1;
				head.m_headImg.url=string.Format("ui://IconHead/{0}",headData.icon);
			}
			else
			{
				var head = obj.m_body as UI_HeadBtn;
				head.m_state.selectedIndex = 0;
				head.m_frame.url=string.Format("ui://IconHead/{0}",headData.icon);
			}
		}

		partial void UnInitLogic(UIContext context){

		}
	}
}
