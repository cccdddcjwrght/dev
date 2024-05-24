using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame.UI.Club 
{
    public partial class UI_ClubIcon
    {
        public void SetData(int headId, int frameId) 
        {
            SetHead(headId);
            SetFrame(frameId);
        }

        public void SetHead(int headId) 
        {
            var cfg = DataCenter.ClubUtil.GetClubHeadCfg(headId);
            m_head.SetIcon(cfg.Icon);
        }

        public void SetFrame(int frameId) 
        {
            var cfg = DataCenter.ClubUtil.GetClubFrameCfg(frameId);
            m_frame.SetIcon(cfg.Icon);
        }
    }
}

