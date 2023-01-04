using System;
using UnityEngine;

namespace RTM.Pools
{
	// Token: 0x020004C3 RID: 1219
	public class DefaultPoolableMono : MonoBehaviour, IPoolable
	{
		// Token: 0x06001EC3 RID: 7875 RVA: 0x00052C12 File Offset: 0x00051012
		void IPoolable.OnRemoved()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x00052C20 File Offset: 0x00051020
		void IPoolable.OnReturned()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x00052C2E File Offset: 0x0005102E
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
			this.pool = (pool as LocalPool<DefaultPoolableMono>);
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x00052C41 File Offset: 0x00051041
		public void ReturnToPool()
		{
			this.pool.ReturnToPool(this);
		}

		// Token: 0x04001328 RID: 4904
		private LocalPool<DefaultPoolableMono> pool;
	}
}
