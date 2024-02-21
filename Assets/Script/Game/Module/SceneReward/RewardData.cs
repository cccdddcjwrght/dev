using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SGame
{
	public struct RewardData:IComponentData
	{
		public Vector3 pos;
		public uint asset;
		public uint excuteID;
	}
}
