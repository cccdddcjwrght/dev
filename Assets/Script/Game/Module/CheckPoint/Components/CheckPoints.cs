using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Transforms;
namespace SGame
{
    public class CheckPointData : IComponentData
    {
        // 检查点
        public List<float3> Value;
    }
    
    public class CheckPoints : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(
            Entity entity,
            EntityManager dstManager,
            GameObjectConversionSystem conversionSystem)
        {
            Transform[] all_transform = transform.GetComponentsInChildren<Transform>();
            CheckPointData v = new CheckPointData();
            v.Value = new List<float3>();
            foreach (var t in all_transform)
            {
                if (t == transform)
                    continue;

                if (t.parent == transform)
                {
                    v.Value.Add(t.transform.position);
                }

                //Entity e = conversionSystem.GetPrimaryEntity(t);
                GameObject.Destroy(t.gameObject);
            }

            dstManager.AddComponentData(entity, v);
        }
    }
}