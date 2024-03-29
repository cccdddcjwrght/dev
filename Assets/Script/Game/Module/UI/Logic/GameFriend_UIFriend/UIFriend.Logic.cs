
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
		private FriendData m_data;
		
		partial void InitLogic(UIContext context){
			//HttpSystem.Instance.Get("")
			FiberCtrl.Pool.Run(Run());

			m_view.m_listFirends.itemRenderer	= ItemRenderFriend;
			m_view.m_listRecomment.itemRenderer = ItemRenderRecomment;
			
		}

		IEnumerator Run()
		{
			HttpResult req = HttpSystem.Instance.Get("http://192.168.10.109:8080/friends?{}");
			yield return req;
			var data = JsonUtility.FromJson<NetFriendData>(req.data);

			var body = JsonUtility.FromJson<FriendData>(data.data);
			m_data = body;
			log.Info("recive http=" + req.data);

			m_view.m_listFirends.numItems = m_data.Friends.Count;
			m_view.m_listRecomment.numItems = m_data.RecommendFriends.Count;
		}
		
		partial void UnInitLogic(UIContext context){

		}

		/*
		FirendData GetFriendData(int index)
		{
			if (index < m_data.HireFriends.Count)
			{
				return m_data.h
			}
		}
		*/


		
		private void ItemRenderFriend(int index, GObject item)
		{
			var view = (UI_FriendItem)item;
			var data = m_data.Friends[index]; // [index];
			view.name = data.name;     //index.ToString();


			if (data.hireTime > 0)
			{
				view.m_state.selectedIndex = 1;
			}
			else
			{
				view.m_state.selectedIndex = 2;
			}
			//view
			
			//view.SetIcon(data.Icon);
			//view.m_title.SetTextByKey(data.Name);
			//view.m_desc.SetTextByKey(data.Des, Mathf.Max(1, data.Value));
			//view.m_type.selectedIndex = data.Mark;
/*
			var str = Utils.ConvertNumberStr(data.Cost(2));
			UIListener.SetIconIndex(view.m_click, (int)data.Cost(1) - 1);
			UIListener.SetText(view.m_click, str);
			view.m_click.onClick.Clear();
			view.m_click.onClick.Add(() => OnItemClick(index, view, data));
			RefreshBtn(item);
			*/
		}
		
		private void ItemRenderRecomment(int index, GObject item)
		{
			var view = (UI_FriendItem)item;
			var data = m_data.RecommendFriends[index]; // [index];
			view.name = data.name;            //index.ToString();
			view.m_state.selectedIndex = 0;

			/*
			view.SetIcon(data.Icon);
			view.m_title.SetTextByKey(data.Name);
			view.m_desc.SetTextByKey(data.Des, Mathf.Max(1, data.Value));
			view.m_type.selectedIndex = data.Mark;

			var str = Utils.ConvertNumberStr(data.Cost(2));
			UIListener.SetIconIndex(view.m_click, (int)data.Cost(1) - 1);
			UIListener.SetText(view.m_click, str);
			view.m_click.onClick.Clear();
			view.m_click.onClick.Add(() => OnItemClick(index, view, data));
			RefreshBtn(item);
			*/
		}
	}
}
