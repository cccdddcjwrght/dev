using log4net;
using SGame;
using Unity.VisualScripting;
using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;
using SGame.Firend;
using Unity.Entities;

namespace SGame.VS
{
    /// <summary>
    /// 通过角色ID获得角色对象
    /// </summary>
	
	[UnitTitle("CreateCharacter")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/Character")]
    public sealed class CreateCharacter : Unit
    {
        private static ILog log = LogManager.GetLogger("game.character");
        
        [DoNotSerialize]
        public ControlInput     inputTrigger;

        // 成功返回流程
        [DoNotSerialize]
        public ControlOutput    outputTrigger;

        /// <summary>
        /// 角色配置表ID
        /// </summary>
        [DoNotSerialize]
        public ValueInput roleID { get; private set; }
        
        /// <summary>
        /// 角色地图位置
        /// </summary>
        [DoNotSerialize]
        public ValueInput mapPos { get; private set; }
        
        /// <summary>
        /// 角色地图标签
        /// </summary>
        [DoNotSerialize]
        public ValueInput mapTag { get; private set; }
        
        
        /// <summary>
        /// 是否启用属性系统
        /// </summary>
        [DoNotSerialize]
        public ValueInput hasAttribute { get; private set; }
        
        /// <summary>
        /// 是否是雇佣好友
        /// </summary>
        /// <returns></returns>
        [DoNotSerialize]
        public ValueInput m_isEmployee { get; private set; }
        
        /// <summary>
        /// 是否是雇佣好友
        /// </summary>
        /// <returns></returns>
        [DoNotSerialize]
        public ValueInput m_playerID { get; private set; }
        
        /// <summary>
        /// 创建的角色Entity
        /// </summary>
        public ValueOutput m_characterEntity { get; private set; }
        private CharacterSpawnResult m_spwanResult;

        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                var id          = flow.GetValue<int>(roleID);
                var tag         = flow.GetValue<string>(mapTag);
                var attr        = flow.GetValue<bool>(hasAttribute);
                var isEmployee  = flow.GetValue<bool>(m_isEmployee);
                var playerID    = flow.GetValue<int>(m_playerID);
                if (isEmployee)
                {
                    // 雇员ID则直接改变玩家ID
                    var friend = FriendModule.Instance.GetHiringFriend();
                    if (friend != null)
                    {
                        playerID = friend.player_id;
                    }
                    else
                    {
                        // 找不到雇员ID
                        log.Error("not found Hiring Friend!");
                    }
                }

                if (string.IsNullOrEmpty(tag))
                {
                    // 直接使用地址
                    var pos = flow.GetValue<int2>(mapPos);
                    m_spwanResult = CharacterModule.Instance.Create(id, GameTools.MapAgent.CellToVector(pos.x, pos.y), attr, playerID);
                }
                else
                {
                    // 使用标签
                    List<Vector2Int> map_pos = GameTools.MapAgent.GetTagGrids(tag);
                    if (map_pos != null && map_pos.Count > 0)
                    {
                        var posIndex = RandomSystem.Instance.NextInt(0, map_pos.Count);
                        var pos = map_pos[posIndex];
                        m_spwanResult = CharacterModule.Instance.Create(id, GameTools.MapAgent.CellToVector(pos.x, pos.y), attr, playerID);
                    }
                    else
                    {
                        log.Error("Tag Not Found=" + tag);
                    }
                }
                return outputTrigger;
            });
            
            roleID          = ValueInput<int>("roleID", 0);
            mapPos          = ValueInput<int2>("mapPos", int2.zero);
            mapTag          = ValueInput<string>("mapTag", "");
            hasAttribute    = ValueInput<bool>("hasAttribute", true);
            m_isEmployee    = ValueInput<bool>("isEmployee", false);
            m_playerID      = ValueInput<int>("playerID", 0);
            m_characterEntity = ValueOutput<CharacterSpawnResult>("Result", (flow)=> m_spwanResult);
            outputTrigger = ControlOutput("Output");
        }
    }
}
