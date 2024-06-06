using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepArea : Step
    {
        public override IEnumerator Excute()
        {
            yield return WaitAreaUnLock();
            Finish();
        }

        public IEnumerator WaitAreaUnLock() 
        {
            int area = (int)m_Config.FloatParam(0);
            while (true) 
            {
                if (ConfigSystem.Instance.TryGet<GameConfigs.RoomAreaRowData>(area, out var c))
                {
                    if (DataCenter.Instance.roomData.current.id <= c.Scene && DataCenter.MachineUtil.IsAreaEnable(area))
                        yield break;
                }
                yield return null;
            }
        }
    }
}

