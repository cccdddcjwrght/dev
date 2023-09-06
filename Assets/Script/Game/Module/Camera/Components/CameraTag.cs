using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SGame
{
    public enum CameraType
    {
        BASE_MAP   = 0, // 场景摄像机
        TRAVEL_MAP = 1, // 出行场景摄像机
        PLAYER     = 2, // 跟随角色的摄像机
        
        MAX        = 3, // 摄像机最大数量
    }
    
    public class CameraTag : MonoBehaviour
    {
        public CameraType cameraType;
    }
}
