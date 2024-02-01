using System.Collections;
using System.Collections.Generic;
using SGame;
using UnityEngine;
using GameConfigs;
using libx;
using log4net;
namespace SGame
{
    // 字体管理类
    public class FontManager : Singleton<FontManager>
    {
        private static ILog log = LogManager.GetLogger("Game.Font");
        private const string BASE_FONT_PATH = "Assets/BuildAsset/Fonts/";
        
        static string GetFontPath(string path)
        {
            return BASE_FONT_PATH + path;
        }
        
        // 初始化字体, 注册游戏内使用到的所有字体
        public IEnumerator Initalize()
        {
            var fonts = ConfigSystem.Instance.LoadConfig<fonts>();
            List<AssetRequest> font_request = new List<AssetRequest>(fonts.DatalistLength);
            for (int i = 0; i < fonts.DatalistLength; i++)
            {
                var f = fonts.Datalist(i);
                AssetRequest req = null;
                if (f.Value.Path.EndsWith("asset"))
                {
                     // sdf font
                     req = Assets.LoadAssetAsync(GetFontPath(f.Value.Path), typeof(TMPro.TMP_FontAsset));
                }
                else
                {
                     req = Assets.LoadAssetAsync(GetFontPath(f.Value.Path), typeof(Font));
                }
                font_request.Add(req);
            }

            // 等待字体注册结束
            for (int i = 0; i < font_request.Count; i++)
            {
                yield return font_request[i];
                if (!string.IsNullOrEmpty(font_request[i].error))
                {
                    log.Error("font load fail=" + font_request[i].error);
                    continue;
                }

                if (font_request[i].asset is Font)
                {
                    // 添加TTF 字体
                    var f = font_request[i].asset as Font;
                    string font_name = fonts.Datalist(i).Value.Name;
                    FairyGUI.FontManager.RegisterFont(new FairyGUI.DynamicFont(font_name, f), font_name);
                }
                else
                {
                    // 添加SDF 字体
                    var f = font_request[i].asset as TMPro.TMP_FontAsset;
                    string font_name = fonts.Datalist(i).Value.Name;
                    var tmpFont = new FairyGUI.TMPFont();
                    tmpFont.fontAsset = f;
                    tmpFont.name = font_name;
                    FairyGUI.FontManager.RegisterFont(tmpFont);
                }
            }
        }
    }
}