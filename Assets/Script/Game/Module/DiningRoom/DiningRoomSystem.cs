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
		private Room _room;
		private RoomRowData _roomCfg;

		private DiningRoomLogic _currentRoom;
		private EventHandleContainer _eHandlers;
		private Entity _animUI;
		private bool _enterAnim;

		public bool isEnterSceneCompleted { get; private set; }

		#region Method

		public void Init(GameWorld world)
		{
			_gameWorld = world;
			InitEvents();
			WorktableHud.Instance.Init();

			EventManager.Instance.Reg(((int)GameEvent.WORK_TABLE_ALL_MAX_LV), OnWorktableAllMaxLv);

		}

		public IEnumerator LoadRoom(int roomID, Action<int> progress = null)
		{
			if (_currentRoom == null || _currentRoom.cfgID != roomID)
			{
				if (ConfigSystem.Instance.TryGet<RoomRowData>(roomID, out var room))
				{
					var lastLogic = _currentRoom;
					_roomCfg = room;
					isEnterSceneCompleted = false;
					_currentRoom = new DiningRoomLogic(roomID);
					_currentRoom.name = room.Resource;
					log.Info("Begin EnterRoom:" + roomID);
					var req = SceneSystemV2.Instance.Load(_currentRoom.name);
					req.logic = Wait();
					req.unloadLogic = _currentRoom.Close;
					if (progress != null)
						req.onStateChange += progress;
					lastLogic?.Close();
					((Func<bool>)(() => req.isDone)).Wait(OnLoadCompleted);
					if (lastLogic != null)
						EventManager.Instance.Trigger(((int)GameEvent.PREPARE_LEVEL_ROOM));
					_sceneFlag = _gameWorld.GetEntityManager().CreateEntity(typeof(LevelScene));
					lastLogic = default;

					return req;
				}
				else
				{
					log.Error($"Cant Find Room:{roomID}");
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


		private void OnLoadCompleted()
		{
			if (_room != null)
			{
				if (!_room.isnew)
				{
					if (DataCenter.GetIntValue(GuideModule.GUIDE_FIRST, 0) == 0)
					{
						//开局动画没播放玩杀掉进程，这里跳过第一步骤
						DataCenter.Instance.guideData.guideStep += 1;
						DataCenter.SetIntValue(GuideModule.GUIDE_FIRST, 1);
					}
					OnEnterRoomCompleted().Start();
				}
				else
				{
					if (DataCenter.GetIntValue(GuideModule.GUIDE_FIRST, 0) == 0)
					{
						EventManager.Instance.Trigger((int)GameEvent.GUIDE_CREATE);
						EventManager.Instance.Trigger((int)GameEvent.GUIDE_FIRST);
						DataCenter.SetIntValue(GuideModule.GUIDE_FIRST, 1);
					}

					OnEnterRoomCompleted(true).Start();
				}
			}
		}

		private IEnumerator OnEnterRoomCompleted(bool wait = false)
		{
			if (wait)
			{
				StaticDefine.G_WAIT_WELCOME = true;
				if (_animUI != default)
					yield return new WaitUIClose(SGame.UI.UIModule.Instance.GetEntityManager(), _animUI);
				_animUI = default;
				UIUtils.OpenUI("welcomenewlevel");
				yield return new WaitUntil(() => !StaticDefine.G_WAIT_WELCOME);
			}
			log.Info("EnterRoom :" + _currentRoom.cfgID);
			EventManager.Instance.AsyncTrigger(((int)GameEvent.ENTER_ROOM), _currentRoom.cfgID);
			EventManager.Instance.AsyncTrigger(((int)GameEvent.AFTER_ENTER_ROOM), _currentRoom.cfgID);
			_gameWorld.GetEntityManager().DestroyEntity(_sceneFlag);
#if !SVR_RELEASE
			log.Info("EnterRoom Event Completed:" + _currentRoom.cfgID);
#endif
			isEnterSceneCompleted = true;
		}

		private IEnumerator Wait()
		{
			if (_currentRoom != null)
			{
#if !SVR_RELEASE
				log.Info("[scene]Start EnterRoom Init");
#endif
				_room = DataCenter.RoomUtil.EnterRoom(_currentRoom.cfgID, true);
				EventManager.Instance.Trigger(((int)GameEvent.BEFORE_ENTER_ROOM), _currentRoom.cfgID);
				if (!string.IsNullOrEmpty(_roomCfg.Decor))
					UIUtils.OpenUI("scenedecorui");
				var flag = DataCenter.IsNew;
				DataCenter.IsNew = false;
				if (flag)
				{
#if ENABLE_SPLASH
					StaticDefine.G_WAIT_VIDEO = true;
					UIUtils.OpenUI("enterscene", -1); 
#endif
				}
				yield return _currentRoom.Wait();
#if !SVR_RELEASE
				log.Info("[scene]End EnterRoom Init");
#endif
				if (StaticDefine.G_WAIT_VIDEO)
				{
#if !SVR_RELEASE
					log.Info("[scene]EnterRoom Splash Start");
#endif
					yield return new WaitUntil(() => StaticDefine.G_VIDEO_COMPLETE);
#if !SVR_RELEASE
					log.Info("[scene]EnterRoom Splash Completed");
#endif
				}
				if (flag)
				{
					_animUI = SGame.UIUtils.OpenUI("welcomeanim");
					yield return new WaitUIOpen(SGame.UI.UIModule.Instance.GetEntityManager(), _animUI);
				}
			}
		}

		#endregion

		#region Event

		private void OnWorktableAllMaxLv()
		{
			ShowCompletedUI().Start();
		}

		IEnumerator ShowCompletedUI()
		{
			yield return null;
			WorktableHud.Instance.Close();
			SGame.UIUtils.OpenUI("levelcomplete");
		}

		#endregion

	}
}
