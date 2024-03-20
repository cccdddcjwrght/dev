using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GameConfigs;
using log4net;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SGame.Dining
{

	public struct LevelScene : IComponentData { }

	public partial class DiningRoomSystem : MonoSingleton<DiningRoomSystem>
	{
		#region Static
		private static ILog log = LogManager.GetLogger("DiningRoom");
		#endregion

		private GameWorld _gameWorld;
		private Entity _sceneFlag;

		private DiningRoomLogic _currentRoom;
		private EventHandleContainer _eHandlers;

		public bool isEnterSceneCompleted { get; private set; }

		#region Method

		public void Init(GameWorld world)
		{
			_gameWorld = world;
			InitEvents();
			WorktableHud.Instance.Init();
		}

		public IEnumerator LoadRoom(int roomID, Action<int> progress = null)
		{
			if (_currentRoom == null || _currentRoom.cfgID != roomID)
			{
				if (ConfigSystem.Instance.TryGet<RoomRowData>(roomID, out var room))
				{
					var lastLogic = _currentRoom;
					isEnterSceneCompleted = false;
					_currentRoom = new DiningRoomLogic(roomID);
					_currentRoom.name = room.Resource;
					var req = SceneSystemV2.Instance.Load(_currentRoom.name);
					req.logic = Wait();
					req.unloadLogic = _currentRoom.Close;
					if (progress != null)
						req.onStateChange += progress;
					lastLogic?.Close();
					((Func<bool>)(() => req.isDone)).Wait(OnEnterRoomCompleted);
					if (lastLogic != null)
						EventManager.Instance.Trigger(((int)GameEvent.PREPARE_LEVEL_ROOM));
					_sceneFlag = _gameWorld.GetEntityManager().CreateEntity(typeof(LevelScene));
					lastLogic = default;

					return req;
				}
			}
			else
			{
				log.Warn("Current Room is :" + roomID);
				progress?.Invoke(-1);
			}
			return null;
		}


		#endregion


		#region Check



		#endregion

		#region Private

		private void InitEvents()
		{
			_eHandlers = new EventHandleContainer();

		}


		private void OnEnterRoomCompleted()
		{
			log.Info("Enter Room :" + _currentRoom.cfgID);
			EventManager.Instance.Trigger(((int)GameEvent.ENTER_ROOM), _currentRoom.cfgID);
			EventManager.Instance.Trigger(((int)GameEvent.AFTER_ENTER_ROOM), _currentRoom.cfgID);
			_gameWorld.GetEntityManager().DestroyEntity(_sceneFlag);
			isEnterSceneCompleted = true;
		}

		private IEnumerator Wait()
		{
			if (_currentRoom != null)
			{
				DataCenter.RoomUtil.EnterRoom(_currentRoom.cfgID, true);
				EventManager.Instance.Trigger(((int)GameEvent.BEFORE_ENTER_ROOM), _currentRoom.cfgID);
				UIUtils.OpenUI("scenedecorui");
				yield return _currentRoom.Wait();

				if (DataCenter.IsNew)
				{
					DataCenter.IsNew = false;
					UIUtils.OpenUI("enterscene", -1);
					while (!StaticDefine.G_VIDEO_COMPLETE)
						yield return null;
				}
			}
		}


		#endregion


	}
}
