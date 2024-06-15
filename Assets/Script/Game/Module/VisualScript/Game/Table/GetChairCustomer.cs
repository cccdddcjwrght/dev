using System;
using log4net;
using SGame;
using Unity.Entities;
using Unity.VisualScripting;
using UnityEngine;

namespace SGame.VS
{
    /// <summary>
    /// 获取座位上角色的AI
    /// </summary>
    [UnitTitle("GetChairCustomer")] 
    [UnitCategory("Game/Table")]
    public sealed class GetChairCustomer : Unit
    {
        private ILog log = LogManager.GetLogger("game.table");
        

        [DoNotSerialize]
        public ValueInput chairData { get; private set; }
        
        [DoNotSerialize]
        public ValueOutput Value { get; private set; }

        protected override void Definition()
        {
            chairData       = ValueInput<ChairData>("chair");
            Value           = ValueOutput<GameObject>("ai", Get);
        }
        
        private GameObject Get(Flow flow)
        {
            var chair = flow.GetValue<ChairData>(chairData);
            if (chair.tableID <= 0)
            {
                throw new Exception("not valid chair");
            }

            var table = TableManager.Instance.Get(chair.tableID);
            if (table == null)
            {
                throw new Exception("table not found");
            }
            
            chair = table.GetChair(chair.chairIndex);
            if (chair.IsEmpty)
            {
                throw new Exception("chair is empty");
            }

            if (chair.playerID != 0)
            {
                // 获取角色AI
                Character character = CharacterModule.Instance.FindCharacter(chair.playerID);
                if (character != null)
                {
                    return character.script;
                }
            }
            
            // 获取对象AI
            if (chair.playerEntity != Entity.Null)
            {
                var car = CarModule.Instance.Get(chair.playerEntity);
                if (car != null)
                {
                    return car.script;
                }
            }

            log.Error("chair customer player not found");
            return null;
        }
    }
}
