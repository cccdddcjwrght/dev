/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_EnterSceneUI : GComponent
    {
        public Controller m___disable;
        public Controller m_show;
        public GGraph m___effect;
        public GLoader m_loader;
        public GButton m_btnGO;
        public GTextField m_title;
        public GTextField m_title2;
        public UI_LevelItem m_level1;
        public UI_LevelItem m_level2;
        public UI_LevelItem m_level3;
        public UI_LevelItem m_level4;
        public GButton m_btnClose;
        public const string URL = "ui://cxpm3jfbicj20";

        public static UI_EnterSceneUI CreateInstance()
        {
            return (UI_EnterSceneUI)UIPackage.CreateObject("EnterScene", "EnterSceneUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___disable = GetControllerAt(0);
            m_show = GetControllerAt(1);
            m___effect = (GGraph)GetChildAt(0);
            m_loader = (GLoader)GetChildAt(1);
            m_btnGO = (GButton)GetChildAt(3);
            m_title = (GTextField)GetChildAt(6);
            m_title2 = (GTextField)GetChildAt(7);
            m_level1 = (UI_LevelItem)GetChildAt(8);
            m_level2 = (UI_LevelItem)GetChildAt(9);
            m_level3 = (UI_LevelItem)GetChildAt(10);
            m_level4 = (UI_LevelItem)GetChildAt(11);
            m_btnClose = (GButton)GetChildAt(12);
        }
    }
}