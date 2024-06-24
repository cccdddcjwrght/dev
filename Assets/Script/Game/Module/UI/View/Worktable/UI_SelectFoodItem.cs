/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Worktable
{
    public partial class UI_SelectFoodItem : GButton
    {
        public Controller m_lock;
        public const string URL = "ui://m8rpv7f9ddqnx";

        public static UI_SelectFoodItem CreateInstance()
        {
            return (UI_SelectFoodItem)UIPackage.CreateObject("Worktable", "SelectFoodItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_lock = GetControllerAt(1);
        }
    }
}