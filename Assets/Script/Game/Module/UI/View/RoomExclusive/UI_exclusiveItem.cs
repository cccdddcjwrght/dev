/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.RoomExclusive
{
    public partial class UI_exclusiveItem : GComponent
    {
        public Controller m_markState;
        public GTextField m_name;
        public GTextField m_info;
        public GLoader m_icon;
        public GLoader m_mark;
        public const string URL = "ui://z18uq7fxf6i32";

        public static UI_exclusiveItem CreateInstance()
        {
            return (UI_exclusiveItem)UIPackage.CreateObject("RoomExclusive", "exclusiveItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_markState = GetControllerAt(0);
            m_name = (GTextField)GetChildAt(2);
            m_info = (GTextField)GetChildAt(3);
            m_icon = (GLoader)GetChildAt(4);
            m_mark = (GLoader)GetChildAt(5);
        }
    }
}