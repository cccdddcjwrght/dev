/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Club
{
    public partial class UI_ClubStair : GComponent
    {
        public Controller m_dir;
        public UI_ClubReward m_reward;
        public const string URL = "ui://kgizakqqlu5m29";

        public static UI_ClubStair CreateInstance()
        {
            return (UI_ClubStair)UIPackage.CreateObject("Club", "ClubStair");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_dir = GetControllerAt(0);
            m_reward = (UI_ClubReward)GetChildAt(1);
        }
    }
}