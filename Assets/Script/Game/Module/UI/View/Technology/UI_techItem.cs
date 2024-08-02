/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Technology
{
    public partial class UI_techItem : GComponent
    {
        public Controller m_state;
        public Controller m_iconImage;
        public GLoader m_icon;
        public GTextField m_Description;
        public GTextField m_update2;
        public GTextField m_update1;
        public GImage m_up;
        public GImage m_lock;
        public GTextField m_unLock;
        public GTextField m_level;
        public GButton m_techBtn;
        public const string URL = "ui://gnu1a8dnijalg";

        public static UI_techItem CreateInstance()
        {
            return (UI_techItem)UIPackage.CreateObject("Technology", "techItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_iconImage = GetControllerAt(1);
            m_icon = (GLoader)GetChildAt(3);
            m_Description = (GTextField)GetChildAt(4);
            m_update2 = (GTextField)GetChildAt(5);
            m_update1 = (GTextField)GetChildAt(6);
            m_up = (GImage)GetChildAt(7);
            m_lock = (GImage)GetChildAt(8);
            m_unLock = (GTextField)GetChildAt(10);
            m_level = (GTextField)GetChildAt(11);
            m_techBtn = (GButton)GetChildAt(12);
        }
    }
}