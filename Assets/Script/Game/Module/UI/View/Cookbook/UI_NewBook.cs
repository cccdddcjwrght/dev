/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_NewBook : GLabel
    {
        public Controller m_uplv;
        public GGraph m___effect;
        public GList m_pros;
        public Transition m_t0;
        public const string URL = "ui://n2tgmsyup8z4l";

        public static UI_NewBook CreateInstance()
        {
            return (UI_NewBook)UIPackage.CreateObject("Cookbook", "NewBook");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_uplv = GetControllerAt(0);
            m___effect = (GGraph)GetChildAt(0);
            m_pros = (GList)GetChildAt(4);
            m_t0 = GetTransitionAt(0);
        }
    }
}