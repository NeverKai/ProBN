using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;

namespace Voxels.TowerDefense
{
	// Token: 0x020006DD RID: 1757
	public class CampaignRendererSorter : CampaignComponent, Campaign.ICampaignGenerator
	{
		// Token: 0x06002D70 RID: 11632 RVA: 0x000AC72C File Offset: 0x000AAB2C
		IEnumerator<GenInfo> Campaign.ICampaignGenerator.OnCampaignGeneration(Campaign campaign)
		{
			Renderer[] renderers = campaign.GetComponentsInChildren<Renderer>(true);
			float minZ = renderers.Min((Renderer x) => x.transform.position.z);
			float maxZ = renderers.Max((Renderer x) => x.transform.position.z);
			foreach (Renderer renderer in renderers)
			{
				renderer.sortingOrder = (int)ExtraMath.RemapValue(renderer.transform.position.z, minZ, maxZ, 0f, 100f);
			}
			yield return new GenInfo("CampaignRendererSorter", GenInfo.Mode.interruptable);
			yield break;
		}
	}
}
