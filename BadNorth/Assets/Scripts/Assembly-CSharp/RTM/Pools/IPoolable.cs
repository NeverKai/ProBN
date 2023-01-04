using System;
using UnityEngine;

namespace RTM.Pools
{
	// Token: 0x020004C4 RID: 1220
	public interface IPoolable
	{
		// Token: 0x06001EC7 RID: 7879
		void SetPool<T>(LocalPool<T> pool) where T : Component, IPoolable;

		// Token: 0x06001EC8 RID: 7880
		void OnRemoved();

		// Token: 0x06001EC9 RID: 7881
		void OnReturned();
	}
}
