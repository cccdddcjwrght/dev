using FairyGUI;
using SGame.UI.Common;

namespace SGame 
{
    public static class PopUtil
    {
        static UI_PopTip __popTip = (UI_PopTip)UIPackage.CreateObject("Common", "PopTip");

        public static void PopTip(GObject target,string tip) 
        {
            __popTip.m_info.SetText(tip);
            GRoot.inst.ShowPopup(__popTip, target, PopupDirection.Up);
        }
    }
}


