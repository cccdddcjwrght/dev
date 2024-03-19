/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Setting
{
    public partial class UI_LanguageUI : GComponent
    {
        public GLabel m_body;
        public GList m_list;
        public GButton m_confirm;
        public const string URL = "ui://dpgt0e2zfpxr19";

        public static UI_LanguageUI CreateInstance()
        {
            return (UI_LanguageUI)UIPackage.CreateObject("Setting", "LanguageUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (GLabel)GetChildAt(0);
            m_list = (GList)GetChildAt(1);
            m_confirm = (GButton)GetChildAt(2);
        }
    }
}