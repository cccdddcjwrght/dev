using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace SGame
{
    public class DataCenter
    {
        // 用户数据
        public Entity        m_data;
        
        private GameWorld    m_world;
        static  DataCenter   s_instance;

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
            return m_world.GetEntityManager().GetComponentData<UserData>(m_data);
        }

        public void SetUserData(UserData data)
        {
            m_world.GetEntityManager().SetComponentData(m_data, data);
        }

        public UserSetting GetUserSetting()
        {
            return m_world.GetEntityManager().GetComponentData<UserSetting>(m_data);
        }
        
        public void SetUserSetting(UserSetting data)
        {
            m_world.GetEntityManager().SetComponentData(m_data, data);
        }

        public EntityManager EntityManager
        {
            get { return m_world.GetEntityManager();  }
        }

        public DataCenter(GameWorld world)
        {
            s_instance = this;
            m_world    = world;
            m_data = EntityManager.CreateEntity(typeof(UserData)
            ,typeof(UserSetting));
            
            EntityManager.SetComponentData(m_data, UserSetting.GetDefault());
            EntityManager.SetComponentData(m_data, UserData.GetDefault());
        }
        
        public void Update()
        {

        }


        public void Shutdown()
        {
            
        }
    }
}
