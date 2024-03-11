/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Loading
{
    public partial class UI_LoadingUI : GComponent
    {
        public GLoader m_bg;
        public GLabel m___logo;
        public Transition m_t0;
        public const string URL = "ui://yontt7myvm0m0";

        public static UI_LoadingUI CreateInstance()
        {
            return (UI_LoadingUI)UIPackage.CreateObject("Loading", "LoadingUI");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GLoader)GetChildAt(0);
            m___logo = (GLabel)GetChildAt(1);
            m_t0 = GetTransitionAt(0);
        }
    }
}