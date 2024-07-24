/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_LeftBtnList : GComponent
    {
        public GList m_right;
        public UI_ActBtn m_treasureBtn;
        public const string URL = "ui://ktixaqlji9gslbo";

        public static UI_LeftBtnList CreateInstance()
        {
            return (UI_LeftBtnList)UIPackage.CreateObject("Main", "LeftBtnList");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_right = (GList)GetChildAt(1);
            m_treasureBtn = (UI_ActBtn)GetChildAt(2);
        }
    }
}