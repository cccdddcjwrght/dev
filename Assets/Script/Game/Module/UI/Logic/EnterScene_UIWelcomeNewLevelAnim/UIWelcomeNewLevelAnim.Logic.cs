
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

		partial void InitLogic(UIContext context)
		{

			var scale = (float)Screen.height / Screen.width;
			if (scale < 1.6f)
				m_view.m_anim.fill = FillType.ScaleMatchHeight;
			openAnim = m_view.m_anim.component as UI_OpenAnim;
			openAnim.visible = false;
			flag = false;
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
					openAnim.visible = true;
					openAnim.m_openanim2.Play(() => OnAnimCompleted().Start());
				});
			}
		}


		partial void UnInitLogic(UIContext context)
		{
			context.onUpdate -= OnUpdate;
		}

		IEnumerator OnAnimCompleted()
		{
			yield return new WaitForSeconds(1f);
			SGame.UIUtils.CloseUIByID(__id);
		}

	}
}
