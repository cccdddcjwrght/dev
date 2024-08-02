
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.EnterScene;
	using System.Collections;

	public partial class UIWelcomeNewLevelAnim
	{
		private UI_OpenAnim openAnim;
		private bool flag;

		int[] soundIDs = new int[] { 35,50 };

		partial void InitLogic(UIContext context)
		{

			context.window.contentPane.fairyBatching = false;
			var scale = (float)Screen.height / Screen.width;
			if (scale < 1.6f)
				m_view.m_anim.fill = FillType.ScaleMatchHeight;
			openAnim = m_view.m_anim.component as UI_OpenAnim;
			openAnim.visible = flag = false;
			m_view.onClick.Add(() => DoContinue());
			context.onUpdate += OnUpdate;
		}

		private void OnUpdate(UIContext context)
		{
			if (!flag && StaticDefine.G_WAIT_WELCOME)
				Play();
		}

		private void Play()
		{
			flag = true;
			if (openAnim != null)
			{
				GTween.To(0, 1, 0.1f).OnComplete(() =>
				{
					SetHook();
					openAnim.visible = true;
					openAnim.m_openanim2.Play(() => OnAnimCompleted().Start());
					34.ToAudioID().PlayAudio();
				});
			}
		}


		partial void UnInitLogic(UIContext context)
		{
			context.onUpdate -= OnUpdate;
		}

		private void SetHook()
		{
			if (openAnim != null)
			{
				for (int i = 1; i < 5; i++)
				{
					try
					{
						openAnim.m_openanim2.SetHook("pass" + i, DoPause);
					}
					catch (System.Exception e)
					{
						log.Warn(e.Message);
					}
				}

				for (int i = soundIDs[0]; i <= soundIDs[1];i++)
				{
					try
					{
						var id = i;
						openAnim.m_openanim2.SetHook("sound" + i, ()=> DoPlaySound(id));
					}
					catch 
					{
					}
				}
			}
		}

		private void DoPause()
		{
			if (openAnim.m_openanim2.playing)
			{
				openAnim.m_openanim2.SetPaused(true);
				LockUI(true, 0.5f);
			}
		}

		private void DoPlaySound(int id) {

			id.ToAudioID().PlayAudio();

		}

		private void DoContinue()
		{
			openAnim.m_openanim2.SetPaused(false);
			m_view.m_next.visible = false;
		}

		private void LockUI(bool state = true, float autounlock = 0)
		{
			const string _key = "newanim";
			if (state)
			{
				UILockManager.Instance.Require(_key);
				openAnim.touchable = false;
				if (autounlock > 0)
					GTween.To(0, 1, autounlock).OnComplete(() => LockUI(false));
			}
			else
			{
				openAnim.touchable = true;
				UILockManager.Instance.Release(_key);
				m_view.m_next.visible = true;
			}
		}

		IEnumerator OnAnimCompleted()
		{
			40.ToAudioID().PlayAudio();
			yield return new WaitForSeconds(1f);
			StaticDefine.G_WAIT_WELCOME = false;
			//m_view.TweenFade(0, 0.5f).OnComplete(() => SGame.UIUtils.CloseUIByID(__id));
			SGame.UIUtils.CloseUIByID(__id);
		}

	}
}
