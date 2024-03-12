
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.SceneDecor;
	using GameConfigs;

	public partial class UIDecor
	{
		private RoomRowData _cfg;
		private float _adjust;
		private GObject _wall;

		partial void InitLogic(UIContext context)
		{
			ConfigSystem.Instance.TryGet<RoomRowData>(DataCenter.Instance.roomData.current.id, out _cfg);
			_adjust = _cfg.Adjust == 0 ? 1 : _cfg.Adjust;
			m_view.m_loader.visible = false;
			var child = _wall = SGame.UIUtils.AddListItem(m_view, res: _cfg.Decor);
			child.pivotAsAnchor= true;
			child.SetPivot(0.5f, 0.5f);
			child.touchable = false;
			child.AddRelation(m_view, RelationType.Width);
			child.AddRelation(m_view, RelationType.Height);
			
			context.onUpdate += SetY;
		}

		partial void UnInitLogic(UIContext context)
		{
			context.onUpdate -= SetY;
		}

		void SetY(UIContext context)
		{
			var v = SceneCameraSystem.Instance.zMove.Rate();
			if (v > 0)
				_wall.y = Screen.height * (v - 1) * _adjust;
		}
	}
}
