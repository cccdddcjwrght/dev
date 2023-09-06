/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_Tip : GComponent
    {
        public Controller m_state;
        public GTextField m_tile1;
        public GTextField m_tile2;
        public const string URL = "ui://ktixaqljf7kela8";

        public static UI_Tip CreateInstance()
        {
            return (UI_Tip)UIPackage.CreateObject("Main", "Tip");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_tile1 = (GTextField)GetChildAt(2);
            m_tile2 = (GTextField)GetChildAt(3);
        }
    }
}