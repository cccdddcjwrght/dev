using System;
using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using GameTools;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
	public struct RewardWait : IComponentData
	{
		public Entity spawn;
		public uint wait;
	}

	public class RewardSystem : ComponentSystem
	{

		private const string c_def_asset = "Assets/BuildAsset/Prefabs/Scenes/other/rewardbox.prefab";
		private uint c_def_asset_id = 0;

		private EndSimulationEntityCommandBufferSystem _command;


		void OnSceneReward(Vector2Int pos, Action call, string asset)
		{
			var e = EntityManager.CreateEntity();

			if (asset == "-1")
			{
				if (ConfigSystem.Instance.TryGet<LevelRowData>(DataCenter.Instance.roomData.roomID, out var levelCfg))
				{
					var index = Math.Clamp(SGame.Randoms.Random._R.Next(0, 2), 0, 1);
					var roleID = levelCfg.CustomerId(index);
					var start = GameTools.MapAgent.RandomPop("start_0");
					SpawnRole(roleID, start, pos, call);
				}
			}
			else
			{
				if (c_def_asset_id == 0) c_def_asset_id = c_def_asset.ToIndex();
				EntityManager.AddComponentData<RewardData>(e, new RewardData()
				{
					asset = string.IsNullOrEmpty(asset) ? c_def_asset_id : asset.ToIndex(),
					pos = MapAgent.CellToVector(pos.x, pos.y),
					excuteID = call.ToIndex(),
					parentID = MapAgent.agent.grid.gameObject.ToIndex()
				});
			}
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
					handler.DestroyEntity(e);
				}
			});


			Entities.WithAll<RewardData>().WithNone<RewardWait>().ForEach((Entity e, ref RewardData data) =>
			{
				var wait = handler.CreateEntity();
				handler.AddComponent<RewardWait>(e, new RewardWait() { spawn = wait });
				handler.AddComponent(wait, new SpawnReq()
				{
					assetID = data.asset,
					parentID = data.parentID,
					pos = data.pos
				});

			});

		}

		private void SpawnRole(int roleID, Vector2Int start, Vector2Int target, Action call = null)
		{
			Move(roleID, start, target, call).Start();
		}

		private IEnumerator Move(int roleID, Vector2Int start, Vector2Int target, Action call = null)
		{
			var wait = CharacterModule.Instance.Create(roleID, MapAgent.CellToVector(start.x, start.y), false);
			yield return new WaitUntil(() => wait.IsReadly());
			var role = CharacterModule.Instance.FindCharacter(wait.entity);
			role.transform.Find("AI").gameObject.SetActive(false);
			role.SetSpeed(GlobalDesginConfig.GetFloat("worker_speed", 20f));
			role.MoveTo(new Unity.Mathematics.int2(target.x, target.y));
			yield return new WaitUntil(() => !role.isMoving);
			EffectSystem.Instance.Spawn3d(25, point: role.transform.position);
			yield return new WaitForSeconds(0.5f);
			CharacterModule.Instance.DespawnCharacterEntity(wait.entity);
			call?.Invoke();
			wait.entity = default;
			wait = default;
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
				 null, transform.localPosition + new Vector3(0, 0.25f, -0.1f));
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