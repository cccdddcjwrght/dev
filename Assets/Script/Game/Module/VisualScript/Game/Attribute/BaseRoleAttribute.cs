using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;
using SGame;

namespace SGame.VS
{
    /// <summary>
    /// 通过角色ID获得角色对象
    /// </summary>
    public enum INPUT_TYPE : uint
    {
        ID      = 0,  // 角色对象ID
        OBJECT  = 1,  // 角色对象
        ROLEID  = 2,  // 角色类型
    }
    
    // 角色煮饭时间
    public abstract class BaseRoleAttribute : Unit
    {
        [DoNotSerialize]
        public ValueInput m_target;    // 角色
        
        [SerializeAs(nameof(inputType))]
        private INPUT_TYPE _inputType;

        [DoNotSerialize]
        [Inspectable, UnitHeaderInspectable("Type")]
        public INPUT_TYPE inputType
        {
            get => _inputType;
            set => _inputType = value;
        }
        
        // 端口定义
        protected override void Definition()
        {
            switch (inputType)
            {
                case INPUT_TYPE.ID:
                    m_target = ValueInput<int>("CharacterID");
                    break;
                case INPUT_TYPE.ROLEID:
                    m_target = ValueInput<int>("RoleID", 1);
                    break;
                case INPUT_TYPE.OBJECT:
                    m_target = ValueInput<Character>("Character");
                    break;
            }
        }

        // 获得角色类型
        protected int GetRoleID(Flow flow)
        {
            if (inputType == INPUT_TYPE.ROLEID)
                return flow.GetValue<int>(m_target);

            Character character = null;
            if (inputType == INPUT_TYPE.OBJECT)
            {
                character =  flow.GetValue<Character>(m_target);
            }
            else
            {
                var characterID =flow.GetValue<int>(m_target);
                character = CharacterModule.Instance.FindCharacter(characterID);
            }

            if (character == null)
                return 0;
            
            var attribute = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<CharacterAttribue>(character.entity);
            return attribute.roleID;
        }
    }
}