using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
/*
public class EntityHandle : MonoBehaviour, IConvertGameObjectToEntity
{
    Entity _entity;
    EntityManager _entityManager;

    public Entity GetEntity() { return _entity; }
    public EntityManager GetEntityManager() { return _entityManager; }

    // OnDestroy 调用
    public void Close()
    {
        _entityManager.DestroyEntity(_entity);
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
        _entity = entity;
        _entityManager = dstManager;
    }
}
*/