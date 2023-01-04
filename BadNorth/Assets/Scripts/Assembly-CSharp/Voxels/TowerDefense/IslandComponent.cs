using System;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200077C RID: 1916
	public abstract class IslandComponent : MonoBehaviour
	{
		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060031C8 RID: 12744 RVA: 0x000806D2 File Offset: 0x0007EAD2
		public Island island
		{
			get
			{
				if (!this._island.Target)
				{
					this._island = base.GetComponentInParent<Island>();
				}
				return this._island;
			}
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x00080705 File Offset: 0x0007EB05
		public void SetIsland(Island island)
		{
			this._island.Target = island;
		}

		// Token: 0x040021A8 RID: 8616
		private WeakReference<Island> _island = new WeakReference<Island>(null);
	}
}
