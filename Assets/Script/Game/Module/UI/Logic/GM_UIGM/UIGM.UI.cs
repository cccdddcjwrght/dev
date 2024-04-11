
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.GM;
	
	public partial class UIGM
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_c1.onChanged.Add(new EventCallback1(_OnC1Changed));
			UIListener.ListenerIcon(m_view.m_btnGM, new EventCallback1(_OnBtnGMClick));
			UIListener.ListenerIcon(m_view.m_excute, new EventCallback1(_OnExcuteClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_c1.onChanged.Remove(new EventCallback1(_OnC1Changed));
			UIListener.ListenerIcon(m_view.m_btnGM, new EventCallback1(_OnBtnGMClick),remove:true);
			UIListener.ListenerIcon(m_view.m_excute, new EventCallback1(_OnExcuteClick),remove:true);

		}
		void _OnC1Changed(EventContext data){
			OnC1Changed(data);
		}
		partial void OnC1Changed(EventContext data);
		void SwitchC1Page(int index)=>m_view.m_c1.selectedIndex=index;
		void _OnBtnGMClick(EventContext data){
			OnBtnGMClick(data);
		}
		partial void OnBtnGMClick(EventContext data);
		void SetC2_InputText(string data)=>UIListener.SetText(m_view.m_excute.m_input,data);
		string GetC2_InputText()=>UIListener.GetText(m_view.m_excute.m_input);
		void _OnExcuteClick(EventContext data){
			OnExcuteClick(data);
		}
		partial void OnExcuteClick(EventContext data);
		void SetLblLevelText(string data)=>UIListener.SetText(m_view.m_lblLevel,data);
		string GetLblLevelText()=>UIListener.GetText(m_view.m_lblLevel);

	}
}
