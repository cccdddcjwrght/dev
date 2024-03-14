
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

			m_view.m_attr.SetText("ui_player_base_attr".Local(null, 0), false);
			m_view.m_list.itemRenderer = OnSetEquipInfo;
			m_view.m_list.SetVirtual();

			goWrapper = new GoWrapper();
			m_view.m_holder.SetNativeObject(goWrapper);
			CreateRole().Start();
		}

		partial void DoShow(UIContext context)
		{
			m_view.m_eqTab.selectedIndex = 0;
		}

		partial void UnInitLogic(UIContext context)
		{

			goWrapper.Dispose();
			goWrapper = null;
		}


		partial void OnEqTabChanged(EventContext data)
		{
			switch (m_view.m_tabs.selectedIndex)
			{
				case 0: OnEquipListPage(); break;
			}
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

			(gObject as UI_Equip).SetInfo(_eqs[index]);
		}

		void OnTouchMove(EventContext context)
		{
			goWrapper.wrapTarget.transform.Rotate(Vector3.up, -swipe.delta.x);

		}

		System.Collections.IEnumerator CreateRole()
		{
			yield return null;
			if (ConfigSystem.Instance.TryGet<GameConfigs.LevelRowData>(DataCenter.Instance.roomData.current.id, out var level))
			{
				if (ConfigSystem.Instance.TryGet<GameConfigs.RoleDataRowData>(level.PlayerId, out var role))
				{
					if (ConfigSystem.Instance.TryGet<GameConfigs.roleRowData>(role.Model, out var model))
					{
						var gen = CharacterGenerator.CreateWithConfig(model.Part);
						var go = gen.Generate();
						if (go)
						{
							var old = goWrapper.wrapTarget;
							if (old) GameObject.Destroy(old);
							goWrapper.SetWrapTarget(go, false);
							go.transform.localScale = Vector3.one * 300;
							go.transform.localRotation = Quaternion.Euler(0, -145, 0);
						}
					}
				}
			}
		}

	}
}
