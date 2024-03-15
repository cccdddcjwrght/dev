using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using log4net;

namespace SGame
{
	// 事件类型
	class EventObject
	{
		public int eventType; // 事件类型
		public object[] vals;        // 事件参数

		public EventObject(int e, object[] v)
		{
			eventType = e;
			vals = v;
		}
	}

	public interface IEventNotify
	{
		bool HasEvent(int id);
		void OnCall(int id, params object[] values);
	}

	// 添加异步触发 和 事件Handle
	public class EventDispatcherEx //: //MonoSingleton<EventManager> {
	{
		private EventDispatcher eventDispatcher;    // 消息触发逻辑
		private Queue<EventObject> eventQueue;      // 消息队列
		private IEventNotify mNotify;            // 通知LUA

		public EventDispatcherEx()
		{
			eventDispatcher = new EventDispatcher();
			eventQueue = new Queue<EventObject>();
		}

		public void SetEventNotify(IEventNotify evt)
		{
			mNotify = evt;
		}

		public void Update()
		{
			int count = eventQueue.Count;
			for (int i = 0; i < count; i++)
			{
				EventObject obj = eventQueue.Dequeue();
				TriggerGeneral(obj.eventType, obj.vals);
			}
		}

		// Add event listener
		public EventHanle Reg(int eventType, Callback function)
		{
			if (eventDispatcher.addEventListener(eventType, function))
			{
				return new EventV0(this, eventType, function);
			}
			return null;
		}

		public EventHanle Reg<T>(int eventType, Callback<T> function)
		{
			if (eventDispatcher.addEventListener<T>(eventType, function))
			{
				return new EventHandleV1<T>(this, eventType, function);
			}
			return null;
		}

		public EventHanle Reg<T, U>(int eventType, Callback<T, U> function)
		{
			if (eventDispatcher.addEventListener<T, U>(eventType, function))
			{
				return new EventHandleV2<T, U>(this, eventType, function);
			}
			return null;
		}

		public EventHanle Reg<T, U, V>(int eventType, Callback<T, U, V> function)
		{
			if (eventDispatcher.addEventListener<T, U, V>(eventType, function))
			{
				return new EventHandleV3<T, U, V>(this, eventType, function);
			}

			return null;
		}

		public EventHanle Reg<T, U, V, P4>(int eventType, Callback<T, U, V, P4> function)
		{
			if (eventDispatcher.addEventListener<T, U, V, P4>(eventType, function))
			{
				return new EventHandleV4<T, U, V, P4>(this, eventType, function);
			}

			return null;
		}

		public EventHanle Reg<T, U, V, P4, P5>(int eventType, Callback<T, U, V, P4, P5> function)
		{
			if (eventDispatcher.addEventListener<T, U, V, P4, P5>(eventType, function))
			{
				return new EventHandleV5<T, U, V, P4, P5>(this, eventType, function);
			}

			return null;
		}
		// Remove event listener
		public void UnReg(int eventType, Callback function)
		{
			eventDispatcher.removeEventListener(eventType, function);
		}

		public void UnReg<T>(int eventType, Callback<T> function)
		{
			eventDispatcher.removeEventListener<T>(eventType, function);
		}

		public void UnReg<T, U>(int eventType, Callback<T, U> function)
		{
			eventDispatcher.removeEventListener<T, U>(eventType, function);
		}

		public void UnReg<T, U, V>(int eventType, Callback<T, U, V> function)
		{
			eventDispatcher.removeEventListener<T, U, V>(eventType, function);
		}

		public void UnReg<T, U, V, P4>(int eventType, Callback<T, U, V, P4> function)
		{
			eventDispatcher.removeEventListener<T, U, V, P4>(eventType, function);
		}

		public void UnReg<T, U, V, P4, P5>(int eventType, Callback<T, U, V, P4, P5> function)
		{
			eventDispatcher.removeEventListener<T, U, V, P4, P5>(eventType, function);
		}

		// 同步触发事件
		public void Trigger(int eventType)
		{
			eventDispatcher.dispatchEvent(eventType);

			int evt = (int)eventType;
			if (mNotify != null && mNotify.HasEvent(evt))
			{
				mNotify.OnCall(evt);
			}
		}

		public void Trigger<T>(int eventType, T argv1)
		{
			eventDispatcher.dispatchEvent<T>(eventType, argv1);

			int evt = (int)eventType;
			if (mNotify != null && mNotify.HasEvent(evt))
			{
				mNotify.OnCall(evt, argv1);
			}
		}

		public void Trigger<T, U>(int eventType, T argv1, U argv2)
		{
			eventDispatcher.dispatchEvent<T, U>(eventType, argv1, argv2);

			int evt = (int)eventType;
			if (mNotify != null && mNotify.HasEvent(evt))
			{
				mNotify.OnCall(evt, argv1, argv2);
			}
		}

		public void Trigger<T, U, V>(int eventType, T argv1, U argv2, V argv3)
		{
			eventDispatcher.dispatchEvent<T, U, V>(eventType, argv1, argv2, argv3);

			int evt = (int)eventType;
			if (mNotify != null && mNotify.HasEvent(evt))
			{
				mNotify.OnCall(evt, argv1, argv2, argv3);
			}
		}

		public void Trigger<T, U, V, P4>(int eventType, T argv1, U argv2, V argv3, P4 argv4)
		{
			eventDispatcher.dispatchEvent<T, U, V, P4>(eventType, argv1, argv2, argv3, argv4);

			int evt = (int)eventType;
			if (mNotify != null && mNotify.HasEvent(evt))
			{
				mNotify.OnCall(evt, argv1, argv2, argv3, argv4);
			}
		}


		public void Trigger<T, U, V, P4, P5>(int eventType, T argv1, U argv2, V argv3, P4 argv4, P5 argv5)
		{
			eventDispatcher.dispatchEvent<T, U, V, P4, P5>(eventType, argv1, argv2, argv3, argv4, argv5);

			int evt = (int)eventType;
			if (mNotify != null && mNotify.HasEvent(evt))
			{
				mNotify.OnCall(evt, argv1, argv2, argv3, argv4, argv5);
			}
		}

		// 异步触发事件, 下一帧触发事件
		public void AsyncTrigger(int eventType, params object[] vals)
		{
			eventQueue.Enqueue(new EventObject(eventType, vals));
		}

		// 通用函数调用速度慢, 主要给LUA调用 (注意:会产生GC)
		public void TriggerGeneral(int eventType, params object[] vals)
		{
			Delegate call = null;
			if (eventDispatcher.eventTable.TryGetValue(eventType, out call))
			{
				try
				{
					call.DynamicInvoke(vals);
				}
				catch (Exception e)
				{
					Debug.LogError($"event args error : {eventType}");
				}
			}

			// 调用lua脚本啦
			int evt = (int)eventType;
			if (mNotify != null && mNotify.HasEvent(evt))
			{
				mNotify.OnCall(evt, vals);
			}
		}

		// 清空调用
		public void Clear()
		{
			eventDispatcher.clear();
			eventQueue.Clear();         // 消息队列
			mNotify = null;             // 通知LUA
		}

		// 判断是否包含事件
		public bool IsContains(int key)
		{
			return eventDispatcher.IsContains(key);
		}
	}

	// 全局事件
	public class EventManager : Singleton<EventManager>
	{
		private static ILog log = LogManager.GetLogger("system.event");
		EventDispatcherEx m_events = new EventDispatcherEx();

		public void SetEventNotify(IEventNotify evt)
		{
			m_events.SetEventNotify(evt);
		}

		public void Update()
		{
			m_events.Update();
		}

		// Add event listener
		public EventHanle Reg(int eventType, Callback function)
		{
			return m_events.Reg(eventType, function);
		}

		public EventHanle Reg<T>(int eventType, Callback<T> function)
		{
			return m_events.Reg(eventType, function);
		}

		public EventHanle Reg<T, U>(int eventType, Callback<T, U> function)
		{
			return m_events.Reg(eventType, function);
		}

		public EventHanle Reg<T, U, V>(int eventType, Callback<T, U, V> function)
		{
			return m_events.Reg(eventType, function);
		}

		public EventHanle Reg<T, U, V, P4>(int eventType, Callback<T, U, V, P4> function)
		{
			return m_events.Reg(eventType, function);
		}

		public EventHanle Reg<T, U, V, P4, P5>(int eventType, Callback<T, U, V, P4, P5> function)
		{
			return m_events.Reg(eventType, function);
		}

		// Remove event listener
		public void UnReg(int eventType, Callback function)
		{
			m_events.UnReg(eventType, function);
		}

		public void UnReg<T>(int eventType, Callback<T> function)
		{
			m_events.UnReg(eventType, function);
		}

		public void UnReg<T, U>(int eventType, Callback<T, U> function)
		{
			m_events.UnReg(eventType, function);
		}

		public void UnReg<T, U, V>(int eventType, Callback<T, U, V> function)
		{
			m_events.UnReg(eventType, function);
		}

		public void UnReg<T, U, V, P4>(int eventType, Callback<T, U, V, P4> function)
		{
			m_events.UnReg(eventType, function);
		}

		public void UnReg<T, U, V, P4, P5>(int eventType, Callback<T, U, V, P4, P5> function)
		{
			m_events.UnReg(eventType, function);
		}

		// 同步触发事件
		public void Trigger(int eventType)
		{
#if !EVENT_LOG_OFF && DEBUG
			log.Info("Trigger Event=" + eventType); 
#endif
			m_events.Trigger(eventType);
		}

		public void Trigger<T>(int eventType, T argv1)
		{
#if !EVENT_LOG_OFF && DEBUG
			log.Info("Trigger Event=" + eventType); 
#endif
			m_events.Trigger(eventType, argv1);
		}

		public void Trigger<T, U>(int eventType, T argv1, U argv2)
		{
#if !EVENT_LOG_OFF && DEBUG
			log.Info("Trigger Event=" + eventType); 
#endif
			m_events.Trigger(eventType, argv1, argv2);
		}

		public void Trigger<T, U, V>(int eventType, T argv1, U argv2, V argv3)
		{
#if !EVENT_LOG_OFF && DEBUG
			log.Info("Trigger Event=" + eventType); 
#endif
			m_events.Trigger(eventType, argv1, argv2, argv3);
		}

		public void Trigger<T, U, V, P4>(int eventType, T argv1, U argv2, V argv3, P4 argv4)
		{
#if !EVENT_LOG_OFF && DEBUG
			log.Info("Trigger Event=" + eventType); 
#endif
			m_events.Trigger(eventType, argv1, argv2, argv3, argv4);
		}


		public void Trigger<T, U, V, P4, P5>(int eventType, T argv1, U argv2, V argv3, P4 argv4, P5 argv5)
		{
#if !EVENT_LOG_OFF && DEBUG
			log.Info("Trigger Event=" + eventType); 
#endif
			m_events.Trigger(eventType, argv1, argv2, argv3, argv4, argv5);
		}

		// 异步触发事件, 下一帧触发事件
		public void AsyncTrigger(int eventType, params object[] vals)
		{
#if !EVENT_LOG_OFF && DEBUG
			log.Info("Trigger Event Async=" + eventType); 
#endif
			m_events.AsyncTrigger(eventType, vals);
		}

		// 通用函数调用速度慢, 主要给LUA调用 (注意:会产生GC)
		public void TriggerGeneral(int eventType, params object[] vals)
		{
#if !EVENT_LOG_OFF && DEBUG
			log.Info("Trigger Event General=" + eventType); 
#endif
			m_events.TriggerGeneral(eventType, vals);
		}

		// 清空调用
		public void Clear()
		{
			m_events.Clear();
		}
	}
}