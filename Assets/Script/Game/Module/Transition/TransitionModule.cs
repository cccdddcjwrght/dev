using FairyGUI;
using SGame.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame 
{
    public enum FlightType 
    {
        GOLD    = 1,
        DIAMOND = 2,
        BOX     = 3,
        PET     = 4,
    }

    public class TransitionModule : Singleton<TransitionModule>
    {
        private EventHandleContainer m_EventHandle = new EventHandleContainer();
        private Dictionary<int, int> m_DependDict = new Dictionary<int, int>();

        public static float duration = 1.5f;

        public void Initalize() 
        {
            m_EventHandle += EventManager.Instance.Reg<int>((int)GameEvent.ENTER_ROOM, CreateFlightUI);
        }

        public void CreateFlightUI(int scene) 
        {
            UIRequest.Create(World.DefaultGameObjectInjectionWorld.EntityManager, SGame.UIUtils.GetUI("flight"));
        }


        List<int>     m_TempIdList      = new List<int>();
        List<Vector2> m_TempVecList     = new List<Vector2>();
        Dictionary<int, Vector2> m_TempDict = new Dictionary<int, Vector2>();

        public void PlayFlight(GList list, List<int[]> reward) 
        {
            m_TempIdList.Clear();
            m_TempVecList.Clear();
            m_TempDict.Clear();
            for (int i = 0; i < reward.Count; i++)
            {
                int[] r = reward[i];
                if (r.Length >= 2) 
                {
                    int type = r[0];
                    int itemId = r[1];
                    if (type == 1 && CheckIsTranId(itemId))
                    {
                        Vector2 pos = ConvertGObjectGlobalPos(list.GetChildAt(i));
                        m_TempDict.Add(itemId, pos);
                    } 
                }
            }

            foreach (var t in m_TempDict)
                m_TempIdList.Add(t.Key);

            m_TempIdList.Sort();
            foreach (var id in m_TempIdList)
            {
                if (m_TempDict.TryGetValue(id, out var pos))
                    m_TempVecList.Add(pos);
            }
            PlayFlight(m_TempIdList, m_TempVecList);
        }

        public void PlayFlight(List<int> ids, List<Vector2> startPos) 
        {
            EventManager.Instance.Trigger((int)GameEvent.FLIGHT_LIST_CREATE, ids, startPos, Vector2.zero, duration);
        }

        public void PlayFlight(GObject gObject, int id, float offsetX = 0, float offsetY = 0) 
        {
            if (gObject == null || gObject.isDisposed) return;
            var startPos = ConvertGObjectGlobalPos(gObject) + new Vector2(offsetX, offsetY);
            EventManager.Instance.Trigger((int)GameEvent.FLIGHT_SINGLE_CREATE, id, startPos, Vector2.zero, duration);
        }

        public void AddDepend(int id)
        {
            if (!m_DependDict.ContainsKey(id)) m_DependDict[id] = 0;
            m_DependDict[id]++; 
        }

        public void SubDepend(int id) 
        {
            m_DependDict[id]--;
        }

        public bool IsShow(int id) 
        {
            if(m_DependDict.ContainsKey(id))
                return m_DependDict[id] > 0;
            return false;
        }

        //检测奖励id是否是需要播放飞行特效
        public bool CheckIsTranId(int id) 
        {
            if (id == (int)FlightType.GOLD || id == (int)FlightType.DIAMOND || CheckIsBox(id) || CheckIsPet(id))
                return true;
            return false;
        }

        //检测是否是宝箱 --对应物品表配置
        public bool CheckIsBox(int id)
        {
            if (ConfigSystem.Instance.TryGet<GameConfigs.ItemRowData>(id, out var data)) 
                if (data.Type == 3 && data.SubType == 1) return true;
            
            return false;
        }

        public bool CheckIsPet(int id) 
        {
            if (ConfigSystem.Instance.TryGet<GameConfigs.ItemRowData>(id, out var data))
            {
                if (data.Type == 10) return true;
            }
            return false;
        }

        public Vector2 ConvertGObjectGlobalPos(GObject gObject) 
        {
            Vector2 ret = gObject.LocalToGlobal(Vector2.zero);
            ret = GRoot.inst.GlobalToLocal(ret);

            if (gObject.pivot == Vector2.zero) 
            {
                ret.x += gObject.actualWidth * 0.5f;
                ret.y += gObject.actualHeight * 0.5f;
            }
            return ret;
        }
    }

}

