
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	using System.Collections.Generic;

	public partial class UIPlayer
	{
		private GoWrapper goWrapper;
		private SwipeGesture swipe;

		private List<EquipItem> _eqs;

		partial void InitLogic(UIContext context)
		{
			swipe = new SwipeGesture(m_view.m_model);
			swipe.onMove.Add(OnTouchMove);
			m_view.m_eqTab.selectedIndex = -1;

			m_view.m_list.itemRenderer = OnSetEquipInfo;
			m_view.m_list.SetVirtual();

			goWrapper = new GoWrapper();
			m_view.m_holder.SetNativeObject(goWrapper);
			RefreshModel();
		}

		partial void DoShow(UIContext context)
		{
			m_view.m_eqTab.selectedIndex = 0;
			SetPlayerEquipsInfo();
			OnDataRefresh(false);
		}

		partial void UnInitLogic(UIContext context)
		{
			goWrapper.Dispose();
			goWrapper = null;
			DataCenter.EquipUtil.CancelAllNewFlag();
		}

		void OnDataRefresh(bool refreshtabs = true)
		{
			m_view.m_attr.SetTextByKey("ui_player_base_attr", DataCenter.EquipUtil.GetRoleEquipAddValue());
			if (refreshtabs) OnEqTabChanged(null);
		}

		partial void OnEqTabChanged(EventContext data)
		{

			switch (m_view.m_tabs.selectedIndex)
			{
				case 0: OnEquipListPage(); break;
			}
		}

		void SetPlayerEquipsInfo()
		{
			var eqs = DataCenter.Instance.equipData.equipeds;

			UIListenerExt.SetEquipInfo(m_view.m_eq1, eqs[1]);
			UIListenerExt.SetEquipInfo(m_view.m_eq2, eqs[2]);
			UIListenerExt.SetEquipInfo(m_view.m_eq3, eqs[3]);
			UIListenerExt.SetEquipInfo(m_view.m_eq4, eqs[4]);
			UIListenerExt.SetEquipInfo(m_view.m_eq5, eqs[5]);

		}

		void OnEquipListPage()
		{
			_eqs = DataCenter.Instance.equipData.items;
			_eqs.Sort((a, b) =>
			{
				var c = a.type.CompareTo(b.type);
				if (c == 0)
				{
					c = -a.quality.CompareTo(b.quality);
					if (c == 0) c = a.cfgID.CompareTo(b.cfgID);
				}
				return c;

			});
			m_view.m_list.numItems = _eqs.Count;
		}

		void OnSetEquipInfo(int index, GObject gObject)
		{

			(gObject as UI_Equip).SetEquipInfo(_eqs[index]);
			(gObject as UI_Equip).onClick.Clear();
			(gObject as UI_Equip).onClick.Add(() => OnEqClick(gObject, _eqs[index]));
		}

		void OnEqClick(GObject gObject, EquipItem data)
		{
			if (data != null && data.cfgID > 0)
			{
				data.isnew = 0;
				SGame.UIUtils.OpenUI("eqtipsui", data);
				if (gObject != null)
					UIListener.SetControllerSelect(gObject, "__redpoint", 0, false);
			}
		}

		void OnTouchMove(EventContext context)
		{
			goWrapper.wrapTarget.transform.Rotate(Vector3.up, -swipe.delta.x);

		}

		void RefreshModel()
		{
			CreateRole().Start();
		}

		System.Collections.IEnumerator CreateRole()
		{
			yield return null;
			var gen = CharacterGenerator.CreateWithConfig(DataCenter.EquipUtil.GetRoleEquipString());
			while (!gen.ConfigReady) yield return null;
			var go = gen.Generate();
			if (go)
			{
				var old = goWrapper.wrapTarget;
				if (old) GameObject.Destroy(old);
				goWrapper.SetWrapTarget(go, false);
				go.transform.localScale = Vector3.one * 300;
				go.transform.localRotation = Quaternion.Euler(0, -145, 0);
				go.SetLayer("UILight");
			}
		}

	}
}
