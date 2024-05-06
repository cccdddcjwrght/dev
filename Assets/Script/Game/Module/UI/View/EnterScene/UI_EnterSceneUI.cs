/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_EnterSceneUI : GComponent
    {
        public Controller m_show;
        public GGraph m___effect;
        public GList m_list;
        public GButton m_close;
        public GLoader m_region;
        public GTextField m_title2;
        public GTextField m_title3;
        public GTextField m_title;
        public GGroup m_titleGroup;
        public GButton m_btnGO;
        public GTextField m_tips;
        public GGroup m_tipGroup;
        public GLoader m_loader;
        public const string URL = "ui://cxpm3jfbicj20";

        public static UI_EnterSceneUI CreateInstance()
        {
            return (UI_EnterSceneUI)UIPackage.CreateObject("EnterScene", "EnterSceneUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_show = GetControllerAt(0);
            m___effect = (GGraph)GetChildAt(0);
            m_list = (GList)GetChildAt(1);
            m_close = (GButton)GetChildAt(2);
            m_region = (GLoader)GetChildAt(3);
            m_title2 = (GTextField)GetChildAt(5);
            m_title3 = (GTextField)GetChildAt(6);
            m_title = (GTextField)GetChildAt(10);
            m_titleGroup = (GGroup)GetChildAt(11);
            m_btnGO = (GButton)GetChildAt(13);
            m_tips = (GTextField)GetChildAt(15);
            m_tipGroup = (GGroup)GetChildAt(16);
            m_loader = (GLoader)GetChildAt(17);
        }
    }
}