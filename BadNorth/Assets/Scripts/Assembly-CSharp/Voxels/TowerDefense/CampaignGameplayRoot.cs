using System;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;

namespace Voxels.TowerDefense
{
	// Token: 0x020008A6 RID: 2214
	internal class CampaignGameplayRoot : MonoBehaviour, IGameSetup
	{
		// Token: 0x060039DA RID: 14810 RVA: 0x000FD568 File Offset: 0x000FB968
		void IGameSetup.OnGameAwake()
		{
			base.gameObject.SetActive(false);
			Singleton<Stack>.instance.stateCampaign.OnChange += base.gameObject.SetActive;
			this.newCampaigners = base.GetComponentsInChildren<CampaignManager.INewCampaign>(true);
			this.campaignExiters = base.GetComponentsInChildren<CampaignManager.IExitCampaign>(true);
			CampaignManager.onNewCampaign += this.OnNewCampaign;
			CampaignManager.onExitCampaign += this.OnExitCampaign;
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x000FD5E0 File Offset: 0x000FB9E0
		private void OnNewCampaign(CampaignManager manager, Campaign campaign)
		{
			foreach (CampaignManager.INewCampaign newCampaign in this.newCampaigners)
			{
				newCampaign.OnNewCampaign(manager, campaign);
			}
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x000FD614 File Offset: 0x000FBA14
		private void OnExitCampaign(CampaignManager manager, Campaign campaign)
		{
			foreach (CampaignManager.IExitCampaign exitCampaign in this.campaignExiters)
			{
				exitCampaign.OnCampaignExit(manager, campaign);
			}
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x000FD648 File Offset: 0x000FBA48
		private void OnDestroy()
		{
			CampaignManager.onNewCampaign -= this.OnNewCampaign;
			CampaignManager.onExitCampaign -= this.OnExitCampaign;
		}

		// Token: 0x040027E5 RID: 10213
		private CampaignManager.INewCampaign[] newCampaigners;

		// Token: 0x040027E6 RID: 10214
		private CampaignManager.IExitCampaign[] campaignExiters;
	}
}
