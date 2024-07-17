using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConfigs;

namespace SGame 
{
    public interface IStep 
    {
        public void SetData(GuideFingerHandler handler, GuideRowData config);

        /// <summary>
        /// 执行当前指引步骤
        /// </summary>
        public IEnumerator Excute();

        /// <summary>
        /// 当前指引完成
        /// </summary>
        public void Finish();

        public void Dispose();

        public void Stop();

    }

    public class Step : IStep
    {
        public GuideRowData m_Config;
        public GuideFingerHandler m_Handler;

        public void SetData(GuideFingerHandler handler, GuideRowData config)
        {
            m_Handler = handler;
            m_Config = config;
        }

        public virtual IEnumerator Excute()
        {
            yield break;
        }

        public void Finish()
        {
            Debug.Log(string.Format("<color=cyan> guideId:{0} step:{1} cmd: {2}  finish time : {3} </color>", m_Config.GuideId, m_Config.Step, m_Config.Cmd, Time.realtimeSinceStartupAsDouble));
            Dispose();
            UILockManager.Instance.Require("guide_step_runing");
            m_Handler.DisableControl(true);
            //excute next step
            EventManager.Instance.Trigger((int)GameEvent.STEP_NEXT, m_Config.GuideId);
        }

        public virtual void Dispose()
        {

        }

        public virtual void Stop()
        {
            Dispose();
        }
    }
}

