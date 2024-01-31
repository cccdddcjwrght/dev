using Unity.Entities;
namespace SGame
{
    [GenerateAuthoringComponent]
    public struct CharacterAttribue : IComponentData
    {
        /// <summary>
        /// 角色ID配置表 RoleData ID
        /// </summary>
        public int roleID;

        /// <summary>
        /// 角色类型
        /// </summary>
        public int roleType;
    }
}