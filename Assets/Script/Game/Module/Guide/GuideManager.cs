using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConfigs;
using SGame.Dining;

namespace SGame 
{
    public class GuideManager : Singleton<GuideManager>
    {
        //是否打开获取路径
        bool isGetPath = false;

        bool isCoerceGuide = false;
        /// <summary>
        /// 是否是强指引
        /// </summary>
        public bool IsCoerce { get { return isCoerceGuide; } }

        public bool showClickArea = false;

        //正在执行的指引
        List<GuideRuntimeData> runtimeDataList = new List<GuideRuntimeData>();

        public void Initalize() 
        {
            EventManager.Instance.Reg<int>((int)GameEvent.GUIDE_FINISH, FinishGuide);
            EventManager.Instance.Reg<int>((int)GameEvent.AFTER_ENTER_ROOM, (s) => 
            {
                Utils.Timer(0.5f, null, completed: CheckRecruitOpen);
            });
            GRoot.inst.onClick.Add(GuideClick);
        }

        /// <summary>
        /// 开始执行指引
        /// </summary>
        public void StartGuide(int guideId) 
        {
            StopGuide(guideId);
            if (!ContainGuide(guideId)) 
            {
                Debug.Log(string.Format("<color=red>guide config is not id:{0}</color>", guideId));
                return;
            } 
            var guideRuntimeData = new GuideRuntimeData(guideId);
            runtimeDataList.Add(guideRuntimeData);
            guideRuntimeData.Run(guideId);

            EventManager.Instance.Trigger((int)GameEvent.GAME_MAIN_REFRESH);
            EventManager.Instance.Trigger((int)GameEvent.GUIDE_START);
        }

        public void StopGuide(int guideId) 
        {
            var index = runtimeDataList.FindIndex((r) => r.guideId == guideId);
            if (index >= 0)
            {
                Debug.Log(string.Format("<color=red>stop guide id: {0}</color>", guideId));
                runtimeDataList[index].FinishGuide(1);
                runtimeDataList.RemoveAt(index);
            }
        }

        /// <summary>
        /// 清理除主线以外的指引（任务指引）
        /// </summary>
        public void ClearOtherGuide() 
        {
            for (int i = runtimeDataList.Count - 1; i >= 0; i--)
            {
                int guideId = runtimeDataList[i].guideId;
                if (ConfigSystem.Instance.TryGet<GuideRowData>((c) => c.GuideId == guideId, out var cfg) && cfg.GuideType == 1) 
                {
                    runtimeDataList[i].FinishGuide(1);
                    runtimeDataList.RemoveAt(i);
                }
            }
        }

        public void FinishGuide(int guideId) 
        {
            var index = runtimeDataList.FindIndex((r) => r.guideId == guideId);
            if (index == -1) 
            {
                Debug.Log(string.Format("<color=red>not current guide : {0}</color>", guideId));
                return;
            }
            runtimeDataList.RemoveAt(index);

            //当前指引结束后执行下一个指引（主线指引才生效）
            var config = GetGuideConfig(guideId);
            if (config.IsValid() && config.GuideType == 0) 
            {
                int nextGuideId = guideId + 1;
                DataCenter.Instance.guideData.guideId = nextGuideId;
                StartGuide(nextGuideId);
            }
        }

        //检测配置是否有该指引
        public bool ContainGuide(int guideId) 
        {
            if (ConfigSystem.Instance.TryGet<GameConfigs.GuideRowData>((c)=> c.GuideId == guideId, out var cfg))
                return true;
            return false;
        }


        public GuideRowData GetGuideConfig(int guideId) 
        {
            if (ConfigSystem.Instance.TryGet<GameConfigs.GuideRowData>((c) => c.GuideId == guideId, out var cfg))
                return cfg;
            return default;
        }

        /// <summary>
        /// 设置当前是否是强指引
        /// </summary>
        /// <param name="state"></param>
        public void SetCoerceGuideState(bool state) 
        {
            isCoerceGuide = state;
        }


        RegionHit mono;
        public void CheckRecruitOpen() 
        {
            if (DataCenter.Instance.roomData.roomID != 1) return;
            
            if (mono == null) 
            {
                var go = GameObject.FindGameObjectWithTag("recruit");
                mono = go?.GetComponent<RegionHit>();
            }

            if (mono) 
            {
                var region = DataCenter.MachineUtil.GetWorktable(mono.region);
                if (region != null)
                {
                    var v = GlobalDesginConfig.GetIntArray("guide_recruit");
                    var worktable = DataCenter.MachineUtil.GetWorktable(v[0]);
                    mono.gameObject?.SetActive(worktable?.lv >= v[1]);
                }
            }
        }

        public bool GetWorktableShow() 
        {
            if (DataCenter.Instance.roomData.roomID != 1) return true;

            var v = GlobalDesginConfig.GetIntArray("guide_recruit");
            var worktable = DataCenter.MachineUtil.GetWorktable(v[0]);
            return worktable?.lv >= v[1];
        }


        public void GuideClick() 
        {
            EventManager.Instance.Trigger((int)GameEvent.GUIDE_CLICK);
        }

        public void Update() 
        {

            if (Input.GetKeyDown(KeyCode.F4)) 
                isGetPath = true;

            if (Input.GetKeyDown(KeyCode.F3))
                showClickArea = !showClickArea;

            if (isGetPath) 
            {
                if (Input.GetMouseButtonDown(0)) 
                {
                    //坐标
                    Vector2 pos = Input.mousePosition;
                    pos.y = Screen.height - pos.y;
                    pos = GRoot.inst.GlobalToLocal(pos);
                    Debug.Log(string.Format("<color=yellow> Pos {0}</color>", pos));
                    UIUtils.CloseUIByName("dialogue");
                }

                if (Input.GetMouseButtonDown(0) && Stage.isTouchOnUI) 
                {
                    List<string> uiNames = new List<string>();
                    GObject clickGObject = GRoot.inst.touchTarget;
                    while (clickGObject != GRoot.inst && clickGObject != null) 
                    {
                        if(clickGObject is GComponent) uiNames.Add(clickGObject.name);
                        clickGObject = clickGObject.parent;
                    }
                    uiNames.Reverse();
                    string path = string.Join(".", uiNames);
                    Debug.Log(string.Format("<color=yellow>{0}</color>", path));

                    GUIUtility.systemCopyBuffer = path;
                }
            }
        }
    }
}


