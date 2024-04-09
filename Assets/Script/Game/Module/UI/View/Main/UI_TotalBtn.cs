/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_TotalBtn : GComponent
    {
        public GTextField m_num;
        public const string URL = "ui://ktixaqljpynwlb2";

        public static UI_TotalBtn CreateInstance()
        {
            return (UI_TotalBtn)UIPackage.CreateObject("Main", "TotalBtn");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_num = (GTextField)GetChildAt(1);
        }
    }
}