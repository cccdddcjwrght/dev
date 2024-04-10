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

		private Action<int, GObject> eqclick;

		public UI_EquipPage Init(Action<int, GObject> eqclick)
		{

			this.eqclick = eqclick;
			m_eq1.onClick.Clear();
			m_eq1.onClick.Add((e) => OnEqClick(1, e));
			m_eq2.onClick.Clear();
			m_eq2.onClick.Add((e) => OnEqClick(2, e));
			m_eq3.onClick.Clear();
			m_eq3.onClick.Add((e) => OnEqClick(3, e));
			m_eq4.onClick.Clear();
			m_eq4.onClick.Add((e) => OnEqClick(4, e));
			m_eq5.onClick.Clear();
			m_eq5.onClick.Add((e) => OnEqClick(5, e));
			m_eq6.onClick.Clear();
			m_eq6.onClick.Add((e) => OnEqClick(6, e));

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
			RefreshAttr();
			if (roleData == null)
			{
				IList<BaseEquip> eqs = DataCenter.Instance.equipData.equipeds;

				UIListenerExt.SetEquipInfo(m_eq1, eqs[1],true , 1);
				UIListenerExt.SetEquipInfo(m_eq2, eqs[2], true, 2);
				UIListenerExt.SetEquipInfo(m_eq3, eqs[3], true, 3);
				UIListenerExt.SetEquipInfo(m_eq4, eqs[4], true, 4);
				UIListenerExt.SetEquipInfo(m_eq5, eqs[5], true, 5);
				UIListenerExt.SetEquipInfo(m_eq6, eqs[6], true, 6);
			}
			else
			{
				for (int i = 1; i <= 6; i++)
				{
					var e = roleData.equips.Find(e => e.type == i);
					UIListenerExt.SetEquipInfo(GetChild("eq" + i), e, true , i);
				}
			}

			return this;
		}

		public UI_EquipPage RefreshAttr()
		{
			m_attr.SetTextByKey("ui_player_base_attr", DataCenter.EquipUtil.GetRoleEquipAddValue(roleData != null ? roleData.equips : null));
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


		private void OnEqClick(int index, EventContext context)
		{
			eqclick?.Invoke(index, context.sender as GObject);
			if (roleData != null && roleData.isEmployee)
			{
				var e = roleData.equips.Find(e => e != null && e.type == index);
				if (e != null)
					SGame.UIUtils.OpenUI("eqtipsui", e, false);
			}
		}

		IEnumerator CreateRole()
		{
			yield return null;
			var wait = Utils.GenCharacter(DataCenter.EquipUtil.GetRoleEquipString(roleData != null ? roleData.roleTypeID : 0, roleData?.equips));
			yield return wait;
			var go = wait.Current as GameObject;
			if (go)
			{
				if (goWrapper != null)
				{
					var old = goWrapper.wrapTarget;
					if (old) GameObject.Destroy(old);
					goWrapper.SetWrapTarget(go, false);
					go.transform.localScale = Vector3.one * 300;
					go.transform.localRotation = Quaternion.Euler(0, -145, 0);
					go.SetLayer("UILight");
					yield break;
				}
				GameObject.Destroy(go);
			}
		}



		void OnTouchMove(EventContext context)
		{
			goWrapper.wrapTarget.transform.Rotate(Vector3.up, -swipe.delta.x);

		}
	}
}
