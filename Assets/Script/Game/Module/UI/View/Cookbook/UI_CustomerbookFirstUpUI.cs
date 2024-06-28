/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_CustomerbookFirstUpUI : GComponent
    {
        public GGraph m_mask;
        public GTextField m_title;
        public GGraph m___effect;
        public GLoader m_icon;
        public Transition m_t0;
        public const string URL = "ui://n2tgmsyusj1y14";

        public static UI_CustomerbookFirstUpUI CreateInstance()
        {
            return (UI_CustomerbookFirstUpUI)UIPackage.CreateObject("Cookbook", "CustomerbookFirstUpUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_mask = (GGraph)GetChildAt(0);
            m_title = (GTextField)GetChildAt(2);
            m___effect = (GGraph)GetChildAt(3);
            m_icon = (GLoader)GetChildAt(4);
            m_t0 = GetTransitionAt(0);
        }
    }
}