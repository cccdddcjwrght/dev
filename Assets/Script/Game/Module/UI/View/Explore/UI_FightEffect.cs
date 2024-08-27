/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_FightEffect : GLabel
    {
        public GGraph m__hit;
        public GGraph m__attack;
        public GGraph m__steal;
        public GGraph m__dizz;
        public const string URL = "ui://ow12is1hm3fd2f";

        public static UI_FightEffect CreateInstance()
        {
            return (UI_FightEffect)UIPackage.CreateObject("Explore", "FightEffect");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m__hit = (GGraph)GetChildAt(0);
            m__attack = (GGraph)GetChildAt(1);
            m__steal = (GGraph)GetChildAt(2);
            m__dizz = (GGraph)GetChildAt(3);
        }
    }
}