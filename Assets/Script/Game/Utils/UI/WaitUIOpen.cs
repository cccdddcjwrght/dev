using System.Collections;
using SGame.UI;
using Unity.Entities;

/// 等待UI结束
namespace SGame
{
	public class WaitUIOpen : IEnumerator
	{
		static public bool needWaitAnimation;
		static public int needDelay;



		private Entity m_eUI;
		private EntityManager m_mgr;
		private bool m_waitAnimation;
		private int m_delay;

		public WaitUIOpen(EntityManager mgr, Entity e)
		{
			m_mgr = mgr;
			m_eUI = e;
			m_waitAnimation = needWaitAnimation;
			m_delay = needDelay;

			needDelay = 0;
			needWaitAnimation = false;

		}

		public bool MoveNext()
		{
			if (m_mgr.Exists(m_eUI) == false)
			{
				return false;
			}
			if (m_mgr.HasComponent<UIInitalized>(m_eUI) && !m_mgr.HasComponent<DespawningEntity>(m_eUI))
			{
				if (m_waitAnimation)
				{
					var win = m_mgr.GetComponentObject<UIWindow>(m_eUI);
					if (!win.BaseValue.isReadyShowed) return true;
				}

				if (m_delay-- > 0) return true;

				return false;
			}
			return true;
		}

		public object Current
		{
			get
			{
				return m_eUI;
			}
		}

		public void Reset()
		{

		}
	}
}