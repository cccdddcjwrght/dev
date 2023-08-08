using log4net;

namespace SGame.UI
{
    public class UIUtils
    {
        private const string ROOT_PATH       = "Assets/BuildAsset/";
        private const string DESC_BUNDLE_FMT = ROOT_PATH + "UI/{0}_fui.bytes";
        private const string RES_BUNDLE_FMT  = ROOT_PATH + "UI/{0}";
        
        static ILog log = LogManager.GetLogger("xl.ui");

        /// <summary>
        /// 将res 的路径换为真实的bundle资源路径
        /// </summary>
        /// <param name="name">UI 包名</param>
        /// <return>bundle资源路径</return>
        private static string RuledAssetBundleName(string name)
        {
            return libx.Utility.RuledAssetBundleName(name);
        }

#if UNITY_EDITOR
        public static string GetPackageDescPath(string pkgName)
        {
            string descBundlePath = string.Format(RES_BUNDLE_FMT, pkgName);
            return descBundlePath;
        }
#endif

        /// <summary>
        /// 将res 的路径换为真实的 bundle资源路径
        /// </summary>
        /// <param name="pkgName">UI 包名</param>
        /// <param name="descBundlePath">UI 描述bundle路径</param>
        /// <param name="resBundlePath">UI 资源bundle路径</param>
        /// <return>是否有效</return>
        public static bool GetPackageDescAndResName(string pkgName, out string descBundlePath, out string resBundlePath)
        {
            var descFilePath = string.Format(DESC_BUNDLE_FMT, pkgName).ToLower();
            resBundlePath = string.Format(RES_BUNDLE_FMT, pkgName).ToLower();
            log.Info("desc bundlePath =" + descFilePath);
            log.Info("res bundlePath =" + resBundlePath);

            //// 由于bundle 经过了MD5 的转换, 因此获取bundle路径时要再转换一次
            //// 通过资源路径获得bundle 名称
            libx.Assets.GetAssetBundleName(descFilePath, out descBundlePath);

            // 将res 的路径换为真实的
            resBundlePath = RuledAssetBundleName(resBundlePath);

            log.Info("desc bundlePath =" + descBundlePath);
            log.Info("res bundlePath =" + resBundlePath);
            return true;
        }
    }
}
