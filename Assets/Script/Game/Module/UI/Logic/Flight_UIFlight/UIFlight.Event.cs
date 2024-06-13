
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
		//Stack<GLoader> loaderStack = new Stack<GLoader>();
		public float speed = GlobalDesginConfig.GetFloat("get_effect_speed"); //特效移动速度

		bool m_IsSet;

		partial void InitEvent(UIContext context){
			m_EventHandle += EventManager.Instance.Reg<int, Vector2, Vector2, float>((int)GameEvent.FLIGHT_SINGLE_CREATE, Play);
			m_EventHandle += EventManager.Instance.Reg<List<int>, List<Vector2>, Vector2, float>((int)GameEvent.FLIGHT_LIST_CREATE, Play);

			m_EventHandle += EventManager.Instance.Reg<int, int>((int)GameEvent.RECORD_PROGRESS, PlayRankShow);
		}
		partial void UnInitEvent(UIContext context){
			m_EventHandle.Close();
			m_EventHandle = null;
		}

		void Play(List<int> ids, List<Vector2> startPos, Vector2 endPos, float duration) 
		{
			float timer = 0;
			int index = 0;
			ids.ForEach((id)=> 
			{
				Utils.Timer(0.1f, null, delay: timer, completed: () =>
				{
					Play(id, startPos[index], endPos, duration);
					index++;
				});
				timer += 0.5f;
			});
		}

		void Play(int id, Vector2 startPos, Vector2 endPos, float duration) 
		{
			AddEffect(id, startPos, endPos, duration);
		}

		void AddEffect(int id, Vector2 startPos, Vector2 endPos, float duration) 
		{

			int effectId1 = id + 20;
			int effectId2 = id + 22;

			if (TransitionModule.Instance.CheckIsBox(id))
			{
				effectId1 = 26;
				effectId2 = id;
				if (ConfigSystem.Instance.TryGet<GameConfigs.ItemRowData>(id, out var config)) 
				{
					m_view.m_Box.SetIcon(config.Icon);
				}
				TransitionModule.Instance.AddDepend((int)FlightType.BOX);
			}
			else if (TransitionModule.Instance.CheckIsPet(id))
			{
				effectId1 = 26;
				effectId2 = 500;
				TransitionModule.Instance.AddDepend((int)FlightType.PET);
			}
			else
			{
				TransitionModule.Instance.AddDepend(id);
			}
			Refresh();

			if (endPos == Vector2.zero) endPos = GetFinalPos(id);
			var graph1 = GetGraph(startPos.x, startPos.y);

			float d = Vector3.Distance(startPos, endPos);
			float time = d / speed;

			EffectSystem.Instance.AddEffect(effectId1, graph1);
			if (ConfigSystem.Instance.TryGet<GameConfigs.effectsRowData>(effectId1, out var data)) 
			{
				Utils.Timer(time, null, m_view, completed: () => Push(graph1, id));
			}

			var graph2 = GetGraph(startPos.x, startPos.y);
			EffectSystem.Instance.AddEffect(effectId2, graph2);


			GTween.To(startPos, endPos, time).SetTarget(m_view).OnUpdate((t) =>
			{
				graph2.SetXY(t.value.x, t.value.y);
			}).OnComplete(()=> 
			{
				Push(graph2, id);
				if (TransitionModule.Instance.CheckIsBox(id)) TransitionModule.Instance.SubDepend((int)FlightType.BOX);
				else if (TransitionModule.Instance.CheckIsPet(id)) TransitionModule.Instance.SubDepend((int)FlightType.PET);
				else TransitionModule.Instance.SubDepend(id);

				Refresh();
			});
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
            for (int i = 0; i < holder.displayObject.gameObject.transform.childCount; i++)
				GameObject.Destroy(holder.displayObject.gameObject.transform.GetChild(i).gameObject);
			//if (id == (int) FlightType.BOX)
			//	loaderStack.Push(holder.asLoader);
			//else
			graphStack.Push(holder.asGraph);
		}

		Vector2 GetFinalPos(int id) 
		{
			if (id == (int)FlightType.GOLD) return m_view.m_Gold.xy;
			else if (id == (int)FlightType.DIAMOND) return m_view.m_Diamond.xy;
			else if (TransitionModule.Instance.CheckIsBox(id)) return m_view.m_Box.xy + new Vector2(m_view.m_Box.width * 0.5f, m_view.m_Box.height * 0.5f);
			else if (TransitionModule.Instance.CheckIsPet(id)) return m_view.m_Pet.xy;
			return default;
		}

        void Refresh() 
		{
			SetPos();
			SetGoldText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)FlightType.GOLD)));
			SetDiamondText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)FlightType.DIAMOND)));
			m_view.m_totalBtn.GetChild("num").text = string.Format("X{0}", ReputationModule.Instance.GetTotalValue());

			m_view.m_Gold.visible = TransitionModule.Instance.IsShow((int)FlightType.GOLD);
			m_view.m_totalBtn.visible = TransitionModule.Instance.IsShow((int)FlightType.GOLD);
			m_view.m_Diamond.visible = TransitionModule.Instance.IsShow((int)FlightType.DIAMOND);
			m_view.m_Box.visible = TransitionModule.Instance.IsShow((int)FlightType.BOX);
			m_view.m_Pet.visible = TransitionModule.Instance.IsShow((int)FlightType.PET);
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

					var boxGObject = ui.Value.contentPane.GetChildByPath("leftList.right.eqgift");
					if (boxGObject != null) 
						m_view.m_Box.xy = GRoot.inst.GlobalToLocal(boxGObject.LocalToGlobal(Vector2.zero));

					m_view.m_Pet.xy = ui.Value.contentPane.GetChildByPath("petBtn").xy;
					m_view.m_totalBtn.xy = ui.Value.contentPane.GetChildByPath("totalBtn").xy;
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
