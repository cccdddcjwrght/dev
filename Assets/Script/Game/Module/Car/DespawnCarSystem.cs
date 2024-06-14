using Unity.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    public class DespawnCarSystem : ComponentSystem
    {
        private List<CarMono> m_destoryGameObject = new List<CarMono>();
            
        protected override void OnUpdate()
        {
            Entities.WithAll<DespawningTag, CarData>().ForEach((Entity entity, CarMono car) =>
            {
                m_destoryGameObject.Add(car);
                PostUpdateCommands.DestroyEntity(entity);
            });
            
            foreach (var item in m_destoryGameObject)
            {
                GameObject.Destroy(item.gameObject);
            }
            m_destoryGameObject.Clear();
        }
    }
}
