/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SGame.UI.Cookbook
{
    public partial class UI_WorkerProgress : GProgressBar
    {
        public Controller m_lock;
        public const string URL = "ui://n2tgmsyuadfx2k";

        public static UI_WorkerProgress CreateInstance()
        {
            return (UI_WorkerProgress)UIPackage.CreateObject("Cookbook", "WorkerProgress");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_lock = GetControllerAt(0);
        }
    }
}