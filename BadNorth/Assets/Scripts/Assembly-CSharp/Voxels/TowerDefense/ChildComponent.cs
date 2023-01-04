using System;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006DC RID: 1756
	public abstract class ChildComponent<T> : MonoBehaviour where T : Component
	{
		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06002D6E RID: 11630 RVA: 0x0007BE7A File Offset: 0x0007A27A
		public T manager
		{
			get
			{
				if (!this._t.Target)
				{
					this._t = this.GetDisabledComponentInParent<T>();
				}
				return this._t;
			}
		}

		// Token: 0x04001DFE RID: 7678
		private WeakReference<T> _t = new WeakReference<T>((T)((object)null));
	}
}
