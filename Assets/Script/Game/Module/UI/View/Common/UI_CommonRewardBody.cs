/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_CommonRewardBody : GLabel
    {
        public GList m_list;
        public GGraph m___effect;
        public GLoader m_loader;
        public GRichTextField m_tips;
        public Transition m_doshow;
        public const string URL = "ui://2w8thcm7qc8m3lk0";

        public static UI_CommonRewardBody CreateInstance()
        {
            return (UI_CommonRewardBody)UIPackage.CreateObject("Common", "CommonRewardBody");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChildAt(0);
            m___effect = (GGraph)GetChildAt(4);
            m_loader = (GLoader)GetChildAt(5);
            m_tips = (GRichTextField)GetChildAt(6);
            m_doshow = GetTransitionAt(0);
        }
    }
}