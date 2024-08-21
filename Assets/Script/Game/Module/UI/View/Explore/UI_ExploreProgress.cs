/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Explore
{
    public partial class UI_ExploreProgress : GProgressBar
    {
        public GTextField m___text;
        public const string URL = "ui://ow12is1hpm5b18";

        public static UI_ExploreProgress CreateInstance()
        {
            return (UI_ExploreProgress)UIPackage.CreateObject("Explore", "ExploreProgress");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m___text = (GTextField)GetChildAt(3);
        }
    }
}