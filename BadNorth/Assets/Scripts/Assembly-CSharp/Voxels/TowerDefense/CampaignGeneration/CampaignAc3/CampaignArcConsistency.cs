using System;
using System.Collections;
using System.Collections.Generic;

namespace Voxels.TowerDefense.CampaignGeneration.CampaignAc3
{
	// Token: 0x020006CE RID: 1742
	public class CampaignArcConsistency : CampaignComponent, Campaign.ICampaignCreator
	{
		// Token: 0x06002D31 RID: 11569 RVA: 0x000A890C File Offset: 0x000A6D0C
		IEnumerator Campaign.ICampaignCreator.OnCampaigCreation(Campaign campaign, ProtoCampaign protoCampaign)
		{
			IEnumerator<object> enumerable = Singleton<LevelArcConsistency>.instance.AssignThingsToCampaign(campaign.campaignSave, protoCampaign);
			while (enumerable.MoveNext())
			{
				object obj = enumerable.Current;
				yield return obj;
			}
			yield break;
		}
	}
}
