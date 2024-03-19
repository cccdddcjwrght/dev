
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.EnterScene;

	public partial class UIEnterScene
	{
		private int _nextScene = 0;

		partial void InitLogic(UIContext context)
		{
			_nextScene = (context.GetParam()?.Value as object[]).Val<int>(0);
			if (_nextScene > 0)
				PlayEffect().Start();
			else
				SGame.UIUtils.CloseUIByID(__id);
		}


		System.Collections.IEnumerator PlayEffect()
		{
			var e = EffectSystem.Instance.AddEffect(7, m_view);
			yield return EffectSystem.Instance.WaitEffectLoaded(e);
			var go = EffectSystem.Instance.GetEffect(e);
			var com = go.GetComponent<EnterNextSceneComponent>().Set(_nextScene - 2).Play();
			yield return new WaitForSeconds(0.5f);
			yield return Dining.DiningRoomSystem.Instance.LoadRoom(_nextScene);
			com.continueFlag = true;
			while (com.director.time < com.director.duration * 0.9f)
				yield return null;
			EffectSystem.Instance.ReleaseEffect(e);
			SGame.UIUtils.CloseUIByID(__id);
		}

		partial void UnInitLogic(UIContext context)
		{

		}
	}
}
