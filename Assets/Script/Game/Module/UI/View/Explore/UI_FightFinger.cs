/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightFinger : GLabel
    {
        public Transition m_play;
        public const string URL = "ui://ow12is1hel3n3i";

        public static UI_FightFinger CreateInstance()
        {
            return (UI_FightFinger)UIPackage.CreateObject("Explore", "FightFinger");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_play = GetTransitionAt(0);
        }
    }
}