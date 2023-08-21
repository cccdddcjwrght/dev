
using System.Collections.Generic;
namespace SGame
{
    public class PropertyManager : Singleton<PropertyManager>
    {
        // 数据存储
        private Dictionary<int, ItemGroup> m_Values;

        public void Initalize()
        {
            m_Values = new Dictionary<int, ItemGroup>(32);
        }

        // 获取道具管理器
        public ItemGroup GetGroup(ItemType type)
        {
            return GetGroup((int)type);
        }
        
        public ItemGroup GetGroup(int type)
        {
            if (m_Values.TryGetValue(type, out ItemGroup group))
            {
                return group;
            }

            group = new ItemGroup();
            m_Values.Add(type, group);
            return group;
        }

        public ItemGroup GetUserGroup(int playerId)
        {
            return GetGroup(ItemType.USER);
        }
    }
}