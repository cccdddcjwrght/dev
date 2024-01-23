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
	/// 获取相关Tag名队列的空闲位置
	/// 工作台 RoomMachine.ID
	/// 或者配置表和场景里面关联的Tag
	/// </summary>

	public class WorkQueueSystem : MonoSingleton<WorkQueueSystem>
	{
		private Dictionary<string, WorkQueue> _queues = new Dictionary<string, WorkQueue>();
		private List<Worker> _waitWorkers = new List<Worker>();

		public const string SEAT_TAG = "seat";
		public const string SERVE_TAG = "serve_1";
		public const string PLACE_TAG = "place_1";


		/// <summary>
		/// 添加一个工作位
		/// </summary>
		/// <param name="name"></param>
		/// <param name="index">格子索引</param>
		public void AddWorker(string name, int index)
		{
			if (string.IsNullOrEmpty(name)) return;
			if (!_queues.TryGetValue(name, out var q))
				q = new WorkQueue();
			var w = new Worker() { queue = name, index = index, cell = MapAgent.IndexToGrid(index) };
			OnAddWorker(ref w);
			q.frees.Add(w);
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

		/// <summary>
		/// 获取空闲工作位
		/// </summary>
		/// <param name="name"></param>
		/// <param name="worker"></param>
		/// <param name="condition"></param>
		/// <returns></returns>
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

		/// <summary>
		/// 释放
		/// </summary>
		/// <param name="worker"></param>
		public void Free(Worker worker)
		{
			if (worker.queue == null) return;
			_waitWorkers.Add(worker);
			if (_queues.TryGetValue(worker.queue, out var q))
			{
				var idx = q.wokers.FindIndex(w => w.Equals(worker));
				if (idx >= 0) q.wokers.RemoveAt(idx);
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

		private void OnAddWorker(ref Worker worker)
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