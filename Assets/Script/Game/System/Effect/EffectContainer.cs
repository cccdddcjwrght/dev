using Unity.Entities;
using System;
using UnityEngine;
using FairyGUI;
using System.Collections.Generic;

namespace SGame
{
	public class EffectContainer : EC<EffectContainer>
	{
		private WeakReference<EffectSystem> reference;

		public EffectContainer()
		{
			reference = new WeakReference<EffectSystem>(EffectSystem.Instance);
		}

		protected override void DoDestroy(Entity e)
		{
			if (e != Entity.Null)
			{
				if (reference != null && reference.TryGetTarget(out var s))
				{
					var c = s.GetEffect(e);
					if (c) c.SetActive(false);
					if (s.World.EntityManager.Exists(e))
						s.CloseEffect(e);
				}
			}
		}

		protected override void OnDispoed()
		{
			base.OnDispoed();
			reference?.SetTarget(null);
			reference = null;
		}
	}

	public partial class EffectSystem
	{
		private Dictionary<uint, EffectContainer> _referencers = new Dictionary<uint, EffectContainer>();
		private Index<int> _refIndexs = new Index<int>();

		public Entity AddEffect(int effectId, object parent = default, object referencer = null, Vector3 position = default)
		{
			Entity entity = default;
			if (parent is GameObject go)
				entity = Spawn3d(effectId, go, position);
			else if (parent is GGraph holder)
				entity = SpawnUI(effectId, holder);
			else if (parent is GComponent gObject && gObject.GetChild("__effect") is GGraph g)
				entity = SpawnUI(effectId, g);
			if (entity != default && referencer != null)
			{
				var key = _refIndexs.IndexOf(referencer.GetHashCode());
				if (!_referencers.TryGetValue(key, out var con))
					_referencers[key] = con = new EffectContainer();
				EffectContainer.AddItem(entity, con);
			}
			return entity;
		}

		public void ReleaseEffect(object effect)
		{
			if (effect is Entity e)
				CloseEffect(e);
			else
			{
				var key = _refIndexs.IndexOf(effect.GetHashCode());
				if (_referencers.TryGetValue(key, out var con))
				{
					con.Release();
					_referencers.Remove(key);
				}
			}
		}

	}

}
