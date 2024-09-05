
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Hud;
	using System.Data;

	public partial class UISystemTip
	{
		private float _oldY;
		private GObject _tipsItem;
		private GObject _mask;
		private float _oldHeight;

		partial void InitLogic(UIContext context)
		{
			_tipsItem = m_view.GetChild("n3");
			_mask = m_view.GetChild("n2");
			_mask.SetPivot(0, 0.5f);
			_oldHeight = _mask.height;
			_oldY = _tipsItem.y;

			m_view.visible = false;
			m_view.z = -550;
			context.window.AddEventListener("UpdateTip", OnUpdateTip);
			context.content.xy = Vector2.zero;
			if (context.gameWorld.GetEntityManager().HasComponent<UIParam>(context.entity))
			{
				UIParam param = context.gameWorld.GetEntityManager().GetComponentObject<UIParam>(context.entity);
				DoShow(param.Value);
			}
			else
			{
				m_view.visible = false;
			}
		}

		void DoShow(object value)
		{
			if (value is string s)
				ShowText(s);
			else if (value is object[] args && args.Length > 0)
			{
				var time = args.Val<float>(1);
				ShowText(args[0].ToString(), time);
			}
		}

		void ShowText(string name, float delay = 0)
		{
			log.Debug("show tips=" + name + " delay=" + delay);
			if (string.IsNullOrEmpty(name) || name == "-1")
			{
				m_view.m_myfloat.Stop(setToComplete: false, false);
				m_view.visible = false;
				return;
			}

			m_view.m_myfloat.Stop(false, false);
			m_view.visible = true;
			m_view.m_title.text = name;
			_mask.height = _oldHeight;
			_tipsItem.y = _oldY;
			_tipsItem.alpha = 0f;
			if (delay > 0)
			{
				_tipsItem.TweenFade(1, 0.2f);
				m_view.m_myfloat.Play(1, delay, startTime: 0.3f, -1f, OnCompleted);
				_mask.height = 3000;
			}
			else
				m_view.m_myfloat.Play(1, Time.deltaTime, OnCompleted);
		}

		void OnUpdateTip(EventContext fcontex)
		{
			DoShow(fcontex.data);
		}

		partial void UnInitLogic(UIContext context)
		{
			m_view.m_myfloat.Stop();
		}

		void OnCompleted()
		{
			m_view.visible = false;
		}

	}
}
