using System;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007E3 RID: 2019
	public abstract class SquadComponent : MonoBehaviour
	{
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x0600346E RID: 13422 RVA: 0x000C8933 File Offset: 0x000C6D33
		public Squad squad
		{
			get
			{
				if (!this._squad)
				{
					this._squad.Target = base.GetComponentInParent<Squad>();
				}
				return this._squad;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x000C8961 File Offset: 0x000C6D61
		public NavigationMesh navMesh
		{
			get
			{
				return NavigationMesh.instance;
			}
		}

		// Token: 0x040023C8 RID: 9160
		private WeakReference<Squad> _squad = new WeakReference<Squad>(null);
	}
}
