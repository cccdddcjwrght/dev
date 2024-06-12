/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_PassItem : GComponent
    {
        public Controller m_dir;
        public Controller m_isMeet;
        public GLoader m_icon;
        public GTextField m_name;
        public GButton m_btn;
        public GImage m_lock;
        public const string URL = "ui://cxpm3jfb9n6p34";

        public static UI_PassItem CreateInstance()
        {
            return (UI_PassItem)UIPackage.CreateObject("EnterScene", "PassItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_dir = GetControllerAt(0);
            m_isMeet = GetControllerAt(1);
            m_icon = (GLoader)GetChildAt(2);
            m_name = (GTextField)GetChildAt(3);
            m_btn = (GButton)GetChildAt(4);
            m_lock = (GImage)GetChildAt(5);
        }
    }
}