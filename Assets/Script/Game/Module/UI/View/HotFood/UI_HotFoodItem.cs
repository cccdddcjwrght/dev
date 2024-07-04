/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.HotFood
{
    public partial class UI_HotFoodItem : GComponent
    {
        public Controller m_check;
        public Controller m_state;
        public GLoader m_icon;
        public GGraph m_mask;
        public const string URL = "ui://zo86yb1ivlyvc";

        public static UI_HotFoodItem CreateInstance()
        {
            return (UI_HotFoodItem)UIPackage.CreateObject("HotFood", "HotFoodItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_check = GetControllerAt(0);
            m_state = GetControllerAt(1);
            m_icon = (GLoader)GetChildAt(2);
            m_mask = (GGraph)GetChildAt(4);
        }
    }
}