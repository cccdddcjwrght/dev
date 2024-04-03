/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.RoomExclusive
{
    public partial class UI_RoomExclusiveUI : GLabel
    {
        public GGraph m_mask;
        public GTextField m_select;
        public GTextField m_tip;
        public GList m_list;
        public GGroup m_group;
        public const string URL = "ui://z18uq7fxf6i31";

        public static UI_RoomExclusiveUI CreateInstance()
        {
            return (UI_RoomExclusiveUI)UIPackage.CreateObject("RoomExclusive", "RoomExclusiveUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_mask = (GGraph)GetChildAt(0);
            m_select = (GTextField)GetChildAt(2);
            m_tip = (GTextField)GetChildAt(3);
            m_list = (GList)GetChildAt(4);
            m_group = (GGroup)GetChildAt(5);
        }
    }
}