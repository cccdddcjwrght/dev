using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Transforms;

namespace GameTools.Paths
{
/*
public class PlayerAgent : MonoBehaviour
{
	[SerializeField]
	private EntityHandle _entityHandle;

	public bool isManual = true;

	private void Awake()
	{
		_entityHandle = _entityHandle ?? GetComponent<EntityHandle>();
	}

	private void Start()
	{
		Debug.Log("entity = " + _entityHandle.GetEntity());

		if (_entityHandle == null)
			Debug.Log("entity is null");
		else
			Debug.Log("entity handle is not null!");

	}

	void UpdatePosition()
	{
		var mgr = _entityHandle.GetEntityManager();
		var e = _entityHandle.GetEntity();
		Translation trans = mgr.GetComponentData<Translation>(e);
		this.transform.position = trans.Value;
	}


	private void Update()
	{
		if (isManual && Input.GetMouseButtonDown(0))
		{
			var mgr = _entityHandle.GetEntityManager();
			var e = _entityHandle.GetEntity();

			var wPos = AStar.hit;
			if (AStar.IsInMap(wPos))
			{
				var _map_pos = AStar.GetGridPos(wPos);
				var _start_pos = AStar.GetGridPos(transform.position);

				int2 map_pos = new int2(_map_pos.x, _map_pos.y);
				int2 start_pos = new int2(_start_pos.x, _start_pos.y);


				var p = new FindPathParams { start_pos = start_pos, end_pos = map_pos };
				if (mgr.HasComponent<FindPathParams>(e))
					mgr.SetComponentData(e, p);
				else
					mgr.AddComponentData(e, p);
			}
		}
		UpdatePosition();
	}


}
*/
}
