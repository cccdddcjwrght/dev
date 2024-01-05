using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tagmark : MonoBehaviour
{
	public enum TagEnum
	{
		[InspectorName("地形")]
		Island,
		[InspectorName("交互")]
		Interaction,
		[InspectorName("特效")]
		Effect,
		[InspectorName("初始地块")]
		Orgin,
		[InspectorName("合并网格")]
		Combine,
	}

	[InspectorName("类型")]
	public TagEnum Tag;

}
