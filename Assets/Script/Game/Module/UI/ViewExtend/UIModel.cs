using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FairyGUI;
using UnityEngine;

namespace SGame
{
	public partial class UIModel
	{
		protected GGraph holder;
		protected GoWrapper goWrapper;
		protected Animator animator;

		protected object data;
		private bool _isLoaded;

		private Func<object, IEnumerator> _loader;
		private Action<UIModel> _loaded;

		private UnityEngine.Coroutine _loadCoroutine;

		private float _scale;
		private Vector3 _pos;
		private Vector3 _rot;

		public UIModel(GGraph graph)
		{
			_scale = 200;
			this.holder = graph;
			this.goWrapper = new GoWrapper();
			holder.SetNativeObject(goWrapper);
		}

		public UIModel SetData(object data)
		{
			this.data = data;
			return this;
		}

		public UIModel SetLoad(Func<object, IEnumerator> load)
		{
			this._loader = load;
			return this;
		}

		public UIModel SetModelLoadedCall(Action<UIModel> action)
		{
			_loaded = action;
			return this;
		}

		public UIModel SetScale(float scale)
		{
			this._scale = scale;
			return this;
		}

		public UIModel SetTRS(Vector3 pos, Vector3 rot, float scale = 1)
		{
			this._pos = pos;
			this._rot = rot;
			SetScale(scale);
			return this;
		}

		public UIModel RefreshModel(string animaName = null, float delay = 0)
		{
			_isLoaded = false;
			_loadCoroutine?.Stop();
			_loadCoroutine = CreateModel(animaName, delay).Start();
			return this;
		}

		public UIModel ClearModel()
		{
			animator = null;
			if (goWrapper != null && goWrapper.wrapTarget != null)
			{
				GameObject.Destroy(goWrapper.wrapTarget);
				goWrapper.SetWrapTarget(null, false);
			}
			return this;
		}

		public UIModel Reset()
		{
			_loadCoroutine?.Stop();
			_loadCoroutine = null;
			animator = null;
			data = null;
			_loaded = default;
			_loader = null;
			ClearModel();
			return this;
		}

		public GameObject GetModel()
		{
			return goWrapper != null ? goWrapper.wrapTarget : default;
		}

		public GGraph GetRoot() 
		{
			return holder;
		}

		public UIModel Play(string animation)
		{
			if (this.animator != null)
			{
				animator.CrossFade(animation , 0.2f);
			}
			return this;
		}

		public bool IsLoadCompleted()
		{
			return _isLoaded;
		}

		public void Release()
		{
			Reset();
			goWrapper?.Dispose();
			goWrapper = null;
			holder = null;
		}

		private void RefreshTRS(GameObject g = null)
		{
			g = g ?? GetModel();
			if (g)
			{
				g.transform.localPosition = _pos;
				g.transform.localRotation = Quaternion.Euler(_rot);
				g.transform.localScale = Vector3.one * _scale;
			}
		}

		IEnumerator CreateModel(string animaName = null, float delay = 0)
		{
			yield return null;
			if (data == null)
			{
				GameDebug.LogError("没有设置data");
				yield break;
			}
			if (_loader == null)
			{
				GameDebug.LogError("没有设置加载器");
				yield break;
			}
			if (delay > 0)
				yield return new WaitForSeconds(delay);

			var wait = _loader(data);
			yield return wait;
			var go = wait.Current as GameObject;
			if (go)
			{
				if (goWrapper != null)
				{
					var old = goWrapper.wrapTarget;
					if (old) GameObject.Destroy(old);
					goWrapper.SetWrapTarget(go, false);
					go.SetActive(false);
					go.SetLayer("UILight");
					RefreshTRS(go);
					go.SetActive(true);
					_isLoaded = true;
					_loaded?.Invoke(this);
					animator = go.GetComponent<Animator>() ?? go.GetComponentInChildren<Animator>();
					animator?.Play(animaName ?? "idle");
					yield break;
				}
				GameObject.Destroy(go);
			}

		}

	}

}
