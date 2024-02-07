using Unity.Mathematics;

namespace SGame
{
    /// <summary>
    /// 座位类型
    /// </summary>
    public enum CHAIR_TYPE
    {
        UNKNOWN  = 0,
        ORDER    = 1, // 点菜，送菜 区
        CUSTOMER = 2, // 顾客, 收菜 区
        OPERATOR = 3, // 操作区
    }

    /// <summary>
    /// 座位数据
    /// </summary>
    public struct ChairData
    {
        // 地图位置
        public int2   map_pos;

        // 所属的桌子ID
        public int          tableID;

        // 座椅在桌子上的位置
        public int          chairIndex;

        // 占用角色ID
        public int          playerID;

        // 座位类型
        public CHAIR_TYPE   type;

        /// <summary>
        /// 判断座位是否位空
        /// </summary>
        public bool IsEmpty => playerID == 0;
        
        /// <summary>
        /// 判断是否是空的
        /// </summary>
        public bool IsEmptyChair => Equals(ChairData.Empty);

        public static ChairData Empty => new ChairData() { type = CHAIR_TYPE.UNKNOWN, playerID = 0, chairIndex = 0, tableID = 0, map_pos = int2.zero };

        /// <summary>
        /// 相等判定
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ChairData other)
        {
            return other.playerID == this.playerID 
                && other.type == this.type &&
                other.map_pos.Equals( this.map_pos) &&
                other.chairIndex == this.chairIndex &&
                other.tableID == this.tableID;
        }

        public bool Equals(object other)
        {
            if (other.GetType() != typeof(ChairData))
                return false;
            return Equals((ChairData)other);
        }

        public static bool operator == (ChairData lhs, ChairData rhs) => lhs.Equals(rhs);
        public static bool operator !=(ChairData lhs, ChairData rhs) => !(lhs == rhs);

    }
}
