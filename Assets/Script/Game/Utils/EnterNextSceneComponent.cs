using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace SGame
{

	public class EnterNextSceneComponent : MonoBehaviour
	{
		[System.Serializable]
		public class StepNode
		{
			public List<GameObject> actives;
			public List<GameObject> flytarget;
			public List<GameObject> gos;
			public float flytime;
			public GameObject to;
			public int trackindex = -1;
		}

		[Tooltip("ÒÑÍê³É")]
		public int index;
		public PlayableDirector director;
		public float pausetime;
		public bool continueFlag;
		public float flytime;
		public float flydelay;
		public float showdelay;
		public List<StepNode> steps;


		public double duration { get { return (director.playableAsset as TimelineAsset).duration; } }

		private StepNode _node;


		public EnterNextSceneComponent Set(int index)
		{
			//this.index = index;
			for (int i = 0; i < steps.Count; i++)
				SetActiveNode(steps[i], i < index);
			if (index <= steps.Count)
				_node = steps[Mathf.Clamp(index, 0, steps.Count - 1)];
			return this;
		}

		[ContextMenu("Play")]
		public EnterNextSceneComponent Play()
		{
			/*if (_node == null)
				Set(index++);*/
			if (director != null)
			{
				director.Stop();
				director.Play();
				_node.gos?.ForEach(s => s.SetActive(true));
				StartCoroutine(Wait());
				StartCoroutine(Fly());
			}
			return this;
		}

		void SetActiveNode(StepNode node, bool state)
		{
			if (node != null)
			{
				node.actives.ForEach(s => s.SetActive(state));
				if (node.trackindex >= 0)
				{
					var ts = director.playableAsset as TimelineAsset;
					if (ts != null)
					{
						var a = ts.GetOutputTrack(node.trackindex);
						if (a) a.muted = state;
					}
				}
			}
		}

		IEnumerator Fly()
		{
			if (flydelay <= 0)
				yield return null;
			else
				yield return new WaitForSecondsRealtime(flydelay);
			if (_node != null)
			{
				var flys = _node.flytarget;
				var time = _node.flytime > 0 ? _node.flytime : flytime;
				var to = _node.to;

				flys.ForEach(s =>
				{
					s.SetActive(false);
					s.transform.position = UnityEngine.Random.insideUnitCircle * 6 + new Vector2(0, 12);
				});
				while (true)
				{
					yield return null;
					time -= Time.deltaTime;
					foreach (var item in flys)
					{
						if (!item.activeSelf) item.SetActive(true);
						var d = to.transform.position - item.transform.position;
						var sp = time > 0 ? d / time : d;
						item.transform.position += sp * Time.deltaTime;
					}
					if (time <= 0) break;
				}

				if (showdelay > 0)
					yield return new WaitForSecondsRealtime(showdelay);
				SetActiveNode(_node, true);
				flys.ForEach(s => s.SetActive(false));
				_node = default;
			}
		}

		IEnumerator Wait()
		{
			if (pausetime > 0)
			{
				continueFlag = false;
				while (director.time < pausetime)
					yield return null;
				director.Pause();
				while (!continueFlag)
					yield return null;
				director.Resume();
			}
		}

	}

}