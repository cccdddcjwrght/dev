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
            Dispose();
            //excute next step
            EventManager.Instance.Trigger((int)GameEvent.STEP_NEXT);
        }

        public virtual void Dispose()
        {

        }
    }
}

