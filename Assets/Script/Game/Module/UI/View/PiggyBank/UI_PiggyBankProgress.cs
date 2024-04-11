/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.PiggyBank
{
    public partial class UI_PiggyBankProgress : GProgressBar
    {
        public GTextField m_midValue;
        public GTextField m_maxValue;
        public const string URL = "ui://k2a58dz5a9b7r";

        public static UI_PiggyBankProgress CreateInstance()
        {
            return (UI_PiggyBankProgress)UIPackage.CreateObject("PiggyBank", "PiggyBankProgress");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_midValue = (GTextField)GetChildAt(4);
            m_maxValue = (GTextField)GetChildAt(6);
        }
    }
}