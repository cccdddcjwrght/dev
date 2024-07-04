/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_HotFoodBtn : GButton
    {
        public Controller m_hoting;
        public GLoader m_hotFood;
        public GImage m_progress;
        public const string URL = "ui://ktixaqljvlyvlbl";

        public static UI_HotFoodBtn CreateInstance()
        {
            return (UI_HotFoodBtn)UIPackage.CreateObject("Main", "HotFoodBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_hoting = GetControllerAt(0);
            m_hotFood = (GLoader)GetChildAt(2);
            m_progress = (GImage)GetChildAt(3);
        }
    }
}