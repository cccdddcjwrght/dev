using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    public class GuideRuntimeData
    {
        int __guideId;    //当前指引id
        int __stepIndex;  //当前步骤索引

        UnityEngine.Coroutine m_Coroutine;
        GuideFingerHandler __handler;

        public int guideId { get { return __guideId; } }

        List<Step> steps = new List<Step>();
        EventHandleContainer __event = new EventHandleContainer();
        public GuideRuntimeData(int guideId)
        {
            __guideId = guideId;
            __stepIndex = 0;
            __event += EventManager.Instance.Reg<int>((int)GameEvent.STEP_NEXT, Run);
            __handler = new GuideFingerHandler();

            InitStep();
        }

        //初始化指引表中对应指引的所有步骤
        public void InitStep() 
        {
            if (ConfigSystem.Instance.TryGets<GameConfigs.GuideRowData>((c) => c.GuideId == __guideId, out var configs)) 
            {
                configs.ForEach((c) =>
                {
                    Type type = Type.GetType("SGame.Step" + c.Cmd);
                    var step = Activator.CreateInstance(type) as Step;
                    step.SetData(__handler, c);
                    steps.Add(step);
                });
            } 
        }

        public void Run(int guideId)
        {
            UILockManager.Instance.Release("guide_step_runing");
            __handler.DisableControl(false);
            if (guideId != __guideId) return;
            //清除主线以外的指引
            if (ConfigSystem.Instance.TryGet<GameConfigs.GuideRowData>((i) => i.GuideId ==__guideId && i.Step == __stepIndex, out var cfg) && cfg.GuideType == 0) 
            {
                GuideManager.Instance.CheckRecruitOpen();
                GuideManager.Instance.ClearOtherGuide();
                EventManager.Instance.Trigger((int)GameEvent.GUIDE_STEP, cfg.Id);
            }

            if (__stepIndex < steps.Count)
            {
                var config = steps[__stepIndex].m_Config;
                Debug.Log(string.Format("<color=green>cur guide id:{0}, step: {1}, cmd: {2}</color>", __guideId, __stepIndex, steps[__stepIndex].m_Config.Cmd));
                Debug.Log(string.Format("<color=cyan> guideId:{0} step:{1} cmd: {2}  start time : {3} </color>", config.GuideId, config.Step, config.Cmd, Time.realtimeSinceStartupAsDouble));
                m_Coroutine = steps[__stepIndex++].Excute().Start();
            }
            else
            {
                FinishGuide();
            }
        }

        /// <summary>
        /// 0指引正常结束， 1中途终止
        /// </summary>
        /// <param name="code"></param>
        public void FinishGuide(int code = 0)
        {
            __event.Close();
            __event = null;
            if (code == 0)
            {
                Debug.Log(string.Format("<color=green>guide finish id: {0} </color>" , __guideId));
                EventManager.Instance.Trigger((int)GameEvent.GUIDE_FINISH, __guideId);
            }
            else if(code == 1) 
            {
                int lastStepIndex = __stepIndex - 1;
                steps[lastStepIndex].Stop();
                m_Coroutine.Stop();
                Debug.Log(string.Format("<color=red>guide stop id:{0} step: {1}</color>", __guideId, lastStepIndex));
            }
            Dispose();
        }

        public void Dispose() 
        {
            steps.Clear();
        }

    }
}

