
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Club;
	
	public partial class UIClubCreate
	{
		int m_HeadId = 1;
		int m_FrameId = 1;

		partial void InitLogic(UIContext context){

			UpdateCreateDiamond();
			UpdateClubHead(m_HeadId, m_FrameId);
		}

		public void UpdateCreateDiamond() 
		{
			m_view.m_currencyValue.SetText("X" + DataCenter.ClubUtil.CREATE_DIAMOND);
		}

		/// <summary>
		/// 更新俱乐部头像
		/// </summary>
		/// <param name="headId"></param>
		/// <param name="frameId"></param>
		public void UpdateClubHead(int headId, int frameId) 
		{
			m_view.m_clubIcon.SetData(headId, frameId);
			m_HeadId = headId;
			m_FrameId = frameId;
		}

        partial void OnResetClick(EventContext data)
        {
			SGame.UIUtils.OpenUI("clubselect", m_HeadId, m_FrameId);
		}

        partial void OnCreateBtnClick(EventContext data)
        {
			string clubName = m_view.m_input.text;
			if (clubName == string.Empty)
			{
				"@ui_club_tip_input".Tips();
				return;
			}

			if (!PropertyManager.Instance.CheckCount((int)ItemID.DIAMOND, DataCenter.ClubUtil.CREATE_DIAMOND, 1))
			{
				"@ui_club_tip_noenough".Tips();
				return;
			}

			var costStr = Utils.GetItemName(1, (int)ItemID.DIAMOND) + "X" + DataCenter.ClubUtil.CREATE_DIAMOND;
			SGame.UIUtils.Confirm("@ui_club_title_warn", string.Format(UIListener.Local("ui_club_hint_create"), costStr, clubName), (index) =>
			{
				if(index == 0)RequestExcuteSystem.Instance.CreateClubReq(clubName, m_HeadId, m_FrameId).Start();
			}, new string[] { "@ui_club_btn_confirm1", "@ui_club_btn_cancel1" });
		}

        partial void UnInitLogic(UIContext context){

		}
	}
}
