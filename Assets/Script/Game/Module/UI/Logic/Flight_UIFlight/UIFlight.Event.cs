
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Flight;
    using System.Collections.Generic;
    using Unity.Entities;
    using GameConfigs;

    public partial class UIFlight
	{
		public EventHandleContainer m_EventHandle = new EventHandleContainer();
		Stack<GGraph> graphStack = new Stack<GGraph>();
		public float speed = GlobalDesginConfig.GetFloat("get_effect_speed"); //特效移动速度
		private Vector2 _exploreXY;

		bool m_IsSet;

		partial void InitEvent(UIContext context){
			m_EventHandle += EventManager.Instance.Reg<int, Vector2, Vector2, float>((int)GameEvent.FLIGHT_SINGLE_CREATE, Play);
			m_EventHandle += EventManager.Instance.Reg<List<int>, List<Vector2>, Vector2, float>((int)GameEvent.FLIGHT_LIST_CREATE, Play);
			m_EventHandle += EventManager.Instance.Reg<List<int>, List<Vector2>, Vector2, float, int>((int)GameEvent.FLIGHT_LIST_TYPE, Play);
			m_EventHandle += EventManager.Instance.Reg<int, Vector2, Vector2, float, int>((int)GameEvent.FLIGHT_SINGLE_TYPE, Play);

			m_EventHandle += EventManager.Instance.Reg<int, int>((int)GameEvent.RECORD_PROGRESS, PlayRankShow);
		}
		partial void UnInitEvent(UIContext context){
			m_EventHandle.Close();
			m_EventHandle = null;
		}

		void Play(List<int> ids, List<Vector2> startPos, Vector2 endPos, float duration) => Play(ids, startPos, endPos, duration, 0);

		void Play(List<int> ids, List<Vector2> startPos, Vector2 endPos, float duration, int type = 0) 
		{
			float timer = 0;
			int index = 0;
			ids.ForEach((id)=> 
			{
				Utils.Timer(0.01f, null, delay: timer, completed: () =>
				{
					Play(id, startPos[index], endPos, duration, type);
					index++;
				});
				timer += 0.5f;
			});
		}

		void Play(int id, Vector2 startPos, Vector2 endPos, float duration) => Play(id, startPos, endPos, duration, 0);

		void Play(int id, Vector2 startPos, Vector2 endPos, float duration, int type = 0) 
		{
 			AddEffect(id, startPos, endPos, duration, type);
		}

		void AddEffect(int id, Vector2 startPos, Vector2 endPos, float duration, int type) 
		{
			PlayAudio(id);
			int flyEffectID = GetFlyEffectID(id);   //飞行特效id
			//初始点特效
			int startEffectID = GetBoomEffectID(id, type);

			if (TransitionModule.Instance.CheckIsBox(id))
			{
				ConfigSystem.Instance.TryGet<GameConfigs.ItemRowData>(id, out var config);
				m_view.m_Box.SetIcon(config.Icon);
			}
			SetPos();
			TransitionModule.Instance.AddDepend(GetDependType(id));
			if (endPos == Vector2.zero) endPos = GetFinalPos(id);
			var graph1 = GetGraph(startPos.x, startPos.y);

			float d = Vector3.Distance(startPos, endPos);
			float time = d / speed ;
			float delay = time * 0.9f;

			EffectSystem.Instance.AddEffect(startEffectID, graph1);
			Utils.Timer(time, null, m_view, completed: () => Push(graph1, id));

			var graph2 = GetGraph(startPos.x, startPos.y);
			EffectSystem.Instance.AddEffect(flyEffectID, graph2);

			GTween.To(startPos, endPos, time).SetTarget(m_view).OnUpdate((t) =>
			{
				graph2.SetXY(t.value.x, t.value.y);
			}).OnComplete(()=> 
			{
				Refresh();
				//延迟一点时间
				Utils.Timer(delay, null, m_view, completed: () =>
				{
					 Push(graph2, id);
					 TransitionModule.Instance.SubDepend(GetDependType(id));
					 Refresh();
				 });
			});
		}


		//type -1 小的炸开特效 2-大的炸开特效（金币钻石才有）其他的默认26特效id
		int GetBoomEffectID(int id, int type) 
		{
			if (id == 1)
			{
				if (type == 2) return 46;
				return 21;
			}

			else if (id == 2)
			{
				if (type == 2) return 47;
				return 22;
			}
			else if (id == 3)
				return 61;
			else if (id == ((int)ItemID.EXP)) return 63;
			return 26;
		}

		int GetFlyEffectID(int id) 
		{
			if (id == 1) return 23;         //金币飞行特效id
			else if (id == 2) return 24;    //钻石飞行特效id
			else if (id == 3) return 62;
			else if (id == ((int)ItemID.EXP)) return 64;
			//宠物蛋飞行特效
			else if (TransitionModule.Instance.CheckIsPet(id)) return 500;
			return id;
		}

		int GetDependType(int id) 
		{
			if (TransitionModule.Instance.CheckIsBox(id)) return (int)FlightType.BOX;
			else if (TransitionModule.Instance.CheckIsPet(id)) return (int)FlightType.PET;
			return id;
		}

		void PlayAudio(int id) 
		{
			if (id == 1) 4.ToAudioID().PlayAudio();
			else if (id == 2) 5.ToAudioID().PlayAudio();
		}


        GGraph GetGraph(float x, float y)
        {
			GGraph holder;
			if (graphStack.Count > 0)
				holder = graphStack.Pop();
            else
            {
                holder = new GGraph();
                holder.SetSize(1, 1);
                m_view.AddChild(holder);
			}
			holder.SetXY(x, y);
			holder.visible = true;
            return holder;
        }

		void Push(GObject holder, int id) 
		{
			holder.visible = false;
			//holder.asGraph.SetNativeObject(null);
			//holder.Dispose();
			graphStack.Push(holder.asGraph);
		}

		Vector2 GetFinalPos(int id) 
		{
			if (id == (int)FlightType.GOLD) return m_view.m_Gold.xy - new Vector2(m_view.m_Gold.width * 0.5f, 0);
			else if (id == (int)FlightType.DIAMOND) return m_view.m_Diamond.xy - new Vector2(m_view.m_Diamond.width * 0.5f, 0);
			else if (id == (int)FlightType.SHOVEL) return _exploreXY;
			else if (TransitionModule.Instance.CheckIsBox(id)) return m_view.m_Box.xy + new Vector2(m_view.m_Box.width * 0.5f, m_view.m_Box.height * 0.5f) + new Vector2(-8, 3)/*偏差*/;
			else if (TransitionModule.Instance.CheckIsPet(id)) return m_view.m_Pet.xy;
			return default;
		}

        void Refresh() 
		{
			SetPos();
			SetGoldText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)FlightType.GOLD)));
			SetDiamondText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)FlightType.DIAMOND)));
			m_view.m_totalBtn.GetChild("num").text = string.Format("X{0}", ReputationModule.Instance.GetTotalValue());

			if (!SGame.UIUtils.CheckIsOnlyMainUI())
			{
				m_view.m_Gold.visible = TransitionModule.Instance.IsShow((int)FlightType.GOLD);
				m_view.m_totalBtn.visible = TransitionModule.Instance.IsShow((int)FlightType.GOLD);
				m_view.m_Diamond.visible = TransitionModule.Instance.IsShow((int)FlightType.DIAMOND);
				m_view.m_Box.visible = TransitionModule.Instance.IsShow((int)FlightType.BOX);
				m_view.m_Pet.visible = TransitionModule.Instance.IsShow((int)FlightType.PET);
			}
			else 
			{
				m_view.m_Gold.visible = false;
				m_view.m_totalBtn.visible = false;
				m_view.m_Diamond.visible = false;
				m_view.m_Box.visible = false;
				m_view.m_Pet.visible = false;
			}
		}


		void SetPos() 
		{
			//if (m_IsSet) return;
			var e = SGame.UIUtils.GetUIEntity("mainui");
			if (e != Entity.Null)
			{
				var ui = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentObject<SGame.UI.UIWindow>(e);
				if (ui != null)
				{
					m_view.m_Gold.xy = ui.Value.contentPane.GetChild("Gold").xy;
					m_view.m_Diamond.xy = ui.Value.contentPane.GetChild("Diamond").xy;

					var boxGObject = ui.Value.contentPane.GetChildByPath("leftList.eqgift");
					if(boxGObject == null) boxGObject = ui.Value.contentPane.GetChildByPath("leftList.treasureBtn");
					m_view.m_Box.xy = GRoot.inst.GlobalToLocal(boxGObject.LocalToGlobal(Vector2.zero));
					m_view.m_Pet.xy = ui.Value.contentPane.GetChildByPath("petBtn").xy;
					m_view.m_totalBtn.xy = ui.Value.contentPane.GetChildByPath("totalBtn").xy;
					_exploreXY = ui.Value.contentPane.GetChildByPath("explore").xy;

				}
			}
			//m_IsSet = true;
		}

		void PlayRankShow(int marker, int value) 
		{
			var rankBtn = GetMainBtn("rightList.right.rank");
			var config = RankModule.Instance.GetCurRankConfig();
			if (rankBtn != null && config.IsValid() && marker == config.RankingMarker) 
			{
				m_view.m_rank.visible = true;
				var screenPos = rankBtn.LocalToGlobal(Vector2.zero);
				var logicScreenPos = GRoot.inst.GlobalToLocal(screenPos);
				m_view.m_rank.xy = logicScreenPos;

				m_view.m_rank.GetController("ctrlTime").selectedIndex = 1;
				m_view.m_rank.GetChild("content").SetText(Utils.FormatTime(RankModule.Instance.GetRankTime()));

				if (ConfigSystem.Instance.TryGet<GameConfigs.RankConfigRowData>((r) => r.RankingMarker == marker, out var rankConfig) &&
					ConfigSystem.Instance.TryGet<GameConfigs.ItemRowData>(rankConfig.ItemId, out var itemConfig))
					m_view.m_rankTran.SetIcon(itemConfig.Icon);

				m_view.m_rankTran.SetText("+" + value);
				m_view.m_rankTran.GetTransition("play").Play(() => m_view.m_rank.visible = false);
			}
		}

		public GObject GetMainBtn(string path) 
		{
			var e = SGame.UIUtils.GetUIEntity("mainui");
			if (e != Entity.Null)
			{
				var ui = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentObject<SGame.UI.UIWindow>(e);
				if (ui != null)
				{
					var gObject = ui.Value.contentPane.GetChildByPath(path);
					if (gObject != null)
						return gObject;
				}
			}
			return default;
		}
	}

}
