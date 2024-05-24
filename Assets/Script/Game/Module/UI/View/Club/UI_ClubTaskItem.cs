/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Club
{
    public partial class UI_ClubTaskItem : GComponent
    {
        public GTextField m_des;
        public GLoader m_currencyIcon;
        public GTextField m_value;
        public const string URL = "ui://kgizakqqlu5m26";

        public static UI_ClubTaskItem CreateInstance()
        {
            return (UI_ClubTaskItem)UIPackage.CreateObject("Club", "ClubTaskItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_des = (GTextField)GetChildAt(1);
            m_currencyIcon = (GLoader)GetChildAt(2);
            m_value = (GTextField)GetChildAt(3);
        }
    }
}