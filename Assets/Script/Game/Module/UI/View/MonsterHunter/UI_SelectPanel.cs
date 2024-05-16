/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.MonsterHunter
{
    public partial class UI_SelectPanel : GLabel
    {
        public Controller m_type;
        public Controller m_play;
        public GGraph m_start;
        public GGraph m___effect;
        public GLoader m_bullet;
        public UI_PanelBody m_panel;
        public Transition m_fight;
        public const string URL = "ui://my7wqjw6twfox";

        public static UI_SelectPanel CreateInstance()
        {
            return (UI_SelectPanel)UIPackage.CreateObject("MonsterHunter", "SelectPanel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_play = GetControllerAt(1);
            m_start = (GGraph)GetChildAt(0);
            m___effect = (GGraph)GetChildAt(1);
            m_bullet = (GLoader)GetChildAt(2);
            m_panel = (UI_PanelBody)GetChildAt(5);
            m_fight = GetTransitionAt(0);
        }
    }
}