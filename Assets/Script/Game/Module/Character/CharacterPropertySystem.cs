using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Entities;

namespace SGame
{
    public struct Property : IComponentData
    {
        public int propertyType;
    }

    public struct PropertyChange : IComponentData
    {
    }

    public class CharacterPropertySystem
    {
    }
}