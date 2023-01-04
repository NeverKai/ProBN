using System;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x020006CD RID: 1741
	public abstract class CampaignComponent : MonoBehaviour
	{
		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06002D2F RID: 11567 RVA: 0x000A889C File Offset: 0x000A6C9C
		public Campaign campaign
		{
			get
			{
				if (!this._campaign)
				{
					Transform transform = base.transform;
					while (transform && !this._campaign)
					{
						this._campaign.Target = transform.GetComponent<Campaign>();
						transform = transform.parent;
					}
				}
				return this._campaign;
			}
		}

		// Token: 0x04001DAE RID: 7598
		private WeakReference<Campaign> _campaign = new WeakReference<Campaign>(null);
	}
}
