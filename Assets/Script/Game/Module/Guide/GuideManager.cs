using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConfigs;

namespace SGame 
{
    public class GuideManager : Singleton<GuideManager>
    {
        //�Ƿ�򿪻�ȡ·��
        bool isGetPath = false;

        bool isCoerceGuide = false;
        /// <summary>
        /// �Ƿ���ǿָ��
        /// </summary>
        public bool IsCoerce { get { return isCoerceGuide; } }

        //����ִ�е�ָ��
        List<GuideRuntimeData> runtimeDataList = new List<GuideRuntimeData>();

        public void Initalize() 
        {
            EventManager.Instance.Reg<int>((int)GameEvent.GUIDE_FINISH, FinishGuide);
        }

        /// <summary>
        /// ��ʼִ��ָ��
        /// </summary>
        public void StartGuide(int guideId) 
        {
            var index = runtimeDataList.FindIndex((r) => r.guideId == guideId);
            if (index >= 0) 
            {
                Debug.Log(string.Format("<color=red>guide is running: {0}</color>", guideId));
                runtimeDataList[index].FinishGuide(1);
                runtimeDataList.RemoveAt(index);
            }

            if (!ContainGuide(guideId)) 
            {
                Debug.Log(string.Format("<color=red>guide config is not id:{0}</color>", guideId));
                return;
            } 
            var guideRuntimeData = new GuideRuntimeData(guideId);
            guideRuntimeData.Run();
            runtimeDataList.Add(guideRuntimeData);
        }

        public void FinishGuide(int guideId) 
        {
            var index = runtimeDataList.FindIndex((r) => r.guideId == guideId);
            if (index == -1) 
            {
                Debug.Log("not current guide : " + guideId);
                return;
            }
            runtimeDataList.RemoveAt(index);

            //��ǰָ��������ִ����һ��ָ��������ָ������Ч��
            var config = GetGuideConfig(guideId);
            if (config.IsValid() && config.GuideType == 0) 
            {
                int nextGuideId = guideId + 1;
                DataCenter.Instance.guideData.guideId = nextGuideId;
                StartGuide(nextGuideId);
            }
        }

        //��������Ƿ��и�ָ��
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
        /// ���õ�ǰ�Ƿ���ǿָ��
        /// </summary>
        /// <param name="state"></param>
        public void SetCoerceGuideState(bool state) 
        {
            isCoerceGuide = state;
        }

        public void Update() 
        {
            if (Input.GetKeyDown(KeyCode.F4)) 
                isGetPath = true;

            if (isGetPath) 
            {
                if (Input.GetMouseButtonDown(0) && Stage.isTouchOnUI) 
                {
                    List<string> uiNames = new List<string>();
                    GObject clickGObject = GRoot.inst.touchTarget;
                    while (clickGObject != GRoot.inst) 
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


