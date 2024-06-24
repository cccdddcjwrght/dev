/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_CustomerbookUpUI : GLabel
    {
        public Controller m_take_reward;
        public GLabel m_body;
        public UI_Customer m_customer;
        public GGraph m___effect;
        public GList m_pros;
        public GRichTextField m_txtDesc;
        public GTextField m_txtDesc2;
        public UI_BtnIconItem m_click;
        public Transition m_t0;
        public const string URL = "ui://n2tgmsyutkwvz";

        public static UI_CustomerbookUpUI CreateInstance()
        {
            return (UI_CustomerbookUpUI)UIPackage.CreateObject("Cookbook", "CustomerbookUpUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_take_reward = GetControllerAt(0);
            m_body = (GLabel)GetChildAt(0);
            m_customer = (UI_Customer)GetChildAt(1);
            m___effect = (GGraph)GetChildAt(2);
            m_pros = (GList)GetChildAt(5);
            m_txtDesc = (GRichTextField)GetChildAt(6);
            m_txtDesc2 = (GTextField)GetChildAt(7);
            m_click = (UI_BtnIconItem)GetChildAt(9);
            m_t0 = GetTransitionAt(0);
        }
    }
}