
using Unity.Entities;
using Unity.VisualScripting;

namespace SGame
{
    [Inspectable]
    [System.Serializable]
    public struct UIParamFloat : IComponentData
    {
        [Inspectable]
        public float Value;
    }
}
