/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Common
{
    public partial class UI_PopTip : GComponent
    {
        public GTextField m_info;
        public const string URL = "ui://2w8thcm791jv3lgx";

        public static UI_PopTip CreateInstance()
        {
            return (UI_PopTip)UIPackage.CreateObject("Common", "PopTip");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_info = (GTextField)GetChildAt(1);
        }
    }
}