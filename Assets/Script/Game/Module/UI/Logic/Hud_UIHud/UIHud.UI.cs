
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Hud;
	
	public partial class UIHud
	{
		partial void InitUI(UIContext context){
			UIListener.ListenerIcon(m_view.m_rrr, new EventCallback1(_OnRrrClick));
			UIListener.ListenerIcon(m_view.m_pgr, new EventCallback1(_OnPgrClick));
			UIListener.ListenerIcon(m_view.m_tr, new EventCallback1(_OnTrClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.ListenerIcon(m_view.m_rrr, new EventCallback1(_OnRrrClick),remove:true);
			UIListener.ListenerIcon(m_view.m_pgr, new EventCallback1(_OnPgrClick),remove:true);
			UIListener.ListenerIcon(m_view.m_tr, new EventCallback1(_OnTrClick),remove:true);

		}
		void SetFloatText_TitleText(string data)=>UIListener.SetText(m_view.m_rrr.m_title,data);
		string GetFloatText_TitleText()=>UIListener.GetText(m_view.m_rrr.m_title);
		void _OnRrrClick(EventContext data){
			OnRrrClick(data);
		}
		partial void OnRrrClick(EventContext data);
		void _OnPgrClick(EventContext data){
			OnPgrClick(data);
		}
		partial void OnPgrClick(EventContext data);
		void SetPgrValue(float data)=>UIListener.SetValue(m_view.m_pgr,data);
		float GetPgrValue()=>UIListener.GetValue(m_view.m_pgr);
		void _OnTrClick(EventContext data){
			OnTrClick(data);
		}
		partial void OnTrClick(EventContext data);

	}
}
