/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_CommonRewardUI : GLabel
    {
        public UI_CommonRewardBody m_body;
        public Transition m_doshow;
        public const string URL = "ui://2w8thcm7gkcg3lhz";

        public static UI_CommonRewardUI CreateInstance()
        {
            return (UI_CommonRewardUI)UIPackage.CreateObject("Common", "CommonRewardUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_body = (UI_CommonRewardBody)GetChildAt(0);
            m_doshow = GetTransitionAt(0);
        }
    }
}