using System;
using System.Collections.Generic;
using CS.VT;

namespace Voxels.TowerDefense.ProfileInternals
{
	// Token: 0x02000594 RID: 1428
	[Serializable]
	public class MetaSave
	{
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06002505 RID: 9477 RVA: 0x000744E4 File Offset: 0x000728E4
		public int FirstFreeSlot
		{
			get
			{
				for (int i = 0; i < this.campaigns.Count; i++)
				{
					if (this.campaigns[i] == null)
					{
						return i;
					}
				}
				return -1;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06002506 RID: 9478 RVA: 0x00074524 File Offset: 0x00072924
		public int FirstTakenSlot
		{
			get
			{
				for (int i = 0; i < this.campaigns.Count; i++)
				{
					if (this.campaigns[i] != null)
					{
						return i;
					}
				}
				return -1;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06002507 RID: 9479 RVA: 0x00074561 File Offset: 0x00072961
		public bool HasFreeSlot
		{
			get
			{
				return !this.hasLimitedSlots || 0 <= this.FirstFreeSlot;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06002508 RID: 9480 RVA: 0x0007457D File Offset: 0x0007297D
		public bool HasSaves
		{
			get
			{
				return this.FirstTakenSlot != -1;
			}
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x0007458C File Offset: 0x0007298C
		public Callback<int> DeleteSaveSlot(CampaignSaveMeta campaignMeta)
		{
			string targetFileName = campaignMeta.targetFileName;
			for (int i = 0; i < this.campaigns.Count; i++)
			{
				if (this.campaigns[i] == campaignMeta)
				{
					this.campaigns[i] = null;
					break;
				}
			}
			if (campaignMeta.hasCheckpoint)
			{
				SaveGameUtilities.DeleteFile(campaignMeta.checkpointFileName);
			}
			SaveGameUtilities.DeleteFile(campaignMeta.metaFileName);
			return SaveGameUtilities.DeleteFile(targetFileName);
		}

		// Token: 0x040017A6 RID: 6054
		public readonly int maxCampaignSlots = 5;

		// Token: 0x040017A7 RID: 6055
		public readonly bool hasLimitedSlots = true;

		// Token: 0x040017A8 RID: 6056
		public List<CampaignSaveMeta> campaigns = new List<CampaignSaveMeta>();
	}
}
