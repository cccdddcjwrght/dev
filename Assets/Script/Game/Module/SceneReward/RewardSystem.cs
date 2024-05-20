using System;
using System.Collections;
using System.Collections.Generic;
using GameTools;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
	public struct RewardWait : IComponentData
	{

		public Entity spawn;

	}

	public partial class RewardSystem : ComponentSystem
	{

		private const string c_def_asset = "Assets/BuildAsset/Prefabs/Scenes/other/rewardbox.prefab";
		private uint c_def_asset_id = 0;

		private EndSimulationEntityCommandBufferSystem _command;


		void OnSceneReward(Vector2Int pos, Action call, string asset)
		{
			var e = EntityManager.CreateEntity();
			if (c_def_asset_id == 0) c_def_asset_id = c_def_asset.ToIndex();
			EntityManager.AddComponentData<RewardData>(e, new RewardData()
			{
				asset = string.IsNullOrEmpty(asset) ? c_def_asset_id : asset.ToIndex(),
				pos = MapAgent.CellToVector(pos.x, pos.y),
				excuteID = call.ToIndex(),
				parentID = MapAgent.agent.grid.gameObject.ToIndex()
			});
		}

		protected override void OnCreate()
		{
			base.OnCreate();
			_command = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
			EventManager.Instance.Reg<Vector2Int, Action, string>(((int)GameEvent.SCENE_REWARD), OnSceneReward);

		}

		protected override void OnUpdate()
		{
			var handler = _command.CreateCommandBuffer();

			Entities.WithAll<RewardData, RewardWait>().ForEach((Entity e, ref RewardData data, ref RewardWait w) =>
			{
				if (SpawnSystem.Instance.IsLoaded(w.spawn))
				{
					var go = SpawnSystem.Instance.GetObject(w.spawn);
					if (go)
					{
						var hit = go.AddComponent<RewardHit>();
						hit.call = data.excuteID.FromIndex<Action>(true);
						hit.entity = w.spawn;
					}
					handler.DestroyEntity( e);
				}
			});

			Entities.WithAll<RewardData>().WithNone<RewardWait>().ForEach((Entity e, ref RewardData data) =>
			{
				var wait = handler.CreateEntity();
				handler.AddComponent<RewardWait>( e, new RewardWait() { spawn = wait });
				handler.AddComponent( wait, new SpawnReq()
				{
					assetID = data.asset,
					parentID = data.parentID,
					pos = data.pos
				});

			});

		}
	}


	public class RewardHit : MonoBehaviour, ITouchOrHited
	{
		public Action call;
		public Entity entity;

		public void OnClick()
		{
			3.ToAudioID().PlayAudio();
			EffectSystem.Instance.AddEffect(
				 1, 
				 transform.parent.gameObject,
				 null, transform.localPosition + new Vector3(0, 0.25f, -0.1f) );
			call?.Invoke();
			if (entity != default)
				SpawnSystem.Instance.Release(entity);
		}

		private void OnDestroy()
		{
			call = null;
			entity = default;
		}
	}

}