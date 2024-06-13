
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Guide;
    using System.Collections;

    public partial class UIGuideBack
	{
		string maskMatPath = "Assets/BuildAsset/model/guide/mat/UI_Focus_Mask.mat";

		Material maskMat;
		GuideFingerHandler m_Handler;
		partial void InitLogic(UIContext context){
			context.onUpdate += OnUpdate;
			m_Handler = context.gameWorld.GetEntityManager().GetComponentObject<UIParam>(context.entity).Value as GuideFingerHandler;
			//m_view.m_mask.alpha = m_Handler.config.Alpha;

			libx.Assets.LoadAsset("Assets/BuildAsset/Art/model/guide/mat/UI_Focus_Mask.mat", typeof(Material)).Wait((a)=> 
			{
				if (string.IsNullOrEmpty(a.error)) 
				{
					var mat = a.asset as Material;
					maskMat = mat;
					m_view.m_mask.shape.material = mat;
					var size = m_Handler.GetTargetSize();
					//var pos = m_Handler.GetTargetPos();
					mat.SetFloat("_StartTime", Time.time);
					mat.SetFloat("_Radius", size.x * 0.5f);
				}
			});
		}

		private void OnUpdate(UIContext context) 
		{
			Debug.Log(Time.time);
			var size = m_Handler.GetTargetSize();
			var pos = m_Handler.GetTargetPos();

			m_view.m_blank.xy = pos;
			m_view.m_blank.size = size;
			maskMat?.SetVector("_Center", new Vector4(pos.x, -pos.y));

		}
		partial void UnInitLogic(UIContext context){
			context.onUpdate -= OnUpdate;
		}
	}
}
