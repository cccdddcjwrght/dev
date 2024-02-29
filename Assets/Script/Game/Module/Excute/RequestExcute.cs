using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace SGame
{
	public class InitCallAttribute : Attribute { }

	//操作执行模块
	public partial class RequestExcuteSystem : Singleton<RequestExcuteSystem>
	{
		private static ILog log = LogManager.GetLogger("request.excute");


		private GameWorld _gameWorld;

		public void Init(GameWorld world)
		{
			this._gameWorld = world;
			InitEvent();
			var methods = GetType()
				.GetMethods(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
				.Where(m => Attribute.IsDefined(m, typeof(InitCallAttribute))).ToList();
			try
			{
				methods.Foreach(m => m.Invoke(null, Array.Empty<object>()));
			}
			catch (Exception e)
			{
				log.Error(e.Message + "-" + e.StackTrace);
			}
		}

		private void InitEvent()
		{

		}

	}
}
