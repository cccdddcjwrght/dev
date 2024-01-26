/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Buff
{
    public partial class UI_BuffItem : GComponent
    {
        public GImage m_buffbg;
        public GLoader m_icon;
        public GLoader m_tag;
        public GTextField m_title;
        public GTextField m_desc;
        public UI_ClickBtn m_click;
        public const string URL = "ui://g406runaijal6";

        public static UI_BuffItem CreateInstance()
        {
            return (UI_BuffItem)UIPackage.CreateObject("Buff", "BuffItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_buffbg = (GImage)GetChildAt(0);
            m_icon = (GLoader)GetChildAt(1);
            m_tag = (GLoader)GetChildAt(2);
            m_title = (GTextField)GetChildAt(3);
            m_desc = (GTextField)GetChildAt(4);
            m_click = (UI_ClickBtn)GetChildAt(5);
        }
    }
}