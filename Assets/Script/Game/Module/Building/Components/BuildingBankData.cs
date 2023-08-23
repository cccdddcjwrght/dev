using Unity.Entities;

namespace SGame
{
    public struct BuildingBankData : IComponentData
    {
        /// <summary>
        /// 银行金币
        /// </summary>
        public long Value;
    }
}