/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Club
{
    public partial class UI_ClubRewardIcon : GLabel
    {
        public GTextField m_num;
        public const string URL = "ui://kgizakqqrllf2c";

        public static UI_ClubRewardIcon CreateInstance()
        {
            return (UI_ClubRewardIcon)UIPackage.CreateObject("Club", "ClubRewardIcon");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_num = (GTextField)GetChildAt(1);
        }
    }
}