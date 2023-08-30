using UnityEngine;
using Unity.Entities;

namespace SGame
{
    public class CharacterMoverAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity __entity, EntityManager __dstManager, GameObjectConversionSystem __conversionSystem)
        {
            CharacterMover component = new SGame.CharacterMover();
            EntityManagerManagedComponentExtensions.AddComponentData(__dstManager, __entity, component);
        }
    }
}