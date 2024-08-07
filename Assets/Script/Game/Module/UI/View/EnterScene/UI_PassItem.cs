/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_PassItem : GComponent
    {
        public Controller m_dir;
        public Controller m_isMeet;
        public Controller m_max;
        public Controller m_showFood;
        public GLoader m_icon;
        public GImage m_leftBar;
        public GImage m_rightBar;
        public GLoader m_goBtn;
        public GGroup m_group;
        public GTextField m_name;
        public GList m_foodList;
        public Transition m_right;
        public Transition m_left;
        public Transition m_t2;
        public Transition m_t3;
        public Transition m_t4;
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
            m_max = GetControllerAt(2);
            m_showFood = GetControllerAt(3);
            m_icon = (GLoader)GetChildAt(2);
            m_leftBar = (GImage)GetChildAt(5);
            m_rightBar = (GImage)GetChildAt(6);
            m_goBtn = (GLoader)GetChildAt(11);
            m_group = (GGroup)GetChildAt(12);
            m_name = (GTextField)GetChildAt(16);
            m_foodList = (GList)GetChildAt(17);
            m_right = GetTransitionAt(0);
            m_left = GetTransitionAt(1);
            m_t2 = GetTransitionAt(2);
            m_t3 = GetTransitionAt(3);
            m_t4 = GetTransitionAt(4);
        }
    }
}