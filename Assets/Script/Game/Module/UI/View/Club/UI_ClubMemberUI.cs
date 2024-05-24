/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Club
{
    public partial class UI_ClubMemberUI : GComponent
    {
        public GLabel m_body;
        public GList m_list;
        public GGroup m_content;
        public const string URL = "ui://kgizakqqlu5m27";

        public static UI_ClubMemberUI CreateInstance()
        {
            return (UI_ClubMemberUI)UIPackage.CreateObject("Club", "ClubMemberUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_list = (GList)GetChildAt(2);
            m_content = (GGroup)GetChildAt(3);
        }
    }
}