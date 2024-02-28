using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
    // 获得角色AI 对象
    [UnitTitle("GetCharacterAI")] 
    [UnitCategory("Game/Attribute")]
    public class GetCharacterAI : BaseRoleAttribute
    {
        [DoNotSerialize]
        public ValueOutput resultAI; // 返回AI GameObject对象
        
        // 端口定义
        protected override void Definition()
        {
            base.Definition();

            
            resultAI          = ValueOutput<GameObject>("AI", GetValue);
        }
        

        /// <summary>
        /// 获得订单时间
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        GameObject GetValue(Flow flow)
        {
            var character = GetCharacter(flow);
            if (character == null)
                return null;

            return character.script;
        }
    }
}