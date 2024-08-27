/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightHp : GProgressBar
    {
        public GTextField m_value;
        public UI_FightEffect m_effect;
        public const string URL = "ui://ow12is1hkp2b26";

        public static UI_FightHp CreateInstance()
        {
            return (UI_FightHp)UIPackage.CreateObject("Explore", "FightHp");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_value = (GTextField)GetChildAt(2);
            m_effect = (UI_FightEffect)GetChildAt(3);
        }
    }
}