
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Cookbook;
	using Unity.Entities;
	
	public partial class UICustomerbookFirstUp
	{
		private CustomerBookData m_data;
		private Entity m_entity;
		partial void InitLogic(UIContext context){
			var mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
			m_data = mgr.GetComponentObject<UIParam>(context.entity).Value as CustomerBookData;
			m_entity = context.entity;
			CustomerBookModule.Instance.MarkOpened(m_data);

			m_view.m_mask.onClick.Add(OnMaskClick);

			m_view.m_title.text = m_data.Name;
			m_view.m_icon.SetIcon(m_data.Icon);
		}

		void OnMaskClick()
		{
			SGame.UIUtils.CloseUI(m_entity);
			
			EntityManager mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
			string uiName = "customerbookup";
			Entity ui = UIRequest.Create(mgr, SGame.UIUtils.GetUI(uiName));
			ui.SetParam(m_data);
		}
		
		partial void UnInitLogic(UIContext context){
			//context.window.
		}
	}
}
