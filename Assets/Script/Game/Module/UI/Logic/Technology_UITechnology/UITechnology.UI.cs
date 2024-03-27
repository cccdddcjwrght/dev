
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Technology;
	
	public partial class UITechnology
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.Listener(m_view.m_clickBtn, new EventCallback1(_OnClickBtnClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.Listener(m_view.m_clickBtn, new EventCallback1(_OnClickBtnClick),remove:true);

		}
		void SetTechFrameText(string data)=>UIListener.SetText(m_view.m_techFrame,data);
		string GetTechFrameText()=>UIListener.GetText(m_view.m_techFrame);
		void _OnClickBtnClick(EventContext data){
			OnClickBtnClick(data);
		}
		partial void OnClickBtnClick(EventContext data);

	}
}
