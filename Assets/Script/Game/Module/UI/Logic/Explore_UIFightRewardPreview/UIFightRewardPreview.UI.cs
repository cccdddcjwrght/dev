
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	
	public partial class UIFightRewardPreview
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_ad.onChanged.Add(new EventCallback1(_OnAdChanged));
			UIListener.ListenerClose(m_view.m_mask, new EventCallback1(DoCloseUIClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_ad.onChanged.Remove(new EventCallback1(_OnAdChanged));
			UIListener.ListenerClose(m_view.m_mask, new EventCallback1(DoCloseUIClick),remove:true);

		}
		void _OnAdChanged(EventContext data){
			OnAdChanged(data);
		}
		partial void OnAdChanged(EventContext data);
		void SwitchAdPage(int index)=>m_view.m_ad.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);

	}
}
