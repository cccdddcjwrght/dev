using Unity.Entities;
using UnityEngine;
namespace SGame
{
    // 用户输入操作
    public struct UserInput : IComponentData
    {
        // 滚动骰子
        public bool rollDice; 
    }
    
    public partial class UserInputsystem : SystemBase
    {
        private Entity               m_userInput;          // 用户输入对象
        private EventHandleContainer m_handleContiner;
        private bool                 m_eventRollDice;
        
        protected override void OnCreate()
        {
            base.OnCreate();
            m_userInput = EntityManager.CreateEntity(typeof(UserInput));
            m_handleContiner = new EventHandleContainer();
            //m_handleContiner += EventManager.Instance.Reg((int)GameEvent.PLAYER_ROTE_DICE, OnEventRollDice);
            s_instance = this;
        }
        private static UserInputsystem s_instance;
        public UserInputsystem Instance
        {
            get
            {
                return s_instance;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        public UserInput GetInput()
        {
            return EntityManager.GetComponentData<UserInput>(m_userInput);
        }

        protected override void OnUpdate()
        {
            Entities.ForEach((ref UserInput input) =>
            {
                input.rollDice = m_eventRollDice;
            }).WithoutBurst().Run();

            m_eventRollDice = false;
        }

        public void OnEventRollDice()
        {
            m_eventRollDice = true;
        }
    }
}