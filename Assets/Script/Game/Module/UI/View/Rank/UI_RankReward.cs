/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Rank
{
    public partial class UI_RankReward : GComponent
    {
        public Controller m_size;
        public GLoader m_icon;
        public GTextField m___text;
        public const string URL = "ui://k6z01pjbvi8un";

        public static UI_RankReward CreateInstance()
        {
            return (UI_RankReward)UIPackage.CreateObject("Rank", "RankReward");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_size = GetControllerAt(0);
            m_icon = (GLoader)GetChildAt(1);
            m___text = (GTextField)GetChildAt(2);
        }
    }
}