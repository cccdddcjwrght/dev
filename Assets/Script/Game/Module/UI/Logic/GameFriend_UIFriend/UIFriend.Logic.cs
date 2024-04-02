
using System.Collections;
using System.Collections.Generic;
using log4net;
using SGame.Http;
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.GameFriend;
	using SGame.Firend;
	
	public partial class UIFriend
	{
		//private static ILog log = LogManager.GetLogger("game.friend");
		
		partial void InitLogic(UIContext context){

			//FiberCtrl.Pool.Run(Run());
		}

		IEnumerator Run()
		{
			HttpResult req = HttpSystem.Instance.Get("http://192.168.10.109:8080/friends?{}");
			yield return req;
			var data = JsonUtility.FromJson<NetFriendData>(req.data);

			var body = JsonUtility.FromJson<FriendData>(data.data);
			log.Info("recive http=" + req.data);
			
			FirendModule.Instance.SetData(body);
			FirendModule.Instance.UpdateFriends();

			m_view.m_listFirends.numItems = body.Friends.Count;
			m_view.m_listRecomment.numItems = body.RecommendFriends.Count;
		}


		
		partial void UnInitLogic(UIContext context){

		}
		
	}
}
