using System.Collections;
using Unity.Entities;

namespace SGame
{
	public  abstract partial class ECSSystem<T> : SystemBase where T : ECSSystem<T>
	{
		static public T Instance
		{
			get
			{
				return World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<T>();
			}
		}

		protected override void OnUpdate()
		{

		}

	}
}