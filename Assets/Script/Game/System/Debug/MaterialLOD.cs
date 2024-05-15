using System;
using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using log4net;
using SGame;
using UnityEngine;

/// 根据不同的shader LOD 修改材质
namespace SGame
{
    public class MaterialLOD : MonoBehaviour
    {
        private static ILog log = LogManager.GetLogger("game.materials");
        
        [Serializable]
        public struct LodMaterial
        {
            public Material material;
            public int      lod;
        }

        public  List<LodMaterial> setting_materials;
        
#if !GAME_ART
        void SetMaterial(LodMaterial mat)
        {
            var render = GetComponent<Renderer>();
            if (render == null)
            {
                log.Error("render not found!");
                return;
            }

            render.material = mat.material;
        }

        void UpdateLod()
        {
            int lod = Shader.globalMaximumLOD;
            foreach (var item in setting_materials)
            {
                if (item.lod <= lod)
                {
                    SetMaterial(item);
                    return;
                }
            }
        }

        void UpdateQuatily(int quatily)
        {
            UpdateLod();
        }
        
        public void Start()
        {
            EventManager.Instance.Reg<int>((int)GameEvent.QUATILY_CHANGE, UpdateQuatily);
            UpdateQuatily(GameQuatilySetting.Instance.quatily);
        }

        private void OnDestroy()
        {
            EventManager.Instance.UnReg<int>((int)GameEvent.QUATILY_CHANGE, UpdateQuatily);
        }
#endif
    }
}