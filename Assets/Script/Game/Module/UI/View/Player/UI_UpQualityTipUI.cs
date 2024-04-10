/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Player
{
    public partial class UI_UpQualityTipUI : GLabel
    {
        public Controller m_state;
        public GGraph m_maskbg;
        public GTextField m_qname;
        public GTextField m_attribute;
        public GTextField m_recycle;
        public UI_attrlabel m_addeffect;
        public UI_Equip m_equip;
        public GButton m_close;
        public const string URL = "ui://cmw7t1elwaj63n";

        public static UI_UpQualityTipUI CreateInstance()
        {
            return (UI_UpQualityTipUI)UIPackage.CreateObject("Player", "UpQualityTipUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_maskbg = (GGraph)GetChildAt(0);
            m_qname = (GTextField)GetChildAt(3);
            m_attribute = (GTextField)GetChildAt(5);
            m_recycle = (GTextField)GetChildAt(7);
            m_addeffect = (UI_attrlabel)GetChildAt(9);
            m_equip = (UI_Equip)GetChildAt(10);
            m_close = (GButton)GetChildAt(12);
        }
    }
}