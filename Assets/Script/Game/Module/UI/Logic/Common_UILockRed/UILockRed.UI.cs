
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Common;
	
	public partial class UILockRed
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_type.onChanged.Add(new EventCallback1(_OnTypeChanged));
			m_view.m_flag.onChanged.Add(new EventCallback1(_OnFlagChanged));
			UIListener.Listener(m_view.m_nearflag, new EventCallback1(_OnNearflagClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			m_view.m_flag.onChanged.Remove(new EventCallback1(_OnFlagChanged));
			UIListener.Listener(m_view.m_nearflag, new EventCallback1(_OnNearflagClick),remove:true);

		}
		void _OnTypeChanged(EventContext data){
			OnTypeChanged(data);
		}
		partial void OnTypeChanged(EventContext data);
		void SwitchTypePage(int index)=>m_view.m_type.selectedIndex=index;
		void _OnFlagChanged(EventContext data){
			OnFlagChanged(data);
		}
		partial void OnFlagChanged(EventContext data);
		void SwitchFlagPage(int index)=>m_view.m_flag.selectedIndex=index;
		void _OnNearflagClick(EventContext data){
			OnNearflagClick(data);
		}
		partial void OnNearflagClick(EventContext data);
		void SetNearflagText(string data)=>UIListener.SetText(m_view.m_nearflag,data);
		string GetNearflagText()=>UIListener.GetText(m_view.m_nearflag);

	}
}
