using System;
using System.Collections;
using System.Collections.Generic;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x02000709 RID: 1801
	public class LevelNamer : CampaignComponent, Campaign.ICampaignCreator
	{
		// Token: 0x06002E8C RID: 11916 RVA: 0x000B59D4 File Offset: 0x000B3DD4
		IEnumerator Campaign.ICampaignCreator.OnCampaigCreation(Campaign campaign, ProtoCampaign protoCampaign)
		{
			this.randomNames.Clear();
			this.randomGenericNames.Clear();
			this.randomNames.AddRange(IslandNameTerms.names);
			this.randomNames.Shuffle<string>();
			this.randomGenericNames.AddRange(IslandNameTerms.genericNames);
			this.randomGenericNames.Shuffle<string>();
			List<LevelState> levels = campaign.campaignSave.levelStates;
			levels[0].nameTerm = this.randomGenericNames[0];
			levels[levels.Count - 1].nameTerm = this.randomGenericNames[1];
			this.randomNames.Remove(this.randomGenericNames[0]);
			this.randomNames.Remove(this.randomGenericNames[1]);
			for (int i = 1; i < levels.Count - 1; i++)
			{
				string name = this.randomNames[i % this.randomNames.Count];
				levels[i].nameTerm = name;
				yield return null;
			}
			yield break;
		}

		// Token: 0x04001EC5 RID: 7877
		private List<string> randomNames = new List<string>();

		// Token: 0x04001EC6 RID: 7878
		private List<string> randomGenericNames = new List<string>();
	}
}
