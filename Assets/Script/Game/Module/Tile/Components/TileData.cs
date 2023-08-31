using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SGame
{
    // 地块数据
    public class TileData : IComponentData
    {
        public int          tileId;       // 地块ID       
        public int          pos;          // 地块数据在索引中的位置
        public int          buildingId;   // buildingID
        public float3       Position3d;   // 地块位置
        public Entity       tileEntity;   // tile对象
        public MapType      mapType;      // tile属于那个类型的地图, 有默认和出行地图区别
    }
}
