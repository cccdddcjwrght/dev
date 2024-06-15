/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Guide
{
    public partial class UI_GuideDialogueUI : GComponent
    {
        public GLoader m_icon;
        public GLabel m_dialogue;
        public GGraph m_mask;
        public const string URL = "ui://hebbif0xqpeoa";

        public static UI_GuideDialogueUI CreateInstance()
        {
            return (UI_GuideDialogueUI)UIPackage.CreateObject("Guide", "GuideDialogueUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_icon = (GLoader)GetChildAt(0);
            m_dialogue = (GLabel)GetChildAt(1);
            m_mask = (GGraph)GetChildAt(2);
        }
    }
}