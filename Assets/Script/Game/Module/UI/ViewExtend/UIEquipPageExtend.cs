using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using GameConfigs;
using UnityEngine;

namespace SGame.UI.Player
{
	public partial class UI_EquipPage
	{
		private GoWrapper goWrapper;
		private SwipeGesture swipe;
		private RoleData roleData;

		public UI_EquipPage Init(Action<int, GObject> eqclick)
		{

			m_eq1.onClick.Clear();
			m_eq1.onClick.Add(() => eqclick?.Invoke(1, m_eq1));
			m_eq2.onClick.Clear();
			m_eq2.onClick.Add(() => eqclick?.Invoke(2, m_eq2));
			m_eq3.onClick.Clear();
			m_eq3.onClick.Add(() => eqclick?.Invoke(3, m_eq3));
			m_eq4.onClick.Clear();
			m_eq4.onClick.Add(() => eqclick?.Invoke(4, m_eq4));
			m_eq5.onClick.Clear();
			m_eq5.onClick.Add(() => eqclick?.Invoke(5, m_eq5));


			swipe = new SwipeGesture(m_model);
			swipe.onMove.Add(OnTouchMove);
			goWrapper = new GoWrapper();
			m_holder.SetNativeObject(goWrapper);

			m_attrbtn.onClick.Add(OnAttrBtnClick);
			return this;
		}

		public void Uninit()
		{
			goWrapper?.Dispose();
			goWrapper = null;
			m_attrbtn.onClick.Clear();
		}

		public UI_EquipPage SetInfo(RoleData role)
		{
			roleData = role;
			return this;
		}

		public UI_EquipPage SetEquipInfo()
		{
			var index = roleData == null ? 1 : 0;
			m_attr.SetTextByKey("ui_player_base_attr", DataCenter.EquipUtil.GetRoleEquipAddValue(roleData != null ? roleData.equips : null));

			IList<BaseEquip> eqs = roleData != null ? roleData.equips : DataCenter.Instance.equipData.equipeds;
			UIListenerExt.SetEquipInfo(m_eq1, eqs[index]);
			UIListenerExt.SetEquipInfo(m_eq2, eqs[index + 1]);
			UIListenerExt.SetEquipInfo(m_eq3, eqs[index + 2]);
			UIListenerExt.SetEquipInfo(m_eq4, eqs[index + 3]);
			UIListenerExt.SetEquipInfo(m_eq5, eqs[index + 4]);
			return this;
		}

		public UI_EquipPage RefreshModel()
		{
			CreateRole().Start();
			return this;
		}

		private void OnAttrBtnClick()
		{
			SGame.UIUtils.OpenUI("propertyinfo", roleData);
		}

		IEnumerator CreateRole()
		{
			yield return null;
			var wait = Utils.GenCharacter(DataCenter.EquipUtil.GetRoleEquipString(roleData != null ? roleData.roleTypeID : 0));
			yield return wait;
			var go = wait.Current as GameObject;
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



		void OnTouchMove(EventContext context)
		{
			goWrapper.wrapTarget.transform.Rotate(Vector3.up, -swipe.delta.x);

		}
	}
}
