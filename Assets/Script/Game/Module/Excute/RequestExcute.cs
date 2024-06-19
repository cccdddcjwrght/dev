using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using log4net;
using UnityEngine;

namespace SGame
{
	public class InitCallAttribute : Attribute { }

	//操作执行模块
	public partial class RequestExcuteSystem : Singleton<RequestExcuteSystem>
	{
		private static ILog log = LogManager.GetLogger("request.excute");
		private static DelayExcuter _delayer = DelayExcuter.Instance;
		private static EventManager _eMgr = EventManager.Instance;

		private GameWorld _gameWorld;

		private static string path = "Assets/BuildAsset/VisualScript/Prefabs/ReqExcute.prefab";

		public void Init(GameWorld world, ResourceManager resource)
		{
			this._gameWorld = world;
			InitEvent();
			var methods = GetType()
				.GetMethods(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
				.Where(m => Attribute.IsDefined(m, typeof(InitCallAttribute))).ToList();
			try
			{
				CallInitMethod(methods).Start();
			}
			catch (Exception e)
			{
				log.Error(e.Message + "-" + e.StackTrace);
			}

			var go = resource.LoadPrefab(path);
			if (go)
			{
				go = GameObject.Instantiate(go);
				GameObject.DontDestroyOnLoad(go);
			}
#if !SVR_RELEASE
			UIUtils.OpenUI("gmui");
#endif


		}

		private void InitEvent()
		{
			new WaitEvent<int>(((int)GameEvent.AFTER_ENTER_ROOM)).Wait((e) => AdModule.Instance.ReadyAllAd());
		}

		private IEnumerator CallInitMethod(List<System.Reflection.MethodInfo> methods)
		{
			yield return null;
			methods.Foreach(m => m.Invoke(null, Array.Empty<object>()));
			DataCenter.HunterUtil.Init();
			DataCenter.CookbookUtils.Init();
		}


	}
}
