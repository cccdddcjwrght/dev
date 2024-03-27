/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_LevelItem : GComponent
    {
        public Controller m_state;
        public Controller m_left;
        public GLoader m_icon;
        public GImage m_pass;
        public GImage m_lock;
        public const string URL = "ui://cxpm3jfbome2g";

        public static UI_LevelItem CreateInstance()
        {
            return (UI_LevelItem)UIPackage.CreateObject("EnterScene", "LevelItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_left = GetControllerAt(1);
            m_icon = (GLoader)GetChildAt(0);
            m_pass = (GImage)GetChildAt(1);
            m_lock = (GImage)GetChildAt(2);
        }
    }
}