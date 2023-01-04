using System;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.CampaignGeneration;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200081F RID: 2079
	internal class SeedDisplay : MonoBehaviour, IGameSetup
	{
		// Token: 0x0600364D RID: 13901 RVA: 0x000EA066 File Offset: 0x000E8466
		void IGameSetup.OnGameAwake()
		{
			CampaignManager.onNewCampaign += this.UpdateDisplay;
			CampaignManager.onExitCampaign += this.WipeDisplay;
			this.visibility = base.GetComponentInChildren<IUIVisibility>(true);
			this.SetVisible(false, true);
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x000EA09F File Offset: 0x000E849F
		private void UpdateDisplay(CampaignManager manager, Campaign campaign)
		{
			this.seedValueText.text = string.Format("[0x{0:X8}]", campaign.seed);
			this.SetVisible(true, false);
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x000EA0C9 File Offset: 0x000E84C9
		private void WipeDisplay(CampaignManager arg1, Campaign arg2)
		{
			this.SetVisible(false, false);
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x000EA0D3 File Offset: 0x000E84D3
		private void SetVisible(bool v, bool force = false)
		{
			if (this.visibility != null)
			{
				this.visibility.SetVisible(v, force);
			}
			else
			{
				base.gameObject.SetActive(v);
			}
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x000EA0FE File Offset: 0x000E84FE
		private void OnDisable()
		{
			if (this.visibility != null)
			{
				this.visibility.SetVisible(false, true);
			}
		}

		// Token: 0x040024DB RID: 9435
		[SerializeField]
		private Text seedValueText;

		// Token: 0x040024DC RID: 9436
		private IUIVisibility visibility;
	}
}
