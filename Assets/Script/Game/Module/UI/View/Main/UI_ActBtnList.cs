/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_ActBtnList : GComponent
    {
        public Controller m_side;
        public GList m_right;
        public const string URL = "ui://ktixaqljgmj1v";

        public static UI_ActBtnList CreateInstance()
        {
            return (UI_ActBtnList)UIPackage.CreateObject("Main", "ActBtnList");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_side = GetControllerAt(0);
            m_right = (GList)GetChildAt(0);
        }
    }
}