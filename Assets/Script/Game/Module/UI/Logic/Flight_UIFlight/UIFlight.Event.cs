
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Flight;
    using System.Collections.Generic;

    public partial class UIFlight
	{
		public EventHandleContainer m_EventHandle = new EventHandleContainer();
		Stack<GGraph> stack = new Stack<GGraph>();


		partial void InitEvent(UIContext context){
			m_EventHandle += EventManager.Instance.Reg<int, Vector2, Vector2, float>((int)GameEvent.FLIGHT_SINGLE_CREATE, Play);
			m_EventHandle += EventManager.Instance.Reg<List<int>, Vector2, Vector2, float>((int)GameEvent.FLIGHT_LIST_CREATE, Play);
		}
		partial void UnInitEvent(UIContext context){
			m_EventHandle.Close();
			m_EventHandle = null;
		}

		void Play(List<int> ids, Vector2 startPos, Vector2 endPos, float duration) 
		{
            ids.ForEach((id)=> Play(id, startPos, endPos, duration));
		}

		void Play(int id, Vector2 startPos, Vector2 endPos, float duration) 
		{
			AddEffect(id, startPos, endPos, duration);
		}

		void AddEffect(int id, Vector2 startPos, Vector2 endPos, float duration) 
		{
			TransitionModule.Instance.AddDepend(id);

			int effectId1 = id + 20;
			int effectId2 = id + 22;
			var graph1 = GetGraph(startPos.x, startPos.y);
			EffectSystem.Instance.AddEffect(effectId1, graph1);
			if (ConfigSystem.Instance.TryGet<GameConfigs.effectsRowData>(effectId1, out var data)) 
			{
				Utils.Timer(data.Duration, null, m_view, completed: () => Push(graph1));
			}

			if (endPos == Vector2.zero) endPos = GetFinalPos(id);
			var graph2 = GetGraph(endPos.x, endPos.y);
			EffectSystem.Instance.AddEffect(effectId2, graph2);

			var endTime = GameServerTime.Instance.serverTime + duration;
			GTween.To(startPos, endPos, duration).SetTarget(m_view).OnUpdate((t) =>
			{
				graph2.SetXY(t.value.x, t.value.y);
			}).OnComplete(()=> 
			{
				Push(graph2);
				TransitionModule.Instance.SubDepend(id);
				Refresh();
			});

			Refresh();
		}

        GGraph GetGraph(float x, float y)
        {
			GGraph holder;
			if (stack.Count > 0)
				holder = stack.Pop();
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

		void Push(GGraph holder) 
		{
			holder.visible = false;
			stack.Push(holder);
		}

		Vector2 GetFinalPos(int id) 
		{
			if (id == (int)ItemID.GOLD) return m_view.m_Gold.xy;
			else if(id == (int)ItemID.DIAMOND) return m_view.m_Diamond.xy;

			return default;
		}

        void Refresh() 
		{
			SetGoldText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)ItemID.GOLD)));
			SetDiamondText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)ItemID.DIAMOND)));

			m_view.m_Gold.visible = TransitionModule.Instance.IsShow((int)ItemID.GOLD);
			m_view.m_Diamond.visible = TransitionModule.Instance.IsShow((int)ItemID.DIAMOND);
		}
	}

}
