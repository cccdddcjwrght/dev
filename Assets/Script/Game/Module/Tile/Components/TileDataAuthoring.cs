using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;
using Unity.Entities;

namespace SGame
{
    public class TileDataAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        private static ILog log = LogManager.GetLogger("game.tile");
        
        private const string NORMAT_TAG = "Normal";

        int GetTileId()
        {
            string n = this.gameObject.name;
            string[] id = n.Split("_");
            
            string number_id = id[id.Length - 1];
            if (!int.TryParse(number_id, out int nId))
            {
                log.Error("pase tile id fail name=" + n + " number id=" + number_id);
                return -1;
            }

            return nId;
        }
        
        public void Convert(
            Entity entity,
            EntityManager dstManager,
            GameObjectConversionSystem conversionSystem)
        {
            var mapType = MapType.NORMAL;
            if (transform.parent.name == NORMAT_TAG)
            {
                mapType = MapType.NORMAL;
            }
            else
            {
                mapType = MapType.TRVAL;
            }

            TileData data = new TileData();
            dstManager.AddComponent<TileData>(entity);
            int tileId = GetTileId();
            data.tileId = tileId;
            data.tileEntity = entity;
            dstManager.SetComponentData(entity, data);
        }
    }
}