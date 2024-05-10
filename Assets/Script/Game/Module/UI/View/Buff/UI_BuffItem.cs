/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Buff
{
    public partial class UI_BuffItem : GComponent
    {
        public Controller m_type;
        public Controller m_state;
        public GImage m_buffbg;
        public GLoader m_icon;
        public GLoader m_tag;
        public GTextField m_title;
        public GTextField m_desc;
        public GLoader m_clickBtn;
        public UI_BuffClickBtn m_click;
        public Transition m_hide;
        public const string URL = "ui://g406runaijal6";

        public static UI_BuffItem CreateInstance()
        {
            return (UI_BuffItem)UIPackage.CreateObject("Buff", "BuffItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_state = GetControllerAt(1);
            m_buffbg = (GImage)GetChildAt(1);
            m_icon = (GLoader)GetChildAt(3);
            m_tag = (GLoader)GetChildAt(4);
            m_title = (GTextField)GetChildAt(5);
            m_desc = (GTextField)GetChildAt(6);
            m_clickBtn = (GLoader)GetChildAt(7);
            m_click = (UI_BuffClickBtn)GetChildAt(8);
            m_hide = GetTransitionAt(0);
        }
    }
}