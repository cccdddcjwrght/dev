using SGame;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace SGame 
{
    public class CharacterIdleModule : Singleton<CharacterIdleModule>
    {
        public EventHandleContainer m_EventHandle = new EventHandleContainer();
        //记录下当前待机区域的角色Id
        private Dictionary<int2, int> m_IdlePos = new Dictionary<int2, int>();
        public void Initlaize()
        {
            m_EventHandle += EventManager.Instance.Reg((int)GameEvent.PREPARE_LEVEL_ROOM, Clear);
        }

        public int2 GetCharacterEmptyIdlePos(string tag, int characterID)
        {
            var list = GameTools.MapAgent.GetTagGrids(tag);
            foreach (var item in m_IdlePos)
            {
                if (item.Value == characterID)
                    return item.Key;
            }

            var character = CharacterModule.Instance.FindCharacter(characterID);
            int2 charPos = new int2((int)character.pos.position.x, (int)character.pos.position.z);
            if (list.FindIndex((p)=> p.x == charPos.x && p.y == charPos.y) >= 0) 
            {
                if (!m_IdlePos.ContainsKey(charPos))
                {
                    m_IdlePos.Add(charPos, characterID);
                    return charPos;
                }
            }

            foreach (var v in list)
            {
                int2 pos = new int2(v.x, v.y);
                if (!m_IdlePos.ContainsKey(pos))
                {
                    m_IdlePos.Add(pos, characterID);
                    return pos;
                }
            }
            return default;
        }

        public Vector3 GetIdlePosKey(int characterID) 
        {
            foreach (var item in m_IdlePos)
                if (item.Value == characterID) return new Vector3(item.Key.x, 0, item.Key.y);
            return default;
        }

        public bool CheckCharacterIsIdle(int characterID) 
        {
            if (m_IdlePos.ContainsValue(characterID)) 
            {
                var character = CharacterModule.Instance.FindCharacter(characterID);
                if (character.pos.position == GetIdlePosKey(characterID))
                    return true;
            }
            return false;
        }

        public void RemoveCharacterIdlePos(int characterID)
        {
            if (m_IdlePos.ContainsValue(characterID))
            {
                foreach (var item in m_IdlePos)
                {
                    if (item.Value == characterID) 
                    {
                        m_IdlePos.Remove(item.Key);
                        return;
                    }
                }
            }
        }

        public void Clear() 
        {
            m_IdlePos.Clear();
        }
    }
}


