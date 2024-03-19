using System.Linq;
using SGame.UI;
using log4net;
using SGame.UI.Common;
using SGame.UI.Setting;
using Unity.Entities.UniversalDelegates;
using UnityEngine;

namespace SGame
{

	public class UIHead : IUIScript
	{
		private static ILog log = LogManager.GetLogger("xl.gameui");
		public static IUIScript Create() { return new UIHead(); }
		private UI_HeadBtn m_view;
		private SetData _setData;
		

		public void OnInit(UIContext context)
		{
			m_view = context.content as UI_HeadBtn;
		}

		public void SetUIHead(int headId,int frameId)
		{
			string headIcon=_setData.headDataList.FirstOrDefault(item => item.id == headId)?.icon;
			string frameIcon=_setData.headDataList.FirstOrDefault(item => item.id == frameId)?.icon;

			m_view.m_headImg.url=string.Format("ui://IconHead/{0}",headIcon);
			m_view.m_frame.url=string.Format("ui://IconHead/{0}",frameIcon);

		}

	}
}
