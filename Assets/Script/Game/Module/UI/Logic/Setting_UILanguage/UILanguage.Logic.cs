
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Setting;
	
	public partial class UILanguage
	{
		private SetData setData;
		private int index;
		partial void InitLogic(UIContext context)
		{
			setData = DataCenter.Instance.setData;
			m_view.m_list.selectedIndex = setData.GetIntItemData("language");
			index = m_view.m_list.selectedIndex;
		}

		partial void OnConfirmClick(EventContext data)
		{
			index = m_view.m_list.selectedIndex;
			setData.SetIntItemData("language", index);
			SGame.UIUtils.CloseUIByID(__id);
		}
		partial void UnInitLogic(UIContext context){

		}
	}
}
