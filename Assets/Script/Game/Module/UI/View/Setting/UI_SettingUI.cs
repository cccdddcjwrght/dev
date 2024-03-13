/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Setting
{
    public partial class UI_SettingUI : GComponent
    {
        public GLabel m_body;
        public GLoader m_n13;
        public GButton m_head;
        public UI_NameComponent m_name;
        public GTextField m_allName;
        public GImage m_n14;
        public GList m_list;
        public GImage m_n15;
        public UI_SignBtn m_signBtn;
        public GTextField m_id;
        public GGroup m_n8;
        public const string URL = "ui://dpgt0e2zn1bmo";

        public static UI_SettingUI CreateInstance()
        {
            return (UI_SettingUI)UIPackage.CreateObject("Setting", "SettingUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_n13 = (GLoader)GetChildAt(1);
            m_head = (GButton)GetChildAt(2);
            m_name = (UI_NameComponent)GetChildAt(3);
            m_allName = (GTextField)GetChildAt(4);
            m_n14 = (GImage)GetChildAt(5);
            m_list = (GList)GetChildAt(6);
            m_n15 = (GImage)GetChildAt(7);
            m_signBtn = (UI_SignBtn)GetChildAt(8);
            m_id = (GTextField)GetChildAt(9);
            m_n8 = (GGroup)GetChildAt(10);
        }
    }
}