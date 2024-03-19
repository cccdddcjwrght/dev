
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Setting;
	
	public partial class UISetting
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_head, new EventCallback1(_OnHeadClick));
			m_view.m_name.m_c1.onChanged.Add(new EventCallback1(_OnNameComponent_C1Changed));
			UIListener.Listener(m_view.m_name, new EventCallback1(_OnNameClick));
			m_view.m_signBtn.m_signSate.onChanged.Add(new EventCallback1(_OnSignBtn_SignSateChanged));
			UIListener.Listener(m_view.m_signBtn, new EventCallback1(_OnSignBtnClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_head, new EventCallback1(_OnHeadClick),remove:true);
			m_view.m_name.m_c1.onChanged.Remove(new EventCallback1(_OnNameComponent_C1Changed));
			UIListener.Listener(m_view.m_name, new EventCallback1(_OnNameClick),remove:true);
			m_view.m_signBtn.m_signSate.onChanged.Remove(new EventCallback1(_OnSignBtn_SignSateChanged));
			UIListener.Listener(m_view.m_signBtn, new EventCallback1(_OnSignBtnClick),remove:true);

		}
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void _OnHeadClick(EventContext data){
			OnHeadClick(data);
		}
		partial void OnHeadClick(EventContext data);
		void SetHeadText(string data)=>UIListener.SetText(m_view.m_head,data);
		string GetHeadText()=>UIListener.GetText(m_view.m_head);
		void _OnNameComponent_C1Changed(EventContext data){
			OnNameComponent_C1Changed(data);
		}
		partial void OnNameComponent_C1Changed(EventContext data);
		void SwitchNameComponent_C1Page(int index)=>m_view.m_name.m_c1.selectedIndex=index;
		void SetNameComponent___titleText(string data)=>UIListener.SetText(m_view.m_name.m___title,data);
		string GetNameComponent___titleText()=>UIListener.GetText(m_view.m_name.m___title);
		void _OnNameClick(EventContext data){
			OnNameClick(data);
		}
		partial void OnNameClick(EventContext data);
		void SetNameText(string data)=>UIListener.SetText(m_view.m_name,data);
		string GetNameText()=>UIListener.GetText(m_view.m_name);
		void SetAllNameText(string data)=>UIListener.SetText(m_view.m_allName,data);
		string GetAllNameText()=>UIListener.GetText(m_view.m_allName);
		void _OnSignBtn_SignSateChanged(EventContext data){
			OnSignBtn_SignSateChanged(data);
		}
		partial void OnSignBtn_SignSateChanged(EventContext data);
		void SwitchSignBtn_SignSatePage(int index)=>m_view.m_signBtn.m_signSate.selectedIndex=index;
		void _OnSignBtnClick(EventContext data){
			OnSignBtnClick(data);
		}
		partial void OnSignBtnClick(EventContext data);
		void SetSignBtnText(string data)=>UIListener.SetText(m_view.m_signBtn,data);
		string GetSignBtnText()=>UIListener.GetText(m_view.m_signBtn);
		void SetIdText(string data)=>UIListener.SetText(m_view.m_id,data);
		string GetIdText()=>UIListener.GetText(m_view.m_id);

	}
}
