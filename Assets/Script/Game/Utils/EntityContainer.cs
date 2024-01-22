using System;
using System.Collections.Generic;
using Unity.Entities;
using System.Linq;

namespace SGame
{
	public class EntityContainer : IDisposable
	{
		private List<Entity> entities;
		public Action<Entity> OnDestroy;

		public Entity current;

		public EntityContainer()
		{
			entities = new List<Entity>();
			OnDestroy = null;
		}

		public EntityContainer(Action<Entity> destroy)
		{
			entities = new List<Entity>();
			this.OnDestroy = destroy;
		}

		public EntityContainer Add(Entity e)
		{
			if (e != Entity.Null)
			{
				if (!entities.Contains(e))
					entities.Add(e);
			}
			return this;
		}

		public bool All()
		{
			if (entities != null && entities.Count > 0)
			{
				return entities.All(CheckEntity);
			}
			return false;
		}

		public bool Any()
		{
			if (entities != null && entities.Count > 0)
			{
				return entities.Any(CheckEntity);
			}
			return false;
		}

		public void Remove(Entity e)
		{
			if (e != Entity.Null)
			{
				if (entities.Contains(e))
					entities.Remove(e);
			}
		}

		public void Clear()
		{
			if (entities != null)
				entities.Clear();
		}

		public EntityContainer Foreach(Action<Entity> call)
		{
			if (entities != null && entities.Count > 0)
			{
				entities.ForEach(call);
			}
			return this;
		}

		public virtual bool CheckEntity(Entity e)
		{
			return World.DefaultGameObjectInjectionWorld.EntityManager.Exists(e);
		}

		public void DestroyAllEntity()
		{
			if (entities != null && entities.Count > 0)
			{
				for (int i = 0; i < entities.Count; i++)
				{
					var entity = entities[i];
					if (entity == Entity.Null) continue;
					if (OnDestroy != null) OnDestroy(entity);
					else DoDestroy(entity);
				}
				entities.Clear();
			}
		}

		public void Release()
		{
			Dispose();
		}


		public void Dispose()
		{
			DestroyAllEntity();
			OnDispoed();
			OnDestroy = null;
			entities = null;
		}

		protected virtual void DoDestroy(Entity e)
		{
			if (e != Entity.Null)
				World.DefaultGameObjectInjectionWorld.EntityManager.DestroyEntity(e);
		}

		protected virtual void OnDispoed() { }

		public static EntityContainer operator +(EntityContainer c, Entity e)
		{
			if (e != Entity.Null)
			{
				if (c == null) c = new EntityContainer();
				if (!c.entities.Contains(e))
				{
					c.entities.Add(e);
					c.current = e;
				}
			}
			return c;
		}


	}

	public abstract class EC<T> : EntityContainer where T : EC<T>, new()
	{
		public static EntityContainer AddItem(Entity e, T c = default)
		{
			c = c ?? new T();
			return c + e;
		}
	}

}
