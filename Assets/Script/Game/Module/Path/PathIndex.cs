using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    public struct PathIndex
    {
        public int      Index;          // 路径点上的Index
        public float    distance;       // 剩余距离
        public Vector3  targetPoint;    // 目标点

        public bool isSuccess => Index >= 0;
    }
}