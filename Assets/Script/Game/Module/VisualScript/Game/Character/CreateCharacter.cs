using log4net;
using SGame;
using Unity.VisualScripting;
using Unity.Mathematics;
using UnityEngine;

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

        protected override void Definition()
        {
            inputTrigger = ControlInput("Input", (flow) =>
            {
                var id =  flow.GetValue<int>(roleID);
                var tag = flow.GetValue<string>(mapTag);

                if (string.IsNullOrEmpty(tag))
                {
                    // 直接使用地址
                    var pos = flow.GetValue<int2>(mapPos);
                    CharacterModule.Instance.Create(id, GameTools.MapAgent.CellToVector(pos.x, pos.y));
                }
                else
                {
                    // 使用标签
                    Vector2Int map_pos;
                    if (GameTools.MapAgent.GetTagGrid(tag, out map_pos))
                    {
                        CharacterModule.Instance.Create(id, GameTools.MapAgent.CellToVector(map_pos.x, map_pos.y));
                    }
                    else
                    {
                        log.Error("Tag Not Found=" + tag);
                    }
                }
                return outputTrigger;
            });
            
            roleID      = ValueInput<int>("roleID", 0);
            mapPos      = ValueInput<int2>("mapPos", int2.zero);
            mapTag      = ValueInput<string>("mapTag", "");
            outputTrigger = ControlOutput("Output");
        }
    }
}
