/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_UpQualityTipBody : GLabel
    {
        public Controller m_state;
        public GTextField m_qname;
        public GTextField m_attribute;
        public GTextField m_recycle;
        public UI_attrlabel m_addeffect;
        public UI_Equip m_equip;
        public GButton m_close;
        public GGraph m___effect;
        public Transition m_upqualitytipui;
        public const string URL = "ui://cmw7t1elwaj63n";

        public static UI_UpQualityTipBody CreateInstance()
        {
            return (UI_UpQualityTipBody)UIPackage.CreateObject("Player", "UpQualityTipBody");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_qname = (GTextField)GetChildAt(3);
            m_attribute = (GTextField)GetChildAt(5);
            m_recycle = (GTextField)GetChildAt(7);
            m_addeffect = (UI_attrlabel)GetChildAt(10);
            m_equip = (UI_Equip)GetChildAt(11);
            m_close = (GButton)GetChildAt(13);
            m___effect = (GGraph)GetChildAt(15);
            m_upqualitytipui = GetTransitionAt(0);
        }
    }
}