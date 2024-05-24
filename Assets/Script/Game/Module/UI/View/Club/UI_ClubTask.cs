/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Club
{
    public partial class UI_ClubTask : GComponent
    {
        public GLabel m_body;
        public GTextField m_value;
        public GList m_list;
        public GGroup m_content;
        public const string URL = "ui://kgizakqqrllf2f";

        public static UI_ClubTask CreateInstance()
        {
            return (UI_ClubTask)UIPackage.CreateObject("Club", "ClubTask");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_value = (GTextField)GetChildAt(3);
            m_list = (GList)GetChildAt(5);
            m_content = (GGroup)GetChildAt(6);
        }
    }
}