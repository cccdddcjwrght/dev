/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightAttr : GLabel
    {
        public Controller m_size;
        public Controller m_uptype;
        public Controller m_ftype;
        public Controller m_color;
        public GTextField m_val;
        public GTextField m_val2;
        public const string URL = "ui://ow12is1hpm5b17";

        public static UI_FightAttr CreateInstance()
        {
            return (UI_FightAttr)UIPackage.CreateObject("Explore", "FightAttr");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_size = GetControllerAt(0);
            m_uptype = GetControllerAt(1);
            m_ftype = GetControllerAt(2);
            m_color = GetControllerAt(3);
            m_val = (GTextField)GetChildAt(1);
            m_val2 = (GTextField)GetChildAt(2);
        }
    }
}