
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Setting;
	
	public partial class UIChangeHead
	{
		private AccountData _accountData;
		private int headID;
		private int freamID;

		partial void BeforeInit(UIContext context)
		{
			_accountData = DataCenter.Instance.accountData;
			headID = _accountData.head;
			freamID = _accountData.frame;
		}

		
		partial void InitLogic(UIContext context)
		{
			m_view.m_State.selectedIndex = 0;
			m_view.m_list.itemRenderer = OnItemSet;
		}
		
         /// <summary>
         /// 头像头像框渲染
         /// </summary>
         /// <param name="index"></param>
         /// <param name="item"></param>
		private void OnItemSet(int index, GObject item)
		{
			
		}

		partial void UnInitLogic(UIContext context){

		}
	}
}
