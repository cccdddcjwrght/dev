using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace GameTools.Paths.Test
{
	public class PathView : MonoBehaviour
	{
		public Color color;
		public List<int2> points = new List<int2>();

		private void Awake()
		{
			color = UnityEngine.Random.ColorHSV();
		}

		private void OnDrawGizmos()
		{
			if (points != null && points.Count > 0)
			{
				Gizmos.color = color;
				for (int i = points.Count - 1; i > 0; i--)
					Gizmos.DrawLine(AStar.GetPos( points[i]), AStar.GetPos(points[i-1]));
				Gizmos.DrawCube(AStar.GetPos(points[0]), Vector3.one * 0.2f);
			}
		}

	}

	[UpdateAfter(typeof(FollowSystem))]
	public partial class PathViewSystem : SystemBase
	{
		private EntityQuery m_query;
		private GameObject m_view;
		private Dictionary<int, PathView> m_views;

		protected override void OnCreate()
		{
			m_view = new GameObject("PathView");
			m_views = new Dictionary<int, PathView>();
			m_view.gameObject.SetActive(false);
		}

		protected override void OnUpdate()
		{
			if (!m_view.activeInHierarchy) return;
			Entities
				.WithAll<Follow, PathPositions>()
				.WithNone<FindPathParams>()
				.ForEach((Entity e, in Follow follow) =>
			{
				if (follow.Value >= 0)
				{
					var points = EntityManager.GetBuffer<PathPositions>(e, true);
					if (!m_views.TryGetValue(e.Index, out var view))
						m_views[e.Index] = view = m_view.AddComponent<PathView>();
					view.points.Clear();
					for (int i = 0; i < follow.Value; i++)
						view.points.Add(points[i].Value);
				}
			}).WithoutBurst().Run();
		}


	}
}
