using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace SGame
{
    public class DataCenter
    {
        // 用户数据
        public Entity   m_userData;
        
        private GameWorld m_world;
        static DataCenter s_instance;

        public static DataCenter Instance
        {
            get
            {
                return s_instance;
            }
        }

        // 获取用户信息
        public UserData GetUserData()
        {
            return m_world.GetEntityManager().GetComponentData<UserData>(m_userData);
        }

        public void SetUserData(UserData data)
        {
            m_world.GetEntityManager().SetComponentData(m_userData, data);
        }

        public DataCenter(GameWorld world)
        {
            s_instance = this;
            m_world    = world;
            m_userData = world.GetEntityManager().CreateEntity(typeof(UserData));
        }
        
        public void Update()
        {

        }


        public void Shutdown()
        {
            
        }
    }
}
