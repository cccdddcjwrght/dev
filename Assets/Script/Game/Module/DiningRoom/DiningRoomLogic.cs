using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SGame.Dining
{

	public interface IBuild : IRequest
	{
		public int cfgID { get; }
		public int objID { get; }
	}

	class DiningRoomLogic : IBuild
	{

		private GameTools.Maps.Grid _sceneGrid;
		private List<IBuild> _builds;

		public string name;

		public int cfgID { get; private set; }
		public int objID { get; private set; }
		public string error { get; set; }

		public bool isDone
		{
			get
			{
				return _builds == null || _builds.All(b=>b.isDone);
			}
		}

		public GameTools.Maps.Grid grid { get { return _sceneGrid; } }

		public DiningRoomLogic(int id)
		{
			cfgID = id;
		}


		public bool Init()
		{
			if (_sceneGrid == null)
			{
				InitView();
				InitBuilds();
			}
			return true;
		}

		public IEnumerator Wait(bool isnew = false)
		{
			double time = 1.0;
			while (!isDone) yield return null;
			while ((time -= GlobalTime.deltaTime) > 0) yield return null;
		}

		public void Close()
		{
			cfgID = 0;
			_builds?.ForEach(b => b.Close());
			_builds?.Clear();
			_sceneGrid = null;
		}


		private void InitView()
		{
			_sceneGrid = GameObject.FindAnyObjectByType<GameTools.Maps.Grid>(FindObjectsInactive.Include);
		}


		private void InitBuilds()
		{
			if (_sceneGrid != null)
			{
				//_sceneGrid.GetIDListByBuild()
			}
		}

	}

}
