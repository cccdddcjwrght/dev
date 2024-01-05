using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tagmark : MonoBehaviour
{
	public enum TagEnum
	{
		[InspectorName("����")]
		Island,
		[InspectorName("����")]
		Interaction,
		[InspectorName("��Ч")]
		Effect,
		[InspectorName("��ʼ�ؿ�")]
		Orgin,
		[InspectorName("�ϲ�����")]
		Combine,
	}

	[InspectorName("����")]
	public TagEnum Tag;

}
