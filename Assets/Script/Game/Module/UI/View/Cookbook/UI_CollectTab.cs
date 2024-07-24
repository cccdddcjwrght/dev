/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_CollectTab : GButton
    {
        public Controller m___redpoint;
        public const string URL = "ui://n2tgmsyuadfx27";

        public static UI_CollectTab CreateInstance()
        {
            return (UI_CollectTab)UIPackage.CreateObject("Cookbook", "CollectTab");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___redpoint = GetControllerAt(1);
        }
    }
}