using System.Collections;
using System.Collections.Generic;
using System;
namespace SGame
{
	public interface EventHanle {
		void Close();
	}

	public class EventV0 : EventHanle {
		WeakReference<EventDispatcherEx> managerReference;
		int eventType;
		Callback					func;

		public EventV0(EventDispatcherEx _mgr, int e, Callback f) {
			managerReference = new WeakReference<EventDispatcherEx>(_mgr);
			eventType		 = e;
			func			 = f;
		}

		public void Close() {
			if (managerReference != null)
			{
				if (managerReference.TryGetTarget(out EventDispatcherEx mgr)) {
					mgr.UnReg(eventType, func);
				}

				func = null;
				managerReference = null;
			}
		}
	}

	public class EventHandleV1<T> : EventHanle {
		WeakReference<EventDispatcherEx> managerReference;
		int							eventType;
		Callback<T>					func;

		public EventHandleV1(EventDispatcherEx _mgr, int e, Callback<T> f) {
			managerReference = new WeakReference<EventDispatcherEx>(_mgr);
			eventType		= e;
			func			= f;
		}

		public void Close() {
			if (managerReference != null)
			{
				if (managerReference.TryGetTarget(out EventDispatcherEx mgr))
				{
					mgr.UnReg(eventType, func);
				}

				func = null;
				managerReference = null;
			}


		}
	}

	public class EventHandleV2<T,U> : EventHanle {
		WeakReference<EventDispatcherEx> managerReference;
		int							eventType;
		Callback<T, U>				func;

		public EventHandleV2(EventDispatcherEx _mgr, int e, Callback<T, U> f) {
			managerReference = new WeakReference<EventDispatcherEx>(_mgr);
			eventType = e;
			func = f;
		}

		public void Close() {
			if (managerReference != null)
			{
				if (managerReference.TryGetTarget(out EventDispatcherEx mgr))
				{
					mgr.UnReg(eventType, func);
				}

				func = null;
				managerReference = null;
			}
		}
	}

	public class EventHandleV3<T,U,V> : EventHanle {
		WeakReference<EventDispatcherEx>		managerReference;
		int								eventType;
		Callback<T, U, V>				func;

		public EventHandleV3(EventDispatcherEx _mgr, int e, Callback<T, U, V> f) {
			managerReference = new WeakReference<EventDispatcherEx>(_mgr);
			eventType = e;
			func = f;
		}

		public void Close() {
			if (managerReference != null)
			{
				if (managerReference.TryGetTarget(out EventDispatcherEx mgr))
				{
					mgr.UnReg(eventType, func);
				}

				func = null;
				managerReference = null;
			}
		}
	}

	public class EventHandleV4<T,U,V,P4> : EventHanle {
		WeakReference<EventDispatcherEx> managerReference;
		int							eventType;
		Callback<T, U, V, P4>		func;


		public EventHandleV4(EventDispatcherEx _mgr, int e, Callback<T, U, V, P4> f) {
			managerReference = new WeakReference<EventDispatcherEx>(_mgr);
			eventType = e;
			func = f;
		}

		public void Close() {
			if (managerReference != null)
			{
				if (managerReference.TryGetTarget(out EventDispatcherEx mgr))
				{
					mgr.UnReg(eventType, func);
				}

				func = null;
				managerReference = null;
			}
		}
	}

	public class EventHandleV5<T,U,V,P4,P5> : EventHanle {
		WeakReference<EventDispatcherEx> managerReference;
		int							eventType;
		Callback<T, U, V, P4, P5>	func;

		public EventHandleV5(EventDispatcherEx _mgr, int e, Callback<T, U, V, P4, P5> f) {
			managerReference = new WeakReference<EventDispatcherEx>(_mgr);
			eventType = e;
			func = f;
		}

		public void Close() {
			if (managerReference != null)
			{
				if (managerReference.TryGetTarget(out EventDispatcherEx mgr))
				{
					mgr.UnReg(eventType, func);
				}

				func = null;
				managerReference = null;
			}
		}
	}

	public class EventHandleContainer : EventHanle {
		List<EventHanle> mHandles;

		public EventHandleContainer() {
			mHandles = new List<EventHanle>();
		}


	public static EventHandleContainer operator +(EventHandleContainer container, EventHanle handle) {
			if (handle != null) {
				container.Add(handle);
			}
			return container;
		}


		public void Add(EventHanle handle) {
			mHandles.Add (handle);
		}

		public void Close() {
			foreach (EventHanle h in mHandles) {
				h.Close ();
			}
			mHandles.Clear ();
		}
	}
}