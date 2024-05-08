
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Flight;
    using System.Collections.Generic;
    using Unity.Entities;

    public partial class UIFlight
	{
		public EventHandleContainer m_EventHandle = new EventHandleContainer();
		Stack<GGraph> graphStack = new Stack<GGraph>();
		Stack<GLoader> loaderStack = new Stack<GLoader>();

		bool m_IsSet;

		partial void InitEvent(UIContext context){
			m_EventHandle += EventManager.Instance.Reg<int, Vector2, Vector2, float>((int)GameEvent.FLIGHT_SINGLE_CREATE, Play);
			m_EventHandle += EventManager.Instance.Reg<List<int>, List<Vector2>, Vector2, float>((int)GameEvent.FLIGHT_LIST_CREATE, Play);
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
				timer += 0.2f;
			});
		}

		void Play(int id, Vector2 startPos, Vector2 endPos, float duration) 
		{
			if (TransitionModule.Instance.CheckIsBox(id)) AddBox(id, startPos, endPos, duration);
			else AddEffect(id, startPos, endPos, duration);
		}

		void AddEffect(int id, Vector2 startPos, Vector2 endPos, float duration) 
		{
			TransitionModule.Instance.AddDepend(id);
			Refresh();

			int effectId1 = id + 20;
			int effectId2 = id + 22;
			var graph1 = GetGraph(startPos.x, startPos.y);
			EffectSystem.Instance.AddEffect(effectId1, graph1);
			if (ConfigSystem.Instance.TryGet<GameConfigs.effectsRowData>(effectId1, out var data)) 
			{
				Utils.Timer(data.Duration, null, m_view, completed: () => Push(graph1, id));
			}

			if (endPos == Vector2.zero) endPos = GetFinalPos(id);
			var graph2 = GetGraph(endPos.x, endPos.y);
			EffectSystem.Instance.AddEffect(effectId2, graph2);

			GTween.To(startPos, endPos, duration).SetTarget(m_view).OnUpdate((t) =>
			{
				graph2.SetXY(t.value.x, t.value.y);
			}).OnComplete(()=> 
			{
				Push(graph2, id);
				TransitionModule.Instance.SubDepend(id);
				Refresh();
			});


		}

		public void AddBox(int id, Vector2 startPos, Vector2 endPos, float duration) 
		{
			TransitionModule.Instance.AddDepend((int)FlightType.BOX);
			Refresh();
			var loader = GetLoader(startPos.x, startPos.y);
			loader.SetIcon("ui_shop_icon_box_wood_close");
			if (endPos == Vector2.zero) endPos = GetFinalPos(id);
			GTween.To(startPos, endPos, duration).SetTarget(m_view).OnUpdate((t) =>
			{
				loader.SetXY(t.value.x, t.value.y);
			}).OnComplete(() =>
			{
				Push(loader, (int)FlightType.BOX);
				TransitionModule.Instance.SubDepend((int)FlightType.BOX);
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

		GLoader GetLoader(float x, float y) 
		{
			GLoader loader;
			if (loaderStack.Count > 0)
				loader = loaderStack.Pop();
			else
			{
				loader = new GLoader();
				loader.SetSize(80, 80);
				//loader.SetPivot(0.5f, 0.5f, true);
				loader.fill = FillType.ScaleFree;
				m_view.AddChild(loader);
			}
			loader.SetXY(x, y);
			loader.visible = true;
			return loader;
		}

		void Push(GObject holder, int id) 
		{
			holder.visible = false;
			if (id == (int) FlightType.BOX)
				loaderStack.Push(holder.asLoader);
			else
				graphStack.Push(holder.asGraph);
		}

		Vector2 GetFinalPos(int id) 
		{
			if (id == (int)FlightType.GOLD) return m_view.m_Gold.xy;
			else if (id == (int)FlightType.DIAMOND) return m_view.m_Diamond.xy;
			else if (TransitionModule.Instance.CheckIsBox(id)) return m_view.m_Box.xy;

			return default;
		}

        void Refresh() 
		{
			SetPos();
			SetGoldText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)FlightType.GOLD)));
			SetDiamondText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)FlightType.DIAMOND)));

			m_view.m_Gold.visible = TransitionModule.Instance.IsShow((int)FlightType.GOLD);
			m_view.m_Diamond.visible = TransitionModule.Instance.IsShow((int)FlightType.DIAMOND);
			m_view.m_Box.visible = TransitionModule.Instance.IsShow((int)FlightType.BOX);
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

					var boxGObject = ui.Value.contentPane.GetChildByPath("leftList.right").asList.GetChild("eqgift");
					if (boxGObject != null) 
						m_view.m_Box.xy = ui.Value.contentPane.GetChildByPath("leftList.right").asList.GetChild("eqgift").LocalToGlobal(Vector2.zero);
				}
			}
			//m_IsSet = true;
		}
	}

}
