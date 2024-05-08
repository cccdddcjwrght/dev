/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Rank
{
    public partial class UI_ResultReward : GComponent
    {
        public Controller m_size;
        public GLoader m_icon;
        public GTextField m_num;
        public const string URL = "ui://k6z01pjbvi8up";

        public static UI_ResultReward CreateInstance()
        {
            return (UI_ResultReward)UIPackage.CreateObject("Rank", "ResultReward");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_size = GetControllerAt(0);
            m_icon = (GLoader)GetChildAt(1);
            m_num = (GTextField)GetChildAt(2);
        }
    }
}