/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_HotFoodBtn : GButton
    {
        public Controller m_hoting;
        public Controller m_cd;
        public GImage m_progress;
        public GImage m_cdMask;
        public const string URL = "ui://ktixaqljvlyvlbl";

        public static UI_HotFoodBtn CreateInstance()
        {
            return (UI_HotFoodBtn)UIPackage.CreateObject("Main", "HotFoodBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_hoting = GetControllerAt(0);
            m_cd = GetControllerAt(1);
            m_progress = (GImage)GetChildAt(3);
            m_cdMask = (GImage)GetChildAt(6);
        }
    }
}