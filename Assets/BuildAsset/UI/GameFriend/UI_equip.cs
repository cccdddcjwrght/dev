/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace GameFriend
{
    public partial class UI_equip : GComponent
    {
        public Controller m_type;
        public Controller m_quality;
        public GLoader m_bg;
        public GLoader m_icon;
        public const string URL = "ui://nsy0h7irome2q";

        public static UI_equip CreateInstance()
        {
            return (UI_equip)UIPackage.CreateObject("GameFriend", "equip");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_type = GetControllerAt(0);
            m_quality = GetControllerAt(1);
            m_bg = (GLoader)GetChildAt(0);
            m_icon = (GLoader)GetChildAt(1);
        }
    }
}