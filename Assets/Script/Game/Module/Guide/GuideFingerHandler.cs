using System.Collections;
using FairyGUI;
using GameConfigs;
using UnityEngine;

namespace SGame 
{
    public enum GuideTargetEnum 
    {
        NONE    = 0, //无
        UIPATH  = 1, //ui路径
        GRID    = 2, //地图格子
    }

    public class GuideFingerHandler
    {
        string path;
        bool isFind;

        GuideTargetEnum type;

        //指引目标
        GObject target;
        float[] gridXZ;
        //当前步骤指引配置
        GuideRowData config;
        Vector2 defalutSize = new Vector2(100, 100);

        public void InitConfig(GuideRowData config) 
        {
            this.config = config;
            isFind = false;
            if (config.UIPath != string.Empty) 
            {
                type = GuideTargetEnum.UIPATH;
                path = config.UIPath;
            }
            else if(config.FloatParamLength > 0)
            {
                type = GuideTargetEnum.GRID;
                gridXZ = config.GetFloatParamArray();
            }
        }

        public IEnumerator FindTarget()
        {
            while (!isFind) 
            {
                target = GRoot.inst.GetChildByPath(path);
                isFind = target != null;
                yield return null;
                if (isFind) yield break;
            }
        }

        public IEnumerator WaitFingerClose() 
        {
            while (true) 
            {
                bool isOpen = UIUtils.CheckUIIsOpen("fingerui");
                if (!isOpen) yield break;
                yield return null;
            }
        }

        public IEnumerator WaitGuideMaskClose() 
        {
            while (true) 
            {
                bool isOpen = UIUtils.CheckUIIsOpen("guideback");
                if (!isOpen) yield break;
                yield return null;
            }
        }

        public IEnumerator WaitGuideMaskOpen() 
        {
            while (true) 
            {
                bool isOpen = UIUtils.CheckUIIsOpen("guideback");
                if (isOpen) yield break;
                yield return null;
            }
        }

        public GObject GetTarget() 
        {
            return target;
        }

        public Vector2 GetTargetPos() 
        {
            Vector2 targetPos = Vector2.zero;
            if (type == GuideTargetEnum.UIPATH)
            {
                if (!target.isDisposed) 
                {
                    targetPos = target.LocalToGlobal(Vector2.zero);
                    targetPos = GRoot.inst.GlobalToLocal(targetPos);
                    if(!target.pivotAsAnchor)
                        targetPos += new Vector2(target.width * target.pivot.x, target.height * target.pivot.y);
                }
            }
            else if (type == GuideTargetEnum.GRID) 
            {
                var cellPos = GameTools.MapAgent.CellToVector((int)gridXZ[0], (int)gridXZ[1]);
                targetPos = SGame.UIUtils.WorldPosToUI(cellPos);
            }
            targetPos += new Vector2(config.OffsetXY(0), config.OffsetXY(1));
            return targetPos;
        }

        public Vector2 GetTargetSize() 
        {
            Vector2 size = new Vector2(config.UISize(0), config.UISize(1));
            if (type == GuideTargetEnum.UIPATH)
            {
                if (size == Vector2.zero) size = target.size;
            }
            else if (type == GuideTargetEnum.GRID) 
            {
                size = new Vector2(config.UISize(0), config.UISize(1));
                if(size == Vector2.zero) size = defalutSize;
            }
            return size;
        }

        /// <summary>
        /// 限制镜头拖拽
        /// </summary>
        public void DisableCameraDrag(bool disableDrag) 
        {
            SceneCameraSystem.Instance.disableDrag = disableDrag;
        }

        public void DisableControl(bool disableControl) 
        {
            SceneCameraSystem.Instance.disableControl = disableControl;
        }
    }
}

