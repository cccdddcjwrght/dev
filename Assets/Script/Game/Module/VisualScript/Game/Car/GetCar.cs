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
    /// 获取公交座位信息
    /// </summary>
	
	[UnitTitle("GetCar")] 
    [UnitCategory("Game/Car")]
    public sealed class GetCar : Unit
    {
        private static ILog log = LogManager.GetLogger("game.car");
        
        
        /// <summary>
        /// 汽车对象
        /// </summary>
        [DoNotSerialize]
        public ValueInput carEntity { get; private set; }
        
        
        [DoNotSerialize]
        public ValueOutput carOutput { get; private set; }


        protected override void Definition()
        {
            carEntity           = ValueInput<Entity>("bus");
            carOutput                 = ValueOutput<CarMono>("car", (flow) =>
            {
                var entity = flow.GetValue<Entity>(this.carEntity);
                var EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                if (!EntityManager.HasComponent<CarMono>(entity))
                {
                    return null;
                }

                var carMono = EntityManager.GetComponentObject<CarMono>(entity);
                return carMono;
            });
        }
    }
}
