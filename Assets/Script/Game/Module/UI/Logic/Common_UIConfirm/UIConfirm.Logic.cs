
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Common;
	using System;

	public partial class UIConfirm
	{
		private object[] _args;
		private Action<int> _onClick;

		private bool _clickClose = true;

		partial void InitLogic(UIContext context)
		{

			var args = _args = context.GetParam()?.Value.To<object[]>();

			var type = args.GetArg("type").To<int>();
			var title = args.GetArg("title", "@ui_common_confirm_title")?.ToString();
			var text = args.GetArg("text")?.ToString();
			var btn = args.GetArg("btns").To<string[]>();
			var desc = args.GetArg("desc")?.ToString();
			_onClick = args.GetArg("call").To<Action<int>>();
			_clickClose = args.GetArg("clickclose", true).To<bool>();

			SetContext(args.GetArg("context")?.ToString(), args.GetArg("contextcall").To<Action<GComponent>>());
			SetBtns(btn);
			m_view.m_body.SetText(title);
			m_view.m_body.m_text.SetText(text);
			m_view.m_type.selectedIndex = type;
			m_view.m_body.m_tips.SetText(desc);
			args.GetArg("init").To<Action<GObject>>()?.Invoke(m_view);
		}

		void SetBtns(string[] btns)
		{
			if (btns == null)
			{
				m_view.m_body.m_clickstate.selectedIndex = 1;
				m_view.m_body.m_ok.SetText("@ui_btn_ok");
			}
			else
			{
				m_view.m_body.m_clickstate.selectedIndex = btns.Length;
				if (btns.Length > 0)
				{
					m_view.m_body.m_ok.SetText(btns[0]);
					m_view.m_body.m_click1.SetText(btns[0]);
				}
				if (btns.Length > 1)
					m_view.m_body.m_click2.SetText(btns[1]);

			}
		}

		void SetContext(string res, Action<GComponent> call = null)
		{
			if (string.IsNullOrEmpty(res)) return;
			m_view.m_body.m_context.url = res;
			if (call != null) call(m_view.m_body.m_context.component);
		}

		partial void OnConfirmBody_Click2Click(EventContext data)
		{
			var call = _onClick;
			DoCloseUIClick(null);
			call?.Invoke(1);
		}

		partial void OnConfirmBody_Click1Click(EventContext data)
		{
			var call = _onClick;
			if (_clickClose) DoCloseUIClick(null);
			call?.Invoke(0);
		}

		partial void OnConfirmBody_OkClick(EventContext data)
		{
			OnConfirmBody_Click1Click(null);
		}

		partial void OnUICloseClick(ref bool state)
		{
			_onClick?.Invoke(-1);
		}

		partial void UnInitLogic(UIContext context)
		{
			_args = null;
			_clickClose = false;
			_onClick = null;
		}
	}
}
