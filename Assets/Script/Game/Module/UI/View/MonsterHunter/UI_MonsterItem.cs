/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.MonsterHunter
{
    public partial class UI_MonsterItem : GComponent
    {
        public Controller m_state;
        public Controller m_isfirst;
        public GLoader m_icon;
        public Transition m_t0;
        public Transition m_t3;
        public const string URL = "ui://my7wqjw6twfou";

        public static UI_MonsterItem CreateInstance()
        {
            return (UI_MonsterItem)UIPackage.CreateObject("MonsterHunter", "MonsterItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_isfirst = GetControllerAt(1);
            m_icon = (GLoader)GetChildAt(4);
            m_t0 = GetTransitionAt(0);
            m_t3 = GetTransitionAt(1);
        }
    }
}