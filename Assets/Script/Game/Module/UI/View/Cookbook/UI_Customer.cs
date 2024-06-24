/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_Customer : GComponent
    {
        public Controller m_state;
        public GLoader m_body;
        public GImage m_nobody;
        public const string URL = "ui://n2tgmsyujxg610";

        public static UI_Customer CreateInstance()
        {
            return (UI_Customer)UIPackage.CreateObject("Cookbook", "Customer");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_body = (GLoader)GetChildAt(1);
            m_nobody = (GImage)GetChildAt(2);
        }
    }
}