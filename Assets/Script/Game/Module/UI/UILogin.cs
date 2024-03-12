using SGame.UI;
using log4net;
using SGame.UI.Login;
using Unity.Entities;
using UnityEngine;
using System;
using SGame.Http;
using Unity.Entities.UniversalDelegates;

namespace SGame
{

	public class UILogin : IUIScript
	{
		private static ILog log = LogManager.GetLogger("xl.gameui");
		private FairyGUI.GTextField m_text;
		private UI_Login m_view;

		public void OnInit(UIContext context)
		{
			m_view = context.content as UI_Login;
			m_view.m_btn_login.onClick.Add(OnClick);
			m_view.m_account.text = PlayerPrefs.GetString("user", "test");
			UI.UIUtils.SetLogo(m_view);
		}


		// 按下按钮
		public void OnClick()
		{
			EventManager.Instance.Trigger((int)GameEvent.ENTER_LOGIN, "aaa");
			
			if (!string.IsNullOrEmpty(m_view.m_account.text))
			{
				PlayerPrefs.SetString("user", m_view.m_account.text);

				//
				// WaitHttp.Request("login", HttpMethod.GET)
				// 	.SetData(0, "user_id", m_view.m_account.text)
				// 	.OnSuccess((w, data) =>
				// 	{
				// 		if(w.TryGet<AccountData>(out var r))
				// 		{
				// 			log.Info("log success");
				// 			r.account = m_view.m_account.text;
				// 			r.platform = (int)Application.platform;
				// 			DataCenter.Instance.accountData = r;
				// 			DataCenter.Instance.SetData<Account>(r.To());
				// 			HttpSystem.Instance.SetToken(r.token);
				// 			EventManager.Instance.Trigger((int)GameEvent.ENTER_LOGIN, r.account);
				// 		}
				// 	})
				// 	.OnFail((d) => {
				// 		log.Error(d);
				// 		EventManager.Instance.Trigger((int)GameEvent.ENTER_LOGIN, "1111");
				//
				// 	})
				// 	.Timeout(3f)
				// 	.RunAndWait();

			}
			
		}

		public static IUIScript Create() { return new UILogin(); }

	}
}
