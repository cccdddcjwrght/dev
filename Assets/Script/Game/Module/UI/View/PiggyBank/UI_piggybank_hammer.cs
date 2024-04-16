/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.PiggyBank
{
    public partial class UI_piggybank_hammer : GComponent
    {
        public Transition m_hammer;
        public const string URL = "ui://k2a58dz5i1e5w";

        public static UI_piggybank_hammer CreateInstance()
        {
            return (UI_piggybank_hammer)UIPackage.CreateObject("PiggyBank", "piggybank_hammer");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_hammer = GetTransitionAt(0);
        }
    }
}