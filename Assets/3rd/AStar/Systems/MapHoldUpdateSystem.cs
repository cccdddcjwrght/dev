using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace GameTools.Paths
{
	[UpdateAfter(typeof(FollowSystem))]
	public partial class MapHoldUpdateSystem : SystemBase
	{

		protected override void OnUpdate()
		{
			if (AStar.map == null) return;

			AStar.map?.Hold(-1, -1, 0);
			Entities.WithAll<Translation, Follow>().WithoutBurst().ForEach((Entity e, in Translation t) =>
			{
				var index = AStar.GetGridPos(t.Value);
				AStar.map?.Hold(index.x, index.y, e.Index);

			}).Schedule();
		}


	}

}