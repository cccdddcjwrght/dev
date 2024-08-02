/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Technology
{
    public partial class UI_TechnologyUI : GComponent
    {
        public GLabel m_techFrame;
        public GList m_techList;
        public GLoader m_clickBtn;
        public const string URL = "ui://gnu1a8dnijal0";

        public static UI_TechnologyUI CreateInstance()
        {
            return (UI_TechnologyUI)UIPackage.CreateObject("Technology", "TechnologyUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_techFrame = (GLabel)GetChildAt(0);
            m_techList = (GList)GetChildAt(1);
            m_clickBtn = (GLoader)GetChildAt(2);
        }
    }
}