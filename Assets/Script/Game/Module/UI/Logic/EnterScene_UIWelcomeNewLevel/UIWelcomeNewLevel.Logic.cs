
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.EnterScene;
	using System;
	using GameConfigs;

	public partial class UIWelcomeNewLevel
	{
		private Action _call;

		partial void InitLogic(UIContext context)
		{
			_call = (context.GetParam()?.Value as object[])?.Val<Action>(0);
			var scene = DataCenter.Instance.roomData.roomID;
			ConfigSystem.Instance.TryGet<RoomRowData>(scene, out var cfg);
			m_view.SetIcon(string.IsNullOrEmpty(cfg.StartImage) ? "ui_begin_bg_" + scene : cfg.StartImage);

		}

		partial void UnInitLogic(UIContext context)
		{

		}

		partial void OnClickClick(EventContext data)
		{
			SGame.UIUtils.CloseUIByID(__id);
			StaticDefine.G_WAIT_WELCOME = false;
			if (_call != null) _call();
		}

	}
}
