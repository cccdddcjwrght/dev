using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    public class TileEventTriggerAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        
        public void Convert(
            Entity entity,
            EntityManager dstManager,
            GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddBuffer<TileEventTrigger>(entity);
        }
    }
}