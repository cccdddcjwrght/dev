using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameTools;
using UnityEngine;

namespace SGame
{
	public struct Worker : IEquatable<Worker>
	{
		public string queue;
		public int index;
		public int version;
		public Vector2Int cell;

		public float time;

		public bool Equals(Worker other)
		{
			return other.queue == queue && other.index == index;
		}
	}

	public class WorkQueue
	{
		public string name;
		public List<Worker> frees = new List<Worker>();
		public List<Worker> wokers = new List<Worker>();

		public void Clear()
		{
			frees.Clear();
			wokers.Clear();
		}

	}

	/// <summary>
	/// 简单工作队列
	/// </summary>

	public class WorkQueueSystem : MonoSingleton<WorkQueueSystem>
	{
		private Dictionary<string, WorkQueue> _queues = new Dictionary<string, WorkQueue>();
		private List<Worker> _waitWorkers = new List<Worker>();

		public void AddWorker(string name, int index)
		{
			if (string.IsNullOrEmpty(name)) return;
			if (!_queues.TryGetValue(name, out var q))
				q = new WorkQueue();
			q.frees.Add(new Worker() { queue = name, index = index, cell = MapAgent.IndexToGrid(index) });
		}

		public bool HasWorkQueue(string name)
		{
			if (!string.IsNullOrEmpty(name))
				return _queues.ContainsKey(name);
			return false;
		}

		public WorkQueue GetWorkQueue(string name)
		{
			if (string.IsNullOrEmpty(name)) return default;
			_queues.TryGetValue(name, out var q);
			return q;
		}

		public int GetFreeCount(string name)
		{
			if (_queues.TryGetValue(name, out var q))
				return q.frees.Count;
			return 0;
		}

		public bool Take(string name, out Worker worker, Func<Worker, bool> condition = null)
		{
			worker = default;
			if (string.IsNullOrEmpty(name)) return false;
			if (_queues.TryGetValue(name, out var q) && q.frees.Count > 0)
			{
				var i = 0;
				while (i < q.frees.Count)
				{
					var w = q.frees[i];
					if (condition == null || condition(w))
					{
						worker = w;
						break;
					}
				}
				if (worker.queue != null)
				{
					worker.version++;
					OnSelected(name, ref worker);
					q.frees.RemoveAt(i);
					q.wokers.Add(worker);
					return true;
				}
			}
			return false;
		}

		public void Free(Worker worker)
		{
			if (worker.queue == null) return;
			_waitWorkers.Add(worker);
			if (_queues.TryGetValue(worker.queue, out var q))
			{
				var idx = q.wokers.FindIndex(w => w.Equals(worker));
				if(idx>= 0)q.wokers.RemoveAt(idx);
				q.frees.Add(worker);
			}
		}

		public void Clear()
		{
			if (_queues.Count > 0)
				_queues.Values.ToList().ForEach(a => a.Clear());
			_queues.Clear();
		}

		private void OnSelected(string name, ref Worker worker)
		{

		}

		#region Mono

#if UNITY_EDITOR
		void Updata()
		{

		}
#endif

		#endregion
	}

}