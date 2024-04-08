/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace GameFriend
{
    public partial class UI_MyRole : GComponent
    {
        public GLoader m_heiheh;
        public const string URL = "ui://nsy0h7irome21";

        public static UI_MyRole CreateInstance()
        {
            return (UI_MyRole)UIPackage.CreateObject("GameFriend", "MyRole");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_heiheh = (GLoader)GetChildAt(0);
        }
    }
}