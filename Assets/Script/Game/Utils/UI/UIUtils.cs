using FairyGUI;
using SGame.UI;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

namespace SGame
{
    public class UIUtils
    {
        public static int GetUI(string name)
        {
            if (ConfigSystem.Instance.TryGet<GameConfigs.ui_resRowData>((GameConfigs.ui_resRowData conf) =>
            {
                return conf.Name == name;
            }, out GameConfigs.ui_resRowData conf))
            {
                return conf.Id;
            }

            // 找不到ID
            return 0;
        }

        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <param name="mgr"></param>
        /// <param name="ui">UI 的Entity对象</param>
        public static void CloseUI(EntityManager mgr, Entity ui)
        {
            if (mgr.HasComponent<UIWindow>(ui))
            {
                UIWindow window = mgr.GetComponentData<UIWindow>(ui);
                if (window.Value != null)
                {
                    window.Value.Close();
                }
            }
        }

        /// <summary>
        /// 通过世界坐标转换成控件坐标
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static Vector2 WorldPosToUI(GComponent ui, Vector3 pos)
        {
            Vector3 screenPoint = GameCamera.camera.WorldToScreenPoint(pos);
            screenPoint.y = Screen.height - screenPoint.y;
            return ui.GlobalToLocal(screenPoint);
        }

        /// <summary>
        /// 创建漂字
        /// </summary>
        /// <param name="mgr"></param>
        /// <param name="title">文字</param>
        /// <param name="pos">3d场景中的位置</param>
        /// <param name="color">颜色</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="duration">持续时间</param>
        /// <returns></returns>
        public static Entity ShowTips(EntityManager mgr, string title, float3 pos, Color color, int fontSize, float duration)
        {
            return FloatTextRequest.CreateEntity(mgr, title, pos, color, 50, 2.0f);
        }
    }
}