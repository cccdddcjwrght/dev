
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;

	public partial class UIPlayer
	{
		private GoWrapper goWrapper;
		private SwipeGesture swipe;

		partial void InitLogic(UIContext context)
		{

			swipe = new SwipeGesture(m_view.m_model);
			swipe.onMove.Add(OnTouchMove);
			m_view.m_attr.SetText("ui_player_base_attr".Local(null, 0), false);
			goWrapper = new GoWrapper();
			m_view.m_holder.SetNativeObject(goWrapper);
			CreateRole().Start();
		}

		partial void UnInitLogic(UIContext context)
		{

			goWrapper.Dispose();
			goWrapper = null;
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
