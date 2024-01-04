/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Main
{
    public partial class UI_ShowBtnList : GComponent
    {
        public GList m_left;
        public const string URL = "ui://ktixaqljk0s6law";

        public static UI_ShowBtnList CreateInstance()
        {
            return (UI_ShowBtnList)UIPackage.CreateObject("Main", "ShowBtnList");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_left = (GList)GetChildAt(0);
        }
    }
}