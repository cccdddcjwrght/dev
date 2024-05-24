
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Club;
	using GameConfigs;
	
	public partial class UIClubSelect
	{
		ClubHead clubHead;
		ClubFrame clubFrame;
		int m_HeadId;
		int m_FrameId;

		partial void InitLogic(UIContext context){

			m_view.m_headList.itemRenderer = OnHeadItemRenderer;
			m_view.m_frameList.itemRenderer = OnFrameItemRenderer;

			LoadConfigs();
			var args = context.GetParam()?.Value.To<object[]>();
			LoadHeadOrFrame((int)args[0], (int)args[1]);
		}

		public void LoadConfigs() 
		{
			clubHead = ConfigSystem.Instance.LoadConfig<ClubHead>();
			clubFrame = ConfigSystem.Instance.LoadConfig<ClubFrame>();

			m_view.m_headList.numItems = clubHead.DatalistLength;
			m_view.m_frameList.numItems = clubFrame.DatalistLength;
		}

		public void LoadHeadOrFrame(int headId, int frameId) 
		{
			m_view.m_headList.selectedIndex = headId - 1;
			m_view.m_frameList.selectedIndex = frameId - 1;
			m_view.m_clubIcon.SetHead(headId);
			m_view.m_clubIcon.SetFrame(frameId);

			m_HeadId = headId;
			m_FrameId = frameId;
		}

		public void OnHeadItemRenderer(int index, GObject gObject) 
		{
			var data = clubHead.Datalist(index).Value;
			gObject.SetIcon(data.Icon);
			gObject.onClick.Add(() =>
			{
				m_HeadId = data.Id;
				m_view.m_clubIcon.SetHead(data.Id);
				EventManager.Instance.Trigger((int)GameEvent.CLUB_HEAD_SELECT, m_HeadId, m_FrameId);
			});
		}

		public void OnFrameItemRenderer(int index, GObject gObject)
		{
			var data = clubFrame.Datalist(index).Value;
			gObject.SetIcon(data.Icon);
			gObject.onClick.Add(() =>
			{
				m_FrameId = data.Id;
				m_view.m_clubIcon.SetFrame(data.Id);
				EventManager.Instance.Trigger((int)GameEvent.CLUB_HEAD_SELECT, m_HeadId, m_FrameId);
			});
		}

		partial void UnInitLogic(UIContext context){

		}
	}
}
