
using System;
namespace SGame.UI
{
    // UI信息
    public struct UIInfo
    {
        // FariyGUI原件名
        public string comName; 

        // FariyGUI包名
        public string pkgName;

        public override int GetHashCode()
        {
            return HashCode.Combine(comName, pkgName);
        }
    }
}