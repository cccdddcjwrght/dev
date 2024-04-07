/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.EnterScene
{
    public partial class UI_Levelprogress : GProgressBar
    {
        public GTextField m___title;
        public const string URL = "ui://cxpm3jfbudvny";

        public static UI_Levelprogress CreateInstance()
        {
            return (UI_Levelprogress)UIPackage.CreateObject("EnterScene", "Levelprogress");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___title = (GTextField)GetChildAt(2);
        }
    }
}