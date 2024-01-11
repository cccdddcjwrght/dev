using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Entities;

namespace SGame
{

	public class DataRef<T>
	{
		public struct Index : IComponentData
		{
			public uint index;
			
			static public implicit operator Index(uint index)
			{
				return new Index() { index = index };
			}

			static public implicit operator uint(Index index)
			{
				return index.index;
			}

		}
	}

	partial class DataCenter
	{

		public void SetData<T>(T data) where T : struct, IComponentData
		{
			if (EntityManager.HasComponent<T>(m_data))
				EntityManager.SetComponentData<T>(m_data, data);
			else
				EntityManager.AddComponentData<T>(m_data, data);
		}

		public T GetData<T>() where T : struct, IComponentData
		{
			return EntityManager.GetComponentData<T>(m_data);
		}

		public void UpdateData<T>(DataExcute<T> excute) where T : struct, IComponentData
		{
			if (excute != null)
			{
				var d = EntityManager.GetComponentData<T>(m_data);
				excute(ref d);
				EntityManager.SetComponentData<T>(m_data, d);
			}
		}

	}
}
