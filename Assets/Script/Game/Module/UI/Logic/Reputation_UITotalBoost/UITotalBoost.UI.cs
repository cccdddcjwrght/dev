
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Reputation;
	
	public partial class UITotalBoost
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);

		}
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void SetWorkerText(string data)=>UIListener.SetText(m_view.m_worker,data);
		string GetWorkerText()=>UIListener.GetText(m_view.m_worker);
		void SetCustomerText(string data)=>UIListener.SetText(m_view.m_customer,data);
		string GetCustomerText()=>UIListener.GetText(m_view.m_customer);
		void SetTotalNumText(string data)=>UIListener.SetText(m_view.m_totalNum,data);
		string GetTotalNumText()=>UIListener.GetText(m_view.m_totalNum);

	}
}
