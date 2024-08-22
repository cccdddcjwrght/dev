using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SGame 
{
    public class GuideUtils
    {
        public static int2 GetFirstWorkTableXZ()
        {
            int2 pos = new int2();
            var ws = DataCenter.MachineUtil.GetWorktables((w) => !w.isTable);
            if (ws.Count > 0) 
            {
                var w = ws[0];
                if (ConfigSystem.Instance.TryGet<GameConfigs.RoomMachineRowData>((r) => r.Machine == w.id && r.Nowork != 1, out var data)) 
                {
                    pos.x = data.ObjId(1);
                    pos.y = data.ObjId(2);
                }
            }
            return pos;
        }

        public static bool CheckFirstWorkTableIsActive()
        {
            var ws = DataCenter.MachineUtil.GetWorktables((w) => !w.isTable);
            if (ws.Count > 0 && ws[0].level > 0 && ws[0].stations.Count > 0 && ws[0].stations.Count <= 1)
                return true;
            return false;
        }

        public static void SetGuideFingerUISort() 
        {
            var e = UIUtils.GetUIEntity("guidefinger");
            if (e != Entity.Null) 
            {
                var ui = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentObject<SGame.UI.UIWindow>(e);
                ui.rootUI.sortingOrder = 1;
            }
        }
    }

}

