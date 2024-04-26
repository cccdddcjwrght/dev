using FairyGUI;
using SGame.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame 
{
    public class TransitionModule : Singleton<TransitionModule>
    {
        private EventHandleContainer m_EventHandle = new EventHandleContainer();
        private Dictionary<int, int> m_DependDict = new Dictionary<int, int>();

        public void Initalize() 
        {
            m_EventHandle += EventManager.Instance.Reg<int>((int)GameEvent.ENTER_ROOM, CreateFlightUI);
        }

        public void CreateFlightUI(int scene) 
        {
            UIRequest.Create(World.DefaultGameObjectInjectionWorld.EntityManager, SGame.UIUtils.GetUI("flight"));
        }

        public void PlayFlight(List<int> ids, Vector2 startPos, Vector2 endPos, float duration) 
        {
            
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

        public Vector2 ConvertGObjectGlobalPos(GObject gObject) 
        {
            Vector2 ret = gObject.LocalToGlobal(Vector2.zero);
            ret = GRoot.inst.GlobalToLocal(ret);
            ret.x += gObject.actualWidth * 0.5f;
            ret.y += gObject.actualHeight * 0.5f;
            return ret;
        }
    }

}

