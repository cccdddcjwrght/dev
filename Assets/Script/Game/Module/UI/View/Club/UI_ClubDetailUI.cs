/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Club
{
    public partial class UI_ClubDetailUI : GComponent
    {
        public GButton m_btnClose;
        public GTextField m_title;
        public UI_IconBtn m_remove;
        public GComponent m_Equip;
        public GButton m_btnOK;
        public GGroup m_content;
        public const string URL = "ui://kgizakqqrllf2s";

        public static UI_ClubDetailUI CreateInstance()
        {
            return (UI_ClubDetailUI)UIPackage.CreateObject("Club", "ClubDetailUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_btnClose = (GButton)GetChildAt(1);
            m_title = (GTextField)GetChildAt(2);
            m_remove = (UI_IconBtn)GetChildAt(3);
            m_Equip = (GComponent)GetChildAt(4);
            m_btnOK = (GButton)GetChildAt(5);
            m_content = (GGroup)GetChildAt(6);
        }
    }
}